@ECHO OFF
SET INSTALLDIR=%PROGRAMFILES%\crtcpl

ECHO Apple iMac G3 CRT Control Panel Applet for Microsoft Windows
ECHO.
ECHO "Apple" and "iMac" are registered trademarks of Apple, Inc. All
ECHO other trademarks used in this application are the property of
ECHO their respective owners.
ECHO.
ECHO This software was not produced and is not endorsed by Apple, Inc.
ECHO.
ECHO This software comes with ABSOLUTELY NO WARRANTY, to the extent
ECHO permitted by applicable law. It can cause irreparable hardware
ECHO damage to your computer if used incorrectly.
ECHO.
ECHO By continuing, you agree not to hold the developer(s) liable for
ECHO any problems that occur.
ECHO.
ECHO Installation directory:
ECHO %INSTALLDIR%
ECHO.

CHOICE /M "Do you want to uninstall now?"

IF ERRORLEVEL 2 (
  ECHO Aborting.
  ECHO.
  GOTO EOF
)

VER>NUL

ECHO.
ECHO Now uninstalling...

RMDIR /S /Q "%INSTALLDIR%"
IF ERRORLEVEL 1 GOTO ERROR

REG DELETE "HKEY_CLASSES_ROOT\CLSID\{EA6ACE18-0BCB-47FF-A163-91159F1169CC}" /f
IF ERRORLEVEL 1 GOTO ERROR

REG DELETE "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ControlPanel\NameSpace\{EA6ACE18-0BCB-47FF-A163-91159F1169CC}" /f
IF ERRORLEVEL 1 GOTO ERROR

REG DELETE "HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run" /v "Right Click Assist" /f
IF ERRORLEVEL 1 GOTO ERROR

SCHTASKS /DELETE /TN "Eject iMac G3 CD-ROM Drive" /F

ECHO Uninstall succeeded.

ECHO.

GOTO EOF

:ERROR
ECHO Uninstall failed.
ECHO.

:EOF
PAUSE
