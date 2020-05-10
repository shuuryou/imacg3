using System;
using System.Drawing;
using System.Windows.Forms;

namespace crtcpl
{
    /// <summary>
    /// I want a test pattern screen. This is the best I could come up with.
    /// It isn't as good as I hoped it would be.
    /// </summary>
    public partial class TestPatternForm : Form
    {
        private Bitmap m_TestPattern;

        public enum TestPatternMode
        {
            ScreenAdjust = 0,
            SMPTE = 1,
            FuBK = 2,
            VBW = 3,
            HBW = 4,
            VRGB = 5,
            HRGB = 6,
            RED = 7,
            GREEN = 8,
            BLUE = 9
        }

        private TestPatternMode m_TestPatternMode;

        public TestPatternForm()
        {
            InitializeComponent();
            SetTestPattern(TestPatternMode.ScreenAdjust);

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Selectable, false);
        }

        protected override bool ShowWithoutActivation => true;


        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_NOACTIVATE = 0x08000000;

                CreateParams cp = base.CreateParams;

                cp.ExStyle |= WS_EX_NOACTIVATE;
                return cp;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            if (disposing && this.m_TestPattern != null)
            {
                this.m_TestPattern.Dispose();
            }

            base.Dispose(disposing);
        }

        private void TestPatternForm_Paint(object sender, PaintEventArgs e)
        {
            if (e.Graphics == null)
            {
                return;
            }

            switch (this.m_TestPatternMode)
            {
                case TestPatternMode.ScreenAdjust:
                    if (this.m_TestPattern == null)
                    {
                        return;
                    }

                    using (TextureBrush b = new TextureBrush(this.m_TestPattern))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height);
                    }

                    using (Pen p = new Pen(Color.Red))
                    {
                        e.Graphics.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
                    }

                    return;
                case TestPatternMode.SMPTE:
                case TestPatternMode.FuBK:
                    if (this.m_TestPattern == null)
                    {
                        return;
                    }

                    e.Graphics.DrawImage(this.m_TestPattern, 0, 0, this.Width, this.Height);
                    return;
                case TestPatternMode.VBW:
                    using (SolidBrush b = new SolidBrush(Color.Black))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, this.Width / 2, this.Height);
                    }

                    using (SolidBrush b = new SolidBrush(Color.White))
                    {
                        e.Graphics.FillRectangle(b, this.Width / 2, 0, this.Width / 2, this.Height);
                    }

                    return;
                case TestPatternMode.HBW:
                    using (SolidBrush b = new SolidBrush(Color.Black))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height / 2);
                    }

                    using (SolidBrush b = new SolidBrush(Color.White))
                    {
                        e.Graphics.FillRectangle(b, 0, this.Height / 2, this.Width, this.Height / 2);
                    }

                    return;
                case TestPatternMode.VRGB:
                    using (SolidBrush b = new SolidBrush(Color.Red))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, this.Width / 3, this.Height);
                    }

                    using (SolidBrush b = new SolidBrush(Color.Lime))
                    {
                        e.Graphics.FillRectangle(b, this.Width / 3, 0, this.Width / 3, this.Height);
                    }

                    using (SolidBrush b = new SolidBrush(Color.Blue))
                    {
                        e.Graphics.FillRectangle(b, (this.Width / 3) * 2, 0, this.Width / 3, this.Height);
                    }

                    return;
                case TestPatternMode.HRGB:
                    using (SolidBrush b = new SolidBrush(Color.Red))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height / 3);
                    }

                    using (SolidBrush b = new SolidBrush(Color.Lime))
                    {
                        e.Graphics.FillRectangle(b, 0, this.Height / 3, this.Width, this.Height / 3);
                    }

                    using (SolidBrush b = new SolidBrush(Color.Blue))
                    {
                        e.Graphics.FillRectangle(b, 0, (this.Height / 3) * 2, this.Width, this.Height / 3);
                    }

                    return;
                case TestPatternMode.RED:
                    using (SolidBrush b = new SolidBrush(Color.Red))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height);
                    }

                    return;
                case TestPatternMode.GREEN:
                    using (SolidBrush b = new SolidBrush(Color.Lime))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height);
                    }

                    return;
                case TestPatternMode.BLUE:
                    using (SolidBrush b = new SolidBrush(Color.Blue))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height);
                    }

                    return;
            }
        }

        internal void SetTestPattern(TestPatternMode mode)
        {
            if (!Enum.IsDefined(typeof(TestPatternMode), mode))
                throw new ArgumentOutOfRangeException(nameof(mode));

            this.m_TestPatternMode = mode;

            if (this.m_TestPattern != null)
            {
                this.m_TestPattern.Dispose();
                this.m_TestPattern = null;
            }

            if (this.m_TestPatternMode == TestPatternMode.ScreenAdjust)
            {
                this.m_TestPattern = new Bitmap(24, 24);
                using (Graphics g = Graphics.FromImage(this.m_TestPattern))
                {
                    using (SolidBrush b = new SolidBrush(Color.Black))
                    {
                        g.FillRectangle(b, 0, 0, this.m_TestPattern.Width, this.m_TestPattern.Height);
                    }

                    using (Pen p = new Pen(Color.White))
                    {
                        g.DrawRectangle(p, 0, 0, this.m_TestPattern.Width, this.m_TestPattern.Height);
                    }

                    using (SolidBrush b = new SolidBrush(Color.White))
                    {
                        g.FillRectangle(b, this.m_TestPattern.Width / 2, this.m_TestPattern.Height / 2, 1, 1);
                    }
                }

                goto end;
            }

            if (this.m_TestPatternMode == TestPatternMode.SMPTE)
            {
                this.m_TestPattern = ImageRes.ImageRes.SMPTECARD;
                goto end;
            }

            if (this.m_TestPatternMode == TestPatternMode.FuBK)
            {
                this.m_TestPattern = ImageRes.ImageRes.FUBKCARD;
                goto end;
            }

        end:
            Invalidate();
        }

        private void TestPatternForm_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
                if (f.GetType() == typeof(AppletForm))
                {
                    if (f.Visible)
                    {
                        f.TopMost = false;
                        f.Hide();
                    }
                    else
                    {
                        f.Show();
                        f.Focus();
                        f.TopMost = true;
                    }
                    return;
                }
        }

        private void TestPatternForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                foreach (Form f in Application.OpenForms)
                    if (f.GetType() == typeof(AppletForm))
                    {
                        f.Show();
                        f.Focus();
                        f.TopMost = true;
                        return;
                    }
        }
    }
}