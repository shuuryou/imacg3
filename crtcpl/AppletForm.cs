using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace crtcpl
{
    public partial class AppletForm : Form
    {
        private TestPatternForm m_TestPatternForm = null;
        private SettingsAnalyzerForm m_SettingsAnalyzerForm = null;
        public AppletForm()
        {
            InitializeComponent();

            UCCom.ConnectionClosed += UCCom_ConnectionClosed;
            UCCom.ConnectionOpened += UCCom_ConnectionOpened;

            if (UCCom.IsOpen)
            {
                UpdatePagesFromSRAM();
            }

            Rectangle resolution = Screen.FromHandle(this.Handle).Bounds;

            if (resolution.Width != 640 && resolution.Width != 800 && resolution.Width != 1024)
            {
                this.fuBKTestCardToolStripMenuItem.Enabled = false;
                this.sMPTEColorBarToolStripMenuItem.Enabled = false;
            }
        }

        private void UpdatePagesFromSRAM()
        {
            Trace.Assert(UCCom.IsOpen, "UCCom has no connection at the moment!");

            byte[] sram;

            try
            {
                sram = UCCom.SendCommand(2, 0, 0);
            }
            catch (UCComException ex)
            {
                MessageBox.Show(this, string.Format(CultureInfo.CurrentCulture, StringRes.StringRes.CantUpdatePages,
                    ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            this.SCREEN.SetValues(sram[Constants.CONFIG_OFFSET_BRIGHTNESS], sram[Constants.CONFIG_OFFSET_BRIGHTNESS_DRIVE], sram[Constants.CONFIG_OFFSET_CONTRAST]);

            this.COLORS.SetValues(
                sram[Constants.CONFIG_OFFSET_RED_CUTOFF], sram[Constants.CONFIG_OFFSET_GREEN_CUTOFF], sram[Constants.CONFIG_OFFSET_BLUE_CUTOFF],
                sram[Constants.CONFIG_OFFSET_RED_DRIVE], sram[Constants.CONFIG_OFFSET_GREEN_DRIVE], sram[Constants.CONFIG_OFFSET_BLUE_DRIVE]);

            this.GEOMETRY.SetValues(sram[Constants.CONFIG_OFFSET_HORIZONTAL_POS], sram[Constants.CONFIG_OFFSET_HEIGHT],
                sram[Constants.CONFIG_OFFSET_VERTICAL_POS], sram[Constants.CONFIG_OFFSET_KEYSTONE], sram[Constants.CONFIG_OFFSET_PINCUSHION],
                 sram[Constants.CONFIG_OFFSET_PINCUSHION_BALANCE], sram[Constants.CONFIG_OFFSET_S_CORRECTION], sram[Constants.CONFIG_OFFSET_WIDTH],
                 sram[Constants.CONFIG_OFFSET_PARALLELOGRAM], sram[Constants.CONFIG_OFFSET_ROTATION]);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
                UCCom.ConnectionClosed -= UCCom_ConnectionClosed;
                UCCom.ConnectionOpened -= UCCom_ConnectionOpened;

                if (this.m_TestPatternForm != null)
                {
                    this.m_TestPatternForm.Dispose();
                }

                if (this.m_SettingsAnalyzerForm != null)
                {
                    this.m_SettingsAnalyzerForm.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void Applet_Load(object sender, EventArgs e)
        {
            this.SCREEN.Visible = this.GEOMETRY.Visible = this.COLORS.Visible = this.ADVANCED.Visible = false;
            this.SCREEN.Dock = this.GEOMETRY.Dock = this.COLORS.Dock = this.ADVANCED.Dock = DockStyle.Fill;

            if (string.IsNullOrWhiteSpace(Settings.Default.SerialPort))
            {
                UCCom_ConnectionClosed(null, EventArgs.Empty);
                return;
            }

            screenRadioButton_CheckedChanged(null, EventArgs.Empty);
        }

        private void screenRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();
            this.GEOMETRY.Visible = this.COLORS.Visible = this.ADVANCED.Visible = false;
            this.SCREEN.Visible = true;
            ResumeLayout();
        }

        private void geometryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();
            this.SCREEN.Visible = this.COLORS.Visible = this.ADVANCED.Visible = false;
            this.GEOMETRY.Visible = true;
            ResumeLayout();
        }

        private void colorsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();
            this.SCREEN.Visible = this.GEOMETRY.Visible = this.ADVANCED.Visible = false;
            this.COLORS.Visible = true;
            ResumeLayout();
        }

        private void advancedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();
            this.SCREEN.Visible = this.GEOMETRY.Visible = this.COLORS.Visible = false;
            this.ADVANCED.Visible = true;
            ResumeLayout();
        }

        private void UCCom_ConnectionOpened(object sender, EventArgs e)
        {
            this.screenRadioButton.Enabled = true;
            this.geometryRadioButton.Enabled = true;
            this.colorsRadioButton.Enabled = true;
            this.defaultsButton.Enabled = true;
            this.applyButton.Enabled = false;
            this.showSettingsanalyzerToolStripMenuItem.Enabled = true;

            UpdatePagesFromSRAM();
        }

        private void UCCom_ConnectionClosed(object sender, EventArgs e)
        {
            this.screenRadioButton.Enabled = false;
            this.geometryRadioButton.Enabled = false;
            this.colorsRadioButton.Enabled = false;
            this.defaultsButton.Enabled = false;
            this.applyButton.Enabled = false;
            this.showSettingsanalyzerToolStripMenuItem.Enabled = false;

            advancedRadioButton_CheckedChanged(null, EventArgs.Empty);
        }

        private void defaultsButton_Click(object sender, EventArgs e)
        {
            if (!UCCom.IsOpen)
            {
                return;
            }

            try
            {
                UCCom.SendCommand(5, 0, 0);
            }
            catch (UCComException ex)
            {
                MessageBox.Show(this, string.Format(CultureInfo.CurrentCulture, StringRes.StringRes.CantResetDefaults,
                    ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            UpdatePagesFromSRAM();
            this.applyButton.Enabled = false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (UCCom.IsOpen)
            {
                try
                {
                    UCCom.SendCommand(4, 0, 0);
                }
                catch (UCComException ex)
                {
                    MessageBox.Show(this, string.Format(CultureInfo.CurrentCulture, StringRes.StringRes.CantUndoChanges,
                        ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            Close();
        }
        private void applyButton_Click(object sender, EventArgs e)
        {
            Settings.Save();

            if (!UCCom.IsOpen)
            {
                goto end;
            }

            try
            {
                UCCom.SendCommand(6, 0, 0);
            }
            catch (UCComException ex)
            {
                MessageBox.Show(this, string.Format(CultureInfo.CurrentCulture, StringRes.StringRes.CantApplyChanges,
                    ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return; // Not "goto end;" to allow trying again
            }

        end:
            this.applyButton.Enabled = false;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (this.applyButton.Enabled)
            {
                applyButton_Click(null, EventArgs.Empty);
            }

            Close();
        }

        private void GEOMETRY_GeometryChanged(object sender, GeometryPageEventArgs e)
        {
            if (!UCCom.IsOpen)
            {
                return;
            }

            byte what;

            switch (e.What)
            {
                case GeometryPageEventArgs.ChangedGemoetry.Height:
                    what = Constants.IVAD_SETTING_HEIGHT;
                    break;
                case GeometryPageEventArgs.ChangedGemoetry.Horizontal:
                    what = Constants.IVAD_SETTING_HORIZONTAL_POS;
                    break;
                case GeometryPageEventArgs.ChangedGemoetry.Keystone:
                    what = Constants.IVAD_SETTING_KEYSTONE;
                    break;
                case GeometryPageEventArgs.ChangedGemoetry.Parallelogram:
                    what = Constants.IVAD_SETTING_PARALLELOGRAM;
                    break;
                case GeometryPageEventArgs.ChangedGemoetry.Pincushion:
                    what = Constants.IVAD_SETTING_PINCUSHION;
                    break;
                case GeometryPageEventArgs.ChangedGemoetry.PincushionBalance:
                    what = Constants.IVAD_SETTING_PINCUSHION_BALANCE;
                    break;
                case GeometryPageEventArgs.ChangedGemoetry.SCorrection:
                    what = Constants.IVAD_SETTING_S_CORRECTION;
                    break;
                case GeometryPageEventArgs.ChangedGemoetry.Rotation:
                    what = Constants.IVAD_SETTING_ROTATION;
                    break;
                case GeometryPageEventArgs.ChangedGemoetry.Vertical:
                    what = Constants.IVAD_SETTING_VERTICAL_POS;
                    break;
                case GeometryPageEventArgs.ChangedGemoetry.Width:
                    what = Constants.IVAD_SETTING_WIDTH;
                    break;
                default:
                    Trace.Fail("Unknown geometry changed.");
                    return;
            }

            try
            {
                UCCom.SendCommand(3, what, (byte)e.NewValue);
            }
            catch (UCComException ex)
            {
                MessageBox.Show(this, string.Format(CultureInfo.CurrentCulture, StringRes.StringRes.CantPerformChange,
                    ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            this.applyButton.Enabled = true;
        }

        private void COLORS_ColorChanged(object sender, ColorsPageEventArgs e)
        {
            if (!UCCom.IsOpen)
            {
                return;
            }

            byte what;

            switch (e.Setting)
            {
                case ColorsPageEventArgs.ChangedSetting.RedCutoff:
                    what = Constants.IVAD_SETTING_RED_CUTOFF;
                    break;
                case ColorsPageEventArgs.ChangedSetting.GreenCutoff:
                    what = Constants.IVAD_SETTING_GREEN_CUTOFF;
                    break;
                case ColorsPageEventArgs.ChangedSetting.BlueCutoff:
                    what = Constants.IVAD_SETTING_BLUE_CUTOFF;
                    break;
                case ColorsPageEventArgs.ChangedSetting.RedDrive:
                    what = Constants.IVAD_SETTING_RED_DRIVE;
                    break;
                case ColorsPageEventArgs.ChangedSetting.GreenDrive:
                    what = Constants.IVAD_SETTING_GREEN_DRIVE;
                    break;
                case ColorsPageEventArgs.ChangedSetting.BlueDrive:
                    what = Constants.IVAD_SETTING_BLUE_DRIVE;
                    break;
                default:
                    Trace.Fail("Unknown setting changed.");
                    return;
            }

            try
            {
                UCCom.SendCommand(3, what, (byte)e.NewValue);
            }
            catch (UCComException ex)
            {
                MessageBox.Show(this, string.Format(CultureInfo.CurrentCulture, StringRes.StringRes.CantPerformChange,
                    ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            this.applyButton.Enabled = true;
        }

        private void ADVANCED_SettingChanged(object sender, AdvancedPageEventArgs e)
        {
            this.applyButton.Enabled = true;
        }

        private void SCREEN_ContrastChanged(object sender, ScreenPageEventArgs e)
        {
            if (!UCCom.IsOpen)
            {
                return;
            }

            try
            {
                UCCom.SendCommand(3, Constants.IVAD_SETTING_CONTRAST, (byte)e.NewValue);
            }
            catch (UCComException ex)
            {
                MessageBox.Show(this, string.Format(CultureInfo.CurrentCulture, StringRes.StringRes.CantPerformChange,
                    ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            this.applyButton.Enabled = true;
        }

        private void SCREEN_BrightnessChanged(object sender, ScreenPageEventArgs e)
        {
            if (!UCCom.IsOpen)
            {
                return;
            }

            try
            {
                UCCom.SendCommand(3, Constants.IVAD_SETTING_BRIGHTNESS, (byte)e.NewValue);
            }
            catch (UCComException ex)
            {
                MessageBox.Show(this, string.Format(CultureInfo.CurrentCulture, StringRes.StringRes.CantPerformChange,
                    ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            this.applyButton.Enabled = true;
        }

        private void SCREEN_BrightnessDriveChanged(object sender, ScreenPageEventArgs e)
        {
            if (!UCCom.IsOpen)
            {
                return;
            }

            try
            {
                UCCom.SendCommand(3, Constants.IVAD_SETTING_BRIGHTNESS_DRIVE, (byte)e.NewValue);
            }
            catch (UCComException ex)
            {
                MessageBox.Show(this, string.Format(CultureInfo.CurrentCulture, StringRes.StringRes.CantPerformChange,
                    ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            this.applyButton.Enabled = true;
        }

        private void showTestPatternToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.showTestPatternToolStripMenuItem.Checked)
            {
                this.m_TestPatternForm = new TestPatternForm();
                this.m_TestPatternForm.Show();
                this.TopMost = true;
                this.showTestPatternToolStripMenuItem.Checked = true;
                this.testPatternSelectionToolStripMenuItem.Enabled = true;
                testPatternSelectionToolStripMenuItem_Click(this.screenAdjustToolStripMenuItem, EventArgs.Empty);
                return;
            }
            this.m_TestPatternForm.Close();
            this.m_TestPatternForm.Dispose();
            this.m_TestPatternForm = null;
            this.TopMost = false;
            this.showTestPatternToolStripMenuItem.Checked = false;
            this.testPatternSelectionToolStripMenuItem.Enabled = false;
        }

        private void testPatternSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tag = int.Parse(((ToolStripMenuItem)sender).Tag.ToString(), NumberStyles.None);
            TestPatternForm.TestPatternMode mode = (TestPatternForm.TestPatternMode)tag;

            foreach (ToolStripMenuItem item in this.testPatternSelectionToolStripMenuItem.DropDownItems)
            {
                item.Checked = (item == sender);
            }

            this.m_TestPatternForm.SetTestPattern(mode);
        }

        private void showSettingsanalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.showSettingsanalyzerToolStripMenuItem.Checked)
            {
                this.m_SettingsAnalyzerForm = new SettingsAnalyzerForm();
                this.m_SettingsAnalyzerForm.Show(this);
                this.showSettingsanalyzerToolStripMenuItem.Checked = true;
                return;
            }

            this.m_SettingsAnalyzerForm.Close();
            this.m_SettingsAnalyzerForm.Dispose();
            this.m_SettingsAnalyzerForm = null;
            this.showSettingsanalyzerToolStripMenuItem.Checked = false;
        }
    }
}