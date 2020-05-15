#pragma GCC diagnostic error "-Wall"
#pragma GCC diagnostic error "-Wextra"

#include <Wire.h>
#include <SoftwareWire.h>
#include <ezButton.h>

#pragma GCC diagnostic push
#pragma GCC diagnostic ignored "-Wunused-variable"
/* Why the above?
   GCC whines that the EEPROM global variable is never used anywhere.
   I'm not going to change the code of a library to fix that issue...
*/
#include <EEPROMWearLevel.h>
#pragma GCC diagnostic pop

#include "config.h"

byte                    CURRENT_CONFIG[CONFIG_EEPROM_SLOTS];

const byte              IMAC_G3_SCREEN_EDID[128] = { EDID_IMAC_G3_OPTIMIZED_1_4 };

volatile unsigned short VSYNC_PULSES_RECEIVED    = 0;
volatile unsigned long  VSYNC_LAST               = 0;
unsigned long           LED_ORANGE_LAST_UPDATE   = 0;
byte                    CRT_MASTER_OFF           = 0;

SoftwareWire            IVAD_I2C_BUS(PIN_IVAD_SDA, PIN_IVAD_SCL);

ezButton                SIDE_BUTTON_1(PIN_SIDE_BUTTON_1);
ezButton                SIDE_BUTTON_2(PIN_SIDE_BUTTON_2);
ezButton                POWER_BUTTON(PIN_POWER_BUTTON);

byte                    SERIAL_BUFFER[SERIAL_BUFFER_MAX_SIZE];
byte                    SERIAL_BUFFER_DATA_SIZE;
boolean                 SERIAL_HAVE_DATA = false;

int                     HEADPHONE_SENSE_READINGS[HEADPHONE_SENSE_NUM_READINGS];
byte                    HEADPHONE_SENSE_READ_IDX      = 0;
unsigned short          HEADPHONE_SENSE_TOTAL         = 0;
unsigned short          HEADPHONE_SENSE_AVERAGE       = 0;
unsigned long           HEADPHONE_SENSE_LAST_READ     = 0;

void setup()
{
  pinMode(PIN_SIDE_BUTTON_1, INPUT_PULLUP);
  pinMode(PIN_SIDE_BUTTON_2, INPUT_PULLUP);
  pinMode(PIN_POWER_BUTTON, INPUT_PULLUP);

  pinMode(PIN_LED_ORANGE, OUTPUT);
  pinMode(PIN_LED_GREEN, OUTPUT);

  pinMode(PIN_HEADPHONE_SENSE_1, INPUT);
  pinMode(PIN_HEADPHONE_SENSE_2, INPUT);

  pinMode(PIN_IVAD_SDA, OUTPUT);
  pinMode(PIN_IVAD_SCL, OUTPUT);

  pinMode(PIN_VGA_SDA, OUTPUT);
  pinMode(PIN_VGA_SCL, OUTPUT);
  pinMode(PIN_VGA_VSYNC, INPUT);

  pinMode(PIN_RELAY_PC, OUTPUT);
  pinMode(PIN_RELAY_CRT, OUTPUT);
  pinMode(PIN_RELAY_SPK_AMP, OUTPUT);
  pinMode(PIN_RELAY_RESERVED, OUTPUT);

  digitalWrite(PIN_LED_ORANGE, LOW);
  digitalWrite(PIN_LED_GREEN, LOW);
  digitalWrite(PIN_IVAD_SDA, LOW);
  digitalWrite(PIN_IVAD_SCL, LOW);
  digitalWrite(PIN_VGA_SDA, LOW);
  digitalWrite(PIN_VGA_SCL, LOW);

  digitalWrite(PIN_RELAY_PC, HIGH); // OFF
  digitalWrite(PIN_RELAY_CRT, HIGH); // OFF
  digitalWrite(PIN_RELAY_SPK_AMP, HIGH); // OFF
  digitalWrite(PIN_RELAY_RESERVED, HIGH); // OFF

  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, LOW); // disable useless onboard LED

  attachInterrupt(digitalPinToInterrupt(PIN_VGA_VSYNC), vsync_interrupt_proc, RISING);

  Serial.begin(9600);

  EEPROMwl.begin(CONFIG_EEPROM_VERSION, CONFIG_EEPROM_SLOTS);

  for (int i = 0; i < HEADPHONE_SENSE_NUM_READINGS; i++) {
    HEADPHONE_SENSE_READINGS[i] = 0;
  }

  settings_load();

  edid_setup_i2c();
  ivad_setup_i2c();
}

