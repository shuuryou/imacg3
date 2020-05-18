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

            if (Settings.Default.AdvancedControls)
            {
                this.brightnessTrackBar.Maximum = Constants.IVAD_BRIGHTNESS_MAX_OVERDRIVE;
            }
            else
            {
                this.brightnessTrackBar.Maximum = Constants.IVAD_BRIGHTNESS_MAX;
            }

            this.contrastTrackBar.Minimum = Constants.IVAD_CONTRAST_MIN;
            this.contrastTrackBar.Maximum = Constants.IVAD_CONTRAST_MAX;
        }

        public void SetValues(int brightness, int contrast)
        {
            if (brightness < this.brightnessTrackBar.Minimum)
            {
                brightness = this.brightnessTrackBar.Minimum;
            }
            else if (brightness > this.brightnessTrackBar.Maximum)
            {
                brightness = this.brightnessTrackBar.Maximum;
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
            this.contrastTrackBar.Value = contrast;
        }

        private void brightnessTrackBar_Scroll(object sender, EventArgs e)
        {
            OnBrighnessChanged(new ScreenPageEventArgs(this.brightnessTrackBar.Value));
        }

        private void contrastTrackBar_Scroll(object sender, EventArgs e)
        {
            OnContrastChanged(new ScreenPageEventArgs(this.contrastTrackBar.Value));
        }

        protected virtual void OnBrighnessChanged(ScreenPageEventArgs e)
        {
            BrightnessChanged?.Invoke(this, e);
        }

        protected virtual void OnContrastChanged(ScreenPageEventArgs e)
        {
            ContrastChanged?.Invoke(this, e);
        }

        public event EventHandler<ScreenPageEventArgs> BrightnessChanged;
        public event EventHandler<ScreenPageEventArgs> ContrastChanged;
    }
}
