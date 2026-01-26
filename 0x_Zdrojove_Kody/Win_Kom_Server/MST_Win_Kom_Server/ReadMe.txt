[verze "7.127"] MaR (23.1.2026)
-zapis DIH vzdy na zaklade dohledani CountEntries z DI, oprava metod dohledani nazvu stroju pro konzoli
- zvyseni verze

[verze "7.126"] MaR (15.1.2026)
-mnozstvi na sklade hodnota float je null osetreni pro nenalezene zbozi, vraci se jen http kod 204 NoContent a v tele zadna hodnota
- zvyseni verze


[verze "7.125"] MaR (12.1.2026)
- varianta PostgresSQL v provider SQL FEFO/FIFO
<NewDataSet>
  <dbtypes>
    <dbtype>ms-sql</dbtype>
    <dbconnectionstring>Data Source=ABRASERVER2\SQLEXPRESS;Initial Catalog=FASKABRA_SAB_TEST_MESAK;User ID=xxx;Password=xxx</dbconnectionstring>
    <use>true</use>
  </dbtypes>

  <dbtypes>
    <dbtype>firebird</dbtype>
    <dbconnectionstring>User=xxx;Password=xxx;Database=d:\Abra\abradata\TESTSAB.FDB;DataSource=192.168.10.32;Port=3050;Connection lifetime=15;Pooling=false;Packet Size=8192;ServerType=0;</dbconnectionstring>
    <use>false</use>
  </dbtypes>

  <dbtypes>
    <dbtype>PostgreSQL</dbtype>
    <dbconnectionstring>Host=192.168.10.40;Port=5432;Database=faskabra;Username=xxx;Password=xxx;</dbconnectionstring>
    <use>false</use>
  </dbtypes>
</NewDataSet>

-doplneni Test.asmx pro komunikaci DB
- zvyseni verze

[verze "7.124"] MaR (12.1.2026)
- zvyseni verzi balicku a sjednoceni , firebird a dalsi
- zvyseni verze

[verze "7.123"] MaR (7.1.2026)
- prodej -posilani cislo zakazky v hlavicce, uprava - pozor na typ id a ids moznost spatne konzistence v relacich
- zvyseni verze

[verze "7.122"] MaR (6.1.2026)
- SQL provider FEFOFIFO_Online, rozsireni na Firebird, rozsirena konfigurace o parametry
<dbtypes>
    <dbtype>ms-sql</dbtype>
	<dbconnectionstring>Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=xxx;User ID=xxx;Password=xxx</dbconnectionstring>
	<use>true</use>

    <dbtype>firebird</dbtype>
	<dbconnectionstring>     @"User=xxx;Password=xxx;Database=C:\FASK\ABRA_DB\DATAFB3.FDB;DataSource=localhost;Charset=WIN1250;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;"
                 </dbconnectionstring>
				 <use>false</use>
  </dbtypes>
  
- zvyseni verze

[verze "7.121"] MaR (19.12.2025)
- oprava vratka nova metoda na posilani dat do IS pohoda
- pridani logovani pro vratku
- zvyseni verze

[verze "7.120"] MaR (19.12.2025)
- volny pohyb prodej CZMST_DIH rozsireni prvku pro ulozeni do DB
- zvyseni verze

[verze "7.119"] MaR (15.12.2025)
- vratka nova funkcionalita RadyFunkce = 13 vydvr posle zapornou hodnotu quantity
- zvyseni verze

[verze "7.118"] MaR (15.12.2025)
- provider Pohoda posilani ids:number Rada_Prefix v xml pozadavku
- zvyseni verze

[verze "7.117"] MaR (12.12.2025)
- provider SAB objek CZMST_SI pro JSON IS ABRA, predelani posilaci metody
- zvyseni verze

[verze "7.116"] MaR (10.12.2025)
- provider SAB natvrdo zapis prelokovani do SOPNUMBE
- zvyseni verze

[verze "7.115"] MaR (4.12.2025)
- VydejController/ProcessVydejkaDBFile2/ProcessVydejka/Vydej_Process/ --provider SAB -- pridana vetev prelokovani pro zapis dat do IS ABRA, metoda na odeslani dat do IS ABRA lokace

- zvyseni verze

[verze "7.114"] MaR (10.11.2025)
- Volny pohyb pridan do provider Pohoda cislo zakazky na odesilani do IS pohoda, rozsireni o prvek v dataSetu a prenos z noveho SELECT SQL
- doplneni o stredisko
- pridani text MES- a diakritika do posilaneho XML souboru IS pohody

- zvyseni verze

[verze "7.113"] MaR (7.11.2025)
- Volny pohyb pridan do provider Pohoda cislo zakazky na odesilani do IS pohoda

- zvyseni verze

[verze "7.112"] MaR (29.10.2025)
- prelokovani provider SQL  --Vydej,Prijem,Inventura,Vyroba a Expedice procedura vraci parametr
- doplneny parametry pro procedury Vyroba,Expedice,Prijem v MST_SQL_Config.xml :
  <Expedice>
     <GenerateData_Action>FASK_proc_EXPORT_SQL_FASK_Expedice</GenerateData_Action>
  </Expedice>
  <Vyroba>
     <GenerateData_Action>FASK_proc_EXPORT_SQL_FASK_Vyroba</GenerateData_Action>
  </Vyroba>
    <Prijem>
 <GenerateData_Action>FASK_proc_EXPORT_SQL_FASK_Prijem</GenerateData_Action>
  </Prijem>

- zvyseni verze

[verze "7.111"] MaR (23.10.2025)
- prelokovani provider SQL  Ostatni, procedura vraci parametr

- zvyseni verze

[verze "7.110"] MaR (22.10.2025)
- novy EP(api/Ostatni/GenerateDavkaRequest_Ostatni), EP(api/Ostatni/GenerateDavkaStatus_Ostatni), EP(api/Ostatni/GenerateDavkaStatusDelete_Ostatni)
- BL logiky a metody pro transakce "prelokovani", provider SAB a SQL

- zvyseni verze

[verze "7.109"] MaR (17.10.2025)
- doplneni parametrù do konfiguracniho souboru pro stazeni do terminalu
- zvyseni verze

[verze "7.108"] MaR (15.10.2025)
- novy EP(api/Ciselniky/KatalogTypDokladuExport)
- doplneni procedur pro export dat ciselniku v SQL provideru
- doplneny parametry pro FASK_proc_EXPORT_SQL_FASK_ZASOBY v MST_SQL_Config.xml :

<Sdilene>
<StatusObjectsDirectory>Logs\SoDir\</StatusObjectsDirectory>
<LokaceVychoziParametrNazevPohoda>fask_vychozi_lokace</LokaceVychoziParametrNazevPohoda>
<EvidenceVyrobnichCisel>false</EvidenceVyrobnichCisel>
<EvidenceSarzi>true</EvidenceSarzi>
</Sdilene>
<ExportZasoby>
<ExportSkladFilter>01</ExportSkladFilter>
<ExportTypFilter>1,5</ExportTypFilter>
<ExportovatPouzeAktivniPolozky>true</ExportovatPouzeAktivniPolozky>
<EXZas_DotahovatAlternativniDodavatele>false</EXZas_DotahovatAlternativniDodavatele>
</ExportZasoby>

- zvyseni verze

[verze "7.107"] MaR (10.10.2025)
- lokace nahravani dat do CZMST094 v procedure provider SQL
- zvyseni verze

[verze "7.106"] MaR (3.10.2025)
- nahravani dat lokaci do CZMSTE_SE v provideru SAB pokud je vstupni parametr JSON SOPNUMBE=="prelokovani", doplneni logiky a prirazeni parametru na zaklade selectu z SAB
- zvyseni verze

[verze "7.105"] MaR (25.9.2025)
- EP api/Vydej/GenerateDavkaRequest_Vydej nahravani dat lokaci do CZMSTE_SE v provideru SAB pokud je vstupni parametr JSON SOPNUMBE=="prelokovani"
- zvyseni verze

[verze "7.104"] MaR (24.9.2025)
- lokace provider SAB reimplementace na skcript ciselniku a implementace metody pro plneni PRD souboru
- zvyseni verze

[verze "7.103"] MaR (22.9.2025)
- lokace provider SAB cisty SELECT nad ciselnikem pozic(LogStorePositions) z ABRA IS a transformace dat pro ulozeni do tabulky CZMST094
- zvyseni verze

[verze "7.102"] MaR (17.9.2025)
- lokace provider SAB doplneni SELECT a transformace dat pro ulozeni do tabulky CZMST094
- zvyseni verze

[verze "7.101"] MaR (15.9.2025)
- uprava dat CZMST_SE prvek CZ_DOSLO se nenastavuje pokud je ITEMTYPE=="I" a zaroven je pravda prvek v konfiguraci Konfigurace.Inventura1[0].DavkaTerminalVice
- zvyseni verze

[verze "7.100"] MaR (11.9.2025)
- predpriprava metod pro api/Vydej/GenerateDataRequest_CZMST094_Vydej provider SAB
- implementace dodane query od SAB pro Lokace a zapis dat do FASK tabulky CZMST094
- zvyseni verze

[verze "7.99"] MaR (25.8.2025)
- Ciselniky Zbozi a typ dokladu vyhledavani podle ID skladu napevno retezec, odstranen znak %
- zvyseni verze

[verze "7.98"] MaR (21.8.2025)
- FEFOFIFO rozsireni o prazdny zaznam se state 1 a message zaznam nenalezen
- zvyseni verze

[verze "7.97"] MaR (20.8.2025)
- SQL provider prenaseni logiky FEFOFIFO z providera SAB
- PohodaXML provider prenaseni logiky FEFOFIFO z providera SAB
- zvyseni verze

[verze "7.96"] MaR (18.8.2025)
- SAB provider tabulka CZMST_SkladLokace_Stav logika 4 pro zjisteni sarzi
- zvyseni verze

[verze "7.95"] MaR (18.8.2025)
- rozsireni struktury virtualni tabulky CZMST_SkladLokace_Stav o parametry State a Message, popis v #issue139
- zvyseni verze

[verze "7.94"] MaR (14.8.2025)
-------------------
- metody FEFOFIFO JSON pristup ASYMBO
-rozsireni konfiguracnich parametru:
  MST_ABRA_SAB_Config.xml
    <Informations>
    <FEFOFIFO_I_L_CommandType>StoredProcedure</FEFOFIFO_I_L_CommandType>
    <FEFOFIFO_proc>FASK_proc_FEFOFIFO</FEFOFIFO_proc>
    <FEFOFIFO_pouzita_metoda>1</FEFOFIFO_pouzita_metoda>
    <FEFOFIFO_M3>ms-sql</FEFOFIFO_M3>
  </Informations>

  MST_Pohoda_Config.xml
   <Informations>
    <FEFOFIFO_I_L_CommandType>StoredProcedure</FEFOFIFO_I_L_CommandType>
    <FEFOFIFO_proc>FASK_proc_FEFOFIFO</FEFOFIFO_proc>
    <FEFOFIFO_pouzita_metoda>1</FEFOFIFO_pouzita_metoda>
  </Informations>

  MST_SQL_Config.xml
   <Informations>
    <FEFOFIFO_I_L_CommandType>StoredProcedure</FEFOFIFO_I_L_CommandType>
    <FEFOFIFO_proc>FASK_proc_FEFOFIFO</FEFOFIFO_proc>
    <FEFOFIFO_pouzita_metoda>1</FEFOFIFO_pouzita_metoda>
  </Informations>