void loop()
{  
  SIDE_BUTTON_1.loop();
  SIDE_BUTTON_2.loop();
  POWER_BUTTON.loop();

  if (SIDE_BUTTON_1.isReleased())
  {
    if (CRT_MASTER_OFF == 0)
      CRT_MASTER_OFF = 1;
    else
      CRT_MASTER_OFF = 0;

#ifdef DEBUG
    Serial.println(CRT_MASTER_OFF == 0 ? "CRT MASTER OFF: 0" : "CRT MASTER OFF: 1");
#endif
  }

  if (SIDE_BUTTON_2.isReleased())
  {
    // Turn off CRT and reset settings to default
    emergency_reset();

#ifdef DEBUG
    Serial.println("EMERGENCY RESET");
#endif
  }

  // Power button
  {
    if (POWER_BUTTON.getState() == LOW)
    {
#ifdef DEBUG
      Serial.println("POWER BUTTON");
#endif
      digitalWrite(PIN_RELAY_PC, LOW); // ON
    }
    else
    {
      digitalWrite(PIN_RELAY_PC, HIGH); // OFF
    }
  }

  // Headphone sense
  {
    if (digitalRead(PIN_RELAY_CRT) == HIGH)
    {
      // If the CRT is OFF, the speaker amplifier shall be OFF too.

      if (digitalRead(PIN_RELAY_SPK_AMP) == LOW) {
#ifdef DEBUG
        Serial.println("CRT OFF -> AMPLIFIER OFF");
#endif
        digitalWrite(PIN_RELAY_SPK_AMP, HIGH); // OFF
      }

      // No need to check headphone sense while CRT is off.
      goto skipHeadphone;
    }

    if (HEADPHONE_SENSE_LAST_READ == 0 || millis() - HEADPHONE_SENSE_LAST_READ > HEADPHONE_SENSE_READ_DELAY)
    {
      int val = max(analogRead(PIN_HEADPHONE_SENSE_1), analogRead(PIN_HEADPHONE_SENSE_2));

      // Software debounce ;/
      HEADPHONE_SENSE_TOTAL = HEADPHONE_SENSE_TOTAL - HEADPHONE_SENSE_READINGS[HEADPHONE_SENSE_READ_IDX];
      HEADPHONE_SENSE_READINGS[HEADPHONE_SENSE_READ_IDX] = val;
      HEADPHONE_SENSE_TOTAL = HEADPHONE_SENSE_TOTAL + HEADPHONE_SENSE_READINGS[HEADPHONE_SENSE_READ_IDX];
      HEADPHONE_SENSE_READ_IDX = (HEADPHONE_SENSE_READ_IDX + 1) % HEADPHONE_SENSE_NUM_READINGS;
      HEADPHONE_SENSE_AVERAGE = HEADPHONE_SENSE_TOTAL / HEADPHONE_SENSE_NUM_READINGS;
      HEADPHONE_SENSE_LAST_READ = millis();

      if (HEADPHONE_SENSE_AVERAGE < HEADPHONE_SENSE_THRESHOLD && digitalRead(PIN_RELAY_SPK_AMP) == HIGH)
      {
#ifdef DEBUG
        Serial.println("HEADPHONE DISCONNECTED");
#endif

        // Turn ON amplifier
        digitalWrite(PIN_RELAY_SPK_AMP, LOW); // ON
        goto skipHeadphone;
      }

      if (HEADPHONE_SENSE_AVERAGE >= HEADPHONE_SENSE_THRESHOLD && digitalRead(PIN_RELAY_SPK_AMP) == LOW)
      {
#ifdef DEBUG
        Serial.println("HEADPHONE CONNECTED");
#endif

        // Turn OFF amplifier
        digitalWrite(PIN_RELAY_SPK_AMP, HIGH); // OFF
        goto skipHeadphone;
      }
    }
skipHeadphone:
    ;
  }

  // VSYNC detect
  {
    noInterrupts();
    unsigned long last    = VSYNC_LAST;
    unsigned short pulses = VSYNC_PULSES_RECEIVED;
    interrupts();

    if (last == 0)
      goto skipVSYNC;

    if (CRT_MASTER_OFF == 1)
    {
      if (digitalRead(PIN_RELAY_CRT) == LOW)
        digitalWrite(PIN_RELAY_CRT, HIGH); // OFF

      goto skipVSYNC;
    }

    if (millis() - last < VSYNC_TIMEOUT)
    {
      // Turn ON CRT

      if (digitalRead(PIN_RELAY_CRT) == LOW) // CRT already ON
        goto skipVSYNC;

      if (pulses < VSYNC_PULSES_NEEDED)
      {
#ifdef DEBUG
        Serial.println("Not enough vsync pulses yet.");
#endif
        goto skipVSYNC;
      }

      // CRT power ON
      digitalWrite(PIN_RELAY_CRT, LOW); // ON

#ifdef DEBUG
      Serial.println("CRT POWER ON");
#endif

      ivad_initialize();
      goto skipVSYNC;
    }

    // millis() - last >= VSYNC_TIMEOUT from here, so turn off CRT

    if (digitalRead(PIN_RELAY_CRT) == HIGH) // CRT already OFF
      goto skipVSYNC;

    // CRT power OFF
    digitalWrite(PIN_RELAY_CRT, HIGH); // OFF

#ifdef DEBUG
    Serial.println("CRT POWER OFF");
#endif

    noInterrupts();
    VSYNC_LAST = 0;
    VSYNC_PULSES_RECEIVED = 0;
    interrupts();

skipVSYNC:
    ;
  }

  // LED update
  {
    if (digitalRead(PIN_RELAY_CRT) == HIGH) // OFF
    {
      // CRT is OFF

      digitalWrite(PIN_LED_GREEN, LOW);

      if (CRT_MASTER_OFF == 1)
      {
        // CRT master off is indicated with a permanent orange LED
        digitalWrite(PIN_LED_ORANGE, HIGH);
        goto skipLED;
      }

      // CRT in standby mode is indicated with a flashing orange LED
      if (LED_ORANGE_LAST_UPDATE == 0)
      {
        LED_ORANGE_LAST_UPDATE = millis();
        digitalWrite(PIN_LED_ORANGE, HIGH);
        goto skipLED;
      }

      if (millis() - LED_ORANGE_LAST_UPDATE < LED_ORANGE_BLINK_RATE)
        goto skipLED;

      digitalWrite(PIN_LED_ORANGE, !digitalRead(PIN_LED_ORANGE));

      LED_ORANGE_LAST_UPDATE = millis();

      goto skipLED;
    }

    // digitalRead(PIN_RELAY_CRT) != HIGH) from here
    // CRT is ON, which is indicated by a permanent green LED

    digitalWrite(PIN_LED_ORANGE, LOW);
    digitalWrite(PIN_LED_GREEN, HIGH);
skipLED:
    ;
  }

  // Check if IVAD has something to say XXX TODO
  /*
    {
     while (IVAD_I2C_BUS.available())
     {
       char c = IVAD_I2C_BUS.read();
       #ifdef DEBUG
        Serial.print("IVAD_I2C_BUS: ");
        Serial.println(c, HEX);
       #endif
     }
    }
  */

  serial_processing();
}

