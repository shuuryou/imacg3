using System;
using System.Windows.Forms;

namespace crtcpl
{
    public partial class GeometryPage : UserControl
    {
        private int m_CurrentHorizontal,
            m_CurrentHeight,
            m_CurrentVertical,
            m_CurrentKeystone,
            m_CurrentPincushion,
            m_CurrentWidth,
            m_CurrentParallelogram,
            m_CurrentRotation;

        private readonly TestPatternForm m_TestPatternForm = new TestPatternForm();

        public GeometryPage()
        {
            InitializeComponent();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
                this.m_TestPatternForm.Dispose();
            }
            base.Dispose(disposing);
        }

        public void SetValues(int horizontal, int height, int vertical, int keystone, int pincushion, int width, int parallelogram, int rotation)
        {
            if (horizontal > Constants.IVAD_HORIZONTAL_POS_MAX)
            {
                horizontal = Constants.IVAD_HORIZONTAL_POS_MAX;
            }
            else if (horizontal < Constants.IVAD_HORIZONTAL_POS_MIN)
            {
                horizontal = Constants.IVAD_HORIZONTAL_POS_MIN;
            }

            if (height > Constants.IVAD_HEIGHT_MAX)
            {
                height = Constants.IVAD_HEIGHT_MAX;
            }
            else if (height < Constants.IVAD_HEIGHT_MIN)
            {
                height = Constants.IVAD_HEIGHT_MIN;
            }

            if (vertical > Constants.IVAD_VERTICAL_POS_MAX)
            {
                vertical = Constants.IVAD_VERTICAL_POS_MAX;
            }
            else if (vertical < Constants.IVAD_VERTICAL_POS_MIN)
            {
                vertical = Constants.IVAD_VERTICAL_POS_MIN;
            }

            if (keystone > Constants.IVAD_VERTICAL_POS_MAX)
            {
                keystone = Constants.IVAD_VERTICAL_POS_MAX;
            }
            else if (keystone < Constants.IVAD_VERTICAL_POS_MIN)
            {
                keystone = Constants.IVAD_VERTICAL_POS_MIN;
            }

            if (pincushion > Constants.IVAD_PINCUSHION_MAX)
            {
                pincushion = Constants.IVAD_PINCUSHION_MAX;
            }
            else if (pincushion < Constants.IVAD_PINCUSHION_MIN)
            {
                pincushion = Constants.IVAD_PINCUSHION_MIN;
            }

            if (width > Constants.IVAD_WIDTH_MAX)
            {
                width = Constants.IVAD_WIDTH_MAX;
            }
            else if (width < Constants.IVAD_WIDTH_MIN)
            {
                width = Constants.IVAD_WIDTH_MIN;
            }

            if (parallelogram > Constants.IVAD_PARALLELOGRAM_MAX)
            {
                parallelogram = Constants.IVAD_PARALLELOGRAM_MAX;
            }
            else if (parallelogram < Constants.IVAD_PARALLELOGRAM_MIN)
            {
                parallelogram = Constants.IVAD_PARALLELOGRAM_MIN;
            }

            if (rotation > Constants.IVAD_ROTATION_MAX)
            {
                rotation = Constants.IVAD_ROTATION_MAX;
            }
            else if (rotation < Constants.IVAD_ROTATION_MIN)
            {
                rotation = Constants.IVAD_ROTATION_MIN;
            }

            this.m_CurrentHorizontal = horizontal;
            this.m_CurrentHeight = height;
            this.m_CurrentVertical = vertical;
            this.m_CurrentKeystone = keystone;
            this.m_CurrentPincushion = pincushion;
            this.m_CurrentWidth = width;
            this.m_CurrentParallelogram = parallelogram;
            this.m_CurrentRotation = rotation;
        }