- zvyseni verze

[verze "7.93"] MaR (20.5.2025)
-------------------
- EP informations, dotazeni informaci o polozce, tvorba providera a volaci metody EP
- zvyseni verze

[verze "7.92"] MaR (9.1.2025)
-------------------
- oprava select stazeni ciselniku pracovnici
- zvyseni verze

[verze "7.91"] MaR (7.1.2025)
-------------------
- oprava select FASK_ZASOBY prepare zip DB soubor
- zvyseni verze


[verze "7.90"] MaR (7.11.2024)
-------------------
- stahovani davky info1
- zvyseni verze

[verze "7.89"] MaR (26.9.2024)
-------------------
- stahovani davky, modifikace pro zrychleni stahovani
- zvyseni verze


[verze "7.88"] MaR (11.9.2024)
-------------------
- 3vrstvy tisk z konzole
- AGRO, paletovy stitek, tisk z xamarinu, dohledani hodnot a pridani do promennych pro zpl sablonu
- zvyseni verze

[verze "7.87"] MaR (16.4.2024)
-------------------
- KAPALKY, archivace, vaha, TISK
- zvyseni verze

[verze "7.86"] MaR (23.2.2024)
-------------------
- oprava vydej/vydejky podle itemtype
- zvyseni verze

[verze "7.85"] MaR (2.2.2024)
-------------------

- JSON II.
sklady --
lokace --
meny --
- SAB fill_ZAsoby odstranen order
- zvyseni verze


[verze "7.84"] MaR (19.1.2024)
-------------------

- SAB fill_ZAsoby doplneno o order
- zvyseni verze


[verze "7.83"] MaR (9.1.2024)
-------------------

- zvyseni verze
- JSON ciselniky I. odberatele, typDokladu, pracovnici

[verze "7.82"] MaR (30.11.2023)
-------------------

- zvyseni verze
- oprava v datasetech VYROBA tabulka production_sources prvek dex_row_id MUZE BYT null !!

[verze "7.81"] MaR (29.11.2023)
-------------------

- zvyseni verze
- web API Fask_Events_Insert pres JSON, novy objekt Fask_Events_Rows + metoda pro zápis a validaci dat

[verze "7.80"] MaR (24.11.2023)
-------------------

- zvyseni verze
- ziskani dat k Vydeji, komunikace JSON, BL_Vydej a VydejController

[verze "7.79"] MaR (24.10.2023)
-------------------

- zvyseni verze
- 3vrstvost FASK_FORMULARE, update, delete, insert

[verze "7.78"] MaR (23.08.2023)
-------------------

- zvyseni verze
- archivace konzola IT


[verze "7.77"] MaR (4.08.2023)
-------------------

- zvyseni verze
- 3vrstva komunikace Konzola, archivace production

[verze "7.76"] MaR (28.07.2023)
-------------------

- zvyseni verze
- 3vrstva komunikace Konzola, volny pohyb, nasnimane

[verze "7.75"] MaR (10.07.2023)
-------------------

- zvyseni verze
- 3vrstva komunikace FASK_FORMULARE


[verze "7.74"] MaR (20.06.2023)
-------------------

- zvyseni verze
- prehled odvadeni stavy stroju, uprava filtru cislo sluzby zadavani vice parametru
- zmena namespace pro datatable prace s ciselniky skladu, zacatek 3vrstve komunikace, nedokonceno! implementovat providery!

[verze "7.73"] TaD (25.04.2023)
-------------------

- vytvorena verze E

*********************************************************************************************************


[verze "7.72"] TaD + MaR (25.04.2023)
-------------------
MaR:
- Vyroba, pridan update tabulky production vaha podle GUID

TaD:
- Uprava v SQL provideru update tabulkz CZMST_DI
-odstraneni prodej datasetu a table adapteru


[verze "7.71"] MaR (23.03.2023)
-------------------
-  rozsiren datatable Vyroba.MachineStateSet o prvek ID_group,
prace s ukladanim stavu z ADAM sluzby, rozsireni datatable + uprava logiky zobrazovanych dat stavu z ADAM
- KONZOLE provider API zobrazeni stavu ADAM, rozsireni BO o prvek ID-group

[verze "7.70"] MaR+TaD (16.03.2023)
-------------------
* SERVER - Vyroba_FaskEvents_Controller - api/Vyroba_data_faskevents uprava archivace presun na tisk status 240

[verze "7.69"] MaR (09.03.2023)
-------------------
-  rozsiren datatable Vyroba.MachineStateSet o prvek description, uprava providerSQL rozsireni o dotaz
	description z tabulky MachinesDefinition a dotazeni dat, take uprava setrizeni podle DateModified od nejmladsiho

[verze "7.69"] TaD (09.03.2023)
-------------
- Rozšíøení struktur tabulek
  - CZPRO_VPP
  - CZPRO_VPH
  - Production

- Vytvoøen generator "Seriovych èísel" - zadava se do vyrobních pøíkazù do BarcodeP aby podle toho šlo identifikovat
  - API komunikace
- Odstraneni v èasti vyroba co nejvíc datatable pøi rozøírovani struktur a upravach dotazu

[verze "7.68"] MaR (06.03.2023)
-------------
- vytvoren interface IOdvod_MachineStateSet_GetFiltrovanyMachineStateSets pro praci s daty nad tabulkou MachineStateSetHistory
- zalozen novy BO pro tabulku MachineStateSetHistory reseni pro rest api z konzole

[verze "7.67"] MaR (23.02.2023)
-------------
-ProviderVyroba metoda Get_FE_ByMachineId_Rows, uprava machineId na bocedi 1 a 2


[verze "7.66"] TaD+MaR (20.02.2023)
-------------
-SQL provider + POHODA provider, prodej pridan inser to tabulky CZMST_DIH

-pro sluzbu SSS do API komunikace, rozsirene struktury

- pro API kominikaci s konzolou rozsirene struktury pro UPDATE tabulky Production

[verze "7.65"] TaD (26.01.2023)
-------------
- Pro sledovani vyroby, udelano v REST API metoda "api/agrocs" v POST 
-složí pro testy volaní tisku v AGRO CS
- pøi doimplementovaní metody je možno v budoucnu provest i tisk cez FASK

-Oprava v konstatntach, nazev tabulky : FASK_MachineType

-modul IS POHODA
  - Pridany novy uživatelky parametr do IS POHODA: VPrCZSNumTrIGN
  - ked je true tak se do CZ_SerNumTrack zapiše 11
  - jedná se o benevolenci v zadavani SN a šarže
  - Prijem a Vydej

- Zmeny nazvu nnamespace v datasetoch
- opravene pokud se jedno o E1 pohodu tak su volitelne parametry v podminke, aby SQL nehodilo chybu



[verze "7.64"] TaD (30.11.2022)
-------------
- Oprava po MRathouzskem
  - v logovani byl parametr který vypinal logovaní
  - nevím proè je pomenovany jak print
  - v serveru + konzole pridano natvrdo true aby se logovalo


[verze "7.63"] TaD (30.11.2022)
-------------
Oprava Inventura, pro pohoda ktera neni E1 aby to fungovalo

[verze "7.62"] TaD (23.11.2022)
-------------
- Vytvorene metody v kontroleru pro dotaz z konzole na filtrovane VPH a VPP
-udelane poøadky trochu v BO zdilenych

- Vytvoøen API rozhrani pro tisk etiket z konzole


[verze "7.61"] TaD (09.11.2022)
-------------
Uprava API komunikaci
-ruzne upravy drobnosti
- Android nove konfig parametry


[verze "7.60"] TaD (03.10.2022)
-------------
-Opraveni na výdeji, korektná odpoved ve statusObjektu
- na terminalu se nebude zobrazovat random hláška "OK"



[verze "7.59"] TaD (19.09.2022)
-------------
- Pøedelani Interface na zdileni medzi serverem a konzolu



[verze "7.58"] TaD (15.09.2022)
-------------
-I-Tec uprava provideru
 - vytvoøena logika tisku
 - vypoèet celkove vahy
 - vypoèet objemu jedntlivych baliku + komplet cele

- Uprava namespace co doje@#$ MaR pro ITerminal

[verze "7.57"] TaD (13.07.2022)
-------------
SAB, uprava generovani pøedlohy inventury
- upøidany do selectu join podminky aby se z HIP dotahoval použity sklad. Lebo HIP muže byt jen na jeden sklad


[verze "7.56"] TaD (1.07.2022)
-------------
-Uprava 3 vrsta komunikace s konzolou

-Android
* konfigurace
* uzivatele s pravama