void emergency_reset()
{
  // Reset power off CRT and reset settings to default

  digitalWrite(PIN_LED_GREEN, LOW);
  digitalWrite(PIN_LED_ORANGE, LOW);

  digitalWrite(PIN_RELAY_CRT, HIGH); // OFF
  digitalWrite(PIN_RELAY_SPK_AMP, HIGH); // OFF

  settings_reset_default();
  ivad_write_settings(false);
  settings_store();

  // 10 seconds should be enough to pull the power plug in case
  // magic smoke starts coming out. It also gives the CRT some
  // time to settle after being powered off.

  byte c = 0;
  for (int i = 0; i < 100; i++)
  {
    if (c == 0)
    {
      digitalWrite(PIN_LED_ORANGE, LOW);
      digitalWrite(PIN_LED_GREEN, HIGH);
      c = 1;
    }
    else
    {
      digitalWrite(PIN_LED_GREEN, LOW);
      digitalWrite(PIN_LED_ORANGE, HIGH);
      c = 0;
    }

    delay(100);
  }

  digitalWrite(PIN_LED_GREEN, LOW);
  digitalWrite(PIN_LED_ORANGE, LOW);

  delay(1000);
}

void serial_processing()
{
  byte __errcode = 0;

  for (;;)
  {
    if (Serial.available() <= 0 || SERIAL_HAVE_DATA)
      break;

    byte b = Serial.read();

    SERIAL_BUFFER[SERIAL_BUFFER_DATA_SIZE++] = b;

    if (b == SERIAL_EOL_MARKER && SERIAL_BUFFER_DATA_SIZE == 9)
    {
      SERIAL_HAVE_DATA = true;
      continue;
    }

    if (SERIAL_BUFFER_DATA_SIZE >= SERIAL_BUFFER_MAX_SIZE)
      SERIAL_BUFFER_DATA_SIZE = 0;
  }

  if (!SERIAL_HAVE_DATA)
    return;

#ifdef DEBUG
  Serial.println("serial_processing: SERIAL_HAVE_DATA");
#endif

  if (SERIAL_BUFFER_DATA_SIZE != 9)
  {
#ifdef DEBUG
    Serial.print("serial_processing: Bad data size ");
    Serial.println(SERIAL_BUFFER_DATA_SIZE);
#endif

    SERIAL_HAVE_DATA = false;
    SERIAL_BUFFER_DATA_SIZE = 0;

    return;
  }

  SERIAL_HAVE_DATA = false;
  SERIAL_BUFFER_DATA_SIZE = 0;

  byte id = SERIAL_BUFFER[1];
  byte cmd = SERIAL_BUFFER[2];
  byte valA = SERIAL_BUFFER[3];
  byte valB = SERIAL_BUFFER[4];
  byte chk = SERIAL_BUFFER[6];

  if (SERIAL_BUFFER[0] != 0x07 || SERIAL_BUFFER[5] != 0x03 || SERIAL_BUFFER[7] != 0x04)
  {
    __errcode = 1;
    goto err;
  }

  if (checksum(SERIAL_BUFFER, 6) != chk)
  {
    __errcode = 2;
    goto err;
  }

  switch (cmd)
  {
    default:
      __errcode = 3;
      goto err;
    case 0x01: // Get EEPROM Version
      {
        byte ret[8] { 0x06, id, 0x01, CONFIG_EEPROM_VERSION, 0x03, 0xFF, 0x04, SERIAL_EOL_MARKER };
        ret[5] = checksum(ret, 5);
        Serial.write(ret, 8);
      }
      break;
    case 0x02: // Dump SRAM Config
      {
        byte ret[7 + CONFIG_EEPROM_SLOTS];

        ret[0] = 0x06;
        ret[1] = SERIAL_BUFFER[1];
        ret[2] = CONFIG_EEPROM_SLOTS;
        for (int i = 0; i < CONFIG_EEPROM_SLOTS; i++)
          ret[3 + i] = CURRENT_CONFIG[i];
        ret[2 + CONFIG_EEPROM_SLOTS + 1] = 0x03;
        ret[2 + CONFIG_EEPROM_SLOTS + 2] = checksum(ret, 2 + CONFIG_EEPROM_SLOTS + 1 + 1);
        ret[2 + CONFIG_EEPROM_SLOTS + 3] = 0x04;
        ret[2 + CONFIG_EEPROM_SLOTS + 4] = SERIAL_EOL_MARKER;
        Serial.write(ret, 7 + CONFIG_EEPROM_SLOTS);
      }
      break;
    case 0x03: // IVAD Change Setting
      {
        int result = ivad_change_setting(valA, valB);

        if (result < 0) result = 0;
        else if (result > 100) result = 100;

        if (result != 0)
        {
          // sorry
          __errcode = 100 + result;
          goto err;
        }

        byte ret[7] { 0x06, id, 0x00, 0x03, 0xFF, 0x04, SERIAL_EOL_MARKER };
        ret[4] = checksum(ret, 4);
        Serial.write(ret, 7);
      }
      break;
    case 0x04: // IVAD Reset from EEPROM
      {
        settings_load();
        ivad_write_settings(false);

        byte ret[7] { 0x06, id, 0x00, 0x03, 0xFF, 0x04, SERIAL_EOL_MARKER };
        ret[4] = checksum(ret, 4);
        Serial.write(ret, 7);
      }
      break;
    case 0x05: // EEPROM Reset to Default
      {
        settings_reset_default();
        ivad_write_settings(false);
        settings_store();

        byte ret[7] { 0x06, id, 0x00, 0x03, 0xFF, 0x04, SERIAL_EOL_MARKER };
        ret[4] = checksum(ret, 4);
        Serial.write(ret, 7);
      }
      break;
    case 0x06: // Write SRAM to EEPROM
      {
        settings_store();
        byte ret[7] { 0x06, id, 0x00, 0x03, 0xFF, 0x04, SERIAL_EOL_MARKER };
        ret[4] = checksum(ret, 4);
        Serial.write(ret, 7);
      }
  }

  return;

err:
#ifdef DEBUG
  Serial.print("serial_processing: Send error ");
  Serial.println(__errcode, HEX);
#endif

  byte ret[8] { 0x15, SERIAL_BUFFER[1], 0x01, __errcode, 0x03, 0xFF, 0x04, SERIAL_EOL_MARKER };
  ret[5] = checksum(ret, 5);
  Serial.write(ret, 8);
}

