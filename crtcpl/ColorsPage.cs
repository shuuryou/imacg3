using System;
using System.Windows.Forms;

namespace crtcpl
{
    public partial class ColorsPage : UserControl
    {
        public ColorsPage()
        {
            InitializeComponent();

            this.redTrackBar.Minimum = Constants.IVAD_RED_MIN;
            this.redTrackBar.Maximum = Constants.IVAD_RED_MAX;
            this.greenTrackBar.Minimum = Constants.IVAD_GREEN_MIN;
            this.greenTrackBar.Maximum = Constants.IVAD_GREEN_MAX;
            this.blueTrackBar.Minimum = Constants.IVAD_BLUE_MIN;
            this.blueTrackBar.Maximum = Constants.IVAD_BLUE_MAX;
        }

        public void SetValues(int r, int g, int b)
        {
            if (r < this.redTrackBar.Minimum)
            {
                r = this.redTrackBar.Minimum;
            }
            else if (r > this.redTrackBar.Maximum)
            {
                r = this.redTrackBar.Maximum;
            }

            if (g < this.greenTrackBar.Minimum)
            {
                g = this.greenTrackBar.Minimum;
            }
            else if (g > this.greenTrackBar.Maximum)
            {
                g = this.greenTrackBar.Maximum;
            }

            if (b < this.blueTrackBar.Minimum)
            {
                b = this.blueTrackBar.Minimum;
            }
            else if (b > this.blueTrackBar.Maximum)
            {
                b = this.blueTrackBar.Maximum;
            }

            this.redTrackBar.Value = r;
            this.greenTrackBar.Value = g;
            this.blueTrackBar.Value = b;
        }

        private void redTrackBar_Scroll(object sender, EventArgs e)
        {
            OnColorChanged(new ColorsPageEventArgs(ColorsPageEventArgs.ChangedColor.Red, this.redTrackBar.Value));
        }

        private void greenTrackBar_Scroll(object sender, EventArgs e)
        {
            OnColorChanged(new ColorsPageEventArgs(ColorsPageEventArgs.ChangedColor.Green, this.greenTrackBar.Value));
        }

        private void blueTrackBar_Scroll(object sender, EventArgs e)
        {
            OnColorChanged(new ColorsPageEventArgs(ColorsPageEventArgs.ChangedColor.Blue, this.blueTrackBar.Value));
        }

        protected virtual void OnColorChanged(ColorsPageEventArgs e)
        {
            ColorChanged?.Invoke(this, e);
        }

        public event EventHandler<ColorsPageEventArgs> ColorChanged;
    }
}
