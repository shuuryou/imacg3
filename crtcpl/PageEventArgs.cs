using System;

namespace crtcpl
{
    public class ScreenPageEventArgs : EventArgs
    {
        public ScreenPageEventArgs(int newValue)
        {
            this.NewValue = newValue;
        }

        public int NewValue { get; private set; }
    }

    public class ColorsPageEventArgs : EventArgs
    {
        public enum ChangedColor
        {
            None = 0,
            Red = 1,
            Green = 2,
            Blue = 3
        }

        public ColorsPageEventArgs(ChangedColor color, int newValue)
        {
            if (color == ChangedColor.None)
            {
                throw new ArgumentOutOfRangeException(nameof(color));
            }

            this.Color = color;
            this.NewValue = newValue;
        }

        public ChangedColor Color { get; private set; }

        public int NewValue { get; private set; }
    }

    public class GeometryPageEventArgs : EventArgs
    {
        public enum ChangedGemoetry
        {
            None = 0,
            Horizontal = 1,
            Height = 2,
            Vertical = 3,
            Keystone = 4,
            Pincushion = 5,
            Width = 6,
            Parallelogram = 7,
            Rotation = 8
        }

        public GeometryPageEventArgs(ChangedGemoetry what, int newValue)
        {
            if (what == ChangedGemoetry.None)
            {
                throw new ArgumentOutOfRangeException(nameof(what));
            }

            this.What = what;
            this.NewValue = newValue;
        }

        public ChangedGemoetry What { get; private set; }

        public int NewValue { get; private set; }
    }
}