void vsync_interrupt_proc()
{
  VSYNC_LAST = millis();
  VSYNC_PULSES_RECEIVED++;
}

void edid_setup_i2c()
{
  Wire.begin(0x50);
  Wire.onRequest(edid_on_request);
  Wire.onReceive(edid_on_receive);
}

void edid_on_request()
{
  // Must send EDID first, or we miss the time slot.
  Wire.write(IMAC_G3_SCREEN_EDID, 128);

#ifdef DEBUG
  Serial.println("edid_on_request: Sent EDID");
#endif
}

void edid_on_receive(int byteCount)
{
#ifdef DEBUG
  Serial.print("edid_on_receive: byte count: ");
  Serial.print(byteCount);
#endif

  if (byteCount == 0)
    return; // Shut up GCC unused argument

  while (Wire.available())
  {
#ifdef DEBUG
    char c = Wire.read();
    Serial.print("edid_on_receive: ");
    Serial.println(c);
#else
    Wire.read();
#endif
  }
}

void ivad_setup_i2c()
{
  IVAD_I2C_BUS.begin();
}

void ivad_write(byte address, byte message1)
{
  IVAD_I2C_BUS.beginTransmission(address);
  IVAD_I2C_BUS.write(message1);
  IVAD_I2C_BUS.endTransmission();
}

void ivad_write(byte address, byte message1, byte message2)
{
  IVAD_I2C_BUS.beginTransmission(address);
  IVAD_I2C_BUS.write(message1);
  IVAD_I2C_BUS.write(message2);
  IVAD_I2C_BUS.endTransmission();
}