[verze "7.55"] TaD (21.06.2022)
-------------
Sestaveni verze pro ANDROID, až po TAG v GIT :(
- na miste v agro provoznovani


[verze "7.54"] MaR (13.06.2022)
-------------
- oprava v interfaces ve VyrobaDatSet tabulka VPP, atribut qtydokon povolen zápis null


[verze "7.53"] MaR (10.06.2022)
-------------
- Logins
* loginController uzivatele _API
* ProviderLogin _API
* API_Commans
- Vyroba v SQL modulu !!!
* VyrobaController
* Filtry
* BO Zbozi
* BO Sklady
* BO Groups
* BO Machines
* BO VMachinesOperations
* BO VPH
* BO VPP
* BO Production
* BO Operations
* BO CZMST093
* BO CZMST094
* BO Corrects

+ sestaveni pro AGRO


[verze "7.52"] TaD (13.05.2022) (V Master verze)
-------------
-Konfigutace pro ANDROID

+ Sestaveni pro SAB

[verze "7.52"] MaR (29.03.2022) (v Branch verze)
-------------
-rozsireni struktur pro trivrstvou komunikaci API
-metoda pro odpis status 61/62


[verze "7.51"] TaD (21.03.2022) (V Master verze)
-------------
-Hanibal:
-tisk soupisu rozšíøen o nove parametry v datech
-Provider hanibal tisk rozšíøen o soupis dotahovaní informací
-Uprava tiskove šablony

SAB
* uprava kodu poøadí volani requestù



[verze "7.51"] MaR (04.03.2022) (v Branch verze)
-------------
-rozsireni struktur production


[verze "7.50"] TaD (17.02.2022)
-------------
-pohoda provider
-uprava podminky, když je na OBJ definovan doklad ktery ma vzniknout a je null tak se logika pøeskoèí


[verze "7.49"] TaD (26.11.2021)
-------------
-Zprava hlavne POHODA provider viz reade pohoda

-Prijem PI rozšiøen o AttributeToSN
-prodej DI rozšiøen o AttributeToSN


[verze "7.48"] TaD (03.11.2021)
-------------
-Pohoda provider, vydej s predlohou, grupovani pred importem do IS POHODA
-vytvoøena funkce která podle pravidel volá budto grupovani s SN/šarže a expirace anebo bez

-uprava NuGet Package v serveru


[verze "7.47"] TaD (21.09.2021)
-------------
-Fichema, Vydej generovani predlohy, chyba pri generovani predlohy, špatna transakce a špatny SQL dotaz na predlohu
-Vydej, pridana nova tabulka CZMST_SI_BV
-slouží pro ukladaní vlastností baliku, a použiva se pro tisk do soupisky

-Tisk soupisu rozšíøen o tisk adresy odberatele do hlavièky

-I-Tec provider rozšíøen o implementaci upravy soupisu


[verze "7.46"] TaD (28.07.2021)
-------------
-POHODA, provider
* Vydej, ostranene table adaptery, nahrazene primima dotazama do DB


[verze "7.45"] TaD (20.07.2021)
-------------
-Uprava logovani, vznikl novy logovaci soubor, do ktereho se loguje když neni implementovana metoda v providerovy
-Misto toho aby to šlo do error a matlo to tam

uprava POHODA providera, 
-zjednocena komunikace s IS POHODA pomoci xml pouze do jednoho objektu ktery zabezpeèuje komunikaci
-pridane ignorovani exit kodu, ale loguje sa

-SAB
-uprava logiky petneho zapisu Prijem
-prace ze šaržema, jejich tvorba pokud nejou a zapis jen pomoci ID


[verze "7.44"] TaD (28.06.2021)
-------------
-POHODA:
** Prijem, generovani predlohy, když je E1 tak je novy uživatelsky parametr v IS pohoda, který urèuje zda sledovat expiraci
* Spetny zapis, pokud jsou naše parametry pro sledovani šarže/sn tak se zapisuje do poznamky
* pokud se vede pohoda na šarže tak je zapis primo do pohody
* pridany i zapis expirace do pohody

*v sql komunikaci, odstraneni jeden tableadapter, a dotaz prepsan ruènì... je to lepší

-Provazovani Objednavky a prijemky na urovni pohody, uprava aby ro øešilo i evidenèní èísla

-v PrijemTest implemenovano testovací volaní importu primo do IS

**Prodej:
* Spetny zapis, pokud jsou naše parametry pro sledovani šarže/sn tak se zapisuje do poznamky
* pokud se vede pohoda na šarže tak je zapis primo do pohody



-SAB:
-Vydej, 
*upraveno dotazovaní do skladove menu, kde maju napsanych dodavatelù
*dotahuje se to do VNDDOCNM
* !!!!! ma pouze 21 znaku !!!!

-prevod vydej
*Vymysleny a implementovani, generovani predlohy
*pridane parametry do IS abra, u nas implementovane dotahoni parametru, èíslo èady PRP a cilovy sklad
*Prace s procesnim øizenim

-Prijem
* vypnuto generovani dokladu ke kontrole


MES vsude
-Prodej
* rozšíøení CZMST092 o nové parametry, ktere potlaèují zadavani šarže, SN a expirace i když ne položka podle IS na to sledovana





[verze "7.43"] TaD (14.06.2021)
-------------
-Pohoda provider, softcotton
-generovaní predlohy z faktury
-v SE se rozšiøovaly struktury ale tahle metoda se nevyuživala
-rozšíøeno o tyhle prvky

-uprava sql zakladacich scriptu

!!!!!
-API, pidane prihovny pro API nad android
-provedeny MERGE nad branche android projektu

-GS1 provider, tisk SAB, tisk LOT a SN lokalizovano

-Login.aspx uprava prepinani do Default.aspx
* Response.Redirect("Default.aspx"); 
-ostraneno false


[verze "7.42"] TaD (25.05.2021)
-------------
TierraVerde,
Lokaèní mechanizmus, dotahovaní nazvu do LokMechanizmu
-v kodu chybela medzera



[verze "7.41"] TaD (25.05.2021)
-------------
-Hanibal,
když je VNDITNUM  string.empty tak nefunguje tisk
-opraveno pridana podminka, že když je empty tak ignoruje

-Dale pri odeslani davky všude ošetøeno na to že vnditnum muže byt null


[verze "7.40"] TaD (20.05.2021)
-------------
-SAB,
provider, generovani predlohy, chybelo dotahovani QTYPACK do CZMST_I3

-Spetny zapis, poèitalo se s tym že se budou posilat data do IS ABRA, a abra si vše zpracovava sama, takhle to bylo otestovane a odsouhlasene

-ale po testech, bylo potreba udelat Grupovani když je jedna šarže zadana pod ruznyma MJ
-vše se prepoèitava do hlavnej MJ a uprava requestu, kde se posila množství + MJ

-ITEC

-Vydej, pohoda
- Pridana funènost, že po odeslaní dávky, se online dohledavani k odeslaním kartam služby
-a tyto služby se vkladaji do requestu hned pod danu položku

-pridane 2 nove parametry, ktere jsou z duvodu kontroly vyøizenosti,
-protože se fejkuje tvorba predlohy a tym se kontroluje zda je polžka cela vydana anebo ne
-je potøeba nastavit totožne jak na konzole


[verze "7.39"] TaD (06.05.2021)
-------------
-Pohoda provider, pridane do Prodej, Prijem, Vydej dotahovani ITEMDESC do lok.Mech
-Ošetøene ruzne chyby Hanibal

-SAB, pridana kontrola online na expiraci, zda je projita anebo ne

-Oprava web rozhrani, ukladani konfigurace...




[verze "7.38"] TaD (16.04.2021)
-------------
-SAB, webove rozhraní Inventury
-oprava pøepinani pøehledu na rozhraní

-FIFO/FEFO online dotaz, dle IS ABRA, vynechana podminka pro typ dokladu

-ošetrena možnost nahraz spet inventuru, i když obsahuje nasnimane položky ktere pak z predlohy v ABRE smazala

-Export zasob z IS ABRA, roušíøeno o dotahovani pøiznaku expirace
*puvodne to už bylo zimplementovani ale dle požadavku to bylo zakomentovano, ted jsem to pouze odkomentoval


[verze "7.37"] TaD (13.04.2021)
-------------
SAB, webowe rozhraní Inventury
-Uprava øeštiny, slovièek a rozložení

-Oprava pøi exportu jednej inventury, byl problem s pamatovaním stavu vybraneho DIP
-nastalo to když JiS opravil chybu paradani rozhraní
-Opraveno pomoci Session pamatovani parametru




[verze "7.36"] TaD (09.04.2021)
-------------
-POHODA, generovaní èíselniku zasob pomocí Procedury na SQL serveru, o hoooodne to urychly praci
-plus je možnost volat proceduru i externí službou tøeba...

-SAB, ABRA, Inventura webove rozhraní, pridana možnost exportovat pouze jednu inventuru
-pridany workflow cele prace
-ošetreno hodne stavu

-Tracing pøi spetnem nahravani inventury


[verze "7.35"] TaD (22.03.2021)
-------------
-Disponibilita, oprava kdzy se na Prodeji dela disponibilita, tak neobsahuje SOPNUMBE, tak tam byla chyba pri vraceni hodnot na tesminal

-SAB, FIFO/FEFO online dotaz do DB byl špatnš zadany.



[verze "7.34"] TaD (03.03.2021)
-------------
-SAB
 * Prijem, Vydej, Inventura
 * Alternativní MJ -> alternativní EANs

 - Webove rozhraní, možnost znovu otevøít již uzavøenou inventuru



[verze "7.33"] TaD (25.02.2021)
-------------
-Verze Dašenka
- Oprava Lokaèní mechanizmus,  Pohoda provider, pøi pøeskladnení, udelana logika na mapovani ID, aby ID položky odpovidalo ID Skladu na kterem leží

-TO požadavek. Vytvoøena logika že na POHODA provideru lze z volneho pohybu zbyrat požadavky, ze kterých se pomoci funkce vytvoøí
na vydeji pøedloha.
-Z teho pøedlohy pak vzniká pøevod v lok mech a aj v IS POHODA

- ve FASK_Rady se definuje pro Vydej (Modul VyP) Nazev_Rady, co je DOC_ID z typu dokladu + filtr na SKL_ID

-Konfiguraèný parametr GenerovatPrevodku_TO_Pozadavek na Výdeji, ktere povolí generovani prevodky po odeslani TO požadavku. Defaultne na false




[verze "7.32"] TaD (09.02.2021)
-------------
-Uzavøeni verze Cipisek


[verze "7.31"] TaD (02.02.2021)
-------------
-Uprava serveru aby podporoval 32 i 64 bit system

-SAB, uprava logiky zpracovani dat na inventure


[verze "7.30"] TaD (09.12.2020)
-------------
-Uprava struktury typu dokladu, disponibilita
-Odmazane historice prvky a pridany jeden novy, který urèuje že sa kontroluje disponibilita z zdrovoveho skladu
-odmazane po dohode JiS a JaS že nazev procedury by mnel mnet až server nekde z dispozici, na terminaou to nemá co dìlat...

-Upravena webmetoda která volá disponibilitu... predavany je objekt, pro jednoduchší rozšíøení do budoucn...

-SQL provider, volani exe souboru, rozdelene na 2 parametry, lebo SB root SB muže byt instalovan inde
než je EXE file
-Implementovane do SQL provideru volani EXE souboru pro test komunikace s SB, je to prasarna... SB my mnelo mit svuj vlastný provider

Inventura, uprava update do DB na strane konzoly + serveru to iste... zbavení sa TableAdapteru, + leg
enda na serveru v pøehledu"

-Kompletne ostranenv POHODA provideru tableadaptery pro komunikace s POHODA

[verze "7.29"] TaD (19.11.2020)
-------------
Vyroba rozšíøena o Production_SN a jedho ukladani do sql



[verze "7.28"] TaD (09.11.2020)
-------------

-LokMech
	-SQL provider
	-POHODA provider
		Pridana možnost konfiguraène vydavat do zaporu

-I-Tec provider a Hanibal Provider
	-pøedelane konfigurace, odtranene z BIN

!!!v BIN už neni žádná konfigurace!!!

[verze "7.27"] TaD (03.11.2020)
-------------
-Sjednocena verze s Terminalem


[verze "7.27"] TaD (08.10.2020)
-------------
-Uprava implementace èisleniku v SQL provideru
-nejake drobnosti



[verze "7.26"] TaD (08.10.2020)
-------------
Pøi tisku do QR kodu, nebylo možno tiskout pomlèku, upraveny regularny vyraz

[verze "7.25"] TaD (25.09.2020)
-------------
-Rozsireni o Expiraci pro zasoby a prodej
-pohoda, logovani cesty konfigurace



[verze "7.24"] TaD (11.09.2020)
-------------
-Novy tiskovy provider, do tisku etikety, NARYCHLO SPRAVENE, TODO, objektovo, vic oøetøení atd...
-Provider pro tzisk GS1 kodu, skladani s existujicich dat..

-Vyroba, webmetoda pro generovani šarže

-Rozšíøení production o SERLTNUM, v DB už bylo..

!!! Production.SQL rozšíøeno o SERLTNUM !!!


[verze "7.23"] TaD (28.07.2020)
-------------
-Pohoda provider, prázdna implementace Pracovníku

-Upravy Vyroba, ladení
-Ladení pøidavani Expirace


[verze "7.23"] TaD (28.07.2020)
-------------
-Upravy na SAB provideru + Vyroba revize


[verze "7.22"] TaD (17.07.2020)
-------------
-Revize SQL struktur
-Roušíøeni o Expiraci a o pøiznak jejiho sledovani
-Implementovani Expirace do SAB provideru



[verze "7.21"] TaD (23.06.2020)
-------------
-Uprava SQL struktur

[verze "7.20"] TaD (09.06.2020)
-------------
-provider SAB, udelana Inventura...

[verze "7.19"] TaD (03.06.2020)
-------------
-Upravene ukladani cesty pro Trace
-SAB, pøi generovani pøedlohy, chybìla v SQL dotazu medzera

[verze "7.18"] TaD (03.06.2020)
-------------
-SAB, otestovany pøijem

[verze "7.17"] TaD (02.06.2020)
-------------
-SAB provider, uprava importovani FV z DL

[verze "7.16"] TaD (28.05.2020)
-------------
-Upravy na strane SAB provideru
-Testovani Vydeje, Prijmu...
-Zapoèate skoušeni tvorby Inventury..

[verze "7.15"] TaD (20.05.2020)
-------------
-Uprava regex pri grafickem tisku 2D kodu

-Vytvoøen Provider pro ABRA pro komunikaci poomoci  WEBAPI
-Vyrvoøen projekt FASK.RESHSHARP pro kounikaci pomoci REST API

SAB abra:
	-Implementovany Vydej




[verze "7.14"] TaD (03.03.2020)
-------------
-Pøidana nová grafika pro pøihlasovací okno, Login.aspx

- Odstraneni warningu,
	*pøedelani testoveho skladani xml requestu na objektove skladani (víc jak 1000 øadku kodu smazano)

Kompletne odstraneni .sdf souboru a nahraneho SQLite	

PREVEDENI SERVERU DO VS 2019	
-nutno na IIS nastavit ve fondu aplikaci :
	* Verze .NET CLR na v4.0
	* Povolit 32bitove aplikace  TRUE

Nutno si nainstalovat vo VS2019 extension SQLite/SQL Server Compact Toolbox

-Prepsani všech datasetu(uprava xsd na System.Data.SQLite.EF6), uprava ConnectionStringu ve webconfig

-Pøeneseny ABRA provider
-implementovany 
	-Vydej s pøedlohou
	-Prijem s pøedlohou
	-Inventura s pøedlouhou, generovana cez webove rozhrani
	-export èiselniku skladu

-Inventura, pøidano do ABRA provideru podbarveni stloupce žlutou, když je množtvi vetši jak v predloze

[verze "7.13"] TaD (xx.xx.2020)
-------------
-sestaveni pro carp narychlo...

[verze "7.12"] TaD (26.11.2019)
-------------
- Úpravy Data setù v SQL provider a PohodaXML provider, všude se nachází pouze jeden ConnectionString (CS), a ten smìøuje do settings, a v settings NESMI  byt vyplnìný
-PohodaXML ma konfiguraci ve vlastnim souboru MST_Pohoda_Config.xml
-Provider  SQL na v dll.config souboru konfiguraci CS

- Pøejmenovaní projektu Fask.ModulePohodaXML  na Fask.ModulePohodaXML 

-Uprava algoritmu kontroly disponibility
-Exedice, tisk baliku, Trim nad hodnotama do template

-Expedice, pøeneseni upravy BuxFix z Amalky do Bobeše

-FASK_RADY rozšiøeni o parametry tisku na volnem pohybu, nastaveni tiskarny a ID šablon

-na TiskTest pøidana metoda pro vytisknuti konkretnej tiskovej ulohy




[verze "7.11"] TaD (23.10.2019)
-------------
-Uprava viz. ReadMe
-Hratky s konfiguraci



[verze "7.10"] TaD (15.10.2019)
-------------
-Vytorena verze Bobeš Alfa

-Prodej, pridana WebMetoda pro univerzalne online volani
-Typy dokladu rozširene o cfg_Navrh
-Upravena funkce pro Pøijem s  Pr. nerealizovane prijemky  



[verze "7.9"] TaD (7.10.2019)
-------------
-UZAVØENI verze AMALKA


[verze "7.8"] TaD (3.10.2019)
-------------
-UZAVØENI verze AMALKA, plany pokus
-Sjednoceni verze s terinalem



[verze "7.7"] TaD (10.09.2019)
-------------
-Sjednoceni verze s terinalem
-Predelani kompletne uživatele na FASK_Logins
-novy projekt ktery zpravuje praci s uživatelama a pravama

-Uprava expedice, chybyèka v sql dotazu kde se použival poøad CZMST095



[verze "7.5"] TaD
-------------
-Pridane TODO  na \TODO pro Doxygenu, a take bere
- \bug , chybu  a \buxfix jak opravu chyby 

-Pøedelany tabulky pro zboží z CZMST095 na FASK_ZASOBY
-Okomentovani velke èasti kodu pro automaticke generovani Dokumentace Doxygena
JiS : 15.8.2019
- Logovani tiskovych operaci
- Trasovani casy na milisekundy
- Prenos z MST 6 alfa => MST 7 => MST 7 Pohoda


[verze "7.4"]  TaD (10.07.2019)
-------------
-Upravy prenesene z MSTW6 alfa do 7

-Upravy viz pohoda readme v2.22



[verze "7.3"]  TaD (28.06.2019)
-------------
-Pridana sdilena trida pro kontrolu diponibility


[verze "7.2"]  TaD (11.06.2019)
-------------
-Testovani zipovani SOAP
-Predelani dotahovani MAXlenght z XML soboru

-Ukladani IP každeho terminalu, z requestu


[verze "7.1"]  TaD (10.04.2019)
-------------

Upravy viz. pohoda ReadMe

-SubProjekt Rady rozširen o možnost definovat Stredisko, Cinnost a Zakazka pro vystupni doklad

-Dotaženi Online dodavatele podle ICO
-Volny pohyb, predelane definovani Typu øad na SQL serveru misto xml souboru





[verze "7.0"] 
	TaD (18.03.2019)
-------------
-Založeni verze MSTW 7 klon puvodni MSTW 6 alfa




[verze "6.139"] 
	JiS (28.02.2019)
-------------
SqlCEDBs
- zakladaci skripty a sqlce db templates : oprava numeric => numeric(19,5)

[verze "6.138"] 
	TaD (14.02.2019)
-------------
-Velka revize SQL create all skriptu
-Velka revize SDF souboru podle Zakladaciho skriptu SQL
-Zmeny provedeni i v èasti SQLite pro Android

-z SDF souboru vytažene Zakladaci skripty
-Vytvoøena složka TestCreateSDF, zde by mnela vzniknut funkcionalita 
ktera nahradi fyzicke sdf soubory za textaky s definicema a automaticky mechanizmus pro tvorbu sdf souboru a inicializaci pri update

-v ItnterFace vznikla definice pro dokaženy sdf soubor kde obsahuje Dictionery kde se da dotahnout definice MaxLenght pro jednotlive Column v tabulkach
-Uprava všech Datasetu, odstaneni definic MaxLenght pro stringy, Použiva se novy spusob pro ošetrovany delky 

-Inventra2 Implementace Web, rozhrani do modulu Fask.ModuleSql
-Vytvoøeny novy projekt Rady, ktery bude mit na starosti èíselne øady a vazny do IS pro èiselne øady
-èiselne øady implementovani do modulu Pøijem

-Verze Hanibal, pøedelani Funkci a procedur ktere byly v Databazi Pohody tak ted sou v naši DB ala I-Tec
-!!! Ked se meni nazev DB Pohody tak treba ruène upravit v tychto prodecurach a funkcich na SQLDB!!!






[verze "6.137"] 
	TaD (14.12.2018)
-------------
Uprava POHODA : 14.12.2018 v2.19 TaD


[verze "6.136"] 
	TaD (13.12.2018)
-------------
-Uprava Expedice Tisk Paleta
-Pridana metoda ktera poèíta poèet balíku a korekne vrací poèet baliku na palete


[verze "6.135"] 
	TaD (13.12.2018)
-------------
Uprava POHODA : 13.12.2018 v2.18 TaD



[verze "6.134"] 
	TaD (12.12.2018)
-------------
Uprava POHODA : 12.12.2018 v2.17 TaD


[verze "6.133"] 
	TaD (05.12.2018)
-------------
-Uprava logovani, pridano logovani Datasetu ked obsahuje chybu
-Pridana možnost online dotahovat Odberatele podle èaroveho kodu
-Uprava !!pohoda!!
-Poèatky upravy Konfigurace do grafickej podoby (Zatim neaktivni)


[verze "6.132"] 
	TaD (08.11.2018)
-------------
-Pohoda, upravena kontrola pri odesilani


[verze "6.131"] 
	TaD (06.11.2018)
-------------
-Uprava tisk Expedice SN, když je víc jak 3072 znaku
-Ted se tiskne po jednom øadku
-Uprava dotaženi predkontace pri importu Faktury do Pohody s Adresaøe


[verze "6.130"] 
	TaD (30.10.2018)
-------------
-Uprava Tisk Expedice, dotahovani Kod2 co je v pohode Doprava v zalozce Internet v SKz
-import Faktury, vynechani predkontace
-Modul Vydej ,pridan detail Polozka kde se posila ITEMNMBR a v webconfig se urcuje co se ma delat


[verze "6.129"] 
	TaD (26.10.2018)
-------------
-viz readme pohoda : 26.10.2018 v2.15 TaD
-do VydejTest.asmx je pridana možnost import Faktury Pohoda bez terminalu


[verze "6.128"] 
	TaD (22.10.2018)
-------------
-viz readme pohoda : 22.10.2018 v2.14 TaD


[verze "6.127"] 
	TaD (17.10.2018)
-------------
-I-Tec uprava tisku, dotahovani Adresy do hlavièky a poèet baliku na palete
-viz. readme pohoda



[verze "6.126"] 
	TaD (10.10.2018)
-------------
-Upravy tisky a generovani I-Tec Pohoda

[verze "6.125"] 
	JiS (08.10.2018)
-------------
- odstraneny metody "DeleteFileOnServer" z jednotlivych webovych sluzeb => pouzivat se ma FileTransfer.Delete


[verze "6.124"] 
	TaD (05.10.2018)
-------------
viz.readME Pelacasa verze 05.10.2018 v2.12 TaD



[verze "6.123"] 
	TaD (04.10.2018)
-------------
-Uprava tisku Expedice Paleta
-uprava alforitmu provypoèet velkosti štitku pro SN jak seznam
-Pridana ExpediceTest.asmx pro testovani tisku



[verze "6.122"] 
	TaD (03.10.2018)
-------------
-Vydej, rozšireni interface o testovaci metodu pro import vydejky (implementovani pouze do pohody)
-Readme Pohoda



[verze "6.121"] 
	TaD (27.09.2018)
-------------

-Uprava potlaèeni Typy sledovani pri generovani predlohy z Faktury v modulu pohoda - Softcotton
-Uprava tisku expedice - I-Tec


[verze "6.120"] 
	TaD (26.09.2018)
-------------
-pokusy o tisk odberatele do do hlavièky, neuspešne donucen sestavit verzi...



[verze "6.119"] 
	TaD (24.09.2018)
-------------
-Premenovani metody v print provider
-upravy šablon


[verze "6.118"] 
	JiS (11.09.2018)
-------------
Vydej 
- rozsireni vystupni struktury o prvek QTYSHPPMJ (mnozstvi zadane ve zvolene MJ)


[verze "6.117"]
	TPr (12.09.2018)
-------------
- Pridano zipovani ciselniku v prepare metodach
- FileTransfer - metoda savelog je predelana na odzipovani logu/trace
- pridelany sqlite provider pro inventuru 
- pridane metody pro mazani souboru na serveru

[verze "6.116"] 
	TaD (10.09.2018)
-------------
- Uprava modul pohoda viz ReadMe tam v2.9



[verze "6.115"] 
	TaD (7.09.2018)
-------------
-uprava tisku konfiguraène poèet znaku na vyber šablony Modul.Print.Hanibal
- Uprava modul pohoda viz ReadMe tam



[verze "6.114"] 
	TaD (5.09.2018)
-------------
- Uprava modul pohoda viz ReadMe tam


[verze "6.113"] 
	TaD (4.09.2018)
-------------
-modul vydej, provider pohoda nastavit StatusObject na .SetOK(); aby korektne odpovidal terminalu


[verze "6.112"] 
	TaD (30.08.2018)
-------------
-Uprava soupisu pro I-Tec, lepši zobrazeni èar.kodu
-Pohoda, pridane generovani prevodky na modulu Prodej, po odeslani davky vygeneruje prevodku (konfiguraène)




[verze "6.111"] 
	TaD (29.08.2018)
-------------
-Uprava konfiguracne dotahovani SerNumTrack pri objednavke prijate a objednavke vydane dotahovani primo z SKz
-Generovani Vydej z Objednavky Prijatej cez èteèku
-pri vraceni davky upravene nastaveni CZ_Doslo na 0
-Uprava pri stahovani èiselniku Typ dokladu Accept changes na DataTable
-Konfiguraène možno na vydeji naplnit na Expedici Baleni
-konfiguraène možno na vydeji zapnout importovani vydejky do IS Pohoda

-Na vydeji pribudla nova Webmetoda sloužici pro generovani SSCC kodu pro Baleni/Palety/Balici jednotky...



[verze "6.110"] 
	TaD (16.08.2018)
-------------
-Vytvoøeni modulu I-Tec pro expedici
-Uprava Provideru pohoda
-uprava modulu vydej, konfiguraène zapnutelne plneni Baleni + tvorba baleni

-konfiguraène zapnutelne
-Importovani vydejky do Pohody ( urèite hapruju ceny), žadna reakce na importovanou vydejku, upravy skladovych zasob, priznak vyøizeno atd...



[verze "6.109"] 
	TaD (27.07.2018)
-------------
-Modul pohoda, pridani parsovani Decimal Invariant tvar pri Load responde XML file.


[verze "6.108"] 
	TaD (26.07.2018)
-------------
-Upraveny export Inventura pohoda, I3, chybala poloza weight natvrdo 0


[verze "6.107"] 
	TaD (04.07.2018)
-------------
-upraveny spusob kodovana CODE128C 


[verze "6.106"] 
	TaD (03.07.2018)
-------------
- Pridana složka do ktere se ukladaji vytisknute veci
-cesta pridana do webconfigu
-Pridane kodovani EANu do varianty C
-pridane programove škrtnuti ceny v modulu Hanibal



[verze "6.105"] 
	TaD (02.07.2018)
-------------
-Pridany provider pro tisk
-vytvoren provider pro Hanibal s doplnovanim dat do možnosti tisku etikety
-možnost v providerovy menit meno šablony programovo

-zisklany exe program z Hanibal s podle logiky v nem udelana naše upravena logika pro tisk

-konfiguracne nazvy šablon, texti co se tisknou na etiketzy a connection string na Pohodu


[verze "6.104"] 
	TaD (25.6.2018)
-------------
-rozireni typu dokladu do polozku cfg_delka_SN


[verze "6.103"] 
	TaD (12.6.2018)
-------------
-Modul Peracala Generovani XML pro Prijemku pridane osetreni 
pokud se v polozkach cinnost a stredisko nachazi hodnota 0 tak to nezapise



[verze "6.102"] 
	TaD (8.6.2018)
-------------
-Pri nacitavani nezrealizovanych prijemek se pomoci funkce na SQL serveru vraci seznam nesrealizovanch a nasledne se vznechavaji ty ktere uz sou stažene na ctecke


[verze "6.101"] 
	TaD (8.6.2018)
-------------
-Ivnetura1 upravena tabulka I3 polozka vaha v sdf souboru na numerci 19.5 s puvodnych 18.0
-upravena metoda getInveturaDB kde chybela v I3 vzplnit vaha 
-Inventura I4 tabulka v sdf souboru zmenena polozna vaha na 19.5 numeric


[verze "6.100"] 
	TaD (31.5.2018)
-------------
-První verze projektu MST_W 6 alfa, odštepek od puvodniho MST_W 6
-Programatorska verze pro ruzne optimalizace stahovani, odesilani....

-Upravy pro projekt ZZS, Hanibal a Android (Invetura1)

*****Hlavni zmeny heslovito : 
-Spojeni Kom serveru a Print serveru
-možnost pripojit widows èteèky a Android (pouze na uzivatele a invetura)

-to znamena je na SQL serveru Tabulka CZMST_TERMINAL_DEFINITION ktera podle ID terminal identikuje typ èteèky
a pak nasledne bud zvoli provider pro SQLite(android) anebo normalnu windows  èteèku

-udelane rychlejsi odesilani a stahovani davek cez request a zipovani souboru




================
[verze "6.14"] 
	TaD (31.10.2017)
	JiS (08.11.2017)
-------------
- updavy v Datasetch : Inventura1,Prijem 
v nazvoch CZ_REZ_1_Track na CZ_REZ1_Track a CZ_REZ_2_Track na CZ_REZ2_Track 
- oprava Prijem, Inventura
	=> IsCZ_Rez1_TrackNull() ...

[verze "6.13"] TaD (31.10.2017)
-------------
Inventura2 pole název rozšíøeno z 35 na 60 znakù

- pridana složka update v ktere je umisteny .xml soubor ktery služí na infrmovani uživatele v èteèce že je možno stahnout novu verzi



[verze "6.12"] TaD (19.10.2017)
-------------
-sjednoceni verze serveru a ctecky
-modul KTO


[verze "6.9"] JiS (5.4.2017)
-------------
verze s MST_W

[verze "6.5"] Ta.D (20.2.2017)
-------------
Servis
- Zdroj : rozsireni o prvek Misto
- Priprava davek : odberatel, okruh

---------------------------------------------------------

[verze "6.4"]Ta.D (16.2.2017)
-pridane do module.SBkomplet.Steinex do provider Iventura
-WEBControl Inventura

-----------------------------
[verze "6.3"]Ta.D (9.2.2017)
-Inventura1 pridana weight do CZMST_I3 a CZMST_I4
-prijem.sdf pouze CZMST_PE PONUMBER na 100 znaku. CZMST_PI neslo

[verze "4.108"] JiS (3.10.2016)
-------------
- verze s MST_W
- rozpracovany tisky NEKUPTO

[verze "4.107"] PeV (17.8.2016)
-------------
- sjednoceni verze s MST_W
JiS 23.8.2016
Uprava reseni pro SBKomplet zakazka VICHR Vydej (modem)
Terminal rozsireni filtrovani polozek : Vse / Neuplne / Zadane / Zbyvajici (Klavesova zkratka D1)
Server oprava natahovani polozek alternavinich car.kodu a prace se souborem - vyuziva se databaze sql a tabulka czmst_carkod_altern ...

[verze "4.105"] PeV (11.8.2016)
-------------
Vydej (bez providera a Sql provider)
- moznost vraceni seznamu davek na urciteho uzivatele

[verze "4.104"] PeV (10.8.2016)
-------------
- sjednoceni verze s MST_W

[verze "4.103"] PeV (27.7.2016)
-------------
- sjednoceni verze s MST_W

[verze "4.102"] PeV (26.7.2016)
-------------
JimiTore
- implementace modulu Kontrola
Prodej
- typ dokladu rozsiren o parametr 'cfg_sklady_zmena'

[verze "4.101"] PeV (22.7.2016)
-------------
- sjednoceni verze s MST_W

[verze "4.100"] PeV (21.7.2016)
-------------
- sjednoceni verze s MST_W
Prijem
- rozsireni predlohy o sloupec serltnum (pokud je vyplneny, dojde k predvyplneni)
Expedice
- upravy providera
- pri generovani predlohy prijmu dochazi k zohledneni palet
- oprava, kdy nebylo mozne v baleni zmenit cislo palety

[verze "4.98"] PeV (13.7.2016)
-------------
Expedice
- upravy providera
- funkce vracejici palety nyni vraci sumu mnozstvi/vahy

[verze "4.97"] PeV (29.6.2016)
-------------
Prodej
- rozsireni czmst092 o cfg_tisk_palety

[verze "4.96"] PeV (23.6.2016)
-------------
Prodej
- rozsireni czmst_di o sloupce NMBRPAL, TYPEPAL a PRINTED
- rozsireni czmst092 o cfg_onl_palety_generovat
Fask.ModulePohodaXML.pohoda
Vydej
- doplnena funkce pro dotazeni Vychozi lokace do predlohy vydeje
Prijem
- upraveno dotazeni vychozi lokace pro prijem objednavky vydane -> presunuto do metody Save (az znam vsechny data...)
- odstraneni statickeho datasetu prijmu -> dochazelo k plneni davky vice objednavkami/prevodkami (staticky dataset si pamatoval drive vygenerovane davky)
Inventura
- odstraneni statickeho datasetu inventury
Fask.ModulePohoda
- odstraneni statickeho datasetu prijmu -> dochazelo k plneni davky vice objednavkami (staticky dataset si pamatoval drive vygenerovane davky)
Inventura
- odstraneni statickeho datasetu inventury
Fask.Module.SBKomplet.Steinex
- kompletni prepsani expedice (pridano baleni)
- upravy struktur

[verze "4.95"] PeV (8.6.2016)
-------------
JimiTore
- rozsireni matid na 40 znaku u parametru procedur
Fask.Module.SBKomplet.Steinex
- novy modul
Prijem (Fask.Module.SBKomplet.Steinex)
- pridano plneni czmst_pe_sn
- uprava, aby se hlavicky dotahovaly z czmst_pe
Expedice
- implementace online modulu
- implementace generovani SSCC, TODO: potreba prepsat, vice konfigurovatelne ...
- pri zpracovani davky dochazi k vytvoreni predlohy prijemky
SqlProvider, S4SSql
- pridani vyuziti status object directory
Prodej
- rozsireni czmst095 a czmst_di o sloupec WEIGHT (vaha)
- rozsireni czmst092 o parametry cfg_generovat_sn, cfg_parsovat_ck, cfg_sn_na_davku, cfg_lok_mech_online_pohyby
Servis
- rozsireni tabulky predlohy o ID odberatele
- uprava pohledu, ktery vraci seznam davek
Prijem
- rozsireni struktur o vahu, id palety, typ palety a itemcode

[verze "4.94"] JiS (9.5.2016)
-------------
Obecne
- implementace mechanismu pro rizeni dlouhotrvajicich operaci pomoci StatusObjectu
- trace objekt rozsiren o identifikator pro dohledani a parovani operaci
Provider.Sql a S4SSql
Prodej
- zpracovani dat pouziva StatusObjectsDirectory pro ulozeni status objektu
Fask.ModulePohodaXML
Prodej
- implementace lokacniho mechanismu
- zakomentovani puvodni metody zpracovani dat, ktera komunikovala s pohodou
- uprava struktury ciselniku zbozi (jiz vyuziva aktualni struktury)

[verze "4.93"] PeV (3.5.2016)
-------------
Servis
- rozsireni struktur ZdrojStav a ZdrojPohyb o GPS souradnice a CinnostOznaceni (uklada se Oznaceni z dynamickych tabulek)
- uprava struktur (do tabulky predlohy davky pridan sloupec Barcode a DateCreated obsahujici datum vytvoreni davky)

[verze "4.92"] JiS (28.4.2016)
-------------
Prijem, Vydej
- Generovani davky : upraveno s dotazem na strane terminalu

[verze "4.91"] JiS (25.4.2016)
-------------
Web.config
- "Provider" : pokud je uveden a v Provider.XXXX neni uveden, pak se pouzije jako vychozi provider, pro vsechny nedefinovane

[verze "4.90"] JiS (19.4.2016)
-------------
Prijem, Vydej - GenerovaniDavky
- zmena zpusobu zpracovani generovani davky pro dlouho trvajici operace
Struktury
- pridany definice struktur pro transakcni pridelovani cisel davek modulu
- pridana ulozena procedura pro rizeni pridelovani davek
Paths
- pridana konfiguracni cesta pro StautsObjects 
- StatusObject rozsiren o vlastnost Finished


[verze "4.89"] PeV (15.4.2016)
-------------
Fask.ModulePohodaXML
Vydej
- pridana kontrola delky itemdesc pri generovani davky z faktury
Ciselniky
- zmena struktury ciselniku lokaci (czmst094)
Prodej
- typ dokladu rozsiren o povoleni zobrazeni doporucenych cilovych lokaci + nekolika dalsich parametru

[verze "4.88"] PeV (23.3.2016)
-------------
Prijem (SqlProvider)
- filtrovani podle skladu jiz probiha podle sloupce 'SKL_ID' misto 'Locncode' v metode Prijem_GetPrijemky
Servis
- rozsireni providera o davkove zpracovani
- rozsireni zpracovani davek o moznost volani procedury po zpracovani
- pridana moznost stornovani nerozpracovane davky
- vytvoreni davky a generovani dat prijemky probiha pomoci procedur

[verze "4.87"] PeV (1.3.2016)
-------------
Prijem S4SSql
- uprava datasetu (rozsireni o skl_id a MJ)
Fask.Module.Web.JimiTore.Baleni
- rozsireni prijem id na 16 znaku (chybne zadani) ... oprava je jiz nasazena se starsim cislem verze

[verze "4.86"] PeV (18.2.2016)
-------------
Fask.Module.Web.JimiTore.Baleni
- pridany webove metody pro Prijem

[verze "4.85"] PeV (17.2.2016)
-------------
Fask.ModulePohodaXML
- upravy dotazu v datasetech (pridani chybejicich 'as' a zavorek u joinu)
Prijem
- pridano chybejici nacitani konfigurace aplikace
- upravy prijmu, aby dokazal rozlisoval mezi prevodkami a objednavkami
- sjednocovani funkcionality s vydejem
- upravy LoadResponse_Prevodka_XML
- czmst_pe rozsireno o skl_id
- opravy pri vytvareni requestu pro prevodku
- konfigurace rozsirena o 'Process_UseShellExecute'
- doporucena lokace se jiz uklada do czmst_pe
Vydej
- doporucena lokace se jiz uklada do czmst_se
Lokace
- upravy providera

[verze "4.84"] PeV (1.2.2016)
-------------
Lokace
- pridana moznost mazani zaznamu podle Guid a TypeOfRecord (kvuli prodejnimu modulu)
- rozsireni o praci s sarzemi
- upravy kvuli prodejnimu modulu
- uprava vyhledavani v lokacnim mechanismu tak, aby pri vyhledavani pouze podle itemnmbr, skl_id se nevyhledavalo pomoci sarze (pokud neni zadana) ... viz kmenove karty ANC
Prodej
- czmst092 rozsirena o parametry:
	[cfg_skl_id_dest]		-- zobrazeni zadani ciloveho skladu
	[predvyplnit_skl_id_dest]	-- cilovy sklad, ktery se automaticky vybere pri pridavani zbozi do tabulky czmst_di (pokud existuje a je zapnuty parametr cfg_skl_id_dest ...)
	[cfg_lok_mech]			-- povoleni prace s lokacnim mechanismem
	[cfg_lok_mech_pohyb_type]	-- typ pohybu, ktery se zasle lokacnimu mechanismu (P - prijem, V - vydej, D - defregmentace)
- czmst_di rozsirena o:
	[SKL_ID_DEST]		-- cilovy sklad
- pri zpracovavani davky dochazi k nacteni dat z czmst092, pokud je zapnuty lokacni mechanismus, tak probiha kontrola na guid a pripadne doplneni chybejicich zaznamu
- uprava overovani lokace tak, aby v pripade prijmu se overovala pouze existence lokace a ne i zdali je material na dane lokaci (uprava providera, terminalu, ...)
- pri zpracovani dat se nacte typ dokladu, podle ktereho se urci, zdali je lokacni mechanismus povolen
- pri zpracovani dat probiha kontrola, zdali jsou data jiz ulozena v lokacnim mechanismu, pokud nejsou, dojde k ulozeni
Fask.ModulePohodaXML
Prijem
- implementace lokacniho mechanismu
Fask.Module.Web.JimiTore.Baleni
- pridany webove metody pro Prijem zbytku

[verze "4.83"] PeV (27.1.2016)
-------------
MST_Win_Kom_Server
Fask.Module.Web.JimiTore.Baleni
- doplneni struktur vydeje
Lokace
- tvorba providera pro lokacni mechanismus
Inventura1 (PohodaXML)
- pridani moznosti naplneni lokacniho mechanismu daty z tabulky czmst_i4
Vydejhan
- rozsireni vydejparams.xml o konfiguraci lokacniho mechanismu (zapnuti, timeout, ...)
- pri zpracovavani davky dochazi k nacteni dat z vydejparams, pokud je zapnuty lokacni mechanismus, tak probiha kontrola na guid a pripadne doplneni chybejicich zaznamu
Prijem
- rozsireni prijemparams.xml o konfiguraci lokacniho mechanismu (zapnuti, timeout, ...)
- pri zpracovavani davky dochazi k nacteni dat z prijemparams, pokud je zapnuty lokacni mechanismus, tak probiha kontrola na guid a pripadne doplneni chybejicich zaznamu
- rozsireni providera o 'Online_OverLokace' (pro S4SSql provider se vola puvodni metoda, pro Sql providera probiha kontrola v lokacnim mechanismu)
Prodej
- rozsireni providera o 'Online_OverLokace' (pro S4SSql provider se vola puvodni metoda, pro Sql providera probiha kontrola v lokacnim mechanismu)
- prejmenovani metody 'Online_GetPalety' na 'Online_GetMaterial' a rozsireni providera (pro S4SSql provider se vola puvodni metoda, pro Sql providera vraci data z lokacniho mechanismu)

[verze "4.82"] PeV (11.1.2016)
-------------
Servis
- pridana metoda pro zpracovani davky primo ze serveru
Fask.Module.Web.JimiTore.Baleni
- rozsireni o vydej

[verze "4.81"] PeV (4.1.2016)
-------------
Inventura1
- pred zpracovanim probiha kontrola na existenci prvniho a posledniho guidu zaznamu (pokud guidy nejsou v DB, tak dojde k ulozeni vsech dat do czmst_i4, jinak dojde k zalogovani danych guidu)
Inventura1 SQLProvider
- odstraneni nacitani nastaveni z inventuraparams.xml (nacita se primo v Inventura1.asmx)
Fask.Logging
- pridana logovaci knihovna

[verze "4.80"] JiS, PeV (18.12.2015)
-------------
- sjednoceni veze s MST_Win
Vratky
- Definice struktur
- impelemttace ziskani dat z databaze
- odstraneni (exclude) z projektu

[verze "4.78"] JiS (10.12.2015)
-------------
- sjednoceni veze s MST_Win
Prijem
- rozsireni o prace se sklady, na zaklade potreby Husky (HeliosOrange)

[verze "4.77"] PeV (8.12.2015)
-------------
- sjednoceni verze s MST_Win

[verze "4.76"] PeV (1.12.2015)
-------------
- sjednoceni verze s MST_Win

[verze "4.75"] PeV (26.11.2015)
-------------
Inventura 1 (Fask.ModulePohodaXML)
- pridani zobrazovani caroveho kodu do prehledu dat, exportu dat do CSV a tvorby tiskove sestavy
- odstraneny entery z nazvu pri exportu do CSV
- pridana kontrola na dokoncenost inventury pri ukoncovani (aby se neustale nezvysoval TerminalID o 100)
Prijem
- pri zpracovani davky zapnutem trasovani se loguje ProcessPrijemState (zdali se davka uvolnuje nebo zpracovava)

[verze "4.74"] PeV (19.11.2015)
-------------
Prodej
- typ dokladu (czmst092) rozsiren o konfiguracni parametr 'cfg_predvyplnit_mnozstvi'. Pokud je nastaven na '1', dojde k predvyplneni mnozstvi
Email
- pri posilani logu emailem dochazi k zalogovani hostname

[verze "4.73"] PeV (19.11.2015)
-------------
Jimi Tore
Fask.Module.Web.JimiTore.Baleni
- rozsireni o volani GetBaleniData a BaleniDataCommit webove sluzby Baleni.asmx weboveho serveru
- rozsireno o konfiguraci pro nastaveni modulu
Prijem S4SSql
- pokud se vraci prijemka (nastavuje se CZ_Doslo na 0), tak nedochazi k volani afterProcessedAction

[verze "4.72"] PeV (27.10.2015)
-------------
Servis
- pridana moznost impersonifikace uctu pri ukladani fotek kvuli ulozeni fotek na sitovy disk (klice: 'ImagesUserName', 'ImagesUserPassword', 'ImagesDomainName')

[verze "4.71"] PeV (26.10.2015)
-------------
Prodej
- rozsireni ciselniku typu dokladu (czmst092) o parametr cfg_mnozstvi_ze_zbozi + uprava datasetu. Pokud je zapnuty, je preskoceno zobrazeni dialogu pro zadani mnozstvi a rovnou se nastavi mnozstvi z dane polozky (pripadne z online dotazu doporucenych palet)

[verze "4.70"] JiS (26.10.2015)
-------------
Eurom 
- Hmotnost (Terminal - plugin, rozsiren Prijem - konfigurace)
- Uprava modulu serveru a ciselniku, tak aby se nastavovaly defaultni hodnoty pro ciselniky
- nutne dale dotestovat
Helios Orange 
- prirpava ciselniku zbozi (nutno revidovat - predelat)
Jimi Tore
- priprava pluginu pro terminal a webove sluzby pro tisk balicich etiket (nedodelano)
Fask.Module.Zbozi
- rozsireni o volani OnlineHmotnost webove sluzby Hmotnost.asmx weboveho serveru
- rozsireno o konfiguraci pro nastaveni modulu
Terminal
- dopleni confid.xsd o parametry konfiguraci
- uprava LoginForm o akce nad vstupnimi poli (enter a esc)
- prijem - onlineHmotnost (konfiguracni parametr pro volani webove sluzby Hmotnost.asmx)

[verze 4.69] 15.10.2015 - PeV
------------
- sjednoceni verze s MST_Win

[verze 4.68] 7.10.2015 - PeV
------------
- sjednoceni verze s MST_Win
Prodej
- pridan index pro Serltnum do ciselniku zbozi

[verze 4.57] 6.10.2015 - PeV
------------
Prijem
- implementace online funkce PrijemkaDetail pro S4SSql provider

[verze 4.56] 6.10.2015 - PeV
------------
Prodej
- oprava, kdy se u providera neprenaselo locncodedest do czmst_di

[verze 4.55] 6.10.2015 - PeV
------------
Prodej
- do online overeni lokace se posila parametr pro rozliseni zdrojove a cilove lokace ('S' - overovani zdrojove lokace, 'D' - overovani cilove lokace)
Prijem
- zpracovani prijmu a after process procedura je ve spolecne transakci (pokud se nevola asynchronne)

[verze 4.54] 1.10.2015 - PeV
------------
Prodej
- procedure, ktera vraci seznam doporucenych palet se posila sarze vybraneho zbozi (serltnum)
- do online funkce overeni lokace pridan dalsi navratovy parametr (Message) a novy stav (2), ktery slouzi k zobrazeni chybove hlasky, ktera se nastavi primo v procedure
Prijem
- do online funkce overeni lokace pridan dalsi navratovy parametr (Message) a novy stav (2), ktery slouzi k zobrazeni chybove hlasky, ktera se nastavi primo v procedure

[verze 4.53] 17.9.2015 - PeV
------------
Prodej
- proceduram Prijem_GetDoporuceneLokace_Action, Prijem_OverLokaci_Action, Prijem_GenerateSerltnum_Action se posila id skladu (pokud vraci select, tak se i vraci)
Prijem
- proceduram Prodej_OverLokaci_Action, Prodej_GetLokaci_Action se posila id skladu (pokud vraci select, tak se i vraci)

[verze 4.52] 15.9.2015 - PeV
------------
Prodej
- Online_OverLokace, Online_GetPalety prijimaji id skladu (zatim se nevyuziva)
Prijem
- Online_GetDoporuceneLokace, Online_OverLokace, Online_GenerateSerltnum prijimaji id skladu (zatim se nevyuziva)
- oprava, kdy dochazelo k chybe, pokud se vracela prijemka, ktera nebyla ani jednou otevrena (SqlProvider a S4SSql provider)

[verze 4.51] 9.9.2015 - PeV
------------
Prijem
- DetailItem prijima 'serltnum'
- S4SSql provider rozsiren o moznost nacteni detailu polozky (kvuli tiskum)
- pridana nova tabulka CZMST_PI_F pro informace o porizenych fotografii + rozsireni datasetu a providera S4SSql

[verze 4.50] 20.8.2015 - PeV
------------
Prijem
- procedura pro online kontrolu lokace prijima Itemnmbr
- vytvorena online funkce pro navrat seznamu nezrealizovanych prijemek
Prodej
- procedura pro online kontrolu lokace prijima Itemnmbr
- tabulka CZMST092 rozsirena o sloupec:
	[cfg_onl_dop_pal] [tinyint] NULL + uprava datasetu
	[cfg_onl_over_lokace] [tinyint] NULL + uprava datasetu
	[cfg_onl_over_lokace_dest] [tinyint] NULL + uprava datasetu

[verze 4.49] 17.8.2015 - PeV
------------
Prijem
- vytvorena online funkce Online_GetDoporuceneLokace pro vraceni seznamu doporucenych lokaci (web.config - "Prijem_GetDoporuceneLokace_Action")
- upravy online funkce pro generovani sarze a overeni lokace

Prodej
- upravy online funkci pro overovani lokace a seznamu doporucenych lokaci

[verze 4.48] 3.8.2015 - PeV
------------
- pridan Fask.ModuleS4SSql provider 

Prijem
- vytvorena online funkce pro generovani sarze (web.config - "Prijem_GenerateSerltnum_Action")
- vytvorena online funkce pro overeni lokace (web.config - "Prijem_OverLokaci_Action")
Prodej
- vytvorena online funkce pro overeni lokace (web.config - "Prodej_OverLokaci_Action"). V pripade najiti vhodnejsi lokace ji vraci.
- vytvorena online funkce pro navrat seznamu doporucenych lokaci (web.config - "Prodej_GetLokaci_Action"). V pripade najiti vhodnejsi lokace ji vraci.

[verze 4.47] 28.7.2015 - PeV
------------
Prodej
- tabulka CZMST095 rozsirena o sloupec [SERLTNUM] [char] (21) NULL + uprava datasetu
- tabulka CZMST_DI rozsirena o sloupec [LOCNCODEDEST] [char] (11) NULL + uprava datasetu

[verze 4.46] 28.7.2015 - PeV
------------
Prodej
- pridano trasovani
Prijem
- pridano trasovani do AfterProcessedAction, ProcessPrijemDBFile a ProcessPrijemka

[verze 4.45] 14.7.2015 - PeV
------------
Vydej
- bez providera se jiz prenasi sloupec SKL_ID do tabulky CZMST_SI

[verze 4.44] 13.7.2015 - PeV
------------
Servis
- pridana metoda "ProcessZdrojPohybData" pro nacitani databazoveho souboru czmst_servis_zdrojpohyb + implementovan SQL provider (styl Vyroba)
- pri volani "ProcessZdrojPohybData" dochazi soucasne k aktualizaci zaznamu v czmst_servis_zdrojstav
- vkladani do ZdrojStav pri zpracovavani dat v metode "ProcessZdrojPohybData" probiha v transakci

[verze 4.43] 3.7.2015 - PeV
------------
Servis
- rozsireni CZMST_Servis_Cinnost o sloupec Mandatory (povinne, nepovinne zadavani hodnot)
- rozsireni CZMST_Servis_Zdroj o sloupec Type - nepovinny doprovodny text (suchy,chlazeny)

[verze 4.42] 29.6.2015 - PeV
------------
Ciselnik
- typdokladu rozsiren o sloupec cfg_tisk_soupis + uprava datasetu, provideru, ...

[verze 4.40] 16.6.2015 - PeV
------------
Ciselnik
- pri stahovani men bez providera dojde ke stahnuti databaze bez dat misto zobrazeni vyjimky

[verze 4.39] 8.6.2015 - PeV
------------
Prodej
- uprava metody OverPohyb
- rozsirena tabulka v sqlcedb czmstpwd o jmeno a prijmeni + pridano nacitani

[verze 4.38] 3.6.2015 - PeV
------------
- uprava cest v knihovnach

[verze 4.37] 2.6.2015 - PeV
------------
- vytvorena nova knihovna MyPath pro uchovavani cest k souborum a adresarum (cesty nacitaji v Globals.asax) + samotne pouziti

[verze 4.36] 1.6.2015 - PeV
------------
Inventura1
- GetInventura v rezimu MST 3 (bez pluginu pro IS) nacita hodnotu stav a popis z tabulky CZMST_I1H
- uprava vkladani dat do I1H v PrepareInventuraDB() (null hodnoty)

[verze 4.35] 26.5.2015 - PeV
------------
Inventura1 
- getInventury v rezimu MST 3 (bez pluginu pro IS) nacita hodnotu stav a popis z tabulky CZMST_I1H

[verze 4.34] 12.3.2015 - JiS
------------
Ciselnik zbozi
- rozsireni o tabulku czmst095M - ciselnik cizich men pro cenove urovne
- implementovano pro modul HeliosOrangeSQL
- zmena assemblycopyrigth pouze na fask bez roku (plati obecne...)

[verze 4.27] 9.1.2015 - PeV
------------
ModulePohodaXML
- opravy chyb pri ukladani desetinne tecky do XML souboru (ukladala se carka v nekterych pripadech)
- oprava nacitani textoveho retezce na prijmu

[verze 4.26] 28.11.2014 - PeV
------------
Servis
- servisDS.CZMST_Servis_Cinnost ovìøení Barcode na null

[verze 4.25] 26.11.2014 - PeV
------------
Servis
- pridana prace s dynamickymi tabulkami
- vytvorena nova tabulka CZMST_Servis_Dynamic_Table_Definition
- pridan sloupec do tabulky CZMST_Servis_CinnostNext
- uprava datasetu

[verze 4.24] 13.11.2014 - PeV
------------
- pridan CountEntries do CZMST_DIH kvuli prevodu mezi sklady (+ selectu/insertu neobjektovych adapteru)
- uprava datasetu CZMST_DIH

[verze 4.23] 11.11.2014 - PeV
------------
- pridano trasovani a pridano do Vydeje (moznost zapnuti ve Web.config)
- pridano do tabulek a datasetu doc_id2
- pridan cfg_tisk do CZMST_092, ktery slouzi k urceni, zdali se ma tisknout v Prodej_3.ProdejList.pridatPolozku
- pridan cfg_prevod_sklad do CZMST_092 k urceni, zdali se da prevadet sklad na terminale
- pridan SKL_ID do CZMST_DIH kvuli prevodu mezi sklady
- pridana online metoda OverPohyb do Prodeje
- pridan modul Prytanis

[verze 4.22] 20.10.2014 - JiS
------------
LoginService
- uprava algoritmu pripravy ciselniku

[verze 4.21] 16.9.2014 - JiS
------------
Ukolovani
- Selecty do sql rozsireny o klauzuli "WITH (NOLOCK)"

[verze 4.20] 11.9.2014 - JiS
------------
Verze s MST_W

[verze 4.19] 10.9.2014 - JiS
------------
Servis 
- roziserni o Cinnosti
- rozsireni o prijem fotek do adresare definovano ve web.config... (ImagesDirectory ...)

[verze 4.18] - JiS
------------
Servis
- novy modul

[verze 4.17] 7.7.2014 - JiS
------------
Prijem
- oprava datum prijmu
Verze s MST_W ...

[verze 4.14] 19.3.2014 - JiS
------------
Prodej
- rozsireni DI o prvek ITEMCODE
Prijem
- rozsireni o hlavicku PIH 
	=> datumdokladu

[verze 4.13] 6.6.2014 - JiS
------------
Inventura oprava stare funkcionality
Ukolovani servis

[verze 4.10] 19.3.2014 - JiS
------------
Routines.DestinationCheck : oprava nastaveni nove cesty, pokud soubor jiz existuje ...
=> nelogovaly se existujici davky ...

[verze 4.09] 19.3.2014 - JiS
------------
Prijem
- rozsireni o tabulku predlohy ser.cisel/sarzi 

Vydej
- rozsireni tabulky predlohy ser.cisel/sarzi o mnozstvi

[verze 4.07] 15.1.2014 - JiS
------------
???

[verze 4.01]
------------
19.11.2013 - JiS
- Fask.ModulePohodaXML verze 1.1
- dokladova vazba objv => prijemka
- viz. Readme.Fask.ModulePohodaXML.txt

[verze "4.01"]
--------------
Server Upravy jeste
- Pri exportu vydane objednavky do Pohody doplnena i informace o forme meny
- Export inventury v transakci
- User: pridany pole do interface
- Report Inventura 1: upraveny pole mnozstvi na ciselne typty decimal 
- zobrazeni prihlaseneho uzivatele jako firstname a secondname
- upraven Web.config s komentarem sekci dynamickych knihoven modulu

[verze "4.00"]
--------------
- oddeleni ex dat z Pohody a prenos dat do terminalu
- zrychleni exportu dat z Pohody (zbozi a inventura)
- upraveno nacitani IWebControl provideru (aby se pokazde nenacital znovu)
- upraveno strankovani na inventure
- v modulu PohodaXML upraveno stahovani ciselniku zbozi (XML -> primy pristup do DB)
- rozsirena tabulka CZMSTPWD o sloupec EAN
- vytvoreno rozhranni pro inventuru1 a inventuru2, do datasetu inventur pridany tabulky s hlavickami (GUID)
- otestovany predelane SQL moduly (prijem, vydej, prodej) vcetne statusObjectu
- predelana navratova hodnota pri zpracovani dat na statusobject, aby terminal mel informaci o stavu zpracovani
- online funkce pro logovani vraci i ID uzivatele
- upraveno logovani na terminalu (z DB, heslo, HASH, pripadne online fce), zrusen xml file s loginama
- zastaveni provadeni dotazu pokud je neplatna licence v Application_BeginRequest na zaklade parametru
- pri vytvareni licence kontrola aby aktualni cas nebyl mensi nez cas vytvoreni licence
- vytvorena webova metoda vracejici informace o licenci
- Prijem: predelan do SQLModulu
- Prijem: v datasetu nova tabulka (hlavicky), pri odesilani dat vznika file s informacemi a prubehu zpracovani
- Prijem: prijmani dat vraci StatusObject, aby terminal mel info o nevalidite licince
- nova sluzba login.asmx, vytvoren interface pro prihlasovani uzivatelu na terminulu ....//statusobject, stazeni sqldatabaze uzivateli, take online overeni uzivatele (heslo,login,idterminalu)..proti databazi nebo online            //stazeni databazeno do terminalu viz. ciselniky... podivat se na vytvareni uzivatelu...viz. xdatabase
- Inside: sprava odeslani stavu prijemky (StatusObject)

[verze "3.50"]
--------------
Prodej, Prijem, Vydej, Inventura1
- afterprocess action se vola vzdy, implementace procedury musi pocitat s vicenasobnym vyvolanim i pri opakovanem odeslani dat ...

[verze "3.49"]
--------------
- verze s MST_W
- VydejDetailDavka : nova funkce k dotazeni informaci do prehledu davek ke stazeni na zaklade cisla davky ...

[verze "3.48"]
--------------
- verze s MST_W

[verze "3.47"]
--------------
Tiskarny dokladu
- pridan novy ciselnik dokladovych tiskaren
Vydej
- rozsireni struktur o czmst_sih
	= obsahuje cislo davky
	= nazev vybrane dokladove tiskarny, pokud je na terminalu povoleno a vlozeno
	= ID pracovnika, ktery data davky prevzal, pokud je na terminalu povoleno

[verze "3.44"]
--------------
Udalosti obsluhy
- Novy modul 
Vydejky
- oprava stazeni seznamu vydejek (Zustal sloupec A.NORMA z testu ... ???)


[verze "3.43"]
--------------
verze s MST_W
- Upravy NEKUPTO

[verze "3.42"]
--------------
verze s MST_W

[verze "3.41"]
--------------
Prodej
------
- rozšíøení o èíselník pracovníkù
- výstup rozšíøen o sloupec prac_id : pracovník, který data/zboží pøevzal
- czmst092 : rozšíøen o konfiguraci vyžadování zadání pracovníka

[verze "3.40"]
--------------
verze s MST_W

[verze "3.39"]
--------------
verze s MST_W
- Napojeni na DB Pervasive pomoci konektoru pro PSQL ADO.NET 3.5
	- Inventura1
	- Prijem
	- Vydej
	- Prodej

[verze "3.35"]
--------------
verze s MST_W

[verze "3.34"]
--------------
verze s MST_W

[verze "3.33"]
--------------
verze s MST_W
Informations.DetailItemNumber
- rozsireno o parametr "doklad", dle ktereho je mozne ziskat detailni informace o polozce dokladu

[verze "3.32"]
--------------
verze s MST_W

[verze "3.31"]
--------------
Moznost uzivatelskeho razeni ve webconfig pro sql dotaz v getVydejky.

[verze "3.30"]
--------------
DAtasety vydeje - parametry a opirani o ciselnik.

[verze "3.29"]
--------------
verze s MST_W

[verze "3.28"]
--------------
verze s MST_W

[verze "3.27"]
--------------
verze s MST_W

[verze "3.26"]
--------------
verze s MST_W

[verze "3.23"]
--------------
verze s MST_W

[verze "3.22"]
--------------
Inventura1
- Potlaceni zobrazeni mnozstvi k nasnimani
Obecne
- afterprocessaction 
> Vydej: oprava volani funkce
> pridani logovacich zaznamu v pripade neshody nebo chyby

verze 3.20
----------
- Vydej CZMST_SI.REZ_2

verze 3.19
----------
- verze s MST_W

verze 3.18
----------
- verze s MST_W

verze 3.17
----------
Vydej
- nova funkcionalita : predloha seriovych cisel (tabulka CZMST_SE_SN, podle implementace na Inventura1)
- Zadavani hodnoty LOCNCODE (globalni parametry vydeje - vydejparams.xml)
	> neumoznit zmenu prednastaveneho kodu
	> zakazat rucni potvzeni, umoznit pouze nactenim caroveho kodu scannerem
Emailing
- pridana funcknost dohledovani chybovych logu serveru

verze 3.16
----------
- zvyseni verze s MST_W

verze 3.15
----------
- Zbozi(CZMST095) rozsireno o sloupec REZ1 varchar(10)

verze 3.14
----------
- verze s MST_W

verze 3.13
----------
[28.4.2011]
Inventura2-Evidence majetku
- doplneno AfterProcess_Action parametry
- pridano BeforeProcess_Action parametry + BeforeProcess_Action_CommandTimeout - tato akce probiha pouze synchronne.

[28.3.2011]
AfterProcess_Action_CommandTimeout
- novy parametr nastavujici maximalni dobu cekani na provedeni afterprocess akce po vlozeni dat do databaze ...

verze 3.12
----------
[15.3.2011]
Prijem, Vydej 
- uprava funkce dotazeni dynamickych sloupcu do davek ke stazeni (1. volani je s prazdnym parametrem pro dotazeni dynamickych sloupcu)
Prijem, Vydej, Prodej, Inventura
- AfterAction presunuto na konec zpracovavacich metod, protoze dochazelo pri nedokonceni o pokus Rollback jiz commitnute transakce ...

[25.2.2011]
Firebird.SqlClient aktualizovan i u modulu XDatabase ...

[22.2.2011]
Prijem
- rozsireni o online fci - Detail, ktera zajistuje vraceni detailu objednavky z funkce 

[15.2.2011]
Vydej
- Uprava zobrazeni hlavicek vydejek, rozsireni o stav vydejky (pripraveno, rozpracovano) - dulezite kvuli moznosti ulozeni rozpracovanych vydejek k pozdejsimu dokonceni (parametr: VydejPokracovatNaJinemTerminalu=true)

[8.2.2011]
Sjednoceno volani AfterDataProcessAction pro volani Vydej,Prijem,Prodej,Inventura
	> 1CSC = nutne pridat zabalovaci proceduru pro puvodni volani procedury: 'exec xxx <@CisExpPrik>=sopnumbe' ...

Prodej
- oprava ukladani null hodnot (zejmena SKL_ID, pokud neni vyzadovano zadani cisla skladu z ciselniku)

[3.2.2011]
Pridana sluzba ServerTime pro synchronizaci casu terminalu ...

[12.1.2011]
Prodej, Inventura1
- merne jednotky

verze 3.11
----------
[6.1.2011]
- prijemparams.xml : nove parametry pro predvyplneni hodnot mnozstvi na serveru a pro deaktivaci scanneru

verze 3.10
----------
[14.12.2010]
- prijemparams.xml : popis konfiguracnich parametru
- vydejparams.xml : popis konfiguracnich parametru
- inventuraparams.xml : popis konfiguracnich parametru

verze 3.9
---------
[6.12.2010]
- Vydej - umozneno zpracovani dat s prechodem do stavu rozpracovane vydejky > soucinnost s terminalem...
	    - neni mozne kombinovat s rezimem vice terminalu x 1 davka

verze 3.8
---------
[29.11.2010]
Vydej
- vydejparams - pridan novy funkcni parametr "CONFIG_MNOZSTVI_SCANNEREM", ktery urcuje, zda je mozne na terminalech snimat mnozstvi scannerem(car.kodem)

verze 3.7
---------
[21.10.2010]
Inventura2 - Evidence majetku
- vytvorena nova funkcionalita (pevna vazba na SQL server, Strutkura pro BYZWIN-GOSVO)

verze 3.6
---------
[4.10.2010]
Vydej:
+ GenerovaniDavky - online fce
+ AfterProcessDataAction - automaticka akce po ulozeni dat vydejky
