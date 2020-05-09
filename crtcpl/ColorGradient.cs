using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace crtcpl
{
    public partial class ColorGradient : UserControl
    {
        public ColorGradient()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Selectable, false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (e == null)
            {
                return;
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.Black, Color.Red, 0F))
            {
                e.Graphics.FillRectangle(brush, 0, (this.Height / 4) * 0, this.Width, (this.Height / 4));
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.Black, Color.Lime, 0F))
            {
                e.Graphics.FillRectangle(brush, 0, (this.Height / 4) * 1, this.Width, (this.Height / 4));
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.Black, Color.Blue, 0F))
            {
                e.Graphics.FillRectangle(brush, 0, (this.Height / 4) * 2, this.Width, (this.Height / 4));
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.Black, Color.White, 0F))
            {
                e.Graphics.FillRectangle(brush, 0, (this.Height / 4) * 3, this.Width, (this.Height / 4));
            }
        }
    }
}