void ivad_read(byte address, byte read_max)
{
#ifdef DEBUG
  IVAD_I2C_BUS.requestFrom(address, read_max);

  Serial.print("ivad_read: ");

  while (IVAD_I2C_BUS.available())
  {
    char c = IVAD_I2C_BUS.read();
    Serial.print(c, HEX);
  }

  Serial.println();
#else
  IVAD_I2C_BUS.requestFrom(address, read_max);
  delay(30); // Remove this and it doesn't work
  while (IVAD_I2C_BUS.available())
    IVAD_I2C_BUS.read();
#endif
}

void ivad_initialize()
{
  ivad_write(IVAD_REGISTER_PROPERTY, 0x13, 0x00);
  ivad_read(IVAD_REGISTER_PROPERTY, 1);
  ivad_write(0x53, 0x33);
  ivad_read(0x53, 1);
  ivad_write(IVAD_REGISTER_PROPERTY, 0x13, 0x0B);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_CONTRAST, 0x00); // If this line is missing, you get no image.
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_HEIGHT, 0xE4); // Required for DEGAUSS
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_ROTATION, 0xC9); // Required for DEGAUSS
  ivad_write(0x53, 0x00);
  ivad_read(0x53, 10);
  ivad_write(0x53, 0x0A);
  ivad_read(0x53, 10);
  ivad_write(0x53, 0x14);
  ivad_read(0x53, 10);
  ivad_write(0x53, 0x1E);
  ivad_read(0x53, 10);
  ivad_write(0x53, 0x28);
  ivad_read(0x53, 10);
  ivad_write(0x53, 0x32);
  ivad_read(0x53, 10);
  ivad_write(0x53, 0x3C);
  ivad_read(0x53, 10);
  ivad_write(0x53, 0x46);
  ivad_read(0x53, 10);
  ivad_write(0x53, 0x50);
  ivad_read(0x53, 10);
  ivad_write(0x53, 0x5A);
  ivad_read(0x53, 10);

  ivad_write_settings(true);
}

void ivad_write_settings(bool for_init)
{
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_RED_CUTOFF, CURRENT_CONFIG[CONFIG_OFFSET_RED_CUTOFF]);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_GREEN_CUTOFF, CURRENT_CONFIG[CONFIG_OFFSET_GREEN_CUTOFF]);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_BLUE_CUTOFF, CURRENT_CONFIG[CONFIG_OFFSET_BLUE_CUTOFF]);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_HORIZONTAL_POS, CURRENT_CONFIG[CONFIG_OFFSET_HORIZONTAL_POS]);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_HEIGHT, CURRENT_CONFIG[CONFIG_OFFSET_HEIGHT]);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_VERTICAL_POS, CURRENT_CONFIG[CONFIG_OFFSET_VERTICAL_POS]);

  if (for_init)
    ivad_write(IVAD_REGISTER_PROPERTY, 0x0A, 0x9E);

  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_KEYSTONE, CURRENT_CONFIG[CONFIG_OFFSET_KEYSTONE]);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_PINCUSHION, CURRENT_CONFIG[CONFIG_OFFSET_PINCUSHION]);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_WIDTH, CURRENT_CONFIG[CONFIG_OFFSET_WIDTH]);

  if (for_init)
    ivad_write(IVAD_REGISTER_PROPERTY, 0x0E, 0xC0);

  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_PARALLELOGRAM, CURRENT_CONFIG[CONFIG_OFFSET_PARALLELOGRAM]);

  if (for_init)
    ivad_write(IVAD_REGISTER_PROPERTY, 0x10, 0x40);

  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_BRIGHTNESS, CURRENT_CONFIG[CONFIG_OFFSET_BRIGHTNESS]);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_ROTATION, CURRENT_CONFIG[CONFIG_OFFSET_ROTATION]);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_CONTRAST, CURRENT_CONFIG[CONFIG_OFFSET_CONTRAST]);

  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_RED_DRIVE, CURRENT_CONFIG[IVAD_SETTING_RED_DRIVE]);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_GREEN_DRIVE, CURRENT_CONFIG[IVAD_SETTING_GREEN_DRIVE]);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_BLUE_DRIVE, CURRENT_CONFIG[IVAD_SETTING_BLUE_DRIVE]);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_S_CORRECTION, CURRENT_CONFIG[IVAD_SETTING_S_CORRECTION]);
  ivad_write(IVAD_REGISTER_PROPERTY, IVAD_SETTING_PINCUSHION_BALANCE, CURRENT_CONFIG[IVAD_SETTING_PINCUSHION_BALANCE]);
}

