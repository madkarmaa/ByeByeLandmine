@echo off

FOR %%I IN (.) DO SET CurrentD=%%~nI%%~xI

md "%APPDATA%\Thunderstore Mod Manager\DataFolder\LethalCompany\profiles\Default\BepInEx\plugins\%CurrentD%\" 2> nul

xcopy /Y ".\bin\Debug\netstandard2.1\*.dll" "..\thunderstore\"
xcopy /Y ".\AssetBundles" "..\thunderstore\assets\"

xcopy /Y "..\thunderstore" "%APPDATA%\Thunderstore Mod Manager\DataFolder\LethalCompany\profiles\Default\BepInEx\plugins\%CurrentD%\"