

@ECHO OFF
chcp 65001 >nul
ECHO =============================================================================
ECHO =============================================================================
ECHO Výtam vas v BAT souboru pro smazani všeho: BIN, OBJ, .vs, !Build! a !!!Build!!! složky, *.user, *.suo.
ECHO =============================================================================
ECHO =============================================================================
PAUSE

:choice
ECHO =============================================================================
set /P c=Opravdu si přejete poračovat[A/N]?
if /I "%c%" EQU "A" goto :POKYN_ANO
if /I "%c%" EQU "N" goto :POKYN_NE
goto :choice

PAUSE

:POKYN_ANO
ECHO =============================================================================
ECHO Zvolili jste volbu ano, přikazy budou vykonány...
ECHO =============================================================================
Powershell.exe -executionpolicy remotesigned -File  D:\_w\MES-Projekt\00_remove_all.ps1
Powershell.exe -executionpolicy remotesigned -File  D:\_w\MES-Projekt\04_remove_X.user.ps1
Powershell.exe -executionpolicy remotesigned -File  D:\_w\MES-Projekt\05_remove_X.suo.ps1
ECHO =============================================================================
ECHO Píkazy byly uspěšně vykonány
ECHO =============================================================================
PAUSE
EXIT

:POKYN_NE
ECHO =============================================================================
ECHO Zvolili jste volbu ne. Příkaz bude ukončen
ECHO =============================================================================
PAUSE
EXIT