int ivad_change_setting(const byte ivad_setting, const byte value)
{
#pragma GCC diagnostic push
#pragma GCC diagnostic ignored "-Wtype-limits"
  /* Why the above?

     Some of these checks are redundant. If the minimum allowed is 0 or
     the maximum allowed is 255, "value" can never be smaller or larger.

     GCC does not like that and throws an error. The redundant checks
     could be commented out or removed, but by just telling GCC to shut
     the fuck up here, the checks will automatically apply if the limits
     in config.h ever change in the future. Until then, GCC silently
     optimizes them away, so they do not impact the size of this sketch.
  */

  switch (ivad_setting)
  {
    default:
      return 1;
    case IVAD_SETTING_CONTRAST:
      if (value < IVAD_CONTRAST_MIN) return 2;
      if (value > IVAD_CONTRAST_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_CONTRAST] = value;
      break;
    case IVAD_SETTING_RED_DRIVE:
      if (value < IVAD_RED_DRIVE_MIN) return 2;
      if (value > IVAD_RED_DRIVE_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_RED_DRIVE] = value;
      break;
    case IVAD_SETTING_GREEN_DRIVE:
      if (value < IVAD_GREEN_DRIVE_MIN) return 2;
      if (value > IVAD_GREEN_DRIVE_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_GREEN_DRIVE] = value;
      break;
    case IVAD_SETTING_BLUE_DRIVE:
      if (value < IVAD_BLUE_DRIVE_MIN) return 2;
      if (value > IVAD_BLUE_DRIVE_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_BLUE_DRIVE] = value;
      break;
    case IVAD_SETTING_RED_CUTOFF:
      if (value < IVAD_RED_CUTOFF_MIN) return 2;
      if (value > IVAD_RED_CUTOFF_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_RED_CUTOFF] = value;
      break;
    case IVAD_SETTING_GREEN_CUTOFF:
      if (value < IVAD_GREEN_CUTOFF_MIN) return 2;
      if (value > IVAD_GREEN_CUTOFF_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_GREEN_CUTOFF] = value;
      break;
    case IVAD_SETTING_BLUE_CUTOFF:
      if (value < IVAD_BLUE_CUTOFF_MIN) return 2;
      if (value > IVAD_BLUE_CUTOFF_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_BLUE_CUTOFF] = value;
      break;
    case IVAD_SETTING_HORIZONTAL_POS:
      if (value < IVAD_HORIZONTAL_POS_MIN) return 2;
      if (value > IVAD_HORIZONTAL_POS_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_HORIZONTAL_POS] = value;
      break;
    case IVAD_SETTING_HEIGHT:
      if (value < IVAD_HEIGHT_MIN) return 2;
      if (value > IVAD_HEIGHT_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_HEIGHT] = value;
      break;
    case IVAD_SETTING_VERTICAL_POS:
      if (value < IVAD_VERTICAL_POS_MIN) return 2;
      if (value > IVAD_VERTICAL_POS_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_VERTICAL_POS] = value;
      break;
    case IVAD_SETTING_S_CORRECTION:
      if (value < IVAD_S_CORRECTION_MIN) return 2;
      if (value > IVAD_S_CORRECTION_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_S_CORRECTION] = value;
      break;
    case IVAD_SETTING_KEYSTONE:
      if (value < IVAD_KEYSTONE_MIN) return 2;
      if (value > IVAD_KEYSTONE_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_KEYSTONE] = value;
      break;
    case IVAD_SETTING_PINCUSHION:
      if (value < IVAD_PINCUSHION_MIN) return 2;
      if (value > IVAD_PINCUSHION_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_PINCUSHION] = value;
      break;
    case IVAD_SETTING_WIDTH:
      if (value < IVAD_WIDTH_MIN) return 2;
      if (value > IVAD_WIDTH_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_WIDTH] = value;
      break;
    case IVAD_SETTING_PINCUSHION_BALANCE:
      if (value < IVAD_PINCUSHION_BALANCE_MIN) return 2;
      if (value > IVAD_PINCUSHION_BALANCE_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_PINCUSHION_BALANCE] = value;
      break;
    case IVAD_SETTING_PARALLELOGRAM:
      if (value < IVAD_PARALLELOGRAM_MIN) return 2;
      if (value > IVAD_PARALLELOGRAM_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_PARALLELOGRAM] = value;
      break;
    case IVAD_SETTING_BRIGHTNESS:
      if (value < IVAD_BRIGHTNESS_MIN) return 2;
      if (value > IVAD_BRIGHTNESS_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_BRIGHTNESS] = value;
      break;
    case IVAD_SETTING_ROTATION:
      if (value < IVAD_ROTATION_MIN) return 2;
      if (value > IVAD_ROTATION_MAX) return 3;

      CURRENT_CONFIG[CONFIG_OFFSET_ROTATION] = value;
      break;
  }
#pragma GCC diagnostic pop

  ivad_write(IVAD_REGISTER_PROPERTY, ivad_setting, value);

  CURRENT_CONFIG[CONFIG_OFFSET_CHECKSUM] = checksum(CURRENT_CONFIG, CONFIG_EEPROM_SLOTS - 1);

  return 0;
}

