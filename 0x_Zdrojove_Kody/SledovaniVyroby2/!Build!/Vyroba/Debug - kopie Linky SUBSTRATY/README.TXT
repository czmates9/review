[29.01.2024] v2.41 MaR
=================

- SUBSTRATY Linky zadavani car. kodu po kliknuti prohazu
-zvyseni verze projektu


[20.10.2023] v2.40 MaR
=================

- pri vytvareni souboru Vyroba.prd dotazeni do VPP polozky s BarcodeT, oprava stavajiciho insertu o doplneni BarcodeT
-zvyseni verze projektu


[01.08.2023] v2.39 MaR
=================

- rezim prohazovani, dodelavky na robotech projekt MODBUS ..potreba odzkouset a po odsouhlaseni doplnit logiku na SUBSTRATY
-zvyseni verze projektu


[04.07.2023] v2.38 MaR
=================

- rezim prohazovani, pri zmene neukladat do Fask_Events, interni promenna inkrementace pytlu v prohazovani, priznak rezimu prohazovani
-zvyseni verze projektu

[05.05.2023] v2.37 MaR
=================

- uprava logiky nacitani caroveho kodu

[25.04.2023] v2.36 MaR
=================

- vytvorena verze E

*********************************************************************************************************


2.35 TaD 27.03.2023 
-------------
-viz readme _Modile_.Vyroba_Agro2
-viz readme _Modile_.Vyroba_Agro_Sledovani_Voziku

2.34 TaD 16.03.2023 
-------------
- Pøi zmìnì vyrobku se èarovy kod nastavit na empty


2.33 TaD 09.03.2023 
-------------
- Rozšíøení struktur tabulek
  - CZPRO_VPP
  - CZPRO_VPH
  - Production

2.32 TaD 26.01.2023 
- Uprava v interface pro Remote, pridane metody z projektu _Module_.Vyroba_Agro_SV_Modbus
-Presun tabulek do Interface èasti
- Pøesun BusinesObjektu pro API omunikaci pøesunuto do Interface èasti

-v projektu Database vytvoøene volaní metody z interface

- v APIRemoteLib pøenesene implementace metod z projektu _Module_.Vyroba_Agro_SV_Modbus
- v SQLRemoteLib pouze definované metody ale bez impelmentace

vic info jednotlive moduly readme


2.31 TaD 18.07.2022 (Verze MASTER)
-FASK_Events rzšíøení struktury o :
* BarcodeT
* REZ_1
* REZ_2
* REZ_3
* REZ_4
* REZ_5

- Upravena logika odjezdu poslení palety
- pøi odjezdu NP se sepne definované rele na definovaný èas
-Vytaženy èas do konfigurace

2.30 MaR 22.8.2022
-Konfiguraène potlaèena vizualizace
-uprava globalnych parametru L1-4 na lokalne

2.30 TaD 02.05.2022  (Verze MASTER)
- Rezim SERVIS
-Pri prepnuti do rezimu servis se ulozi event + sepne rele
-pokud je uživatel v režimu servis, tak se ignoruje scanner a adam eventy

2.29 MaR 29.4.2022 (Verze BRANCH)
- AGRO sledovani voziku status 61/62
- komunikace API SQLite

2.28 MaR 6.4.2022 (Verze BRANCH)
- AGRO sledovani voziku
- logovani posilanych dat na TISK
- uprava logiky pri posilani dat na TISK

2.29 TaD 21.03.2022
-AGRO linky
*Rozdelena konfigurace pro kontrolu kodu
-rozdelene na Read a NORead

-ukldani fo fask_events pridane do konstatt, jako text


2.28 TaD 08.03.2022 (Verze MASTER)
-AGRO linky
* Uprava kontroly scan kodu, když je NORead, tak se zapne kontrola kodu algoritmus
* Uprava lokalizace NONE za nenalezeno
* upravena logika, zmìny vyrobku, aby to nenulovalo counter
* Pridany do vypoètu delty, i zadany poèet pytlu na NP



2.27 TaD 11.02.2022
-AGRO linky
-Urava generovani SSCC pri posledni palete
- Uprava možnost prepinat pri podleni palete NP a UP pouze v zadavaich stavech
-Uprava posledni palety,
* uprava stavu potvrzeni palety
* rozdeleno na UP a NP
* pri NP je pouze NE
* pri UP je pouze ANO