        private void topButton_Click(object sender, EventArgs e)
        {
            if (this.hwRadioButton.Checked)
            {
                this.m_CurrentHeight = Math.Min(this.m_CurrentHeight + 1, Constants.IVAD_HEIGHT_MAX);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Height, this.m_CurrentHeight));
                return;
            }

            if (this.posRadioButton.Checked)
            {
                this.m_CurrentVertical = Math.Max(this.m_CurrentVertical - 1, Constants.IVAD_VERTICAL_POS_MIN);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Vertical, this.m_CurrentVertical));
                return;
            }
        }

        private void bottomButton_Click(object sender, EventArgs e)
        {
            if (this.hwRadioButton.Checked)
            {
                this.m_CurrentHeight = Math.Max(this.m_CurrentHeight - 1, Constants.IVAD_HEIGHT_MIN);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Height, this.m_CurrentHeight));
                return;
            }

            if (this.posRadioButton.Checked)
            {
                this.m_CurrentVertical = Math.Min(this.m_CurrentVertical + 1, Constants.IVAD_VERTICAL_POS_MAX);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Vertical, this.m_CurrentVertical));
                return;
            }
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            if (this.hwRadioButton.Checked)
            {
                this.m_CurrentWidth = Math.Min(this.m_CurrentWidth + 1, Constants.IVAD_WIDTH_MAX);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Width, this.m_CurrentWidth));
                return;
            }

            if (this.posRadioButton.Checked)
            {
                this.m_CurrentHorizontal = Math.Min(this.m_CurrentHorizontal + 1, Constants.IVAD_HORIZONTAL_POS_MAX);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Horizontal, this.m_CurrentHorizontal));
                return;
            }

            if (this.pcRadioButton.Checked)
            {
                this.m_CurrentPincushion = Math.Min(this.m_CurrentPincushion + 1, Constants.IVAD_PINCUSHION_MAX);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Pincushion, this.m_CurrentPincushion));
                return;

            }

            if (this.rotRadioButton.Checked)
            {
                this.m_CurrentRotation = Math.Max(this.m_CurrentRotation - 1, Constants.IVAD_ROTATION_MIN);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Rotation, this.m_CurrentRotation));
                return;
            }

            if (this.trapRadioButton.Checked)
            {
                this.m_CurrentKeystone = Math.Max(this.m_CurrentKeystone - 1, Constants.IVAD_KEYSTONE_MIN);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Keystone, this.m_CurrentKeystone));
                return;
            }

            if (this.paraRadioButton.Checked)
            {
                this.m_CurrentParallelogram = Math.Max(this.m_CurrentParallelogram - 1, Constants.IVAD_PARALLELOGRAM_MIN);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Parallelogram, this.m_CurrentParallelogram));
                return;
            }
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            if (this.hwRadioButton.Checked)
            {
                this.m_CurrentWidth = Math.Max(this.m_CurrentWidth - 1, Constants.IVAD_WIDTH_MIN);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Width, this.m_CurrentWidth));
                return;
            }

            if (this.posRadioButton.Checked)
            {
                this.m_CurrentHorizontal = Math.Max(this.m_CurrentHorizontal - 1, Constants.IVAD_HORIZONTAL_POS_MIN);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Horizontal, this.m_CurrentHorizontal));
                return;
            }

            if (this.pcRadioButton.Checked)
            {
                this.m_CurrentPincushion = Math.Max(this.m_CurrentPincushion - 1, Constants.IVAD_PINCUSHION_MIN);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Pincushion, this.m_CurrentPincushion));
                return;
            }

            if (this.rotRadioButton.Checked)
            {
                this.m_CurrentRotation = Math.Min(this.m_CurrentRotation + 1, Constants.IVAD_ROTATION_MAX);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Rotation, this.m_CurrentRotation));
                return;
            }

            if (this.trapRadioButton.Checked)
            {
                this.m_CurrentKeystone = Math.Min(this.m_CurrentKeystone + 1, Constants.IVAD_KEYSTONE_MAX);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Keystone, this.m_CurrentKeystone));
                return;
            }

            if (this.paraRadioButton.Checked)
            {
                this.m_CurrentParallelogram = Math.Min(this.m_CurrentParallelogram + 1, Constants.IVAD_PARALLELOGRAM_MAX);
                OnGeometryChanged(new GeometryPageEventArgs(GeometryPageEventArgs.ChangedGemoetry.Parallelogram, this.m_CurrentParallelogram));
                return;
            }
        }

        private void hwRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();

            this.topButton.Visible = true;
            this.bottomButton.Visible = true;
            this.leftButton.Visible = true;
            this.rightButton.Visible = true;

            this.topButton.Image = ImageRes.ImageRes.RES011;
            this.bottomButton.Image = ImageRes.ImageRes.RES012;
            this.leftButton.Image = ImageRes.ImageRes.RES009;
            this.rightButton.Image = ImageRes.ImageRes.RES010;

            ResumeLayout();
        }

        private void posRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();

            this.topButton.Visible = true;
            this.bottomButton.Visible = true;
            this.leftButton.Visible = true;
            this.rightButton.Visible = true;

            this.topButton.Image = ImageRes.ImageRes.RES007;
            this.bottomButton.Image = ImageRes.ImageRes.RES008;
            this.leftButton.Image = ImageRes.ImageRes.RES005;
            this.rightButton.Image = ImageRes.ImageRes.RES006;

            ResumeLayout();
        }

        private void showTestPatternToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.showTestPatternToolStripMenuItem.Checked)
            {
                this.m_TestPatternForm.Hide();
                this.showTestPatternToolStripMenuItem.Checked = false;
                this.ParentForm.TopMost = false;
                return;
            }

            this.m_TestPatternForm.Show();
            this.showTestPatternToolStripMenuItem.Checked = true;

            this.ParentForm.TopMost = true;
            this.ParentForm.Focus();
        }

        private void GeometryPage_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible && this.showTestPatternToolStripMenuItem.Checked)
            {
                showTestPatternToolStripMenuItem_Click(null, EventArgs.Empty); // Turn off the test pattern
            }
        }

        private void pcRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();

            this.topButton.Visible = false;
            this.bottomButton.Visible = false;
            this.leftButton.Visible = true;
            this.rightButton.Visible = true;

            this.leftButton.Image = ImageRes.ImageRes.RES015;
            this.rightButton.Image = ImageRes.ImageRes.RES016;


            ResumeLayout();
        }

        private void rotRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();

            this.topButton.Visible = false;
            this.bottomButton.Visible = false;
            this.leftButton.Visible = true;
            this.rightButton.Visible = true;

            this.leftButton.Image = ImageRes.ImageRes.RES013;
            this.rightButton.Image = ImageRes.ImageRes.RES014;

            ResumeLayout();
        }

        private void trapRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();

            this.topButton.Visible = false;
            this.bottomButton.Visible = false;
            this.leftButton.Visible = true;
            this.rightButton.Visible = true;

            this.leftButton.Image = ImageRes.ImageRes.RES019;
            this.rightButton.Image = ImageRes.ImageRes.RES020;

            ResumeLayout();
        }

        private void paraRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();

            this.topButton.Visible = false;
            this.bottomButton.Visible = false;
            this.leftButton.Visible = true;
            this.rightButton.Visible = true;

            this.leftButton.Image = ImageRes.ImageRes.RES017;
            this.rightButton.Image = ImageRes.ImageRes.RES018;

            ResumeLayout();
        }

        private void GeometryPage_Load(object sender, EventArgs e)
        {
            hwRadioButton_CheckedChanged(null, EventArgs.Empty);
        }

        protected virtual void OnGeometryChanged(GeometryPageEventArgs e)
        {
            GeometryChanged?.Invoke(this, e);
        }

        public event EventHandler<GeometryPageEventArgs> GeometryChanged;
    }
}