void settings_load()
{
  CURRENT_CONFIG[CONFIG_OFFSET_CONTRAST] = EEPROMwl.read(CONFIG_OFFSET_CONTRAST);
  CURRENT_CONFIG[CONFIG_OFFSET_RED_DRIVE] = EEPROMwl.read(CONFIG_OFFSET_RED_DRIVE);
  CURRENT_CONFIG[CONFIG_OFFSET_GREEN_DRIVE] = EEPROMwl.read(CONFIG_OFFSET_GREEN_DRIVE);
  CURRENT_CONFIG[CONFIG_OFFSET_BLUE_DRIVE] = EEPROMwl.read(CONFIG_OFFSET_BLUE_DRIVE);
  CURRENT_CONFIG[CONFIG_OFFSET_RED_CUTOFF] = EEPROMwl.read(CONFIG_OFFSET_RED_CUTOFF);
  CURRENT_CONFIG[CONFIG_OFFSET_GREEN_CUTOFF] = EEPROMwl.read(CONFIG_OFFSET_GREEN_CUTOFF);
  CURRENT_CONFIG[CONFIG_OFFSET_BLUE_CUTOFF] = EEPROMwl.read(CONFIG_OFFSET_BLUE_CUTOFF);
  CURRENT_CONFIG[CONFIG_OFFSET_HORIZONTAL_POS] = EEPROMwl.read(CONFIG_OFFSET_HORIZONTAL_POS);
  CURRENT_CONFIG[CONFIG_OFFSET_HEIGHT] = EEPROMwl.read(CONFIG_OFFSET_HEIGHT);
  CURRENT_CONFIG[CONFIG_OFFSET_VERTICAL_POS] = EEPROMwl.read(CONFIG_OFFSET_VERTICAL_POS);
  CURRENT_CONFIG[CONFIG_OFFSET_S_CORRECTION] = EEPROMwl.read(CONFIG_OFFSET_S_CORRECTION);
  CURRENT_CONFIG[CONFIG_OFFSET_KEYSTONE] = EEPROMwl.read(CONFIG_OFFSET_KEYSTONE);
  CURRENT_CONFIG[CONFIG_OFFSET_PINCUSHION] = EEPROMwl.read(CONFIG_OFFSET_PINCUSHION);
  CURRENT_CONFIG[CONFIG_OFFSET_WIDTH] = EEPROMwl.read(CONFIG_OFFSET_WIDTH);
  CURRENT_CONFIG[CONFIG_OFFSET_PINCUSHION_BALANCE] = EEPROMwl.read(CONFIG_OFFSET_PINCUSHION_BALANCE);
  CURRENT_CONFIG[CONFIG_OFFSET_PARALLELOGRAM] = EEPROMwl.read(CONFIG_OFFSET_PARALLELOGRAM);
  CURRENT_CONFIG[CONFIG_OFFSET_RESERVED6] = EEPROMwl.read(CONFIG_OFFSET_RESERVED6);
  CURRENT_CONFIG[CONFIG_OFFSET_BRIGHTNESS] = EEPROMwl.read(CONFIG_OFFSET_BRIGHTNESS);
  CURRENT_CONFIG[CONFIG_OFFSET_ROTATION] = EEPROMwl.read(CONFIG_OFFSET_ROTATION);
  CURRENT_CONFIG[CONFIG_OFFSET_CHECKSUM] = EEPROMwl.read(CONFIG_OFFSET_CHECKSUM);

  byte loaded_checksum = CURRENT_CONFIG[CONFIG_OFFSET_CHECKSUM];
  byte expected_checksum = checksum(CURRENT_CONFIG, CONFIG_EEPROM_SLOTS - 1);

  if (loaded_checksum != expected_checksum)
  {
    settings_reset_default();
    settings_store();

#ifdef DEBUG
    Serial.println("Checksum mismatch error. Reset to defaults.");
#endif
  }

#ifdef DEBUG
  Serial.println("Settings loaded.");
#endif
}

