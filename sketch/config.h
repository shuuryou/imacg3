// If you turn on DEBUG then you will not be able to use crtcpl.
// It is possible to debug the serial protocol with DEBUG active
// using software like the excellent SUDT AccessPort 1.37.
//#define DEBUG

#define PIN_SIDE_BUTTON_1         49
#define PIN_SIDE_BUTTON_2         48
#define PIN_POWER_BUTTON          50

#define PIN_LED_ORANGE            52
#define PIN_LED_GREEN             53

#define PIN_HEADPHONE_SENSE_1     A0
#define PIN_HEADPHONE_SENSE_2     A1

#define PIN_IVAD_SCL              12
#define PIN_IVAD_SDA              13

#define PIN_VGA_SDA               20
#define PIN_VGA_SCL               21
#define PIN_VGA_VSYNC             2

#define I2C_ADDR_VGA_EDID         80 // 0x50

#define PIN_RELAY_PC              32
#define PIN_RELAY_CRT             33
#define PIN_RELAY_SPK_AMP         34
#define PIN_RELAY_RESERVED        35

#define CONFIG_EEPROM_VERSION        3
#define CONFIG_EEPROM_SLOTS          20

#define CONFIG_OFFSET_CONTRAST           0
#define CONFIG_OFFSET_RED_DRIVE          1
#define CONFIG_OFFSET_GREEN_DRIVE        2
#define CONFIG_OFFSET_BLUE_DRIVE         3
#define CONFIG_OFFSET_RED_CUTOFF         4
#define CONFIG_OFFSET_GREEN_CUTOFF       5
#define CONFIG_OFFSET_BLUE_CUTOFF        6
#define CONFIG_OFFSET_HORIZONTAL_POS     7
#define CONFIG_OFFSET_HEIGHT             8
#define CONFIG_OFFSET_VERTICAL_POS       9
#define CONFIG_OFFSET_S_CORRECTION       10
#define CONFIG_OFFSET_KEYSTONE           11
#define CONFIG_OFFSET_PINCUSHION         12
#define CONFIG_OFFSET_WIDTH              13
#define CONFIG_OFFSET_PINCUSHION_BALANCE 14
#define CONFIG_OFFSET_PARALLELOGRAM      15
#define CONFIG_OFFSET_BRIGHTNESS_DRIVE   16
#define CONFIG_OFFSET_BRIGHTNESS         17
#define CONFIG_OFFSET_ROTATION           18
#define CONFIG_OFFSET_CHECKSUM           19

#define IVAD_REGISTER_PROPERTY          0x46
#define IVAD_SETTING_CONTRAST           0x00
#define IVAD_SETTING_RED_DRIVE          0x01
#define IVAD_SETTING_GREEN_DRIVE        0x02
#define IVAD_SETTING_BLUE_DRIVE         0x03
#define IVAD_SETTING_RED_CUTOFF         0x04
#define IVAD_SETTING_GREEN_CUTOFF       0x05
#define IVAD_SETTING_BLUE_CUTOFF        0x06
#define IVAD_SETTING_HORIZONTAL_POS     0x07
#define IVAD_SETTING_HEIGHT             0x08
#define IVAD_SETTING_VERTICAL_POS       0x09
#define IVAD_SETTING_S_CORRECTION       0x0A
#define IVAD_SETTING_KEYSTONE           0x0B
#define IVAD_SETTING_PINCUSHION         0x0C
#define IVAD_SETTING_WIDTH              0x0D
#define IVAD_SETTING_PINCUSHION_BALANCE 0x0E
#define IVAD_SETTING_PARALLELOGRAM      0x0F
#define IVAD_SETTING_BRIGHTNESS_DRIVE   0x10
#define IVAD_SETTING_BRIGHTNESS         0x11
#define IVAD_SETTING_ROTATION           0x12

