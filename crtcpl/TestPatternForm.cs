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
        private readonly Bitmap m_TestPattern;

        protected override bool ShowWithoutActivation => true;

        protected override void WndProc(ref Message m)
        {
            const int WM_ACTIVATE = 6;
            const int WA_INACTIVE = 0;

            if (m.Msg != WM_ACTIVATE)
            {
                goto done;
            }

            if (((int)m.WParam & 0xFFFF) == WA_INACTIVE)
            {
                return;
            }

            if (m.LParam != IntPtr.Zero)
            {
                NativeMethods.SetActiveWindow(m.LParam);
            }
            else
            {
                NativeMethods.SetActiveWindow(IntPtr.Zero);
            }

        done:
            base.WndProc(ref m);
        }

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

        public TestPatternForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Selectable, false);

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
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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

            using (TextureBrush b = new TextureBrush(this.m_TestPattern))
            {
                e.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height);
            }

            using (Pen p = new Pen(Color.Red))
            {
                e.Graphics.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
            }
        }
    }
}
