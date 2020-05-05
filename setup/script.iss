#define MyAppName "Apple iMac G3 Tools"
#define MyAppVersion "1.0"
#define MyAppPublisher "TAKEMURA Kiriko"
#define MyAppURL "https://github.com/shuuryou/imacg3/"
#define MyAppExeName "crtcpl.exe"

[Setup]
AppId={{ea6ace18-0bcb-47ff-a163-91159f1169cc}
AppName={#MyAppName}
AppVerName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={commonpf}\crtcpl
DisableProgramGroupPage=yes
LicenseFile=..\LICENSE
InfoBeforeFile=fckaapl.rtf
InfoAfterFile=appnote.rtf
OutputDir=Output
OutputBaseFilename=install_imac_g3_tools
Compression=lzma
SolidCompression=yes
AllowCancelDuringInstall=False
AllowUNCPath=False
UsePreviousGroup=False
CloseApplications=force
RestartApplications=False
UninstallDisplayIcon={app}\crtcpl.exe
MinVersion=0,6.1
WizardSmallImageFile=wiztop.bmp
ArchitecturesInstallIn64BitMode=x64
ArchitecturesAllowed=x86 x64

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"
Name: "japanese"; MessagesFile: "compiler:Languages\Japanese.isl"

[Files]
Source: "..\crtcpl\bin\Release\crtcpl.exe"; DestDir: "{app}"; Flags: ignoreversion; Components: crtcpl
Source: "..\crtcpl\bin\Release\crtcpl.exe.config"; DestDir: "{app}"; Flags: ignoreversion; Components: crtcpl
Source: "..\crtcpl\bin\Release\crtcplres.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: crtcpl
Source: "..\crtcpl\bin\Release\ejectcd.exe"; DestDir: "{app}"; Flags: ignoreversion; Components: ejectcd
Source: "..\crtcpl\bin\Release\ejectcd.exe.config"; DestDir: "{app}"; Flags: ignoreversion; Components: ejectcd
Source: "..\crtcpl\bin\Release\rightclickassist.exe"; DestDir: "{app}"; Flags: ignoreversion; Components: rightclickassist
Source: "..\crtcpl\bin\Release\de\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs; Components: crtcpl
Source: "..\crtcpl\bin\Release\fr\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs; Components: crtcpl
Source: "..\crtcpl\bin\Release\ja\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs; Components: crtcpl

[Run]
Filename: "{app}\rightclickassist.exe"; WorkingDir: "{app}"; Flags: postinstall runasoriginaluser nowait; Description: "Start Right Click Assist now"; Components: rightclickassist
Filename: "{app}\crtcpl.exe"; WorkingDir: "{app}"; Flags: postinstall runasoriginaluser nowait; Description: "Open Apple iMac G3 CRT Control Panel Applet"
Filename: "{sys}\schtasks.exe"; Parameters: "/CREATE /TN ""Auto Eject CD-ROM Drive"" /TR ""'{app}\ejectcd.exe'"" /RU ""SYSTEM"" /RL HIGHEST /SC ONEVENT /EC System /MO ""*[System[Provider[@Name='USER32'] and (EventID=1074)]]"" /F"; WorkingDir: "{sys}"; Flags: runascurrentuser runhidden; Components: ejectcd

[UninstallRun]
Filename: "{sys}\schtasks.exe"; Parameters: "/DELETE /TN ""Auto Eject CD-ROM Drive"" /F"; WorkingDir: "{sys}"; Flags: runhidden; Components: ejectcd
Filename: "{sys}\taskkill.exe"; Parameters: "/f /im rightclickassist.exe"; WorkingDir: "{sys}"; Flags: runhidden waituntilterminated; Components: rightclickassist
Filename: "{sys}\taskkill.exe"; Parameters: "/f /im crtcpl.exe"; WorkingDir: "{sys}"; Flags: runhidden waituntilterminated; Components: crtcpl

[Registry]
Root: "HKCR"; Subkey: "CLSID\{{EA6ACE18-0BCB-47FF-A163-91159F1169CC}"; ValueType: string; ValueData: "Apple iMac G3 CRT Control Panel Applet"; Flags: uninsdeletekey; Components: crtcpl
Root: "HKCR"; Subkey: "CLSID\{{EA6ACE18-0BCB-47FF-A163-91159F1169CC}"; ValueType: string; ValueName: "LocalizedString"; ValueData: "@""{app}\crtcplres.dll"",-101"; Components: crtcpl
Root: "HKCR"; Subkey: "CLSID\{{EA6ACE18-0BCB-47FF-A163-91159F1169CC}"; ValueType: string; ValueName: "InfoTip"; ValueData: "@""{app}\crtcplres.dll"",-102"; Components: crtcpl
Root: "HKCR"; Subkey: "CLSID\{{EA6ACE18-0BCB-47FF-A163-91159F1169CC}"; ValueType: string; ValueName: "System.ApplicationName"; ValueData: "crtcpl"; Components: crtcpl
Root: "HKCR"; Subkey: "CLSID\{{EA6ACE18-0BCB-47FF-A163-91159F1169CC}"; ValueType: string; ValueName: "System.ControlPanel.Category"; ValueData: "2"; Components: crtcpl
Root: "HKCR"; Subkey: "CLSID\{{EA6ACE18-0BCB-47FF-A163-91159F1169CC}\DefaultIcon"; ValueType: string; ValueData: "{app}\crtcpl.exe,0"; Components: crtcpl
Root: "HKCR"; Subkey: "CLSID\{{EA6ACE18-0BCB-47FF-A163-91159F1169CC}\Shell\Open\Command"; ValueType: string; ValueData: "{app}\crtcpl.exe"; Components: crtcpl
Root: "HKLM"; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ControlPanel\NameSpace\{{EA6ACE18-0BCB-47FF-A163-91159F1169CC}"; ValueType: string; ValueData: "Apple iMac G3 CRT Control Panel Applet"; Flags: uninsdeletekey; Components: crtcpl
Root: "HKLM"; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "Right Click Assist"; ValueData: "{app}\rightclickassist.exe"; Flags: uninsdeletevalue; Components: rightclickassist

[Components]
Name: "crtcpl"; Description: "Apple iMac G3 CRT Control Panel Applet"; Types: full compact custom; Flags: fixed
Name: "ejectcd"; Description: "Auto Eject CD-ROM Drive"; Types: full
Name: "rightclickassist"; Description: "Right Click Assist"; Types: full

[ThirdParty]
UseRelativePaths=True

[Code]
function InitializeSetup: Boolean;
begin
  Result := IsDotNetInstalled(net4Client, 0);
  if not Result then
    SuppressibleMsgBox(FmtMessage(SetupMessage(msgWinVersionTooLowError), ['.NET Framework', '4 Client Profile']), mbCriticalError, MB_OK, IDOK);
end;
