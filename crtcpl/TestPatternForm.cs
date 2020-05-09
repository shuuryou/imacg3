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
        private int m_TestPatternMode = 0;

        public TestPatternForm()
        {
            InitializeComponent();

#if MONO
            Activated += TestPatternForm_Activated;
#endif

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Selectable, false);

            SetTestPattern();
        }

        protected override bool ShowWithoutActivation => true;

#if !MONO
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
#endif

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


#if MONO
        private void TestPatternForm_Activated(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
                if (f.GetType() == typeof(AppletForm))
                {
                    f.Focus();
                    return;
                }
        }
#endif

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
                case 0:
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
                case 1:
                case 2:
                    if (this.m_TestPattern == null)
                    {
                        return;
                    }

                    e.Graphics.DrawImage(this.m_TestPattern, 0, 0, this.Width, this.Height);
                    return;
                case 3:
                    using (SolidBrush b = new SolidBrush(Color.Black))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, this.Width / 2, this.Height);
                    }

                    using (SolidBrush b = new SolidBrush(Color.White))
                    {
                        e.Graphics.FillRectangle(b, this.Width / 2, 0, this.Width / 2, this.Height);
                    }

                    return;
                case 4:
                    using (SolidBrush b = new SolidBrush(Color.Black))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height / 2);
                    }

                    using (SolidBrush b = new SolidBrush(Color.White))
                    {
                        e.Graphics.FillRectangle(b, 0, this.Height / 2, this.Width, this.Height / 2);
                    }

                    return;
                case 5:
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
                case 6:
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
                case 7:
                    using (SolidBrush b = new SolidBrush(Color.Red))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height);
                    }

                    return;
                case 8:
                    using (SolidBrush b = new SolidBrush(Color.Lime))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height);
                    }

                    return;
                case 9:
                    using (SolidBrush b = new SolidBrush(Color.Blue))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height);
                    }

                    return;
            }
        }

        private void TestPatternForm_MouseClick(object sender, MouseEventArgs e)
        {
            this.m_TestPatternMode = (this.m_TestPatternMode + 1) % 10;
            SetTestPattern();
        }

        private void SetTestPattern()
        {
            if (this.m_TestPattern != null)
            {
                this.m_TestPattern.Dispose();
                this.m_TestPattern = null;
            }

            if (this.m_TestPatternMode == 0)
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

            if (this.m_TestPatternMode == 1)
            {
                this.m_TestPattern = ImageRes.ImageRes.SMPTECARD;
                goto end;
            }

            if (this.m_TestPatternMode == 2)
            {
                this.m_TestPattern = ImageRes.ImageRes.FUBKCARD;
                goto end;
            }

        end:
            Invalidate();
        }
    }
}