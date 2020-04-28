using System;
using System.Globalization;
using System.Windows.Forms;

namespace crtcpl
{
    public partial class AdvancedPage : UserControl
    {
        public AdvancedPage()
        {
            InitializeComponent();

            UCCom.ConnectionOpened += UCCom_ConnectionOpened;
            UCCom.ConnectionClosed += UCCom_ConnectionClosed;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();

                UCCom.ConnectionOpened -= UCCom_ConnectionOpened;
                UCCom.ConnectionClosed -= UCCom_ConnectionClosed;
            }
            base.Dispose(disposing);
        }

        private void AdvancedPage_Load(object sender, EventArgs e)
        {
            foreach (string port in UCCom.AvailablePorts)
            {
                this.comPortComboBox.Items.Add(port);
            }

            for (int i = 0; i < this.comPortComboBox.Items.Count; i++)
            {
                if (((string)this.comPortComboBox.Items[i]).Equals(Settings.Default.SerialPort, StringComparison.OrdinalIgnoreCase))
                {
                    this.comPortComboBox.SelectedIndex = i;
                    break;
                }
            }

            comPortComboBox_SelectedIndexChanged(null, EventArgs.Empty);
        }

        private void UCCom_ConnectionClosed(object sender, EventArgs e)
        {
            this.comPortComboBox.Enabled = true;
            comPortComboBox_SelectedIndexChanged(null, EventArgs.Empty);
        }

        private void UCCom_ConnectionOpened(object sender, EventArgs e)
        {
            this.comPortComboBox.Enabled = false;
            comPortComboBox_SelectedIndexChanged(null, EventArgs.Empty);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.comPortComboBox.Text))
            {
                MessageBox.Show(this.ParentForm, StringRes.StringRes.ComErrorBadPort,
                    StringRes.StringRes.ComErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            byte[] ret;

            try
            {
                UCCom.Open(this.comPortComboBox.Text);
                ret = UCCom.SendCommand(1, 0, 0);
            }
            catch (UCComException ex)
            {
                if (UCCom.IsOpen)
                {
                    UCCom.Close();
                }

                MessageBox.Show(this.ParentForm, string.Format(CultureInfo.CurrentCulture,
                    StringRes.StringRes.ComErrorOther, ex.Message), StringRes.StringRes.ComErrorTitle,
                     MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (ret == null || ret.Length != 1 || ret[0] != Program.SUPPORTED_EEPROM_VERSION)
            {
                UCCom.Close();

                MessageBox.Show(this.ParentForm, StringRes.StringRes.ComErrorBadVersion,
                    StringRes.StringRes.ComErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            Settings.Default.SerialPort = this.comPortComboBox.Text;
            Settings.Default.Save();
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            UCCom.Close();
        }

        private void comPortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.connectButton.Enabled = !UCCom.IsOpen && this.comPortComboBox.SelectedIndex != -1;
            this.disconnectButton.Enabled = UCCom.IsOpen;
        }
    }
}
