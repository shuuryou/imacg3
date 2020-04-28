# Specification of the Serial Protocol


## Document Revision History

Date|Version|Description
----|-------|-----------
2020/04/26|1.0|Initial release.


## About the Wording Used in this Document

In this document, several words are used to signify the important requirements of the specification. These words are ordinarily written in **bold** type. This section shall define their intended interpretation.

Wording|Intended Interpretation
-------|-----------------------
**must**|This word means that the definition is an absolute requirement of the specification.
**must not** |These words means that the definition is an absolute prohibition of the specification.
**should**|This word means that there may exist valid reasons in particular circumstances to ignore a particular item, but the full implications must be understood and carefully weighed before choosing a different course.
**should not**|These words mean that there may exist valid reasons in particular circumstances when the particular behavior is acceptable or even useful, but the full implications should be understood and the case carefully weighed before continuing.

## Message Format

The micro controller expects a serial port configuration of 9600 bps, with 8 data bits, no parity, and one stop bit (8-n-1).

The client **must** send requests that are in a fixed 9 bytes sequence, given below.

Byte|Meaning|Description
----|-------|-----------
1|Start byte|`07h BEL`
2|ID Byte|Range: `00h` - `FFh`
3|Command byte|See below
4|Value byte A | See below
5|Value byte B |See below
6|End byte A|`03h EOT`
7|Checksum|Checksum of bytes 1 - 6
8|End byte B|`04h ETX`
9|Newline|`0Ah LF`

The micro controller **must** respond as follows:

Byte|Meaning|Description
----|-------|-----------
1|Start byte|Either `06h ACK` or `15h NAK`
2|ID byte|Echo of the ID byte from the client
3|Length byte|Range: `00h` - `FFh`
4..n|Variable payload|Zero or more bytes
n+1|End byte A|`03h EOT`
n+2|Checksum|Checksum of bytes 1 - n+1
n+3|End byte B|`04h ETX`
n+4|Newline|`0Ah LF`

An `ACK` response **must** indicatre that the command was actioned, while a `NAK` response **must** indicate that the command was NOT actioned due to an error or another critical condition.

A `NAK` response **should not** have an empty payload. It **should** instead contain a 1 byte payload with an error code in the range of `00h` - `FFh`.


## Supported Command Bytes

### `01h` Get EEPROM Version

The EEPROM version can be used for compatibility purposes as the format of the configuration stored in the EEPROM may change at any time. Value bytes A and B are ignored but they **should** be `00h`. The response payload is a single byte in the range of `00h` - `FFh` with the EEPROM version.

### `02h` Dump SRAM Config

This will return all settings as they are currently stored in SRAM. Each byte in the response payload represents one setting. The order of the settings can be found in the `config.h` file. Value bytes A and B are ignored but they **should** be `00h`.

### `03h` IVAD Change Setting

Instructs the IVAD board to make a change to the CRT configuration. The value byte A is the setting ID (see `IVAD_SETTING_*` in `config.h`) and value byte B is the value to set. Value bytes A and B are validated by the micro controller. On success, the change is remembered in SRAM but not automatically updated in the EEPROM.

### `04h` IVAD Reset from EEPROM

Replaces all settings currently in SRAM from values in the EEPROM and then sends all settings to the IVAD board in one shot. Value bytes A and B are ignored but they **should** be `00h`.


### `05h` EEPROM Reset to Default

Resets settings in SRAM to their defaults, sends the defaults to the IVAD board in one shot, and then writes them to EEPROM to make them permanent. Value bytes A and B are ignored but they **should** be `00h`.


### `06h` Write SRAM to EEPROM

Writes all settings as they are currently in SRAM to the EEPROM to make them permanent. Value bytes A and B are ignored but they **should** be `00h`.


## Possible Error Codes

A `NAK` response **should** have an error code as described earlier. The error codes are currently defined as follows, but receivers **must** be prepared to accept and process undocumented error codes gracefully.

All commands:

* `01h` Bad markers: either the start byte, the end byte A, and/or the end byte B are incorrect.
* `02h` Checksum mismatch error.
* `03h` Unsupported command byte.

For command _`03h` IVAD Change Setting_, additionally:

* `65`h Unknown setting
* `66h` Value out of range (too low)
* `67h` Value out of range (too high)

## Checksum Algorithm

The checksum **must** be calculated as described in the following algorithm. It shall cover all bytes that precede the checksum byte. The final checksum **must not** be `00h`.

```c
byte checksum(const byte arr[], const int len)
{
  int sum = 1;

  for (int i = 0; i < len; i++)
    sum += arr[i];

  byte ret = 256 - (sum % 256);

  return ret;
}
```
ENDS