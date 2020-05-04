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

CHOICE /M "Do you want to proceed with the installation?"

IF ERRORLEVEL 2 (
  ECHO Aborting.
  ECHO.
  GOTO EOF
)

VER>NUL

ECHO.
ECHO Now copying program files...

IF NOT EXIST "%INSTALLDIR%\." MKDIR "%INSTALLDIR%"
IF ERRORLEVEL 1 GOTO ERROR

XCOPY /I /S /Y * "%INSTALLDIR%\"
IF ERRORLEVEL 1 GOTO ERROR

ECHO.
ECHO Now registering Control Panel icon...

REG ADD "HKEY_CLASSES_ROOT\CLSID\{EA6ACE18-0BCB-47FF-A163-91159F1169CC}" /ve /t REG_SZ /d "Apple iMac G3 CRT Control Panel Applet" /f
IF ERRORLEVEL 1 GOTO ERROR

REG ADD "HKEY_CLASSES_ROOT\CLSID\{EA6ACE18-0BCB-47FF-A163-91159F1169CC}" /v "LocalizedString" /t REG_SZ /d "@\"%INSTALLDIR%\crtcplres.dll\",-101" /f
IF ERRORLEVEL 1 GOTO ERROR

REG ADD "HKEY_CLASSES_ROOT\CLSID\{EA6ACE18-0BCB-47FF-A163-91159F1169CC}" /v "InfoTip" /t REG_SZ /d "@\"%INSTALLDIR%\crtcplres.dll\",-102" /f
IF ERRORLEVEL 1 GOTO ERROR

REG ADD "HKEY_CLASSES_ROOT\CLSID\{EA6ACE18-0BCB-47FF-A163-91159F1169CC}" /v "System.ApplicationName" /t REG_SZ /d "crtcpl" /f
IF ERRORLEVEL 1 GOTO ERROR

REG ADD "HKEY_CLASSES_ROOT\CLSID\{EA6ACE18-0BCB-47FF-A163-91159F1169CC}" /v "System.ControlPanel.Category" /t REG_SZ /d "2" /f
IF ERRORLEVEL 1 GOTO ERROR

REG ADD "HKEY_CLASSES_ROOT\CLSID\{EA6ACE18-0BCB-47FF-A163-91159F1169CC}\DefaultIcon" /ve /t REG_SZ /d "\"%INSTALLDIR%\crtcpl.exe\",0" /f
IF ERRORLEVEL 1 GOTO ERROR

REG ADD "HKEY_CLASSES_ROOT\CLSID\{EA6ACE18-0BCB-47FF-A163-91159F1169CC}\Shell\Open\Command" /ve /t REG_SZ /d "\"%INSTALLDIR%\crtcpl.exe\"" /f
IF ERRORLEVEL 1 GOTO ERROR

REG ADD "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ControlPanel\NameSpace\{EA6ACE18-0BCB-47FF-A163-91159F1169CC}" /ve /t REG_SZ /d "Apple iMac G3 CRT Control Panel Applet" /f
IF ERRORLEVEL 1 GOTO ERROR

ECHO.
ECHO Now creating scheduled task to eject CDs on Windows shut down.
SCHTASKS /CREATE /TN "Eject iMac G3 CD-ROM Drive" /TR "'%INSTALLDIR%\ejectcd.exe'" /RU "SYSTEM" /RL HIGHEST /SC ONEVENT /EC System /MO "*[System[Provider[@Name='USER32'] and (EventID=1074)]]" /F
IF ERRORLEVEL 1 GOTO ERROR

ECHO.
ECHO Configuring Right Click Assist to start on user logon.
REG ADD "HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run" /v "Right Click Assist" /t REG_SZ /d "\"%INSTALLDIR%\rightclickassist.exe\"" /f
IF ERRORLEVEL 1 GOTO ERROR

ECHO.
ECHO Installation succeeded.
ECHO.
ECHO You can find the "iMac G3 Screen Settings" applet in your Control
ECHO Panel under "Hardware and Sound". If you use Windows 10, look for
ECHO it in the classic Control Panel.
ECHO.
ECHO To use Right Click Assist, log off and back on, or restart your
ECHO computer. You can then press CTRL + Left Mouse Button to right click
ECHO in any Windows application. It will probably not work in games, etc.
ECHO To close Right Click Assist, press CTRL+F2 and press the left Mouse
ECHO button.
ECHO.
ECHO Enjoy!
ECHO.

GOTO EOF

:ERROR
ECHO Installation failed.
ECHO.

:EOF
PAUSE
