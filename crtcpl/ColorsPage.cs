using System;
using System.Windows.Forms;

namespace crtcpl
{
    public partial class ColorsPage : UserControl
    {
        public ColorsPage()
        {
            InitializeComponent();

            #region Cutoff
            this.redCutoffTrackBar.Minimum = Constants.IVAD_RED_CUTOFF_MIN;
            this.redCutoffTrackBar.Maximum = Constants.IVAD_RED_CUTOFF_MAX;

            this.redDriveTrackBar.Minimum = Constants.IVAD_RED_DRIVE_MIN;
            this.redDriveTrackBar.Maximum = Constants.IVAD_RED_DRIVE_MAX;

            this.greenCutoffTrackBar.Minimum = Constants.IVAD_GREEN_CUTOFF_MIN;
            this.greenCutoffTrackBar.Maximum = Constants.IVAD_GREEN_CUTOFF_MAX;
            #endregion

            #region Drive
            this.greenDriveTrackBar.Minimum = Constants.IVAD_GREEN_DRIVE_MIN;
            this.greenDriveTrackBar.Maximum = Constants.IVAD_GREEN_DRIVE_MAX;

            this.blueCutoffTrackBar.Minimum = Constants.IVAD_BLUE_CUTOFF_MIN;
            this.blueCutoffTrackBar.Maximum = Constants.IVAD_BLUE_CUTOFF_MAX;

            this.blueDriveTrackBar.Minimum = Constants.IVAD_BLUE_DRIVE_MIN;
            this.blueDriveTrackBar.Maximum = Constants.IVAD_BLUE_DRIVE_MAX;
            #endregion
        }

        public void SetValues(int r_cutoff, int g_cutoff, int b_cutoff, int r_drive, int g_drive, int b_drive)
        {
            #region Cutoff
            if (r_cutoff < this.redCutoffTrackBar.Minimum)
            {
                r_cutoff = this.redCutoffTrackBar.Minimum;
            }
            else if (r_cutoff > this.redCutoffTrackBar.Maximum)
            {
                r_cutoff = this.redCutoffTrackBar.Maximum;
            }

            if (g_cutoff < this.greenCutoffTrackBar.Minimum)
            {
                g_cutoff = this.greenCutoffTrackBar.Minimum;
            }
            else if (g_cutoff > this.greenCutoffTrackBar.Maximum)
            {
                g_cutoff = this.greenCutoffTrackBar.Maximum;
            }

            if (b_cutoff < this.blueCutoffTrackBar.Minimum)
            {
                b_cutoff = this.blueCutoffTrackBar.Minimum;
            }
            else if (b_cutoff > this.blueCutoffTrackBar.Maximum)
            {
                b_cutoff = this.blueCutoffTrackBar.Maximum;
            }

            this.redCutoffTrackBar.Value = r_cutoff;
            this.greenCutoffTrackBar.Value = g_cutoff;
            this.blueCutoffTrackBar.Value = b_cutoff;
            #endregion

            #region Drive
            if (r_drive < this.redDriveTrackBar.Minimum)
            {
                r_drive = this.redDriveTrackBar.Minimum;
            }
            else if (r_drive > this.redDriveTrackBar.Maximum)
            {
                r_drive = this.redDriveTrackBar.Maximum;
            }

            if (g_drive < this.greenDriveTrackBar.Minimum)
            {
                g_drive = this.greenDriveTrackBar.Minimum;
            }
            else if (g_drive > this.greenDriveTrackBar.Maximum)
            {
                g_drive = this.greenDriveTrackBar.Maximum;
            }

            if (b_drive < this.blueDriveTrackBar.Minimum)
            {
                b_drive = this.blueDriveTrackBar.Minimum;
            }
            else if (b_drive > this.blueDriveTrackBar.Maximum)
            {
                b_drive = this.blueDriveTrackBar.Maximum;
            }

            this.redDriveTrackBar.Value = r_drive;
            this.greenDriveTrackBar.Value = g_drive;
            this.blueDriveTrackBar.Value = b_drive;
            #endregion
        }

        private void redCutoffTrackBar_Scroll(object sender, EventArgs e)
        {
            OnColorChanged(new ColorsPageEventArgs(ColorsPageEventArgs.ChangedSetting.RedCutoff, ((TrackBar)sender).Value));
        }

        private void greenCutoffTrackBar_Scroll(object sender, EventArgs e)
        {
            OnColorChanged(new ColorsPageEventArgs(ColorsPageEventArgs.ChangedSetting.GreenCutoff, ((TrackBar)sender).Value));
        }

        private void blueCutoffTrackBar_Scroll(object sender, EventArgs e)
        {
            OnColorChanged(new ColorsPageEventArgs(ColorsPageEventArgs.ChangedSetting.BlueCutoff, ((TrackBar)sender).Value));
        }

        private void redDriveTrackBar_Scroll(object sender, EventArgs e)
        {
            OnColorChanged(new ColorsPageEventArgs(ColorsPageEventArgs.ChangedSetting.RedDrive, ((TrackBar)sender).Value));
        }

        private void greenDriveTrackBar_Scroll(object sender, EventArgs e)
        {
            OnColorChanged(new ColorsPageEventArgs(ColorsPageEventArgs.ChangedSetting.GreenDrive, ((TrackBar)sender).Value));
        }

        private void blueDriveTrackBar_Scroll(object sender, EventArgs e)
        {
            OnColorChanged(new ColorsPageEventArgs(ColorsPageEventArgs.ChangedSetting.BlueDrive, ((TrackBar)sender).Value));
        }

        protected virtual void OnColorChanged(ColorsPageEventArgs e)
        {
            ColorChanged?.Invoke(this, e);
        }

        public event EventHandler<ColorsPageEventArgs> ColorChanged;

        private void ColorsPage_Load(object sender, EventArgs e)
        {
            Control[] ctrls = new Control[]
            {
                this.redDriveLabel, this.redDriveTrackBar,
                this.greenDriveLabel, this.greenDriveTrackBar,
                this.blueDriveLabel, this.blueDriveTrackBar
            };

            foreach (Control ctrl in ctrls)
            {
                ctrl.Enabled = Settings.Default.AdvancedControls;
                ctrl.Visible = Settings.Default.AdvancedControls;
            }

            // When they become invisible, the color gradient fills up their space.
            // This is why we do not need to reposition the TableLayoutPanel here
            // like we have to in ScreenPage.
        }
    }
}