2.26 TaD 10.02.2022
-AGRO linky
-pridany vstup pro ovladani paletizatoru, ale nepouživa se zatim

-posledni paleta, podle toho zda je vybrana ANO anebo NE zda odjela se uklada do zakazka N anebo A... anebo X když nic anebo F když je chyba asi
-upraveho pri posledni paleta 2 se nezvedne counter, nebo se nejedná o realnou paletu ale ignoruje se tato paleta



2.25 TaD 31.01.2022
-AGRO, linky
-Pridana vlastnost vypnout Logovani chyb, teoreticky to zlichly aplikaci 
- a na starych PC kde je maly disk, to nebude zabirat tolik mista


2.24 TaD 13.01.2022
-AGRO, linly
-pridani ukladani delty(rozdilu) do DB do fask_events
-uprava posledni palety, ukladani NP vs UP kontrola
-Uprava hlavni obrazovky, kde se zobrazuji rozdily


2.23 TaD 07.01.2022
-Modul sledovani voziku
-Konfigurace

-modul AGRO linky
-uprava logiky s generovanim SSCC
-Ukladani SSCC i k ZZ
-uklasani NULL statusu k ZZ



2.22 TaD 06.01.2022
-Uprava AGRO linky
-Upravena logika posledni palety, flag nastavovat pouze u NP
-pri posleni palete je default UP
-upravy položka status je 0 pouze kde je NP a UP
-do posledni palety je povinne zadat èislo > 0



2.21 TaD 05.01.2022
-Uprava AGRO linky
-status uklada null, jen tam kde je to chtene tak se uklada 0
-uprava logiky posledni palety, zjednocena pro odhlašovani, aj pri zmene nasledny event


2.20 TaD 17.12.2021
-Uprava konstant NP, UP a ZZ kde všude se propisuji a kde ne
-pri posledni palete a pamatovani predchoziho Vyr. Prikazu, tak pridano pamatovani i BracodeReaded a BarcodeSended


2.19 TaD 16.12.2021
-Uprava zapisu vahy, aby byla vždy 0 


2.18 TaD 15.12.2021
-Uprava AGRO
-posledni paleta logika
-ukladani info efentu pri 
*kontrole kodu
* pokud je okno posledni palety otevrene, ale dojde pytel
-uprava ukladani stavu NP a UP
-novy informativni stav ZZ


2.17 TaD 08.12.2021
-Uprava AGRO, posledni paleta
-kontrola vuèi QTYPACK zadana hodnota
-pridany button, ktery urèuje zda je to NP anebo UP


2.16 TaD 02.12.2021

-Rozšíøení SQL strukur
    FASK_Events
        QTYPACK – pøepoètový koeficient
        PackType – UP (uplná paleta) anebo NP (Neuplna paleta)
        WEIGHT – Váha
    CZPRO_VPP
        BarcodeT – pøíznak zda sledovat è.kod

-Pøi zmìne výrobku si zapamatuje výrobní pøikaz a nasledne když dojde paleta a ukádá se event „posledni paleta 2“ tak se vezme pro insert zapamatovaný vyrobní pøíkaz
-Pokud dojde k naètení EAN kodu z scanneru, tak se skontroluje zda obsahuje pouze èísla. Pokud ne, tak se použije EAN který je v pamìti jak pøedchozí naèten.
-Pøídana kontrola pøiznaku BarcodeT zda je nenulový a obsahuje èíslo 1. Pokud ano tak se vykoná kontrola EAN kodu. Pokud ne tak se cela logika pøeskoèí.
-pøi ohlašení je vždy vyžadovana posledni paleta
-pøi posledni palete je pøidan nový stav, kdy to požaduje potvrdit odjez posledni palety
-Upravene nazvu stavu zmìna vyrobku scan a zmìna vyrobku manual
-uprava stavoveho automatu posledni paleta, kde je dotaz zda odešla posledni paleta



2.15 TaD 12.11.2021
-Uprava logovani, sice to fungovalo ale pri debugu to hazelo otravnì chybu

