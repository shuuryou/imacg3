# Apple iMac G3 CRT and SBC Controller based on an Arduino Mega

**IMPORTANT NOTICE**
There is a much better and hackable project available that does almost the same as this one. If you have electronics skills you'll see first results in under a day's worth of work.

**See here: https://github.com/qbancoffee/imac_g3_ivad_board_init**

This repository is mainly here to give back and comply with the author's LGPL license, as I gratefully borrowed quite a lot of his knowledge.

Most of what you see here unfortunately won't work with the above out of the box.

## What's Here

Project|Language|Description
-------|--------|-----------
crtcpl|C#|The main **CRT** **C**ontrol **P**ane**l** applet. It allows you to configure the CRT picture size, etc. on Windows. It requires the IVAD board to be driven by an Arduino or similar.
crtcplres|-|A resource-only DLL that contains a string table used for Windows Control Panel integration.
ejectcd|C#|A small command line program that ejects all CD-ROM drives in the system. Designed to be run as a scheduled task on system shutdown to emulate the behavior of classic MacOS on an iMac G3.
rightclickassist|C++|A mouse hook that allows Windows to be used with a one-button mouse like the one that shipped with the iMac G3. It remaps the right mouse button to CTRL+ Left Click. This works on any Windows system and with any mouse. It's probably also very nice for laptops with awkward touchpads.
sketch|C|An Arduino sketch that can drive the IVAD board of a slot-loading iMac G3 to use the built-in CRT as a standard VGA monitor. The CRT control panel applet communicates with an Arduino running this sketch using the serial port.

### Building the Projects on Windows

Compile the Windows code with Visual Studio 2019. The solution file should open fine and everything should just compile without errors once you create your own `signing.snk` strong key file or disable the signing of the .NET assemblies in the build properties.

### Building the GUI on Linux

**Don't bother. It's broken. Seriously.**

Still reading? Fine. To compile using Linux (tested using Debian Buster with Mono 5.18.0.240), you can try the following commands. This will build an executable in the `crtcpl/bin/Mono` folder that can be run using Mono.

```bash
apt-get install mono-runtime mono-devel
sn -k signing.snk
xbuild /p:Configuration=Mono crtcpl.sln
```

You should get a few warnings, but no errors.

Caveats:
* Only `crtcpl` is built. The other projects do not support Linux and no Linux support is planned.
* A window will open but the controls will be largely unusable due to bugs in Mono's WinForms implementation. Short of building an entirely new GUI for Mono that doesn't use any automatic layouting, I see no easy way to fix this.

### Building the Sketch

The Arduino sketch complies with the standard Arduino development tools. It will run on an Arduino Mega 2560 Rev3 or a compatible clone.

## Translations

Four languages are currently available: English, Japanese, French and German. Corrections and additions are welcome.

## Screenshots

Below are screenshots of the control panel applet.  The other projects don't have a GUI to show here.

![](https://raw.githubusercontent.com/shuuryou/imacg3/master/screenshots/screenshot1.png)

![](https://raw.githubusercontent.com/shuuryou/imacg3/master/screenshots/screenshot2.png)

![](https://raw.githubusercontent.com/shuuryou/imacg3/master/screenshots/screenshot3.png)

![](https://raw.githubusercontent.com/shuuryou/imacg3/master/screenshots/screenshot4.png)

![](https://raw.githubusercontent.com/shuuryou/imacg3/master/screenshots/screenshot5.png)

## Legal

"Apple" and "iMac" are registered trademarks of Apple, Inc. All other trademarks used in this application are the property of their respective owners.

This software was not produced and is not endorsed by Apple, Inc.

This software comes with ABSOLUTELY NO WARRANTY, to the extent permitted by applicable law. It can cause irreparable hardware damage to your computer if used incorrectly.