void settings_store()
{
  EEPROMwl.update(CONFIG_OFFSET_CONTRAST, CURRENT_CONFIG[CONFIG_OFFSET_CONTRAST]);
  EEPROMwl.update(CONFIG_OFFSET_RED_DRIVE, CURRENT_CONFIG[CONFIG_OFFSET_RED_DRIVE]);
  EEPROMwl.update(CONFIG_OFFSET_GREEN_DRIVE, CURRENT_CONFIG[CONFIG_OFFSET_GREEN_DRIVE]);
  EEPROMwl.update(CONFIG_OFFSET_BLUE_DRIVE, CURRENT_CONFIG[CONFIG_OFFSET_BLUE_DRIVE]);
  EEPROMwl.update(CONFIG_OFFSET_RED_CUTOFF, CURRENT_CONFIG[CONFIG_OFFSET_RED_CUTOFF]);
  EEPROMwl.update(CONFIG_OFFSET_GREEN_CUTOFF, CURRENT_CONFIG[CONFIG_OFFSET_GREEN_CUTOFF]);
  EEPROMwl.update(CONFIG_OFFSET_BLUE_CUTOFF, CURRENT_CONFIG[CONFIG_OFFSET_BLUE_CUTOFF]);
  EEPROMwl.update(CONFIG_OFFSET_HORIZONTAL_POS, CURRENT_CONFIG[CONFIG_OFFSET_HORIZONTAL_POS]);
  EEPROMwl.update(CONFIG_OFFSET_HEIGHT, CURRENT_CONFIG[CONFIG_OFFSET_HEIGHT]);
  EEPROMwl.update(CONFIG_OFFSET_VERTICAL_POS, CURRENT_CONFIG[CONFIG_OFFSET_VERTICAL_POS]);
  EEPROMwl.update(CONFIG_OFFSET_S_CORRECTION, CURRENT_CONFIG[CONFIG_OFFSET_S_CORRECTION]);
  EEPROMwl.update(CONFIG_OFFSET_KEYSTONE, CURRENT_CONFIG[CONFIG_OFFSET_KEYSTONE]);
  EEPROMwl.update(CONFIG_OFFSET_PINCUSHION, CURRENT_CONFIG[CONFIG_OFFSET_PINCUSHION]);
  EEPROMwl.update(CONFIG_OFFSET_WIDTH, CURRENT_CONFIG[CONFIG_OFFSET_WIDTH]);
  EEPROMwl.update(CONFIG_OFFSET_PINCUSHION_BALANCE, CURRENT_CONFIG[CONFIG_OFFSET_PINCUSHION_BALANCE]);
  EEPROMwl.update(CONFIG_OFFSET_PARALLELOGRAM, CURRENT_CONFIG[CONFIG_OFFSET_PARALLELOGRAM]);
  EEPROMwl.update(CONFIG_OFFSET_RESERVED6, CURRENT_CONFIG[CONFIG_OFFSET_RESERVED6]);
  EEPROMwl.update(CONFIG_OFFSET_BRIGHTNESS, CURRENT_CONFIG[CONFIG_OFFSET_BRIGHTNESS]);
  EEPROMwl.update(CONFIG_OFFSET_ROTATION, CURRENT_CONFIG[CONFIG_OFFSET_ROTATION]);
  EEPROMwl.update(CONFIG_OFFSET_CHECKSUM, CURRENT_CONFIG[CONFIG_OFFSET_CHECKSUM]);

#ifdef DEBUG
  Serial.println("Settings stored.");
#endif
}

void settings_reset_default()
{
  CURRENT_CONFIG[CONFIG_OFFSET_CONTRAST]           = 0xFE;
  CURRENT_CONFIG[CONFIG_OFFSET_RED_DRIVE]          = 0x93;
  CURRENT_CONFIG[CONFIG_OFFSET_GREEN_DRIVE]        = 0x93;
  CURRENT_CONFIG[CONFIG_OFFSET_BLUE_DRIVE]         = 0x8F;
  CURRENT_CONFIG[CONFIG_OFFSET_RED_CUTOFF]         = 0x80;
  CURRENT_CONFIG[CONFIG_OFFSET_GREEN_CUTOFF]       = 0x80;
  CURRENT_CONFIG[CONFIG_OFFSET_BLUE_CUTOFF]        = 0x78;
  CURRENT_CONFIG[CONFIG_OFFSET_HORIZONTAL_POS]     = 0xB0;
  CURRENT_CONFIG[CONFIG_OFFSET_HEIGHT]             = 0xF0;
  CURRENT_CONFIG[CONFIG_OFFSET_VERTICAL_POS]       = 0x4D;
  CURRENT_CONFIG[CONFIG_OFFSET_S_CORRECTION]       = 0x9A;
  CURRENT_CONFIG[CONFIG_OFFSET_KEYSTONE]           = 0x9B;
  CURRENT_CONFIG[CONFIG_OFFSET_PINCUSHION]         = 0xCB;
  CURRENT_CONFIG[CONFIG_OFFSET_WIDTH]              = 0x19;
  CURRENT_CONFIG[CONFIG_OFFSET_PINCUSHION_BALANCE] = 0x7B;
  CURRENT_CONFIG[CONFIG_OFFSET_PARALLELOGRAM]      = 0xC6;
  CURRENT_CONFIG[CONFIG_OFFSET_RESERVED6]          = 0x7B;
  CURRENT_CONFIG[CONFIG_OFFSET_BRIGHTNESS]         = 0x0A;
  CURRENT_CONFIG[CONFIG_OFFSET_ROTATION]           = 0x42;
  CURRENT_CONFIG[CONFIG_OFFSET_CHECKSUM]           = checksum(CURRENT_CONFIG, CONFIG_EEPROM_SLOTS - 1);

#ifdef DEBUG
  Serial.println("Settings reset to default.");
#endif
}

byte checksum(const byte arr[], const int len)
{
  int sum = 1; // Checksum may never be 0.

  for (int i = 0; i < len; i++)
    sum += arr[i];

  byte ret = 256 - (sum % 256);

  return ret;
}
