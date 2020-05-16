#if !ROCKYHILL
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;

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

        public static ReadOnlyCollection<string> AvailablePorts =
            new ReadOnlyCollection<string>(SerialPort.GetPortNames());

        public static ReadOnlyCollection<int> AvailableBitRates = new
                ReadOnlyCollection<int>(new int[] { 300, 600, 1200, 2400, 4800, 9600,
                    14400, 19200, 28800, 31250, 38400, 57600, 115200 });

        public static bool IsOpen { get; private set; }
        public static void Open(string port, int rate)
        {
            Logging.WriteBannerToLog("Open");

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

            Logging.WriteLineToLog("Create serial port. Port={0}, Rate={1}", port, rate);
            s_SerialPort = new SerialPort(port, rate, Parity.None, 8, StopBits.One)
            {
                WriteTimeout = 1000,
                ReadTimeout = 1000,
                DtrEnable = false // DTR resets Arduino
            };

            Logging.WriteLineToLog("Open the port now.");
            try
            {
                s_SerialPort.Open();
            }
            catch (UnauthorizedAccessException e)
            {
                Logging.WriteLineToLog("Exception from serial port: {0}", e);
                throw new UCComException(StringRes.StringRes.UCComUnauthorizedAccess, e);
            }
            catch (IOException e)
            {
                Logging.WriteLineToLog("Exception from serial port: {0}", e);
                throw new UCComException(StringRes.StringRes.UCComIOError, e);
            }

            Logging.WriteLineToLog("Compatibility check now.");
            byte[] ret;
            try
            {
                ret = SendCommandInternal(1, 0, 0);

                if (ret == null || ret.Length != 1 || ret[0] != Constants.SUPPORTED_EEPROM_VERSION)
                {
                    throw new UCComException(StringRes.StringRes.UCComBadVersion);
                }
            }
            catch (UCComException)
            {
                CloseInternal();
                throw;
            }

            Logging.WriteLineToLog("Serial port is open.");
            IsOpen = true;

            ConnectionOpened?.Invoke(null, EventArgs.Empty);
        }

        public static void Close()
        {
            Logging.WriteBannerToLog("Close");

            if (!IsOpen)
            {
                return;
            }

            CloseInternal();

            Logging.WriteLineToLog("Serial port is closed.");
            IsOpen = false;

            ConnectionClosed?.Invoke(null, EventArgs.Empty);
        }

        private static void CloseInternal()
        {
            Logging.WriteBannerToLog("CloseInternal");

            if (s_SerialPort != null)
            {
                Logging.WriteLineToLog("Dispose serial port.");
                s_SerialPort.Close();
                s_SerialPort.Dispose();
            }

            Logging.WriteLineToLog("Set serial port to null.");
            s_SerialPort = null;
        }

        public static byte[] SendCommand(byte cmd, byte valA, byte valB)
        {
            Logging.WriteBannerToLog("SendCommand");

            if (!IsOpen)
            {
                throw new InvalidOperationException("Serial port connection is not open.");
            }

            return SendCommandInternal(cmd, valA, valB);
        }

        private static byte[] SendCommandInternal(byte cmd, byte valA, byte valB)
        {
            Logging.WriteBannerToLog("SendCommandInternal");

            int tries = 0;

        again:
            tries++;

            if (tries > 3)
            {
                Logging.WriteLineToLog("Too many retries. Failing.");
                throw new UCComException(StringRes.StringRes.UCComNoResponse);
            }

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

            Logging.WriteLineToLog("Send this payload to microcontroller:");
            Logging.WriteBufferToLog(payload, 0, payload.Length);

            byte[] response = new byte[255];
            int readsofar = 0;
            int length;

            Logging.WriteLineToLog("Wait for response now.");

            try
            {
                for (int iterations = 0; ; iterations++)
                {
                    Logging.WriteLineToLog("Iteration {0}", iterations);


                    int read = s_SerialPort.Read(response, readsofar, response.Length - readsofar);
                    readsofar += read;

                    Logging.WriteLineToLog("Read so far from serial port: {0} bytes. In this round: {1} bytes.", readsofar, read);

                    if (readsofar == 0)
                    {
                        Logging.WriteLineToLog("Read nothing. Resend command.");
                        goto again;
                    }

                    if (readsofar < 7)
                    {
                        Logging.WriteLineToLog("Read too little to have valid response. Read more.");
                        continue;
                    }

                    length = response[2];
                    Logging.WriteLineToLog("Length from response: {0} bytes", length);

                    if (readsofar < 7 + length)
                    {
                        Logging.WriteLineToLog("Read too little according to length. Read more.");
                        continue;
                    }

                    if (response[readsofar - 1] != '\n')
                    {
                        Logging.WriteLineToLog("Still missing EOL marker.");
                        continue;
                    }

                    if (iterations > 1000)
                    {
                        Logging.WriteLineToLog("Too many iterations!! Crash now!");
                        throw new UCComException(StringRes.StringRes.UCComNoResponse);
                    }

                    // Looks like we have everything
                    Logging.WriteLineToLog("Got response OK!");
                    break;
                }
            }
            catch (IOException e)
            {
                Logging.WriteLineToLog("Exception from serial port: {0}", e.ToString());
                throw new UCComException(StringRes.StringRes.UCComIOError, e);
            }
            catch (TimeoutException)
            {
                Logging.WriteLineToLog("Time out from serial port.");
                goto again;
            }

            if (response[0] != 0x06)
            {
                Logging.WriteLineToLog("Command did not execute successfully.");
                throw new UCComException(StringRes.StringRes.UCComBadResponse,
                    new InvalidOperationException("Command did not execute successfully."));
            }

            if (response[1] != s_Counter)
            {
                Logging.WriteLineToLog("Invalid payload ID. This is not a reponse to the request. Got {0}, but wanted {1}.",
                    response[1], s_Counter);

                throw new UCComException(StringRes.StringRes.UCComBadResponse,
                    new InvalidDataException("Invalid payload ID. This is not a reponse to the request."));
            }

            if (7 + length > response.Length)
            {
                Logging.WriteLineToLog("Reponse is not complete.");

                throw new UCComException(StringRes.StringRes.UCComBadResponse,
                 new InvalidDataException("Reponse is not complete."));
            }

            if (response[2 + length + 1] != 0x03)
            {
                Logging.WriteLineToLog("Missing end byte A in response.");

                throw new UCComException(StringRes.StringRes.UCComBadResponse,
                    new InvalidDataException("Missing end byte A in response."));
            }

            byte expected_checksum = Checksum(response, 0, 2 + length + 1 + 1);

            if (response[2 + length + 2] != expected_checksum)
            {
                Logging.WriteLineToLog("Checksum mismatch in response. Got {0}, but expected {1}.",
                    response[2 + length + 2], expected_checksum);

                throw new UCComException(StringRes.StringRes.UCComBadResponse,
                    new InvalidDataException("Checksum mismatch in response."));
            }

            if (response[2 + length + 3] != 0x04)
            {
                Logging.WriteLineToLog("Missing end byte B in response.");

                throw new UCComException(StringRes.StringRes.UCComBadResponse,
                    new InvalidDataException("Missing end byte B in response."));
            }

            byte[] ret = new byte[length];
            Buffer.BlockCopy(response, 3, ret, 0, length);

            Logging.WriteLineToLog("The response from microcontroller was:");
            Logging.WriteBufferToLog(response, 0, readsofar);

            Logging.WriteLineToLog("The command response is:");
            Logging.WriteBufferToLog(ret, 0, ret.Length);

            return ret;
        }

        private static byte Checksum(byte[] arr, int offset, int length)
        {
            Logging.WriteBannerToLog("Checksum");

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

            Logging.WriteLineToLog("The checksum was calculated to be 0x{0:X2}.", ret);

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
#endif