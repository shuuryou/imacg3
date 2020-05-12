using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace crtcpl
{
    /// <summary>
    /// Handles Communication with the microcontroller
    /// 
    /// This is a wrapper around SerialPort so that the actual control panel applet does not
    /// depend on the SerialPort class and we could replace it with something nicer like I2C
    /// some day in the future.... lol, as if.
    /// </summary>
    public static class UCCom
    {
        private static SerialPort s_SerialPort;
        private static int s_Counter = 0;
        private static readonly ManualResetEventSlim s_GotResponse = new ManualResetEventSlim(false);
        private static readonly byte[] s_ResponseBuffer = new byte[255];
        private static int s_ResponseBufferLength = 0;

        public static ReadOnlyCollection<string> AvailablePorts =
            new ReadOnlyCollection<string>(SerialPort.GetPortNames());

        public static ReadOnlyCollection<int> AvailableBitRates = new
                ReadOnlyCollection<int>(new int[] {
                    300, 600, 1200, 2400, 4800, 9600, 19200,
                    38400, 57600, 115200, 230400, 460800, 921600 });

        public static bool IsOpen { get; private set; }
        public static void Open(string port, int rate)
        {
            if (port == null)
            {
                throw new ArgumentNullException(nameof(port));
            }

            if (!AvailableBitRates.Contains(rate))
            {
                throw new ArgumentOutOfRangeException(nameof(rate));
            }

            if (IsOpen)
            {
                return;
            }

            s_SerialPort = new SerialPort(port, rate, Parity.None, 8, StopBits.One)
            {
                WriteTimeout = 1000,
                DtrEnable = false
            };

            try
            {
                s_SerialPort.Open();
            }
            catch (UnauthorizedAccessException e)
            {
                throw new UCComException(StringRes.StringRes.UCComUnauthorizedAccess, e);
            }
            catch (IOException e)
            {
                throw new UCComException(StringRes.StringRes.UCComIOError, e);
            }

            kickoffRead();

            IsOpen = true;

            ConnectionOpened?.Invoke(null, EventArgs.Empty);
        }

        private static void kickoffRead()
        {
            byte[] buf = new byte[255];

            s_SerialPort.BaseStream.BeginRead(buf, 0, buf.Length, delegate (IAsyncResult ar)
            {
                int actualLength;

                try
                {
                    actualLength = s_SerialPort.BaseStream.EndRead(ar);
                }
                catch (Exception)
                {
                    if (!IsOpen)
                    {
                        return;
                    }
                    else
                    {
                        /*Under mono an exception is thrown and the GUI hangs if the serial port is opened
                         * immediately after closing it. It seems there are several threads
                         * still waiting to die left over from the previous 
                         * serialport instance. Waiting from 5 to 10 seconds before opening
                         * the port works but who has time to wait.....
                         * This works with mono and it could be a temporary workaround
                         * until understood better.
                         */
#if MONO
                        goto done;
#else
                        throw;
#endif

                    }
                }

                Buffer.BlockCopy(buf, 0, s_ResponseBuffer, s_ResponseBufferLength, actualLength);
                s_ResponseBufferLength += actualLength;

                if (s_ResponseBufferLength < 7) // Smallest possible response
                {
                    goto done;
                }

                int length = s_ResponseBuffer[2];

                if (s_ResponseBufferLength < 7 + length)
                {
                    goto done;
                }

                if (buf[actualLength - 1] != '\n')
                {
                    goto done;
                }

                s_GotResponse.Set();

            done:
                kickoffRead();
            }, null);
        }

        public static void Close()
        {
            if (!IsOpen)
            {
                return;
            }

            IsOpen = false;

            s_SerialPort.Close();
            s_SerialPort.Dispose();
            s_SerialPort = null;
            s_GotResponse.Reset();

            ConnectionClosed?.Invoke(null, EventArgs.Empty);
        }

        public static byte[] SendCommand(byte cmd, byte valA, byte valB)
        {
            if (!IsOpen)
            {
                throw new InvalidOperationException("Serial port connection is not open.");
            }

            int tries = 0;

        again:
            tries++;
            s_ResponseBufferLength = 0;
            s_GotResponse.Reset();

            byte[] payload = new byte[9];

            s_Counter = (s_Counter + 1) % 256;

            payload[0] = 0x07;
            payload[1] = (byte)s_Counter;
            payload[2] = cmd;
            payload[3] = valA;
            payload[4] = valB;
            payload[5] = 0x03;
            payload[6] = Checksum(payload, 0, 6);
            payload[7] = 0x04;
            payload[8] = (byte)'\n';

            s_SerialPort.Write(payload, 0, payload.Length);

            if (!s_GotResponse.Wait(500))
            {
                if (tries < 3)
                {
                    goto again;
                }
                else
                {
                    throw new UCComException(StringRes.StringRes.UCComNoResponse);
                }
            }

            payload = s_ResponseBuffer;

            if (payload[0] != 0x06)
            {
                throw new UCComException(StringRes.StringRes.UCComBadResponse,
                    new InvalidOperationException("Command did not execute successfully."));
            }

            if (payload[1] != s_Counter)
            {
                throw new UCComException(StringRes.StringRes.UCComBadResponse,
                    new InvalidDataException("Invalid payload ID. This is not a reponse to the request."));
            }

            int length = payload[2];

            if (7 + length > payload.Length)
            {
                throw new UCComException(StringRes.StringRes.UCComBadResponse,
                    new InvalidDataException("Reponse is not complete."));
            }

            if (payload[2 + length + 1] != 0x03)
            {
                throw new UCComException(StringRes.StringRes.UCComBadResponse,
                    new InvalidDataException("Missing end byte A in response."));
            }

            if (payload[2 + length + 2] != Checksum(payload, 0, 2 + length + 1 + 1))
            {
                throw new UCComException(StringRes.StringRes.UCComBadResponse,
                    new InvalidDataException("Checksum mismatch in response."));
            }

            if (payload[2 + length + 3] != 0x04)
            {
                throw new UCComException(StringRes.StringRes.UCComBadResponse,
                    new InvalidDataException("Missing end byte B in response."));
            }

            byte[] ret = new byte[length];
            Buffer.BlockCopy(payload, 3, ret, 0, length);

            return ret;
        }

        private static byte Checksum(byte[] arr, int offset, int length)
        {
            if (arr == null)
            {
                throw new ArgumentNullException(nameof(arr));
            }

            if (offset < 0 || offset > arr.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (length <= 0 || offset + length > arr.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            int sum = 1; // Checksum may never be 0.

            for (int i = offset; i < offset + length; i++)
            {
                sum += arr[i];
            }

            int ret = 256 - (sum % 256);

            return (byte)ret;
        }

        public static event EventHandler<EventArgs> ConnectionOpened;
        public static event EventHandler<EventArgs> ConnectionClosed;
    }

    [Serializable]
    public class UCComException : Exception
    {
        public UCComException() { }
        public UCComException(string message) : base(message) { }
        public UCComException(string message, Exception inner) : base(message, inner) { }
        protected UCComException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
