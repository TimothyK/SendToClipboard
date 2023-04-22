SendToClipboard
===============
This is a simply little program that can be used to send the full path of a filename from Windows Explorer to the clipboard.  A minimize shortcut to this program should be placed in your SendTo folder, which integrates it with Windows Explorer.  The installer (MSI) for this project will create this shortcut and name it "Filename To Clipboard".

To use this program, right click on a file in Windows Explorer.  Choose Send to-Filename to Clipboard from the popup menu.  The program will run quickly then exit with a Beep to indicate that the filename has been successfully copied to the clipboard.

You can then paste the full pathname into other programs.  The clipboard will contain plain text format so that you can paste into any program (e.g. Notepad).  The formatting also supports HTML so that you can paste into most email programs.

The HTML format will create 2 hyperlinks out of the filename.  The first hyperlink is the directory where the file is stored.  Clicking on this will open up Windows Explorer to that directory.  The second hyperlink is on the filename itself.  It will open the file in its default viewer.  

Mapped drives letters will be reported as full UNC paths.  Therefore the hyperlinks will work on computers that don't have the same drive letter mappings.

Download
========
See the Releases for the an MSI installer.  https://github.com/TimothyK/SendToClipboard/releases/tag/v1.0

Registry Hacks
==============
Add the tool to your Windows Explorer main context meny (instead of the Send To folder) with the following registry hacks.  These can be saved to a .reg file and imported by double clicking the file

```
Windows Registry Editor Version 5.00

[HKEY_CLASSES_ROOT\*\shell\SendToClipboard]
@="Filename To Clipboard"
"Icon"="C:\\Program Files (x86)\\SendToClipboard\\SendToClipboard.exe"

[HKEY_CLASSES_ROOT\*\shell\SendToClipboard\command]
@="C:\\Program Files (x86)\\SendToClipboard\\SendToClipboard.exe \"%L\""

[HKEY_CLASSES_ROOT\Folder\shell\SendToClipboard]
@="FIlename To Clipboard"
"Icon"="C:\\Program Files (x86)\\SendToClipboard\\SendToClipboard.exe"

[HKEY_CLASSES_ROOT\Folder\shell\SendToClipboard\command]
@="C:\\Program Files (x86)\\SendToClipboard\\SendToClipboard.exe \"%L\""
```

License
=======
This software is Public Domain.  Anyone can use the source code in any way they wish.  Users are expected to only use this software for good and not for evil.

Dependencies
============
- Microsoft .NET Framework 3.5 Client Profile
- Solution file is for Microsoft Visual Studio 2012.  Programming language is VB.
- Setup.msi created with WiX 3.7 (http://wixtoolset.org/)