#define IVAD_CONTRAST_MIN               0xB5 // Most dark
#define IVAD_CONTRAST_MAX               0xFF // Most bright
#define IVAD_RED_CUTOFF_MIN             0x00 // Most low
#define IVAD_RED_CUTOFF_MAX             0xFF // Most high
#define IVAD_GREEN_CUTOFF_MIN           0x00 // Most low
#define IVAD_GREEN_CUTOFF_MAX           0xFF // Most high
#define IVAD_BLUE_CUTOFF_MIN            0x00 // Most low
#define IVAD_BLUE_CUTOFF_MAX            0xFF // Most high
#define IVAD_RED_DRIVE_MIN              0x00 // Most low
#define IVAD_RED_DRIVE_MAX              0xFF // Most high
#define IVAD_GREEN_DRIVE_MIN            0x00 // Most low
#define IVAD_GREEN_DRIVE_MAX            0xFF // Most high
#define IVAD_BLUE_DRIVE_MIN             0x00 // Most low
#define IVAD_BLUE_DRIVE_MAX             0xFF // Most high
#define IVAD_HORIZONTAL_POS_MIN         0x80 // Most left
#define IVAD_HORIZONTAL_POS_MAX         0xFF // Most right
#define IVAD_HEIGHT_MIN                 0x80 // Most small
#define IVAD_HEIGHT_MAX                 0xFE // Most big
#define IVAD_VERTICAL_POS_MIN           0x00 // Most low
#define IVAD_VERTICAL_POS_MAX           0x7F // Most high
#define IVAD_S_CORRECTION_MIN           0x80 // Most low
#define IVAD_S_CORRECTION_MAX           0xFF // Most high
#define IVAD_KEYSTONE_MIN               0x80 // Most thin at top
#define IVAD_KEYSTONE_MAX               0xFF // Most thin at bottom
#define IVAD_PINCUSHION_MIN             0x80 // Most left
#define IVAD_PINCUSHION_MAX             0xFF // Most right
#define IVAD_PINCUSHION_BALANCE_MIN     0x00 // Most low
#define IVAD_PINCUSHION_BALANCE_MAX     0xFF // Most high
#define IVAD_WIDTH_MIN                  0x00 // Most thin
#define IVAD_WIDTH_MAX                  0x7F // Most thick
#define IVAD_PARALLELOGRAM_MIN          0x80 // Most left
#define IVAD_PARALLELOGRAM_MAX          0xFF // Most right
#define IVAD_BRIGHTNESS_DRIVE_MIN       0xC0 // Most bright
#define IVAD_BRIGHTNESS_DRIVE_MAX       0xFF // Most dim
#define IVAD_BRIGHTNESS_MIN             0x00 // Most dim
#define IVAD_BRIGHTNESS_MAX             0x0A // Most bright according to Apple Display Service utility
#define IVAD_BRIGHTNESS_MAX_OVERDRIVE   0x32 // Maximum the IVAD bord will accept in real life
#define IVAD_ROTATION_MIN               0x00 // Most left
#define IVAD_ROTATION_MAX               0x7F // Most right

#define EDID_IMAC_G3_VANILLA \
    0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x00, 0x06, 0x10, 0x05, 0x9d, \
    0x01, 0x01, 0x01, 0x01, 0x00, 0x08, 0x01, 0x01, 0x08, 0x1b, 0x14, 0x96, \
    0xe8, 0x66, 0xe9, 0x9c, 0x57, 0x4c, 0x96, 0x26, 0x10, 0x48, 0x4c, 0x00, \
    0x02, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, \
    0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x88, 0x13, 0x80, 0xc0, 0x20, 0xe0, \
    0x22, 0x10, 0x10, 0x40, 0x13, 0x00, 0x0e, 0xc8, 0x10, 0x00, 0x00, 0x1e, \
    0x60, 0x18, 0x20, 0xf0, 0x30, 0x58, 0x20, 0x20, 0x10, 0x50, 0x13, 0x00, \
    0x0e, 0xc8, 0x10, 0x00, 0x00, 0x1e, 0x00, 0x00, 0x00, 0xfd, 0x00, 0x4b, \
    0x75, 0x3c, 0x3c, 0x08, 0x00, 0x0a, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, \
    0x00, 0x00, 0x00, 0xfc, 0x00, 0x69, 0x4d, 0x61, 0x63, 0x0a, 0x20, 0x20, \
    0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x00, 0xc9

#define EDID_IMAC_G3_OPTIMIZED_1_4  \
  0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x00, 0x06, 0x10, 0x05, 0x9d, \
  0x01, 0x01, 0x01, 0x01, 0xff, 0x0a, 0x01, 0x04, 0x08, 0x1b, 0x14, 0x96, \
  0xe8, 0x66, 0xe9, 0x9c, 0x57, 0x4c, 0x96, 0x26, 0x10, 0x48, 0x4c, 0x00, \
  0x02, 0x00, 0x31, 0x79, 0x45, 0x63, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, \
  0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x88, 0x13, 0x80, 0xc0, 0x20, 0xe0, \
  0x22, 0x10, 0x10, 0x40, 0x13, 0x00, 0x0e, 0xc8, 0x10, 0x00, 0x00, 0x1e, \
  0x60, 0x18, 0x20, 0xf0, 0x30, 0x58, 0x20, 0x20, 0x10, 0x50, 0x13, 0x00, \
  0x0e, 0xc8, 0x10, 0x00, 0x00, 0x1e, 0x00, 0x00, 0x00, 0xfd, 0x00, 0x4b, \
  0x75, 0x3c, 0x3c, 0x08, 0x00, 0x0a, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, \
  0x00, 0x00, 0x00, 0xfc, 0x00, 0x69, 0x4d, 0x61, 0x63, 0x20, 0x47, 0x33, \
  0x0a, 0x20, 0x20, 0x20, 0x20, 0x20, 0x00, 0x3d

#define VSYNC_TIMEOUT          5000
#define VSYNC_PULSES_NEEDED    5

#define LED_ORANGE_BLINK_RATE  2000

#define SERIAL_BUFFER_MAX_SIZE 0x20
#define SERIAL_EOL_MARKER      '\n'

#define HEADPHONE_SENSE_NUM_READINGS 3
#define HEADPHONE_SENSE_READ_DELAY   250
#define HEADPHONE_SENSE_THRESHOLD    100
