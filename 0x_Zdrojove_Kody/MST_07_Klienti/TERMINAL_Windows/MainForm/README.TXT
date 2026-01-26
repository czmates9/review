
4.0 TaD 19.03.2019

-Vznik MSTW 4



3.2 TaD 21.11.2018
-Uprava koknfigurace a konfiguraènich souboru
-Uprava naèitavani RFID dat pomoci BeginInvoke
-Stabilni a fungujici verze


3.1 TaD 18.10.2018
-uprava stahovani a odesilani èiselniku pomocu ZIP tak jak ma 6 alfa
-uprava prihlasovani, plus overovani vuèi AD



3.0 TaD 20.7.2018
-Vytvoøeni klonu projektu Sledovani vyroby nadop. 
-Odstranene èasti vyroby a upraveny projekt pro možnost spoluprace s MSTW 6 alfa server,
-upravene stahovani èiselniku
-upravene prihlaovani do aplikace cez CZMSTPWD tabulku
-modul ZZS pro evidenci pradla zachovan na principu modulu Prodej, prace s typy dokladu


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