# ttnh
Test Task Name Helper

How to use:
> NameHelper.exe "tRamp mcdonalD" "thOMas dEMark" "oTTo VoN Bismarck"

Format names like:
* tRamp mcdonalD -> Tramp McDonald
* oTTo VoN Bismarck -> Otto von Bismarck

Could be extended with plugin dll:
* implement **IFormatRule** to handle prefix, suffix or some thing else
* implement **IConfigInitializer** to load data from config file
* add your rule to *RuleConfig.xml*
* place dll with *NameHelper.exe*
