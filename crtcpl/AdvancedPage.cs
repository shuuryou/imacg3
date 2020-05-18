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

            foreach (int rate in UCCom.AvailableBitRates)
            {
                this.rateComboBox.Items.Add(rate);
            }

            for (int i = 0; i < this.comPortComboBox.Items.Count; i++)
            {
                if (((string)this.comPortComboBox.Items[i]).Equals(Settings.Default.SerialPort, StringComparison.OrdinalIgnoreCase))
                {
                    this.comPortComboBox.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < this.rateComboBox.Items.Count; i++)
            {
                if (((int)this.rateComboBox.Items[i]).Equals(Settings.Default.SerialRate))
                {
                    this.rateComboBox.SelectedIndex = i;
                    break;
                }
            }

            // Fake it once on load since we don't know the state at this point
            if (UCCom.IsOpen)
            {
                UCCom_ConnectionOpened(null, EventArgs.Empty);
            }
            else
            {
                UCCom_ConnectionClosed(null, EventArgs.Empty);
            }

            this.advancedCheckBox.Checked = Settings.Default.AdvancedControls;

            // Do this here so the above doesn't trigger the message box
            this.advancedCheckBox.CheckedChanged += advancedCheckBox_CheckedChanged;
        }

        private void UCCom_ConnectionClosed(object sender, EventArgs e)
        {
            this.comPortLabel.Enabled =
                this.comPortComboBox.Enabled =
                this.rateLabel.Enabled =
                this.rateComboBox.Enabled = true;

            ComboBox_SelectedIndexChanged(null, EventArgs.Empty);
        }

        private void UCCom_ConnectionOpened(object sender, EventArgs e)
        {
            this.comPortLabel.Enabled =
                this.comPortComboBox.Enabled =
                this.rateLabel.Enabled =
                this.rateComboBox.Enabled = false;

            ComboBox_SelectedIndexChanged(null, EventArgs.Empty);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.comPortComboBox.Text))
            {
                MessageBox.Show(this.ParentForm, StringRes.StringRes.ComErrorBadPort,
                    StringRes.StringRes.ComErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (string.IsNullOrWhiteSpace(this.rateComboBox.Text))
            {
                MessageBox.Show(this.ParentForm, StringRes.StringRes.ComErrorBadRate,
                    StringRes.StringRes.ComErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            try
            {
                UCCom.Open(this.comPortComboBox.Text, (int)this.rateComboBox.Items[this.rateComboBox.SelectedIndex]);
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

            Settings.Default.SerialPort = this.comPortComboBox.Text;
            Settings.Default.SerialRate = (int)this.rateComboBox.SelectedItem;
            OnSettingChanged(new AdvancedPageEventArgs());
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            UCCom.Close();
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSettingChanged(new AdvancedPageEventArgs());
            this.connectButton.Enabled = !UCCom.IsOpen &&
                (this.comPortComboBox.SelectedIndex != -1 && this.rateComboBox.SelectedIndex != -1);
            this.disconnectButton.Enabled = UCCom.IsOpen;
        }

        private void advancedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.AdvancedControls = this.advancedCheckBox.Checked;
            OnSettingChanged(new AdvancedPageEventArgs());

            if (!this.advancedCheckBox.Checked)
            {
                return;
            }

            DialogResult res = MessageBox.Show(this.ParentForm, StringRes.StringRes.AdvancedControlsAreDangerous,
                    StringRes.StringRes.AdvancedControlsAreDangerousTitle, MessageBoxButtons.YesNo,
                     MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (res != DialogResult.Yes)
            {
                this.advancedCheckBox.Checked = false;
            }
        }

        protected virtual void OnSettingChanged(AdvancedPageEventArgs e)
        {
            SettingChanged?.Invoke(this, e);
        }

        public event EventHandler<AdvancedPageEventArgs> SettingChanged;
    }
}