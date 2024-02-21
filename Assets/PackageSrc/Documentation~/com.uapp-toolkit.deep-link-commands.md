Test Sample on Windows
=========

1. Build Winndows build
1. Open regedit
1. Add string key "Computer\HKEY_CLASSES_ROOT\uapptoolkit\URL Protocol" with empty value
1. Add string key "Computer\HKEY_CLASSES_ROOT\uapptoolkit\shell\open\command\(Default)" with value "<build path>\UAppToolkit.DeepLinkCommands.exe "%1""
1. Run command **start "uapptoolkit://winbuild?action=invite2&parent-name=vovkasu&parent-id=123456"**