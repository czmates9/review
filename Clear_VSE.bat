

@ECHO OFF
SETLOCAL
CHCP 65001 >NUL

REM Skript se vzdy spousti z korene repozitare, kde je ulozen.
PUSHD "%~dp0" || (
    ECHO Nepodarilo se otevrit adresar repozitare.
    EXIT /B 1
)

ECHO =============================================================================
ECHO Smazou se generovane slozky bin, obj, .vs, !Build! a !!!Build!!!
ECHO a uzivatelske soubory *.user a *.suo pouze uvnitr:
ECHO %CD%
ECHO =============================================================================

SET "VOLBA="
SET /P "VOLBA=Opravdu chcete pokracovat [A/N]? "
IF /I "%VOLBA%"=="N" GOTO :Zruseno
IF /I NOT "%VOLBA%"=="A" (
    ECHO Neplatna volba. Zadejte A nebo N.
    POPD
    EXIT /B 2
)

PowerShell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0\00_remove_all.ps1"
IF ERRORLEVEL 1 GOTO :Chyba

PowerShell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0\04_remove_X.user.ps1"
IF ERRORLEVEL 1 GOTO :Chyba

PowerShell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0\05_remove_X.suo.ps1"
IF ERRORLEVEL 1 GOTO :Chyba

ECHO Cisteni bylo uspesne dokonceno.
POPD
EXIT /B 0

:Zruseno
ECHO Cisteni bylo zruseno.
POPD
EXIT /B 0

:Chyba
ECHO Pri cisteni doslo k chybe.
POPD
EXIT /B 1
