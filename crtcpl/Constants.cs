namespace crtcpl
{
    public static class Constants
    {
        public const byte SUPPORTED_EEPROM_VERSION = 0x03;

        // If you change the order here, take care to also change the list
        // in the constructor of SettingsAnalyzerForm.cs
        public const int CONFIG_OFFSET_CONTRAST = 0;
        public const int CONFIG_OFFSET_RED_DRIVE = 1;
        public const int CONFIG_OFFSET_GREEN_DRIVE = 2;
        public const int CONFIG_OFFSET_BLUE_DRIVE = 3;
        public const int CONFIG_OFFSET_RED_CUTOFF = 4;
        public const int CONFIG_OFFSET_GREEN_CUTOFF = 5;
        public const int CONFIG_OFFSET_BLUE_CUTOFF = 6;
        public const int CONFIG_OFFSET_HORIZONTAL_POS = 7;
        public const int CONFIG_OFFSET_HEIGHT = 8;
        public const int CONFIG_OFFSET_VERTICAL_POS = 9;
        public const int CONFIG_OFFSET_S_CORRECTION = 10;
        public const int CONFIG_OFFSET_KEYSTONE = 11;
        public const int CONFIG_OFFSET_PINCUSHION = 12;
        public const int CONFIG_OFFSET_WIDTH = 13;
        public const int CONFIG_OFFSET_PINCUSHION_BALANCE = 14;
        public const int CONFIG_OFFSET_PARALLELOGRAM = 15;
        public const int CONFIG_OFFSET_RESERVED6 = 16;
        public const int CONFIG_OFFSET_BRIGHTNESS = 17;
        public const int CONFIG_OFFSET_ROTATION = 18;
        public const int CONFIG_OFFSET_CHECKSUM = 19;

        public const int IVAD_SETTING_CONTRAST = 0x00;
        public const int IVAD_SETTING_RED_DRIVE = 0x01;
        public const int IVAD_SETTING_GREEN_DRIVE = 0x02;
        public const int IVAD_SETTING_BLUE_DRIVE = 0x03;
        public const int IVAD_SETTING_RED_CUTOFF = 0x04;
        public const int IVAD_SETTING_GREEN_CUTOFF = 0x05;
        public const int IVAD_SETTING_BLUE_CUTOFF = 0x06;
        public const int IVAD_SETTING_HORIZONTAL_POS = 0x07;
        public const int IVAD_SETTING_HEIGHT = 0x08;
        public const int IVAD_SETTING_VERTICAL_POS = 0x09;
        public const int IVAD_SETTING_S_CORRECTION = 0x0A;
        public const int IVAD_SETTING_KEYSTONE = 0x0B;
        public const int IVAD_SETTING_PINCUSHION = 0x0C;
        public const int IVAD_SETTING_WIDTH = 0x0D;
        public const int IVAD_SETTING_PINCUSHION_BALANCE = 0x0E;
        public const int IVAD_SETTING_PARALLELOGRAM = 0x0F;
        // public const int IVAD_SETTING_RESERVED6 = 0x10;
        public const int IVAD_SETTING_BRIGHTNESS = 0x11;
        public const int IVAD_SETTING_ROTATION = 0x12;

        public const int IVAD_CONTRAST_MIN = 0xB5; // Most dark
        public const int IVAD_CONTRAST_MAX = 0xFF; // Most bright
        public const int IVAD_RED_CUTOFF_MIN = 0x00; // Most low
        public const int IVAD_RED_CUTOFF_MAX = 0xFF; // Most high
        public const int IVAD_GREEN_CUTOFF_MIN = 0x00; // Most low
        public const int IVAD_GREEN_CUTOFF_MAX = 0xFF; // Most high
        public const int IVAD_BLUE_CUTOFF_MIN = 0x00; // Most low
        public const int IVAD_BLUE_CUTOFF_MAX = 0xFF; // Most high
        public const int IVAD_RED_DRIVE_MIN = 0x00; // Most low
        public const int IVAD_RED_DRIVE_MAX = 0xFF; // Most high
        public const int IVAD_GREEN_DRIVE_MIN = 0x00; // Most low
        public const int IVAD_GREEN_DRIVE_MAX = 0xFF; // Most high
        public const int IVAD_BLUE_DRIVE_MIN = 0x00; // Most low
        public const int IVAD_BLUE_DRIVE_MAX = 0xFF; // Most high
        public const int IVAD_HORIZONTAL_POS_MIN = 0x80; // Most left
        public const int IVAD_HORIZONTAL_POS_MAX = 0xFF; // Most right
        public const int IVAD_HEIGHT_MIN = 0x80; // Most small
        public const int IVAD_HEIGHT_MAX = 0xFE; // Most big
        public const int IVAD_VERTICAL_POS_MIN = 0x00; // Most low
        public const int IVAD_VERTICAL_POS_MAX = 0x7F; // Most high
        public const int IVAD_S_CORRECTION_MIN = 0x80; // Most low
        public const int IVAD_S_CORRECTION_MAX = 0xFF; // Most high
        public const int IVAD_KEYSTONE_MIN = 0x80; // Most thin at top
        public const int IVAD_KEYSTONE_MAX = 0xFF; // Most thin at bottom
        public const int IVAD_PINCUSHION_MIN = 0x80; // Most left
        public const int IVAD_PINCUSHION_MAX = 0xFF; // Most right
        public const int IVAD_PINCUSHION_BALANCE_MIN = 0x00; // Most low
        public const int IVAD_PINCUSHION_BALANCE_MAX = 0xFF; // Most high
        public const int IVAD_WIDTH_MIN = 0x00; // Most thin
        public const int IVAD_WIDTH_MAX = 0x7F; // Most thick
        public const int IVAD_PARALLELOGRAM_MIN = 0x80; // Most left
        public const int IVAD_PARALLELOGRAM_MAX = 0xFF; // Most right
        public const int IVAD_BRIGHTNESS_MIN = 0x00; // Most dim
        public const int IVAD_BRIGHTNESS_MAX = 0x0A; // Most bright according to Apple Display Service utility
        public const int IVAD_BRIGHTNESS_MAX_OVERDRIVE = 0x32; // Maximum the IVAD bord will accept in real life
        public const int IVAD_ROTATION_MIN = 0x00; // Most left
        public const int IVAD_ROTATION_MAX = 0x7F; // Most right

        // Regarding IVAD_BRIGHTNESS_MAX_OVERDRIVE:
        // I did a SCREEN potentiometer adjustment before Rocky Hill discovered the "official" maximum.
        // I guess that means my master brightness is now too low. If I crank up brightness all the way
        // to the maximum and then also dial the contrast all the way up, I get bad screen distortion
        // that goes away very slowly if the CRT is sufficiently warmed up, or instantly if the dials
        // are turned down again.
    }
}
