using System;
using System.Windows.Forms;

namespace crtcpl
{
    public partial class ScreenPage : UserControl
    {
        public ScreenPage()
        {
            InitializeComponent();

            this.brightnessTrackBar.Minimum = Constants.IVAD_BRIGHTNESS_MIN;
            this.brightnessTrackBar.Maximum = Settings.Default.AdvancedControls ?
                Constants.IVAD_BRIGHTNESS_MAX_OVERDRIVE : Constants.IVAD_BRIGHTNESS_MAX;

            this.brightnessDriveTrackBar.Minimum = Constants.IVAD_BRIGHTNESS_DRIVE_MIN;
            this.brightnessDriveTrackBar.Maximum = Constants.IVAD_BRIGHTNESS_DRIVE_MAX;

            this.contrastTrackBar.Minimum = Constants.IVAD_CONTRAST_MIN;
            this.contrastTrackBar.Maximum = Constants.IVAD_CONTRAST_MAX;

            this.brightnessDriveGroupBox.Visible =
                this.brightnessDriveGroupBox.Enabled = Settings.Default.AdvancedControls;

            // It moves on its own later due to WinForms positioning it based on its
            // Anchor property, but that's relative to its starting position, which
            // we need to correct once.
            this.tableLayoutPanel.Top = (this.Height - this.tableLayoutPanel.Height) / 2;
        }

        public void SetValues(int brightness, int brightness_drive, int contrast)
        {
            if (brightness < this.brightnessTrackBar.Minimum)
            {
                brightness = this.brightnessTrackBar.Minimum;
            }
            else if (brightness > this.brightnessTrackBar.Maximum)
            {
                brightness = this.brightnessTrackBar.Maximum;
            }

            if (brightness_drive < this.brightnessDriveTrackBar.Minimum)
            {
                brightness_drive = this.brightnessDriveTrackBar.Minimum;
            }
            else if (brightness_drive > this.brightnessDriveTrackBar.Maximum)
            {
                brightness_drive = this.brightnessDriveTrackBar.Maximum;
            }

            if (contrast < this.contrastTrackBar.Minimum)
            {
                contrast = this.contrastTrackBar.Minimum;
            }
            else if (contrast > this.contrastTrackBar.Maximum)
            {
                contrast = this.contrastTrackBar.Maximum;
            }

            this.brightnessTrackBar.Value = brightness;
            this.brightnessDriveTrackBar.Value = brightness_drive;
            this.contrastTrackBar.Value = contrast;
        }

        private void brightnessTrackBar_Scroll(object sender, EventArgs e)
        {
            OnBrighnessChanged(new ScreenPageEventArgs(this.brightnessTrackBar.Value));
        }

        private void brightnessDriveTrackBar_Scroll(object sender, EventArgs e)
        {
            // The brightness drive maximum is most dim, while the brightness maximum is most bright.
            // We subtract drive maximum from drive value to make it act like the brightness slider.
            OnBrighnessDriveChanged(new ScreenPageEventArgs(this.brightnessDriveTrackBar.Maximum - this.brightnessDriveTrackBar.Value));
        }

        private void contrastTrackBar_Scroll(object sender, EventArgs e)
        {
            OnContrastChanged(new ScreenPageEventArgs(this.contrastTrackBar.Value));
        }

        protected virtual void OnBrighnessChanged(ScreenPageEventArgs e)
        {
            BrightnessChanged?.Invoke(this, e);
        }

        protected virtual void OnBrighnessDriveChanged(ScreenPageEventArgs e)
        {
            BrightnessDriveChanged?.Invoke(this, e);
        }

        protected virtual void OnContrastChanged(ScreenPageEventArgs e)
        {
            ContrastChanged?.Invoke(this, e);
        }

        public event EventHandler<ScreenPageEventArgs> BrightnessChanged;
        public event EventHandler<ScreenPageEventArgs> BrightnessDriveChanged;
        public event EventHandler<ScreenPageEventArgs> ContrastChanged;
    }
}
