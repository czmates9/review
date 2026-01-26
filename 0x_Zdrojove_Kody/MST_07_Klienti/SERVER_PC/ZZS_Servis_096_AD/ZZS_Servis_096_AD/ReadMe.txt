****************************************************************************************************************************************
Popis zmen verzi

Synchroniator CZMST096 s AD domeny
================

[verze "1.2"] TaD (14.11.2018)
-pridane Logovani když se vykona synchronizace

[verze "1.1"] TaD (13.11.2018)
-První verze služby pro synchronizaci čísleniku pracovníkú

Konfiguračne parametry : 
ZZS_Servis_096_AD.Properties.Settings.ConnectionString - Connection string na DB 

DomenaAD  - string - Domena vuči ktere se AD pripojuje a dotahuje
Period - int - čas v jakem se služba spouští a kontroluje jaky je čas
Log - bool - čí se má logovat
TimeSyncMIN - int - čas pro minuty kdy se má vykonat synchronizace
TimeSyncHO - int -čas pro hodiny kdy se má vykonat synchronizace
Login - string - Login pro ověření vuči AD
PassWord - string - Heslo pro ověření vuči AD