-Uprava okna poslendi paleta
* pridani stavovy automat pro klavesnici aby se mnela podle stavu zadavani
* pridano ošetøeni že musi byt vyplneni obou hodnot

-Uprava zadavani posledni palety jak pri zmìne tak pri odhlašení



2.14 TaD 20.10.2021
- Uprava chyby pro AGRO
- vypoèty kolik je a kolik zbyva na pøikazu

-Nove okno pro poslední paletu
-pouze pri zmìne vyrobku
-nove eventy do FASK_Events

-upravz procedur do events trigru


2.13 TaD 07.10.2021
- AGRO
-Upravy s ovladanim houkaèky


2.12 TaD 01.10.2021
- AGRO
- konfig parametr kde urèuje kde je zdroj pro BarcodeSended
- upravene zobrazovani zadanej položky z vyr. pr. Zobrazovany je pozue nazev
- do èasti kde se zobrazuji palety, tak pridane zobrazene množství z vyr. pr 
- a realne dopoèitavane množství které zbívá

-upraveno zadavani vyr. pr. kde se vyhledava položka pouze na zadanem VPH


2.11 TaD 21.09.2021
Z duvodu AGRO modulu, rozšiøeno o struktury MES7 - Odvadeni vyrovy VPP a VPH
-vytvoøena synchronizace
-Pridano zadavani VPP a VPH informací
-konfiguraènì zapnutelne kontroly naskenovaneho kodu vuèí vyr. prikazu

-Pri zmene vyrobku se zadava novy vyr. prikaz a polozka



2.10 TaD 03.09.2021
-Predelane na 32Bit release
-Predelane FASK_Logins struktura do MES7 univer struktur
-Opravena zmena šarže pod heslem, bylo to velky špatny...
-odstranene pdb z release


2.9 TaD 31.8.2021
-Pridan projekt AGRO2 y stareho øešení
-Vytvoøen novy projekt Palety_SSCC pro generovani SSCC

-rozšíøešene struktry FASK_Evenets
-Predelane komunikace v SQLCECommLib a SQLCommLib z table adapteru na pøimo dotaze


2.8 TaD 4.4.2018
-Vytvoøeni klonu a preneseni projektu do VisualStudia 2010
-Uprava všech projektu vramci tohoto solution aby smerovaly do rovnakeho !Build!
-Založeni noveho modulu pro ZZS na zaklade modulu Prodej z MST 6 alfa
-Využitit Komunikaèneho serveru z MSTW Kom Server 6 alfa

:: ZZS :
-Vytvoøen modul pro ZZS,  ktery pracuje davkovym spusobem
-Udelan hlavý Form do ktereho lze pridavat tlaèitka
-Na každe tlaèitko je navazany typ dokladu pod kterym se vykonava dávka

**Pridane Stahovani èíselníku : 
-Odberatele (nepouziva se zatim...)
-Pracovnici (nepouziva se zatim...) 
-Sklady
-TypDokladu (Nejduležitejší)
-Zbozi (slouži na dotahovani informaci o položke podle ITEMNMBR ked se nasnima EPC anebo podle CZCarKod, VNDITNUM)

-sdf soubor ktery je zahrnut v projektu
-Prodej.sdf (jak šablona pro tvorbu davky)

** Vytvoøen Univerzalni Form pro spracovani nasminanych dat budto RFID nebo carovym kodem
-Predavany parametr do tohoto formu je Typ dokladu (ktery je definovany jak ENUM TypPohybu v RFID_Data.cs)
, èislo davky a dalsi potrebne infrmace....

-Pro nacteni RFID je implementovat Interface IRFIDProvider pro praci s RFID
-implementovany sou dve knihovny : Pro DataLogick a SR_5102

-Po otevøeni davky se automaticky zapne RFID ètení ktere je signalizovano zelenim podbarvenim tlaèitka
-Pokud se naète RFID Tag tak se jeho hodnota EPC zobrazi v levé èasti s count hodnout kolikrat se už naèetl
-EPC hodnota se bere jak SERLTNUM (seriove, jedineène èislo tagu) 

