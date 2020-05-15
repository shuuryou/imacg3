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
        public enum ChangedSetting
        {
            None = 0,
            RedCutoff = 1,
            GreenCutoff = 2,
            BlueCutoff = 3,
            RedDrive = 4,
            GreenDrive = 5,
            BlueDrive = 6
        }

        public ColorsPageEventArgs(ChangedSetting setting, int newValue)
        {
            if (setting == ChangedSetting.None)
            {
                throw new ArgumentOutOfRangeException(nameof(setting));
            }

            this.Setting = setting;
            this.NewValue = newValue;
        }

        public ChangedSetting Setting { get; private set; }

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
            PincushionBalance = 6,
            SCorrection = 7,
            Width = 8,
            Parallelogram = 9,
            Rotation = 10
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