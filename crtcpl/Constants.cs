namespace crtcpl
{
    public static class Constants
    {
        public const int CONFIG_OFFSET_CONTRAST = 0;
        public const int CONFIG_OFFSET_RESERVED1 = 1;
        public const int CONFIG_OFFSET_RESERVED2 = 2;
        public const int CONFIG_OFFSET_RESERVED3 = 3;
        public const int CONFIG_OFFSET_RED = 4;
        public const int CONFIG_OFFSET_GREEN =5;
        public const int CONFIG_OFFSET_BLUE = 6;
        public const int CONFIG_OFFSET_HORIZONTAL_POS = 7;
        public const int CONFIG_OFFSET_HEIGHT = 8;
        public const int CONFIG_OFFSET_VERTICAL_POS = 9;
        public const int CONFIG_OFFSET_RESERVED4 = 10;
        public const int CONFIG_OFFSET_KEYSTONE = 11;
        public const int CONFIG_OFFSET_PINCUSHION = 12;
        public const int CONFIG_OFFSET_WIDTH = 13;
        public const int CONFIG_OFFSET_RESERVED5 = 14;
        public const int CONFIG_OFFSET_PARALLELOGRAM =15;
        public const int CONFIG_OFFSET_RESERVED6 = 16;
        public const int CONFIG_OFFSET_BRIGHTNESS = 17;
        public const int CONFIG_OFFSET_ROTATION = 18;
        public const int CONFIG_OFFSET_CHECKSUM = 19;

        public const int IVAD_SETTING_CONTRAST = 0x00;
        public const int IVAD_SETTING_HORIZONTAL_POS = 0x07;
        public const int IVAD_SETTING_HEIGHT = 0x08;
        public const int IVAD_SETTING_VERTICAL_POS = 0x09;
        public const int IVAD_SETTING_KEYSTONE = 0x0B;
        public const int IVAD_SETTING_PINCUSHION = 0x0C;
        public const int IVAD_SETTING_WIDTH = 0x0D;
        public const int IVAD_SETTING_PARALLELOGRAM = 0x0F;
        public const int IVAD_SETTING_BRIGHTNESS = 0x11;
        public const int IVAD_SETTING_ROTATION = 0x12;
        public const int IVAD_SETTING_RED = 0x04;
        public const int IVAD_SETTING_GREEN = 0x05;
        public const int IVAD_SETTING_BLUE = 0x06;

        public const int IVAD_CONTRAST_MIN = 0xB5; // Most dark
        public const int IVAD_CONTRAST_MAX = 0xFF; // Most bright
        public const int IVAD_HORIZONTAL_POS_MIN = 0x80; // Most left
        public const int IVAD_HORIZONTAL_POS_MAX = 0xFE; // Most right
        public const int IVAD_HEIGHT_MIN = 0x80; // Most small
        public const int IVAD_HEIGHT_MAX = 0xFE; // Most big
        public const int IVAD_VERTICAL_POS_MIN = 0x01; // Most low
        public const int IVAD_VERTICAL_POS_MAX = 0x7F; // Most high
        public const int IVAD_KEYSTONE_MIN = 0x80; // Most thin at top
        public const int IVAD_KEYSTONE_MAX = 0xFE; // Most thin at bottom
        public const int IVAD_PINCUSHION_MIN = 0x80; // Most left
        public const int IVAD_PINCUSHION_MAX = 0xFE; // Most right
        public const int IVAD_WIDTH_MIN = 0x01; // Most thin
        public const int IVAD_WIDTH_MAX = 0x7F; // Most thick
        public const int IVAD_PARALLELOGRAM_MIN = 0x80; // Most left
        public const int IVAD_PARALLELOGRAM_MAX = 0xFE; // Most right
        public const int IVAD_BRIGHTNESS_MIN = 0x00; // Most dim
        public const int IVAD_BRIGHTNESS_MAX = 0x32; // Most bright
        public const int IVAD_ROTATION_MIN = 0x01; // Most left
        public const int IVAD_ROTATION_MAX = 0x7F; // Most right
        public const int IVAD_RED_MIN = 0x00; // Most low
        public const int IVAD_RED_MAX = 0xFF; // Most high
        public const int IVAD_GREEN_MIN = 0x00; // Most low
        public const int IVAD_GREEN_MAX = 0xFF; // Most high
        public const int IVAD_BLUE_MIN = 0x00; // Most low
        public const int IVAD_BLUE_MAX = 0xFF; // Most high
    }
}