**Logika** RFID
-po prvem naèteni hodnoty  EPC se online dotazem zistí stav v Lokaènim mechanizmu
-pokud lokacni mechanizmus vrati null tak nastala nejaka chyba tak se do vystupni tabulky DI vyplni pouze zname informace
-ale pokud Lokacni mechanizmus vrati relevantni informace tak se pokraèuje,
-pokud nevrati žadní øádek tak se do DI nevloží nic, protože položka nebyla nalezena na sklade
-pokud se vrati nejake informace tak se vezme první øadek, ten je prohlašen za spravny (mnelo by to vratit iba jeden øadek)
a na ten se vyplni do DI aby na Kom servery mohl byt vykonan pohyb.
-Pokud je najdea položka v Lok. Mechanizmu tak se pokouši dohledat informace v 095 (èiselnik zboži)
-pokud najde tak vyplni relevantne informace, pokud nenajde tak nevyplni a o zbytek se postara Kom Server

**Logika** Skener
-po naèitani caroveho kodu naplni dataset z 095 pouze s èarovym kodem položku
-pokud nenajde žadnu nebo víc jak jednu tak neudela nic...
-pokud najde prave jednu položku tak ju vloži
-Otevøe dialogove okno kde zažada vložit množstvi
-po vloženi množtvi nahraje nasnimani kod do leve èasti s množtvím 1 jak novy øadek
-nasledne vloži všechny nalezene informace do DI tabulky


-Dale tento Form obsahuje možnost zmazat položku v prave èasti.
-pokud se smaže položka v prave èasti a už je minimalne jednou naètena v leve èasti tak se do prave èasti už nenaète
-pokud se naète davka ktera je na disku rozpracovana (pokud nastane nejaka chyba a zustane tam) tak se data mužou naèist duplicitne, pak je nutno duplicitu ruène smazat, nebo pri odeslani zahlasi chybu

-Pro tento projekt byl upraven Kom server aby byl zaroven take PrintServer, proto projekt 6alfa už neobsahuje separatny print server
-Tento univerzalny Form take obsahuje tlaèitko Tisk, ktery vytiskne Report pro dany Typ dokladu s datama ktere su momentalne nasnimane v prave èasti.

-Po odeslani davky dojde k pohybom v lokaènim mechanizmu a automatickemu vytisknuti protokolu na A4
-možnosti opusteni tohoto formu su iba dve, budto odeslat davku anebo smazat davku...

**Konfigurace
-v konfuguraci projektu vznikly dve nove podzaložky,
*ZZS
-Obsahuje nastaveni adresy na Kom. Server
-Obsahuje nastaveni Serioveho portu pro RFID

*Update
-pokus o preneseni updatovaciho mechanizmu z CE, MSTW6 alfa na windows form aplication,
-rozdelane, otestovane na MSTW ale netestovani na tomto projkte

*****frmMainApp*****
-Pridany prehled všech otevøenych modulu
-Zakazani kiosk modu
-do Program.cs pridane Killnuti procesu explorer.exe, to zabespeèí fullscreen programu (cez preprocesor spustitelne iba v release, pri debug explorer nekillne...)



2.7 Ta.D 28.8.2017
-v ICommDatabase/ICommDatabase.cs rozrireni metody  UserEventsInsert o rez_2
-v Vyroba/frMainApp.cs pridano do insert rez_2
-SQLCECommLib/main.cs upraveno pridani rez_2
-SQLCommLib/main.cs upraveno pridani rez_2

-v Database/Vyroba.xsd rorsireni tabulky FASK_UserEvents o rez_2 o maximalni delke 25 znaku
-SQLCECommLib/DSVyroba.xsd pridano do do tabulky FASK_UserEvents o rez_2 o maximalni delke 25 znaku
-SQLCommLib/DSVyroba.xsd pridano do do tabulky FASK_UserEvents o rez_2 o maximalni delke 25 znaku
-v ICommDatabase/DSVyroba.xsd rorsireni tabulky FASK_UserEvents o rez_2 o maximalni delke 25 znaku
-ve všech datasetoch skontrolovane SELECT,INSERT,UPDATE,DELETE a ostatni aby spravne fungovaly


2.6
- verze s upravou modulu

2.3
- pridano znemozneni uzavreni aplikace v pripade, ze je prihlasen nejaky uzivatel.