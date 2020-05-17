# Apple iMac G3 CRT and SBC Controller based on an Arduino Mega

**IMPORTANT NOTICE**
There is a much better and hackable project available that does almost the same as this one. If you have electronics skills you'll see first results in under a day's worth of work.

**See here: https://github.com/qbancoffee/imac_g3_ivad_board_init**

This repository is mainly here to give back and comply with the author's LGPL license, as I gratefully borrowed quite a lot of his knowledge. He has also kindly made his Arduino sketch fully compatible so you can use  the software offered here without issues.

## What's Here

Project|Language|Description
-------|--------|-----------
crtcpl|C#|The main **CRT** **C**ontrol **P**ane**l** applet. It allows you to configure the CRT picture size, etc. on Windows. It requires the IVAD board to be driven by an Arduino or similar.
crtcplres|-|A resource-only DLL that contains a string table used for Windows Control Panel integration.
ejectcd|C#|A small command line program that ejects all CD-ROM drives in the system. Designed to be run as a scheduled task on system shutdown to emulate the behavior of classic MacOS on an iMac G3.
rightclickassist|C++|A mouse hook that allows Windows to be used with a one-button mouse like the one that shipped with the iMac G3. It remaps the right mouse button to CTRL+ Left Click. This works on any Windows system and with any mouse. It's probably also very nice for laptops with awkward touchpads.
setup|Inno Script|A setup program that installs the above components on a Windows computer.
sketch|C|An Arduino sketch that can drive the IVAD board of a slot-loading iMac G3 to use the built-in CRT as a standard VGA monitor. The CRT control panel applet communicates with an Arduino running this sketch using the serial port.

### Building the Projects on Windows

Compile the Windows code with Visual Studio 2019. The solution file should open fine and everything should just compile without errors once you create your own `signing.snk` strong key file or disable the signing of the .NET assemblies in the build properties.

### Building the GUI on Linux (Even on a Raspberry pi)

To compile using Linux (tested using Debian Buster with Mono 5.18.0.240),Ubuntu 18.04 and Raspian Buster. You can try the following commands. This will build an executable in the `crtcpl/bin/Mono` folder that can be run using Mono.

Debian Buster and Raspian Buster
```bash
apt-get install mono-runtime mono-devel
sn -k signing.snk
xbuild /p:Configuration=Mono crtcpl.sln
```
Ubuntu 18.04
```bash
apt-get install mono-runtime mono-devel
sn -k signing.snk
msbuild /p:Configuration=Mono crtcpl.sln
```
xbuild will work as well on Ubuntu 18.04 but it is deprecated and will be removed.


You should get a few warnings, but no errors.

Please note:
* Only `crtcpl` is built. The other projects do not support Linux and no Linux support is planned.
* There are rendering bugs in Mono's WinForms implementation. Most of them have been mitigated to make the program usable but a few visual glitches might remain.
* Linux being Linux, it considers your Arduino to be a typewriter/modem and will pull the DTR line of the USB serial port high. Doing this will reset the Arduino. To stop this retarded behavior, use `stty -F /dev/ttyACM0 -hupcl`, where `ttyACM0` has to be replaced with whatever the OS assigned as the device for your arduino. Hint: try looking at the output of `dmesg` to find it.

### Building the Sketch

The Arduino sketch complies with the standard Arduino development tools. It will run on an Arduino Mega 2560 Rev3 or a compatible clone.

## Translations

These languages are currently available:
* English
* Spanish (by [Rocky Hill](https://github.com/qbancoffee/imac_g3_ivad_board_init "Rocky Hill"))
* French
* German
* Japanese

Corrections and additions are welcome.

### How to Translate the CRT Control Panel Applet

To add another language, you just have add new RESX files and translate them. This is possible by hand but that's a somewhat painful and involved process. Instead, consider using a dedicated tool like [ResX Translator](https://github.com/HakanL/resxtranslator "ResX Translator"). I used release 2.9, which you can [grab here](https://github.com/HakanL/resxtranslator/releases/tag/v2.9 "grab here").

This tool makes translating (or correcting my English) super easy. Simply open the program, go to **File > Open directory**, select the "crtcpl" folder, and open it. Then just go through all of the files shown, and add your language via **Languages > Add new language**. Now you can translate away.

Once you recompile the code, your translation is available for use. The program should automatically pick the correct language based on your current system language.

To get your translation or changes included in the official distribution, please use the pull request feature from GitHub.

## Screenshots

Below are screenshots of the control panel applet.  The other projects don't have a GUI to show here.

![](https://raw.githubusercontent.com/shuuryou/imacg3/master/screenshots/screenshot1.png)

![](https://raw.githubusercontent.com/shuuryou/imacg3/master/screenshots/screenshot2.png)

![](https://raw.githubusercontent.com/shuuryou/imacg3/master/screenshots/screenshot3.png)

![](https://raw.githubusercontent.com/shuuryou/imacg3/master/screenshots/screenshot4.png)

![](https://raw.githubusercontent.com/shuuryou/imacg3/master/screenshots/screenshot5.png)

## Videos

Because seeing is believing...

### Demo After Closing Up
[![My iMac G3 Mod: Demo After Closing Up](https://res.cloudinary.com/marcomontalbano/image/upload/v1588750730/video_to_markdown/images/vimeo--415400271-c05b58ac6eb4c4700831b2b3070cd403.jpg)](https://vimeo.com/415400271 "My iMac G3 Mod: Demo After Closing Up")

### Side Button Demo
[![My iMac G3 Mod: Side Button Demo](https://res.cloudinary.com/marcomontalbano/image/upload/v1588751025/video_to_markdown/images/vimeo--415301822-c05b58ac6eb4c4700831b2b3070cd403.jpg)](https://vimeo.com/415301822 "My iMac G3 Mod: Side Button Demo")

### Amplifier Working
[![My iMac G3 Mod: Amplifier Working](https://res.cloudinary.com/marcomontalbano/image/upload/v1588751064/video_to_markdown/images/vimeo--415301287-c05b58ac6eb4c4700831b2b3070cd403.jpg)](https://vimeo.com/415301287 "My iMac G3 Mod: Amplifier Working")

### CRT Control Panel Applet
[![My iMac G3 Mod: CRT Control Panel Applet](https://res.cloudinary.com/marcomontalbano/image/upload/v1588750923/video_to_markdown/images/vimeo--415301897-c05b58ac6eb4c4700831b2b3070cd403.jpg)](https://vimeo.com/415301897 "My iMac G3 Mod: CRT Control Panel Applet")

### Turning on the CRT
[![My iMac G3 Mod: Turning on the CRT](https://res.cloudinary.com/marcomontalbano/image/upload/v1588750799/video_to_markdown/images/vimeo--415404947-c05b58ac6eb4c4700831b2b3070cd403.jpg)](https://vimeo.com/415404947 "My iMac G3 Mod: Turning on the CRT")

### Testing the Relay Board
[![My iMac G3 Mod: Testing the Relay Board](https://res.cloudinary.com/marcomontalbano/image/upload/v1588751098/video_to_markdown/images/vimeo--415405680-c05b58ac6eb4c4700831b2b3070cd403.jpg)](https://vimeo.com/415405680 "My iMac G3 Mod: Testing the Relay Board")

### Testing an Amplifier Board
[![My iMac G3 Mod:  Testing an Amplifier Board](https://res.cloudinary.com/marcomontalbano/image/upload/v1588751119/video_to_markdown/images/vimeo--415405633-c05b58ac6eb4c4700831b2b3070cd403.jpg)](https://vimeo.com/415405633 "My iMac G3 Mod:  Testing an Amplifier Board")

### Installed as an Interactive Piece of Furniture
[![My iMac G3 Mod: Installed as an Interactive Piece of Furniture](https://res.cloudinary.com/marcomontalbano/image/upload/v1589635273/video_to_markdown/images/vimeo--419256479-c05b58ac6eb4c4700831b2b3070cd403.jpg)](https://vimeo.com/419256479 "My iMac G3 Mod: Installed as an Interactive Piece of Furniture")

## Legal

"Apple" and "iMac" are registered trademarks of Apple, Inc. All other trademarks used in this application are the property of their respective owners.

This software was not produced and is not endorsed by Apple, Inc.

This software comes with ABSOLUTELY NO WARRANTY, to the extent permitted by applicable law. It can cause irreparable hardware damage to your computer if used incorrectly.

