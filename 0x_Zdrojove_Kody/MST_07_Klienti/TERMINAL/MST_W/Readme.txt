****************************************************************************************************************************************
Popis zmen verzi
================

[verze "7.76"] TaD (25.04.2023)
-------------------

- vytvorena verze E

*********************************************************************************************************

[verze "7.55"] TaD (26.01.2023)
- benevoletní sběr dat
- Prijem, implemntovana čast CZ_SerNum_Track = 11
- Prodej, implemntovana čast CZ_SerNum_Track = 11
- Vydej, implemntovana čast CZ_SerNum_Track = 11


[verze "7.54"] TaD (29.11.2022)
- Vydej s predlohou
  - nová funčnost, přídan dotaz pro přeskočeni zadavani rozmeru + vahy
  - konfiguracne vyplnutelne
  -za chodu prepnutelne

[verze "7.53"] TaD (03.10.2022)
- Parsovani, přidana nová varianta pro SAB, jedná se o dodavatele BELDICO
  - nedodržovaly normy ohledně datumu v GS1
  - norma v GS1 je YYMMDD a oni mněly YYYYMM
  - místo dne se dava natvrdo 28, protože unor
- Přídano do multipars podminek všude

-přidána kontrola do FIFOFEFOonline, kontrola zda byla nalezena šarže online
-přidana kontrola, pokud se kod parsovaní, zda obsahuje šarži vyplnenou. Pokud ne, požaduje ji zadat


[verze "7.52"] TaD (19.09.2022)
- Předelani Interface na zdileni medzi serverem a konzolu

[verze "7.51"] TaD (15.09.2022)
-Parsovani, odtraneni interface ICodeSerltnmbr
 * duvod, že vždy rozhoduje příznak sledovaní, co se chce sledovat
 * pokud je to SN tak je AI 21
 * pokud je to šarže tak je AI 10
-proto je tento interface zbytečny, a akorat delál chaos v vracenych datech
-pokud chce byt nejaka logika tak na urovni aplikace, ne na urovni parsovaní třídy

-Vydej, uprveno zobrazovaní předlohy
- přidany order podle QTYPACK aby hlavní MJ byla vždy první a zobrazována v seznamu

-Vydej, režim hromadneho generovaní balíku. Přidana kontrola na přeplnění



[verze "7.50"] TaD (31.05.2022)
SAB, uprava parsovaní 
- Australan
- Neznamy KOD
* Pridane interface pri ISarze, z duvodu vydeje

[verze "7.49"] TaD (13.05.2022)
-Uprava inventura,
-i kdyz že parsovani GS1 kod, a obsahuje AI 30
- tak je možnost editovat množství


[verze "7.48"] TaD (21.03.2022)
-revize kodu

[verze "7.47"] TaD (28.02.2022)
-SAB
* uprava parsovani GS1
* pridán AI 240
- InnovaMedical, taky zahrnut AI240


[verze "7.46"] TaD (17.02.2022)
-Vydej, přidany novych konfiguračnych parametru
* přeskočit dotaz na tisk soupisu
* automaticky davat množstvi 1 při tisku soupisu

* automaticky přeskočit okno "pokračovat na jinem terminalu"
* pridane vybyratko, zda Ano/Ne pokračovat na jime terminalu při přeskočeni okna

-Parametry implementovany do kodu, aby delaly to co maji



[verze "7.45"] TaD (26.11.2021)
-Uprava parsovaní kodu GS1, pridany AI 93
-Vytvořena nová parsovací metoda pro GS1 která obsahuje Expiraci kde den je 0 potom se dává 28 natvrdo

-Prijem:
* pridána nová varianta CZ_SerNumTrack = 10
* zde je sledovana SN aj šarže(SttributeToSN)
* tabulka PI rozdirena o AttributeToSN

-Prodej:
* pridána nová varianta CZ_SerNumTrack = 10
* zde je sledovana SN aj šarže(SttributeToSN)
* tabulka DI rozdirena o AttributeToSN
* Pridana možnost na typu dokladu potlačit zadavani AttributeToSN i když je CZSerNumTrack = 10

-Vydej
* Podle typu sledovani upraveno dotaženi hodnoty z parsovaci logiky



[verze "7.44"] TaD (03.11.2021)
-Prijem, uprava prace s objektem palety pokud se neřeší
-uprava prodej, automatika pri stahovani davky a žadna neni

-API odesilani souboru, prace s ceritikatem



[verze "7.43"] TaD (21.09.2021)
-Prijem s predlohou, implemnetcace možnosti tisku paletoveho listku a paletove etikety
-Doimplementovano, netestovano

-Vydej, imlementovana logika hromadne vykrivani baliku + tisk
- V menu se musí ručne prepnou mod...

-Vydej: nova tabulka CZMST_SI_BV, kde se ukladají vlastnosti baliku
-přenaší se až do SQL serveru
-tiskne se na soupis


[verze "7.42"] TaD (28.07.2021)
-Prijem s predlohou, implemnetcace možnosti tisku paletoveho listku a paletove etikety
-Zatim se nepouživa


[verze "7.41"] TaD (30.06.2021)
-Inventura, lokalizovano když se zadava šarže/sn aby text odpovidal
-oprava chyby, když se napipnul složeny kod, tak ho to chtelo pak zadat znovu



[verze "7.40"] TaD (28.06.2021)
-Vydej, FEFO/FIFO uprava lokalizace když je šarže anebo SN

-Prodej, pridane nove parametry do typu dokladu
-I když je karta sledovana na šarže, SN anebo expiraci tak je možnost natvrdo na typu dokladu toto sledovani vypnout



[verze "7.39"] TaD (14.06.2021)
-Uprava, parsovani kodu, australska norma + když je datum pouze 2 číslama, tak pridany pattern 2099

-uprava tisk Prodej a Prijem, tisk GS1, predavany parametr, přiznak sledovani



[verze "7.38"] TaD (25.05.2021)
-Vydej, ošetřeno všude že VNDITNUM muže byt null, protože v IS POHODA Hanibal použiva dopravu(Služba) která nemá vyplnené č.kody


[verze "7.37"] TaD (20.05.2021)
-Parsovani kodu, do expirace pridano možnost parsovat až do roku 2099 kedže rok se vyjadruje v dvou čislech

-Odstraneno omilem podminen online expirace SN

-Do parsovana pridany novy Interface
-tenhle interface v sobe nese informaci o šarži
-upravene všechny GS1  alternativy, ošetrena chyb
-Parsovani kodu, byla zde chyba v GS1 kodoch a variantach, kde se to zacyklovalo
-uprava multiparse okna, kde do 10 zobrazuje šaržu
- a u 21 zobrazuje SN

-GS1 kod hodeny prioritovo nad HIBC


[verze "7.36"]
TaD (06.05.2021)
-Online kontrola expirace na vydeji
-Oprava ruznych chyb ktere vyplynuly


[verze "7.35"]
TaD (09.04.2021)
-FIFO/FEFO, UserFriendly zmena hlašky 
-při stahovaní a mazani souboru na serveru byly TimeOut natvrdo 1000, to je asi hodne malo
-Změneno aby prebiral hodnotu nastravitelnu v konfiguraci

[verze "7.34"]
TaD (03.03.2021)
-Vydej, predhled doplnený o MJ


[verze "7.33"]
TaD (25.02.2021)
-Verze Dašenka
-Prodej, tisk soupisu, rozšířen o predavani MJ
-Vydej, nova varianta tisku, ala TierraVerde, kde se tiskne rozdilovy soupis, ale včetně nulovych položek

-oprava:
konfigurace, ukladani parametru DisponibilityHlaska a DisponibilityZvuk



[verze "7.32"]
TaD (09.02.2021)
-Uzavření verze Cipisek

[verze "7.31"]
TaD (02.02.2021)

-Vydej s předlohou, chyba pri odmazavi položek v default. Chyba pri kopirovani delete sql dotazu
- tisk etikety na Balik. Pri dotahovani položky byla chyba že když je stejna položka, tak i tak to dotahhlo všechny SN... odkaz na ORD as SOPNUMBE přidán

-TIsk na inventure, pridana možnost vyvolat pomoci F6

-Parsovani Expirace ve formatu RRMMDD, pokud se zadalo napr. 301030 tak se očekavalo 30.10.2030 ale parsovani vyplulo 30.10.1930
-uprava pomoci System.Globalization.CultureInfo objektu ktery nese parametr TwoDigitYearMax kde se nastavil 2099
-Upraveno na Inventure, Prijmu, Vydeji a prodeji

-Vydej množství po jednom na tlačitko 4

[verze "7.30"]
JiS + TaD (19.01.2021)
-Vydej
	* Vytvořena nova kopie logiky, kde je pridana logika prelokovani zbytku

-Vydej
    - množství z lokace předávat dále a nabízet toto množství z lokace 
    - zbývající množství (lokace - výdej) pro přelokování neumožnit editovat

[verze "7.29"]
TaD (09.12.2020)
-Uprava struktury typu dokladu, disponibilita
-Odmazane historice prvky a pridany jeden novy, který určuje že sa kontroluje disponibilita z zdrovoveho skladu

-Oprava kontoly disponibility... pretypovavani na decimal, kdy metoda vraceja občas INT64 a občas Double


[verze "7.28"]
TaD (09.11.2020)
-přidaná/předelaná funkčnost F10 na volném pohybu
-online pohled na stav LokMech
-Konfiguračně nastavitelné



[verze "7.27"] 
TaD (03.11.2020)
-Parsovani Prodej, pri zadavani mnozstvi a šarže
-možnost vypnout parsovani
-pridana logika parsovani + ptani se

-Konfigurace, opravene ukladani parametru pro Generovani Dokladu po napipnuti kodu

-Vydej, pod F10 jedna z variant je okno kde se zobrazi stav v LokMech
-po vyberu pomoci "enter" je možno daný řadek rovno vybrat a vykrit

[verze "7.25"] 
TaD (21.10.2020)
-SAB, vytvořene další prarsovani pro BALTON kody
-uprava multipars okna, ktere už akceptupe všechny kody založene ná GS1 , i když jsou to custom

-Uprava volneho pohybu, aby mnel vlastnosti jak ostatne moduly

-při parsovani rozdelene SN a šarža

-Při uprave nasnimaneho množství na vydeji, upraveny algoritmus vypočtu, aby to bylo spravně


[verze "7.24"] 
TaD (16.10.2020)
-Expedice, baliky, počitani baliku, další upravy...
- Vydej, uprava množství v nasnimanych



[verze "7.23"] 
TaD (25.9.2020)
-Automatika na Prodeji, upravy
-Expedice, baliky, počitani baliku

[verze "7.22"] 
TaD (11.9.2020)
-Přidany novy parametr, pro vypnuti/zapnuti FIFO/FEFO na vydeji

SCANNER, opravená velka chyba, provider MC3090 natvrdo orezaval delku stringu na 55 znaku...


-Přenesená automaticka z MSTW6, plus trošku upravena
-Na prodeji byly chyby s praci l Lokalnou DB, upravene...


[verze "7.21"] 
JiS (22.7.2020)
- Inventura, nasnimane zobrazeni hodnoty expirace
- Online gorazeni expiraci pro rucni vybery
+ expirace vlastni dialog pro scanner / InputBox => InputBoxExpirace (pridan scanner parsing na typ ICodeExpiration)

TaD (17.7.2020)
-Implementovane zracovavani Expirace na :
-Vydej + (FIFO FEFO logika...)
-Prijem
-Invetura

-Vydej a prijem, nasnimane rozšířene o pohled na expiraci


[verze "7.20"] TaD (23.6.2020)
-Parsing, implementovana australna norma pro SAB
-preneseny BugFix z Bobeše

[verze "7.19"] TaD (09.06.2020)
-Inventura, při nasnimani parsovaneho kodu, se kontroluje šarže vuči datum v CZMST_I2

[verze "7.18"] TaD (03.06.2020)
-Otestovane parsovani projmu
-Rozpracovane parsovani na Inventure

[verze "7.17"] TaD+JiS (02.06.2020)
JiS-Parsovani GS1 kodu implementovane další možnosti
TaD- parsovani a vyplnovani na přijmu


[verze "7.16"] TaD (28.05.2020)
-JiS vytvořil logiku parsovani čarovych kodu
-přidal možnosz multiparsvani

-Rozpracovany pokus k parsovani na Prijmu....

[verze "7.15"] TaD (XX.XX.20XX)
- ServerCertificateTrust : modifikace kvuli spousteni, kdy online akce pri spusteni nebyly korektne nastaveny

-Kopletene predelana Inventura1 a otestovana
-Predelany Prodej

-Predelany Uživatele, Tady byl kompletne odstraneny TableAdapter a nahrane metodama v Controleru(jednalo se jen o 3 SELECTy) + Objektove Parametry
-Predelany Prodej, Tady byl kompletne odstraneny TableAdapter a nahrane metodama v Controleru + Objektove Parametry
-Predelany TypyDokladu, Tady byl kompletne odstraneny TableAdapter a nahrane metodama v Controleru + Objektove Parametry
-Predelany Strediska, Tady byl kompletne odstraneny TableAdapter a nahrane metodama v Controleru + Objektove Parametry
-Predelany Sklady, Tady byl kompletne odstraneny TableAdapter a nahrane metodama v Controleru + Objektove Parametry

-ODBERATELE, moc navazbeny na SERVIS, nutno predelat cely servis, pak se pujde zbavit odberateluTableAdapteru

-Přidany Controler pro Ukolovani, Tisky, Inventura2
-Vytvořeny GlobalObjekt pro Ukolovani
-Včechny Datasety přenesene do Complete, a puvodne zbavene TableAdapteru, ktere jsou přeneseny do zvlašt třidy
-Kde to šlo, tak sem přepsal metody ručně, a zbavil se TableAdapteru(exclude z projektu)

-Problem je v Servisu, tam je spusob budto davkovz anebo NEdavkovy tak nevím jak to předelat když nebude davkovy tak nemužu použit GlobalObjekt s davkou...

-Servis predelany tak aby šel projekt přeložit, ale chce to ešte dotahnut a dodelat.. je to zamotanější než sem si myslel....


-Predelana Inventura2 na praci s Controlerem
-Predelana Inventura1 aby nemnela TableAdaptery
-Predelany Prijem aby nemnel TableAdaptery
-Predelany Vydej aby neobdahoval TableAdaptery

-Inventura2 odstraneni TableAdapteru

-JiS predelal servis na praci s controlerama

-Ostraneni Servis TableAdapteru

Terminal, Prejmenovani SQLCEDBs složku na SQLiteDBs 
-Predelani NAMESPACE datasetu ( + v celem projekte)
-Predelani tridy Controleru na zdilene


[verze "7.14"] TaD (03.03.2019)
-Vytvořena nejaka prvni verze SQLite verze MST(ted už asi MES), 
-Sprovozneni Vydej a Inventura.


[verze "7.13"] JiS (XX.XX.20XX)
- Prevod na sqlite databazi

[verze "7.13"] TaD (XX.XX.20XX)
-prebrane z MST_RF, v main formu a v Logins formu se zobrazuje Nazev terminalu a IP terminalu


[verze "7.12"] TaD (26.11.2019)

-Uprava pri stahovani čiselniku, ošetřeni zrušeni okna pri stahovani
-v Main, pridane načteni konfigurace PRODEJ, pro zabraneni dotazu exportu čiselniku v hlavnem menu


[verze "7.11"] TaD (23.10.2019)
-Uprava, Datasety uzivatele, odstraneni MaxLenght

[verze "7.10"] TaD (15.10.2019)
-Verze Bobeš ALFA

-Uprava, PrijemFormReport presunit do Forms, využiva se pro Prijem a pro Prodej

-Typ dokladu rozšiřen o cfg_Navrh
-Prodej : funkčnost Sklad/Expedice/Rozdelit 
-Uprava prehrani zvuku na synchronne

-Prodej, vyber odberatele, Select prvniho řadku

-ProdejList - kompletne okomentovany cely Form a roztrideny


[verze "7.9"] TaD (7.10.2019)
-UZAVŘENI verze AMALKA


[verze "7.8"] TaD (2.10.2019)
-Prodej, po uspešnem načteni položky se ma prehrat zvuk UspesneNacteni.wav
-pridana možnost, když neni vyplnen tak se nepřehraje, a v settings je parametr 'PrijemSoundUspesneVlozeni' kterym je možnost zmenit zvuk


[verze "7.7"] TaD (10.09.2019)
-Update všech webreferenci na terminalu
-PrintProviderWebService , pridana  nova webreference na TiskTest
-PrintProviderWebService upravene umisteni webmetod na serveru


[verze "7.6"] TaD (8.08.2019)
-Pridane prasovani kodu wahoveho ale jen 12 znaku
-Uprava parsovani vahy NE norma, ale pouze z 5 znaku / 10



[verze "7.5"] TaD (7.08.2019)
Update webreferenci
-Oprava Prijmu odeslani davky
-Oprava Vydeje vraceni davky



[verze "7.4"] TaD (10.07.2019)
-Sjednoceni verze s Serverem
-Upravy v FormCongig ukladani Hromadne SN
-ListPolozek Vydej, zakomentovana čast pro tisk adresy pri sloučene davce



[verze "7.2"] TaD (10.04.2019)


-Prijem, pridana možnost konfiguračne pri tisku prebirat mnozstvi vytisknutych etiket s zadaneho množství
-Volny pohyb, pridana možnost konfiguračne pri tisku prebirat mnozstvi vytisknutych etiket s zadaneho množství
 
-Volny pohyb, pridana možnost navazat na F10 podle konfigurace ruzne vlastnosti

-Volny pohyb, PrijemList, StatusBar, ostranen Sklad, a pridano T ktere je bud 1 Tisk, anebo 0 NETisk

- Volny pohyb, Vyber odběratele, pribudla mořnost ONLINE dohledavat pomoci ICO


[verze "7.1"] TaD (20.03.2019)

-Sjednoceni logiky tisku v PrijemList a ProdejList



[verze "7.0"] TaD (18.03.2019)

-První verze projektu MST_W 7, odštepek od puvodniho MST_W 6 alfa






ALFA- vyvojarska verze projektu
================
[verze "6.140"] JiS (28.02.2019)
Konfiguracni dialog
- Pocesteni neceskych vyrazu
- Redesign polozek 
	> zruseny Anchors na Right, kde to slo
	> autoscroll na tabpages vsude

[verze "6.139"] TaD (15.02.2019)
-Uprava logiky tisku Alternativnych štitku anebo defaultnych, 
a zda se ma tisknout defaultni anebo alternativny s Dotazem anebo bez dotazu

Upraveno na modulu Prijem a Volny pohyb


[verze "6.138"] TaD (14.02.2019)
-PRESKOČENI VERZE, duvod: sjednoceni s serverem

-Vznikel mechanizmus ktery dotahuje s sdf souboru/ definici MaxLenght pro jednotlive prvky v tabulkach
-Nasledne se to použiva na orezavani a kontrolu zadavanych hodnot

-Uprava všech DataSetu, odstraneni prvku MaxLenght ktery ořezaval stringy
-Uprava kodu všude kde bylo natvrdo v kode nastaveno MaxLen 
-ANEBO všude kde se to rebiralo z DataSetu tak se používá nová funkcionalita

-Prijem a prodej Sjednoceni dialogu tisku s Cenout pri načteni položky anebo ručně cez menu


[verze "6.134"] TaD (12.12.2018)
-PRESKOČENI VERZE, duvod: sjednoceni s serverem
-Uprava scaneru, možnost prepnut na režim kontinualniho scanovani, 
"Prepinatka" sou definovana:
-Hlavni menu
-Volny pohyb, 
*volba odberatele
* hlavni list
Prijem Hlavni list

-Uprava stahovani dat, sekali se vlakna a pak padala aplikace

[verze "6.126"] TaD (05.12.2018)
-Uprava ukladani parametru Scanner
-Uprava tisku Vydej
-Uprava volny pohyb, tisky Hanibal
-Uprava Odberatele, možnost scannerem vybrat položku a online dohleda odběratele(dodavatele)
-Tisk, pridan možnost tisk s Cenou nebo bez ceny (řešeni Hanibal)


[verze "6.125"] TaD (06.11.2018)
-Uprava tisku SN v časti Vydej

[verze "6.124"] TaD (30.10.2018)
-Vydej, tisk, pridana funkce pamatovani Rozmeru + vahy naposledny zadanej
-Tisk baleni, pridana hodnota Doprava z pohody dotahovana online pro každu položku cez server pomoci procedury



[verze "6.123"] TaD (25.10.2018)
-Vydej, konfiguračne možno potlačit DialogDavkaNenalezenaVygenerovat a DialogNaDiskuNejsouDavkyStahnout 


[verze "6.122"] TaD (24.10.2018)
-Uprava konfigurace, Textboxy aby se spravne zobrazovali
-Pridana možnost na modulu Vydej prepnout pomoci menu režim snimani SN



[verze "6.121"] TaD (17.10.2018)
-konfiguračne možno povolit nebo zakazat zobrazeni tlačitek v moulu expedice
-pro hromadnem generovani SN nelze zadat 0
-do predlohy SE vydej se prebira číslo objednavky odberatele VNDDOCNM
-Vydej, list položek  tisk paletove(balikove)
*pridany tisk adresy odberatele
*pridany tisk čisla objednavky odberatele



[verze "6.120"] TaD (10.10.2018)
-možnost konfiguracne potlačit tlačitka v modulu Expedice
-Hromadne SN, nelze zadat pocet nula
-Tisk I-Tec, v hlačičke je čislo objednavky odberatele

[verze "6.119"] TaD (05.10.2018)
-Uprava Vydej Tisk Baleni, uprava vypočtu velkosti štitku
-v Settings.xml sou nove prarametry :
      <add key="Vydej_Baleni_Tisk_Font_Size_Width" value="14" />
      <add key="Vydej_Baleni_Tisk_Font_Size_Height" value="15" />
      <add key="Vydej_Baleni_Tisk_Font_Size_Medzera" value="8" />
      <add key="Vydej_Baleni_Tisk_Font_Size_ZnakuNaRadek" value="85" />
Pro nastaveni tisku Baleni


[verze "6.118"] TaD (04.10.2018)
-Expedice, tisk pomoci serveru, predavane parametry do tisku, nazvy šablon a nazev tiskarny


[verze "6.117"] TaD (27.09.2018)
-Povoleni wait dialogu


[verze "6.116"] TaD (26.09.2018)
-Sestavena verze "nejaka" pro I-TEC...


[verze "6.115"] TaD (24.09.2018)
-od JiS uprava referenci čiselniky +
: 
zip oprava 
terminal maze pres filetransfer.deleteciselnikonserver



[verze "6.114"] TaD (24.09.2018)
-Upravene na modulu vydej Grupovani položek pro I-Tec s vypisovani SN za sebou v řadku
-stejne grupovani udelane pro tisk Baleni i soupisky 
+ upravy od JiS a TPr ktere nejou popsane...


[verze "6.113"] TaD (18.09.2018)
-Upraveni modulu Expedice pro I-Tec aby bral s Balenibuffer hodnoty


[verze "6.112"] TPr (12.09.2018)
-oddelana webreference filetransfer
-pridelane seviceSessions pro vsechny webreference
-pridane zipovani ke vsem ciselnikum
-predelany upload logu, pridane zipovani


[verze "6.111"] TaD (12.09.2018)
-Uprava konfigurace preneseni Dialogu z projektu MSTW6
-pridani modul vydej konfigurace zobrazeni dialogu po nasnimani položky
-modul vydej do detailu nasnimane pridany pohled na MJ
-modul vydej do detailu polozky pridany pohled na Note a MJ
-modul vydej odeslani davky po ukončeni ptat se na odeslani davky


[verze "6.110"] TaD (4.09.2018)
-Uprava Dispose v Formoch kde se volaji jine formy ale opakovane a sou jak promenne Globalne
-Uprava v nekterych formoch nebyly Using u vytvareni form objektu

[verze "6.109"] TaD (30.08.2018)

-Uprava Linkovani skeneru, ošetreni když se chce nalinkovat delegat na skener a už je nejaky nalinkovany tak to hodi chybu a nevykona



[verze "6.108"] TaD (29.08.2018)
-Vydej, pridan tisk Paletovych listku a Soupisky
-Konfiguracne mozno zadata ID sequence pro generovani SSCC kodu na vydej pro Balik/Paletu/Prepravnu jednotku.... 
-Konfiguracne možno povilit hromadne zadavani SN plus možno nastavit podlednych N znaku jak numeric
-Synchornizace času presunta pred licence z duvodu když je na čtačke špatny čas tak se nekorekne načte licence a hlasi DEMO
-Modul Prijem upravena hlaška když neexistuje Sklad.sdf tak nelze spustit modul Vydej a Prijem. Upraveno do lepší podoby



[verze "6.107"] TaD (15.08.2018)

-Modul Vydej, pridana možnost hromadneho zadavani SN + generovani SN primo na čtečce


[verze "6.106"] TaD (31.07.2018)
-uprava logovana innerexception v downloadfile
-Upravene stahovani, ošetrene deleni nulou ktere by nemnelo nastat ale nastalo
-Uprava v Inventure form Nasnimane2 pokud nejou žadna data tak neni co k zobrazeni a hadzeloto nullrefren exception, opraveno/upraveno


[verze "6.105"] TaD (26.07.2018)
-osetrena varianta nevraceni zadnych dat z SQL pri nerealizovanych prijemkach


[verze "6.104"] TaD (03.07.2018)
-konfiguračne možno potlačit dialog ktery se pta zda vytisknout štitek
-v PrijemList vynikla možnost bud m menu nebo klavesovou skratkou 5 vypnout nebo zapnout tisk štitku
-pokud je zapnute automnozstvijedna tak se vytiskne pouze jeden štitek
-pokud je vypnuto tak se se pta na zadavane mnozstvi


[verze "6.103"] TaD (25.6.2018)
-Rozsireni tabulky typy dokladu o pole cfg_delka_SN
-do Trace pridana Hlavička souboru, prehlednejsi pri zpracovani
-Pridana trida pro programove zistovani verze dll souboru

-Prodej List, rozsiren o novy detail nasnimanich podle verze MSTW6




[verze "6.102"] TaD (8.6.2018)
-Modul prijem, pridano zobrazovani reportu ktere zobrazuje rozdeleni množstvi prijateho na Sklad, na expedici popr. rozdelit.
-Pridana možnost aby bylo automaticky zadavano množství 1
-množstvi jedna se prepina klavesovou skratkou 4
-pridano konfiguračne zda se bude nebo nebude zobrazovar report
-pridano konfiguračne ci se bude rozhadzovat na sklad nebo na expedici
-pri rozhodovani kam bude umisteho zboži se prehravaji 3 zvuky
-jmena tychto souboru lze nadefinovat s settings.xml
-pokud je zadavani po 1 aktivovano nebude se zobrazovat report (nema vyznam protoze se jde bud jenom na sklad nebo jen na expecidi)
- v status baru je oznaceni S:1 nebo S0 pro identifikaci stavu zadavani mnozstvi
-Upravea chybova hlaška. na modulu Prijem když neni stažen čiselnik Sklady.sdf tak to lidsky upozorni.
-Upravene slučovani davek, uprava CountEntries na string v kodu čtečky, na strane serveru je pořad brane jako int

-V okne nerealizovane přijemky je možnost konfiguračne v okne v menu povolit nebo zakazat dotahovani všech nerealizovanych prijekem hned pri otevřeni okna nebo až po možnosti hledat čarovym kodem
-pridana možnost dohledavat objednavky podle položek v nich obsaženych

-Modul vydej, opravena chyba pri odesilani davky, ked nastane chyba tak je možnost odeslat znovu


[verze "6.101"] TaD (1.6.2018)
-Upraveny printer factory ked parsuje XML a obsahuje komentar tak to už nehodi chybu, 
- duvod chyby že coment je typu XmlComment ale node je typu XmlElemment



[verze "6.100"] TaD (31.5.2018)
-První verze projektu MST_W 6 alfa, odštepek od puvodniho MST_W 6
-Programatorska verze pro ruzne optimalizace stahovani, odesilani....

-Upravy pro projekt ZZS, Hanibal a Android (Invetura1)

*****Hlavni zmeny heslovito : 
-Nove licencovani
-Updater, možnost updatovat čtečky jednoduše
-udelane rychlejsi odesilani a stahovani davek cez request a zipovani souboru



************************************************************************************************************************************************************************************

[verze "6.16"] TaD (5.2.2018)
-uprava Lokalizace, vlastni lokalizace nacivala vlastnosti formu z resx souboru, po compile je z toho .resource soubor ktery se rychleji načitava pomoci rescourcemanageru
ktery sme upravily pro naše potřeby


[verze "6.15"] JM (10.11.2017)
- upload logu a trace na server v urcitych intervalech (+ config separator dat v logu ve formatu csv, interval odesilani na server, zapnuti/vypnuti)
Vydej
- pridany trace merici kolik casu klient travi svymi operacemi
- JiS => oprava podminky zobrazeni dialogu zadani serioveho cisla ...

[verze "6.14"] TaD (3.11.2017)
---------------
- Vydej: config pridana možnost zadavat hodnoty REZ1 a REZ2, zmena nazvu rez hodnt
- Vydej, listpolozek3 pridana moznost dazadavt rez1 a rez2 hodnoty
- do nastaveni varian configu pridany config pro Bachl RFID
- upravena varianta configu pro Labara

[verze "6.13"] TaD (31.10.2017)
---------------

Inventura2 pole název rozšířeno z 35 na 60 znaků

RFID - v config v text pridane tlacitko pro smazani textboxu
- pri povoleni/zakazani UHF upravena znovu inicializace rfid skeneru. pridany switch na volbu typu skeneru 

-pridano pri inicilizaci skerenu nastaveni Modu.

- do konfigurace pridana moznost autmatickeho update/upgrade z komunikacneh serveru


[verze "6.13"] JM (25.10.2017), JiS (27.10.2017)
---------------
TERMINAL\Fask.Parsing\ParsingFactory.cs
    ParseBarcodeSlashSarze(string data)
    kdyz car. kod obsahuje vice lomitek '/' tak se BarcodeSlashSarze rozdeli na 2 casti podle prvniho nalezeneho lomitka
    
TERMINAL\MST_W\Vydej_3\ListPolozek3.cs
    podminene nezobrazovat dialog na zadani sarze pokud CZ_SerNum_Track == 2 (BarcodeSlashSarze)
    
Prijem, Vydej, Prodej, Inventura: JiS (27.10.2017)
- uprava prace s BarcodeSlashSarze => pokud je nacten car.kod. a polozka je typu Sledovani na Sarze, pak se nezobrazuje dialog pro zadavani sarze a parsovana sarze se pouzije automaticky
    
[verze "6.12"] JM (11.10.2017)
---------------
SERVER\Fask.Server.Interfaces\DataSets\Vydej.*
SERVER\MST_Win_Kom_Server\SqlCEDBs\Vydej.sdf
SERVER\MST_Win_Kom_Server\SqlCEDBs\DataSets\Vydej.*
	pridani sloupcu CZ_REZ1_TRACK a CZ_REZ2_TRACK
	kvuli ignorovani zadavani typu palet podle CZ_REZ1_TRACK

TERMINAL\MST_W\SqlCEDBs\DataSets\Vydej.*
	pridani sloupcu CZ_REZ1_TRACK a CZ_REZ2_TRACK
	kvuli ignorovani zadavani typu palet podle CZ_REZ1_TRACK

TERMINAL\MST_W\Config\formConfig.*
	config pridani bool VydejParsovaniCarovehoKoduPovolit
	v UI: Konfigurace > Výdej > Parametry 1 > checkbox "Povolit parsovaní čárového kódu"

TERMINAL\MST_W\Globals.cs
	bool _vydejParsovaniCarovehoKoduPovolit

TERMINAL\MST_W\Vydej_3\Vydej.cs
	parsovani BarcodeSlashSarze pri sejmuti caroveho kodu

TERMINAL\MST_W\Vydej_3\ListVydejekForm3.cs
	explicitni pretypovani (hlasilo chybu pri prekladu)

TERMINAL\MST_W\Vydej_3\ListPolozek3.cs
	explicitni pretypovani (hlasilo chybu pri prekladu)
	ignorovani zadavani typu palet podle CZ_REZ1_TRACK

[verze "6.11"] TaD (22.8.2017)
---------------
ProdejList
-pridano vypisovani seroveho čisla jako SN pri pridávání položky
  - pridana kontrola zda je seriove čislo prazdne nebo ne, když jo tak se vypiše "-". 
-pridana napoveda serioveho čisla ktere bude pridano


[verze "6.10"] JrS (13.6.2017)
---------------
Vydej
- pridani skrolovacich list do detailu vydeje

[verze "6.9"] JiS (5.4.2017)
-------------
Prodej
- parsovani car.kodu sarze BarcodeSarzeParse

[verze "6.8"] Ta.D. (13.3.2017)
---------------
-pridana možnost v ConfigFormu pro každy modul zapnu/vypnut zvučku po načteni kodu skenerem.
-možnost pridana v modulech : Inventura1, Inventura2,Expedice,Online,Prijem,Prodej,Vydej,Servis
- po testovani verze 4 porobene upravy 

[verze "6.7"] Ta.D (20.2.2017)
---------------
Prijem, Inventura
- prehrani zvuku po naskenovani polozky 

Servis
- Zdroj : rozsireni o prvek Misto

ImageProviders
- foceni pri stisku OK, Menu-OK, Enter


[verze "6.6"] Ta.D (16.2.2017)
- ServisModul pridane do ServisCinostZmena a ServisDynamickaTabulka scanner reactivate
- do každeho ScannerProvider pridane logovani
	
[verze "6.5"] Ta.D (9.2.2017)

Inventura1
- rozsireni struktur o vahu balici jednotky
- Parsovani vahovych kodu pri nacteni car.kodu
	=> automaticke vlozeni a prepocet hmotnosti do zakladni mj
-pridani Vahy do NaplPolozku_sqlce v inventure


[verze "6.4"] Ta.D (7.2.2017)
-Pridani v ProdejList Sumar a Sumar vybrane polozky.
-Pridany Form Prodej Sumar.
- v modulu prodej v PodejList zmenena klavesova skratka z F11 na 7 a z F12 na 8.
pro moznost pouziti na MC2180. 
-CZMST_DI  pridan select CountByNmbrpalItemnmbrRuzneSerltnum

Expedice
- oprava aktivace/deaktivace scanneru pri funkce "Hledat dle kodu palety"
- oprava finalize pri ukoncovani formu 
- uprava pripravy soupisu a jeho tisku
	=> stahuji se pro soupis vsechny polozky prikazu
	=> posila se tiskovemu serveru
	
Prijem
- mensi optimalizace prijeminsert



[verze "6.2"] JiS (3.10.2016)
-------------
Inventura1
- parsing BarcodeSlashSarze 

[verze "4.108"] JiS (3.10.2016)
-------------
Vydej
- dialog zadani mnozstvi: pole Note_l zmena barvy na Cervenou (Color.Red)
- problem se zmenou velikosti fontu pisma pri zobrazeni akutalizaci waitdialogu ...

-------------
- bez verze
Prodej
- oprava, kdy pri vyhledavani podle cisla polozky nebylo mozne zadavat pismena
Prijem
- pridana moznost prepinani rezimu na prijmove lokace (nezobrazuje se dialog pro zadani lokace) a neprijmove (dojde k zobrazeni dialogu pro zadani lokace)
-> prevzato z Perlacasa, pracuje s prijmovymi lokacemi!
	1) Režim příjmu na příjmovou lokaci (výchozí režim při otevření dávky)
        -> Provádí se příjem položek na příjmovou lokaci bez zobrazení dialogu pro zadání lokace. Ve status baru je zobrazeno: R:1,PL:ULI5 (režim 1, příjmová lokace ULI5).
    2) Režim příjmu s přímým zalokováním na uživatelem určenou lokaci
        -> Provádí se příjem položek na obsluhou vybranou lokaci (lokace se nastavuje pomocí klávesy F8). Zde nedochází k zobrazení dialogu pro zadání lokace. Ve status baru je zobrazeno: R:1,L:XYZ (režim 1, uživatelem zadaná lokace XYZ).
    3) Režim příjmu s možností nastavení lokace pro každou položku s přednastavením doporučené lokace pro danou položku
        -> Provádí se příjem položek, kdy dochází při každé položce k zobrazení dialogu pro zadání lokace (předvyplňuje se doporučená lokace -> pokud je zavedena pro danou položku). Ve status baru je zobrazeno: R2 (režim 2)
    4) Režim příjmu s přímým zalokováním na uřivatelem určenou lokaci s přednastavením pro každou položku (XYZ)
        -> Provádí se příjem položek, kdy dochází při každé položce k zobrazení dialogu pro zadání lokace (předvyplňuje se uživatelem zvolená lokace). Ve status baru je zobrazeno: R:2,L:XYZ (režim 2, uživatelem zadaná lokace XYZ).
JiS 23.8.2016
Uprava reseni pro SBKomplet zakazka VICHR Vydej (modem)
Terminal rozsireni filtrovani polozek : Vse / Neuplne / Zadane / Zbyvajici (Klavesova zkratka D1)
Server oprava natahovani polozek alternavinich car.kodu a prace se souborem - vyuziva se databaze sql a tabulka czmst_carkod_altern ...

[verze "4.107"] PeV (16.8.2016)
-------------
Expedice
- pri odeslani davky s nasnimanymi paletami dochazi ke kontrole, zdali byl prepravni list vytisknut

[verze "4.106"] PeV (16.8.2016)
-------------
Prodej
- pri tisku palet se nepracuje s menami a DPH

[verze "4.105"] PeV (11.8.2016)
-------------
- sjednoceni verze s MST_Win_Kom_Server

[verze "4.104"] PeV (10.8.2016)
-------------
Prodej
- optimalizace, urychleni vyhledavani a pridavani polozek
- konfigurace aplikace rozsirena o moznost povoleni cen (drive nebylo vubec v konfiguraci)
- konfigurace aplikace rozsirena o moznost vypnuti nacitani nazvu skladu pri vyhledavani polozek (kvuli urychleni)

[verze "4.103"] PeV (27.7.2016)
-------------
Fask.Module.MTJ.JimiTore.Baleni
- pri odchodu z nasnimanych dat (prijem, prijem zbytku, vydej) pridana kontrola, zdali jsou data odeslana

[verze "4.102"] PeV (26.7.2016)
-------------
Prodej
- typ dokladu rozsiren o parametr 'cfg_sklady_zmena' umoznujici prepnout sklad (skl_id) uvnitr davky
- implementace funkce umoznujici zmeny cisla skladu uvnitr davky
Fask.Module.MTJ.JimiTore.Baleni
- implementace modulu Kontrola

[verze "4.101"] PeV (22.7.2016)
-------------
Prijem
- pridana moznost volani online funkce, ktera vrati cislo zdrojoveho a ciloveho skladu

[verze "4.100"] PeV (21.7.2016)
-------------
Prijem
- oprava, kdy po sejmuti caroveho kodu v seznamu polozek formular ztratil focus
- implementace prace s paletami (zobrazeni, filtrovani, vyhledavani, ...)
- prepinani rezimu mezi vse/neuplne nyni zohlednuje filtr na palety
- filtr na palety se do Settings.xml uklada jako enum (puvodni funkcionalitu nebylo mozne pouzit)
- pridano zobrazeni cisel palet do formularu
- pridana informace do status baru o zapnutem filtru na palety
Prodej
- oprava, kdy v nekterych pripadech nedochazelo k zohledneni povoleni/zakazani filtru na ciselnik skladu pri vyhledavani
- pridano zobrazeni cisel palet do formularu
- implementace kontrolnich funkci, aby nebylo mozne davat ruzne sarze pro stejnou polozku
- pridana moznost volani online funkce, ktera vrati cislo zdrojoveho a ciloveho skladu
Expedice
- oprava, kdy v nekterych pripadech nedochazelo k zobrazeni variant polozek v baleni
- oprava, kdy bylo mozne pri baleni pridat polozku i v pripade, kdy nebyla vybrana paleta

[verze "4.99"] PeV (20.7.2016)
-------------
Fask.Module.MTJ.JimiTore.Baleni
- zvetseni velikosti pisma ve vsech formularich

[verze "4.98"] PeV (13.7.2016)
-------------
Prijem
- pridana moznost nezobrazovat dialog zadani mnozstvi pri sejmuti vahoveho kodu
- upravy prijmu, aby byl schopny korektne pracovat s vahovymi kody
Expedice
- upravy formularu, aby ihned doslo k zobrazeni sloupcu datagridu
- uprava tisku soupisu v seznamu palet (netisknou se vsechny polozky na paletach, ale pouze samotne palety)

[verze "4.97"] PeV (29.6.2016)
-------------
Prijem
- seznam polozek, 2 rezimy nastaveni lokace, ktera se bude vyuzivat pri zadavani vsech polozek:
-> 1) povolena lokace na davku a povolena prijmova lokace ... dojde k prepinani mezi vyberem lokace, ktera se ma pouzivat po celou dobu a vyberem prijmove lokace
-> 2) povolena lokace na davku a zakazana prijmova lokace ... dojde k vyberu lokace, ktera se ma pouzivat po celou dobu
-> pridan vypis do statusbaru s jakou lokaci se pracuje (PL (prijmova lokace), L (lokace))
- pri zalokovani pridana moznost nastavit lokaci, ktera se bude po celou dobu pouzivat
-> tlacitko v menu zaskrtnuto - bude se pouzivat
-> tlacitko v menu odskrtnuto - nebude se pouzivat (bude se pokazde ptat na lokaci)
- oprava, kdy bylo mozne tisknout pomoci klavesove zkratky pri zadavani mnozstvi v pripade, kdy byl tisk zakazany
Prodej
- rozsireni typu dokladu o moznost povoleni tisku paletovych listku (cfg_tisk_palety)
- implementace tisku paletovych listku (obdoba soupisu -> hlavicka, polozky a paticka)
-> posilaji se veskere zaznamy, ktere maji stejne cislo palety jako vybrany nasnimany zaznam
- pri tisku paletovych listku se automaticky predvyplnuje mnozstvi 1. TODO: konfiguracne ...
Expedice
- konfigurace rozsirena o moznost povoleni tisku paletovych listku a soupisu
- implementace tisku paletovych listku (obdoba soupisu -> hlavicka, polozky a paticka)
-> posilaji se veskere zaznamy, ktere maji stejne cislo palety jako vybrany nasnimany zaznam
- implementace tisku soupisu palet pri expedici (resp. polozek)
-> pri tisku se prvne nactou online metodou veskere polozky ze serveru (v terminalu je pouze informace o paletach a poctu polozek na nich) a nasledne dojde k odeslani dat na tiskovy server
- TODO: Prijem - u napevno zadanych lokaci umoznit kontrolu a nastaveni doporucene lokace

[verze "4.96"] PeV (24.6.2016)
-------------
Prodej
- uprava generovani SN na davku - pokazde se pri otevreni davky zobrazi dialog s vygenerovanym SN
- uprava generovani SN na davku - vygenerovani aktualniho cisla dne v roce
- konfigurace aplikace rozsirena o moznost globalniho zakazani exportu ciselniku v ramci modulu Prodej (nedochazi tedy pri pokusu o aktualizaci ciselniku k zobrazeni dotazu, zdali exportovat ciselnik)
- pri zadavani mnozstvi se zobrazuje lokace (prevazne kvuli lokacnimu mechanismu)
- rozsireni czmst_di o sloupce NMBRPAL, TYPEPAL a PRINTED
- rozsireni czmst092 o cfg_onl_palety_generovat
- pokud je v konfiguraci aplikace zakazano pouziti filtru zbozi dle ciselniku skladu, tak pri vyhledavani polozky podle CK se jiz tato volba zohlednuje
Expedice
- kompletni prepsani expedice (pridano baleni)
- upravy struktur (Baleni X Expedice)
Servis
- oprava, kdy nedochazelo k zobrazeni nazvu odberatele v seznamu davek

[verze "4.95"] PeV (7.6.2016)
-------------
Fask.Logging
- pridana metoda pro zalogovani sqlce exception
Prijem
- oprava foceni, kdy dochazelo k vytvoreni souboru s duplicitni priponou
- rozsireni struktur o vahu, typ palety, cislo palety a itemcode
- pri zadavani mnozstvi se jiz zobrazuje itemcode a vaha
- pri vyberu alternativ pridano do datagridu zobrazeni vahy a itemcode
- konfigurace rozsirena o moznost parsovani caroveho kodu (+ implementace samotne funkcionality)
Fask.Parsing
- tvorba nove knihovny pro parsovani (momentalne se nepouziva)
- pri parsovani vahoveho kodu se nyni snazi najit prefix 28 nebo 29 (drive bylo pouze 28)
Servis
- pridana konfiguracni moznost zapnuti/vypnuti vyberu okruhu pri generovani davky
- id okruhu, stavu, ... se jiz dotahuji pri plneni seznamu zdroju
- mensi upravy kodu
- upravy prebirani id odberatele a okruhu z nactenych dat
- rozsireni tabulky predlohy o ID odberatele
Prodej
- oprava, kdy se itemcode neprenaselo do vystupnich dat
- rozsireni czmst095 a czmst_di o sloupec WEIGHT (vaha)
- rozsireni czmst092 o parametry cfg_generovat_sn, cfg_parsovat_ck, cfg_sn_na_davku, cfg_lok_mech_online_pohyby
- implementace prace se sarzi (moznost zadat sarzi pri otevirani davky, pripadne moznost zmenit sarzi v seznamu polozek)
Expedice
- implementace modulu
Fask.Module.MTJ.JimiTore.Baleni
- pridana moznost povoleni zmeny podbarveni dialogu a tlacitek (povoleni a barvy (hexa) se nastavuji v 'Fask.Module.MTJ.JimiTore.Baleni.dll.config')

[verze "4.94"] JiS (9.5.2016)
-------------
Lokacni mechanismus
- vetsi modifikace sturkur lokacniho mechanismu
- rozsireni o vazbu PolozkaLokaceVychozi
- upravy dle pripominek zakaznika a analyzy
Over CK
- doplneni o zobrazeni lokaci zbozi z lokacniho mechanismu
- oprava chyby scanneru
- omezeni pri vyhledavani na maximalne 100 zaznamu
Prodejni modul
- uprava vyhledavani dle sarzi/lokaci pri vyberu online sarze/lokace z lokacniho mechanismu
Vydej
- oprava lokalizace pri stornovani vydejky

[verze "4.93"] PeV (3.5.2016)
-------------
Honeywell
- implementace scanneru
- implementace foceni pomoci imageru a fotoaparatu + pridana moznost zapnuti/vypnuti svetla pri foceni
- implementace automatickeho prepinani klavesnice
Prodej
- upravy + doplneni lokalizace aplikace (byl problem s rozslisenim zdrojovy x cilovy sklad)
Servis
- pridana moznost konfiguracne zapnout automaticke odeslani dat na pozadi a automaticke odeslani dat po zadani zdroje
- rozsireni struktur ZdrojStav a ZdrojPohyb o GPS souradnice a CinnostOznaceni (uklada se Oznaceni z dynamickych tabulek)
- rozsireni struktury predlohy davky o Barcode (carovy kod davky) + uprava
- oprava, kdy nedochazelo k odesilani fotografii pri odeslani davky
- oprava, kdy se nemazaly data z ZdrojPohyb po odeslani davky
Lokalizace
- oprava, kdy se logoval neexistujici lokalizacni soubor
- zakomentovani lokalizace formulare u MessageBoxBig a MessageBoxBigTimeout
Foceni
- implementace providera pro foceni
- rozsireni providera o moznost zobrazeni textu ve status baru (pokud se zobrazuje status bar)
- nastavuje se v souboru 'PhotoFactory.xml' (pokud soubor neexistuje, pouzije se automaticky Fask.PhotoProviderBase)

[verze "4.92"] JiS (28.4.2016)
-------------
Prijem, Vydej
- Generovani davky : upraveno s dotazem na strane terminalu
- uprava zobrazeni hodnot v dialozich zadavani: cz_carkod presunut do zobrazeni k ITEMNMBR
	=> ve vetsine pripadu pole cz_carkod obsahuje vlastni oznaceni vyrobku z IS

[verze "4.91"] JiS (28.4.2016)
-------------
- verze vynechana ...

[verze "4.90"] JiS (19.4.2016)
-------------
Prijem, Vydej
- Generovani davky : zmena zpusobu zpracovani pro dlouhotrvajici operace

[verze "4.89"] PeV (15.4.2016)
-------------
Prijem
- uprava formulare nezrealizovanych prijemek, aby barevne oddeloval ruzne radky
- pridat wait cursor pri nacitani doporucenych lokaci (u zalokovani)
- vyber doporucene lokace rozsiren o moznost zadani lokace rucne
- pridany wait cursory pri volani online dotazech
Ciselniky
- tvorba noveho formulare pro vyber lokace (puvodni byl nepouzitelny ...)
- prepsani aplikace tak, aby vyuzivala tento formular misto stareho
- pridana moznost aktualizovat ciselnik lokaci primo z formulare
Prodej
- nazev dialogu pro seznam zbozi nyni zobrazuje nazev dokladu misto nazev prodejniho modulu
- zakomentovani kodu, kde se menila funkcnost aplikace, pokud byl nastaven doc_typ==1
- doplneni lokalizace aplikace
- pri vyberu davky se vypisuje i cilovy sklad
- pri otevirani rozpracovane davky dochazi k nacteni drive nacteneho ciloveho skladu
- rozsireni typu dokladu o nekolik parametru (prevazne pro praci se skaldy a lokacemi -> predvyplneni, povoleni, ...)
Servis
- oprava vyhledavani podle caroveho kodu v pripade, ze carovy kod je null
- impementace foceni pro ES400 a MC45 (je treba nastavit v Settings.xml spravny 'PhotoType')

[verze "4.88"] PeV (23.3.2016)
-------------
- tvorba formulare pro vyber odberatele (nezavisly na prodejnim modulu)
Servis
- upravy struktur + rozsireni datasetu
- implementace davkoveho zpracovani (mozno v konfiguraci aplikace prepinat)
- implementace tridy pro praci s davkami (stahovani, vraceni, odeslani, ...)
- implementace formulare pro vyber okruhu
- implementace formulare ServisMain + pridana moznost aktualizovat ciselnik odberatelu a ZdrojStavu
- implementace formulare ServisDavkyList pro vyber davek
- pridana moznost otevreni davky ihned po stazeni
- pri odesilani davky dochazi take k odeslani fotek
- upravy formulare ServisList (seznam zdroju) + navaznosti tak, aby byl schopny pracovat s davkama
- rozsireni datasetu o nove prvky + upravy aplikace
- rozsirena konfigurace aplikace o moznost zapnuti/povoleni filtru
- prace na moznosti filtrovat zaznamy + vyhledavat s aktivnim filtrem v seznamu zdroju
- filtr v seznamu zdroju si pamatuje posledni svoje nastaveni (pri uzavreni davky) a to se pouzije
- pridano overeni rozpracovani davky pri vraceni davky
- pridana moznost preskocit vyber stavu, pokud je pouze jeden + doplneni do konfigurace
- upravy zpracovani stavu/cinnosti
- opravy nacitani konfigurace aplikace
- doplneni docnumber, id odberatele, id okruhu pri generovani davky
- pri vygenerovani a stazeni davky dojde k nacteni docnumber ze stazene davky
- pokud je davkove zpracovani, neni mozne aktualizovat zdroje/stavy ze seznamu zdroju
- pridana moznost konfiguracniho vypnuti synchronizace pri vyberu zdroje
- rozsireni hlavicek davek o oznaceni okruhu a odberatele
Prodej
- oprava formulare pro vyber odberatele (problem s null hodnotami a opacne nastavenymi podminkami pri nacitani dat)

[verze "4.87"] PeV (1.3.2016)
-------------
- pridano try/catch do GraphicButton + do catch pridano nastaveni fontu (problem s MC92 a tlacitky)
- foceni jiz neotaci obraz
Prijem
- pri vyberu alternativ se zobrazuje merna jednotka
- oprava pouziti spatneho datasetu pro prijemparams vkladani do czmst_pi
Fask.Module.MTJ.JimiTore.Baleni
Prijem
- rozsireni sloupce prijem_id na 16 znaku (puvodne bylo 12)

[verze "4.86"] PeV (18.2.2016)
-------------
Fask.Module.MTJ.JimiTore.Baleni
Prijem
- implementace
- pri zadavani poctu vytisku je mozne zadat 0
Vydej
- umozneni zadani zaporneho mnozstvi
Prijem zbytku
- do CZ_CarKod se vyplnuje carovy kod, ktery vraci online funkce

[verze "4.85"] PeV (17.2.2016)
-------------
Prijem
- upravy zalokovani (konfiguracne moznost zadani mnozstvi)
- upravy prace s prijmovou lokaci (predvoli se naposled zvolena lokace)
- pri zalokovani se predvyplnuje lokace, ktera je ve sloupci czmst_pe.locncode (drive bylo czmst_pi.locncode)
- pri ukonceni zalokovani dojde k zobrazeni potvrzovaciho dialogu
- uprava focusu pri zalokovani
- vyhledavani podle caroveho kodu pri zalokovani zohlednuje vybrany filtr
- pokud je nalezeno vice zaznamu pri zalokovani (po sejmuti CK), dojde k zobrazeni filtru na carovy kod ve status baru
- oprava obcasne nefunkcnosti scanneru pri vyberu davky (pridan scannerstop/start do stornovani a uzavreni prijeky)
- pridana klavesova F2 pro aktualizaci seznamu prijmovych lokaci
- prejmenovani formularu, ktere se pouzivaji pri zalokovani
- zalokovani mnozstvi vyuziva nastaveni, zdali ma byt povoleno snimani scannerem z 'prijemparams.xml'
- sjednoceni filtru zobrazit vse/nezalokovane pod jednu klavesu
- doplneni lokalizace
- prejmenovani sloupce v datagridu z VNDDOCNM na "Dokument"
- oprava vnitrniho kolecka
- oprava prace se scannerem pri generovani stornovani a uzavreni prijemky
- pridana kontrola pri generovani nenalezene vydejky, zdali ponumbe jiz neni v nejake stazene davce
Vydej
- prejmenovani sloupce v datagridu z VNDDOCNM na "Dokument
- pri zadani chybne lokace nebo moc velkeho mnozstvi dojde po navratu chyby z lokacniho mechanismu k opetovnemu zadani mnozstvi a lokace (uzivatelem vyplnene hodnoty se nepredvyplnuji)
- oprava prace se scannerem pri uzavreni vydejky
- oprava generovani dat prikazu
- pridana klavesova zkratka F2 pro aktualizaci seznamu lokaci a mnozstvi materialu (pri zadavani lokace)
- konfiguracne pridana moznost generovat nenalezenou davku
- pridana kontrola pri generovani nenalezene vydejky, zdali sopnumbe jiz neni v nejake stazene davce (generovat jde pouze pri stahovani davky, ne pri zobrazeni existujicich davek)
- pridani dialogu pro zobrazeni alternativ pri zadavani lokace

[verze "4.84"] PeV (1.2.2016)
-------------
- upravy logovani lokacniho mechanismu
Prodej
- rozsireni struktur, datasetu a jejich pouziti (viz server)
- pridana moznost zadani ciloveho skladu pri snimani polozky
- pokud je vybran cilovy sklad, probiha online overeni lokace podle ciloveho skladu, jinak podle zdrojoveho/zadneho
- pri vyberu davky se nacita do datasetu parametr urcujici, zdali ma provadet kontrola na existenci zaznamu pri mazani davky
- implementace lokacniho mechanismu
Fask.Module.MTJ.JimiTore.Baleni
- pridani wait cursoru pri volani online metod
Prijem zbytku
- rozsireni datasetu o delku
- pridana moznost tisku z menu
- upravy, aby bylo mozne zadavat pouze cela cisla (bez desetinnych casti)

[verze "4.83"] PeV (27.1.2016)
-------------
Vydej
- uprava datasetu kvuli rozsireni vydejparams.xml
- implementace lokacniho mechanismu (pri pridani polozek, pri mazani, ...)
- pridana kontrola pri vraceni davky, zdali jsou nejaka data nasnimana a je povolen lokacni mechanismus (pokud je oboje true, neni mozne vratit davku)
Prijem
- uprava datasetu kvuli rozsireni prijemparams.xml
- parametry pouzivaji vlastni dataset
- implementace lokacniho mechanismu (pri pridani polozek, pri mazani, ...)
- tvorba formulare pro vyber prijmove lokace (vyber probiha pri otevirani davky, zapina se v prijemparams.xml)
- pridana kontrola pri vraceni davky, zdali jsou nejaka data nasnimana a je povolen lokacni mechanismus (pokud je oboje true, neni mozne vratit davku)
Inventura1
- uprava datasetu kvuli rozsireni inventuraparams.xml

[verze "4.82"] PeV (14.1.2016)
-------------
Fask.Module.MTJ.JimiTore.Baleni
- implementace vydeje (vyber objednavky a nasledne snimani polozek)
- pridan formular pro vyber mezi modulem baleni a vydejem
- uprava MessageBoxBig, aby fungovalo prehravani zvuku (ikony se zatim nezobrazuji)

Vyber skladu
- pokud neexistuje ciselnik skladu, probehne dotaz na stazeni
- doplneno menu a pridani moznosti aktualizace/stazeni ciselniku skladu primo z dialogu 
- zaneseni do lokalizace aplikace

[verze "4.81"] PeV (4.1.2016)
-------------
Lokalizace
- doplneni chybejici/nove lokalizace (prijem, vydej)
Prijem
- oprava chybove hlasky v pripade, ze sklad nebyl nalezen (vypisoval se sklad z prodeje)

[verze "4.80"] PeV (18.12.2015)
-------------
- oprava chyby pri ukladani konfigurace aplikace
- vytvoren formular SnimatRFID pro nacitani RFID (nacitat jde soucasne pomoci scanneru)
Husky vratka
- pridany formulare HuskyVratkaMain (uvodni formular), HuskyVratkaPotvrzeni (formular slouzici k potvrzovani)
- adresar HuskyVratka odstranen (exclude) z projektu 
- formulare 'SnimatRFID', 'VydejDocipovaniList' a 'VydejOcipovaniList' odstraneny z projektu
- odstranena web reference VratkaService
Vydej
- ocipovani v menu se zobrazuje pouze v pripade, ze je zapnuty RFID scanner a je v konfiguraci aplikace zapnuto ocipovani
- formular 'VydejOcipovaniList' se diva do czmst_se tabulky, 'VydejDocipovaniList' se diva do czmst_si tabulky
- do konfigurace aplikace pridana moznost pouziti skladu, prevzeti id skladu z ciselniku skladu, pouze jeden sklad na davku a predvoleni skladu (pouze v konfiguraci pridano, v aplikaci se nepouziva)
RFID scanner
- pridana casova prodleva 100 ms pri snimani (dochazelo k tomu, ze se nactena data nezobrazovala)

[verze "4.79"] PeV (15.12.2015)
-------------
Vydej
- konfiguracne pridana moznost povoleni ocipovani v seznamu polozek
- pridan formular pro zobrazeni informaci o polozce pri nacitani RFID
HuskyVratka
- novy modul

[verze "4.78"] JiS (10.12.2015)
-------------
Prijem
- rozsireni o prace se sklady, na zaklade potreby Husky (HeliosOrange)

[verze "4.77"] PeV (8.12.2015)
-------------
- uprava snippetu lokalizace aplikace
- pridana lokalizace modulu Prijem, Prodej, Vydej, Inventura a adresare Forms

[verze "4.76"] PeV (1.12.2015)
-------------
- LoginForm vyuziva lokalizaci
- uprava snippetu lokalizace aplikace
Fask.Localization
- pridana knihovna slouzici k praci s lokalizaci
- pridana chybova hlaska v pripade, ze lokalizacni klic chybi v souboru 'Localization.resx'
- knihovna je schopna:
	- lokalizovat formulare (textbox, label, checkbox, menu, ...) 
	- hlasky vyvolane aplikaci ('Zadejte login', 'Aktualizace pristupu', ...)
	- nacitat lokalizace z adresare 'Lokalizace' (viz soubor 'Popis.txt')

[verze "4.75"] PeV (26.11.2015)
-------------
Prodej
- automaticke prebirani mnozstvi ze zbozi se uplatnuje i pri CZ_SerNum_Track = 0

[verze "4.74"] PeV (19.11.2015)
-------------
Prodej
- typ dokladu (czmst092) rozsiren o konfiguracni parametr 'cfg_predvyplnit_mnozstvi'. Pokud je nastaven na '1', dojde k predvyplneni mnozstvi
Fask.Module.MTJ.JimiTore.Baleni
- webservice se inicializuje pouze jednou
- pridana kontrola na to, zdali se vratily data pro tisk

[verze "4.73"] PeV (19.11.2015)
-------------
IModuleProvider
- rozsiren o posilani ID terminalu
PrinterFactory
- pokud tiskovy modul neni nalezen, tak dojde k vyvolani vyjimky (misto navratove hodnoty 'true')
- rozsireni o moznost tisku Baleni
Fask.Module.MTJ.JimiTore.Baleni
- pridana staticka trida pro tisk pomoci PrinterFactory
- rozsireni o konfiguraci pro nastaveni modulu
- pridana moznost zapnuti/vypnuti zobrazeni dialogu pro zadani poctu vytisku
- konfiguracne moznost predvyplneni poctu vytisku (konfigurace ulozena v souboru Fask.Module.MTJ.JimiTore.Baleni.dll.config)
- rozsireni logovani v pripade chyb
Prijem
- vyhledavani nezrealizovane prijemky probiha pomoci 'Like' misto 'where'
- prinada moznost konfiguracne povolit/zakazat zobrazeni dialogu na zadani sarze
Fask.Logging
- rozsireni o moznost zalogovani datasetu (jeho veskere tabulky a vsechny zaznamy)

[verze "4.72"] PeV (27.10.2015)
-------------
- sjednoceni cisla verze

[verze "4.71"] PeV (26.10.2015)
-------------
Prijem
- pridana moznost vyhledat nezrealizovanou prijemku podle cisla objednavky (zadani textu)
Prodej
- rozsireni ciselniku typu dokladu (czmst092) o parametr cfg_mnozstvi_ze_zbozi. Pokud je zapnuty, je preskoceno zobrazeni dialogu pro zadani mnozstvi a rovnou se nastavi mnozstvi z dane polozky (pripadne z online dotazu doporucenych palet)

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


[verze "4.69"] PeV (15.10.2015)
-------------
Prijem
- uprava formatu datumu, ktery se posila na tisk (klic: DATUMPRIJMU, hodnota: DateTime.Now.ToString("d.M.yyyy"))
- uprava formatu mnozstvi, ktere se posila na tisk (klic: QTYSHPPDMJ2, hodnota: pi.QTYSHPPDMJ.ToString("0.0"))

[verze "4.68"] PeV (7.10.2015)
-------------
- pridano logovani neosetrenych vyjimek
- pri foceni je vysledna fotka zrcadlove otocena (problem s MC92)
Prijem
- pokud je v konfiguraci aplikace povolena Obednavka Detail, tak se pri tisku pokazde dotahuji informace o objednavce a polozce ze serveru
- prijemka se jiz neotevre v pripade, ze v konfiguraci je vypnuta funukce "otevrit davku ihned po stazeni" a je zapnuta funkce "Generovat nenalezenou prijemku"
Prodej
- pokud neni nalezen carovy kod/sarze v ciselniku zbozi a jsou zapnute doporucene palety a vyhledavani podle sarzi, posila se online funkci pro doporucene palety aktualne vybrany sklad
- pridan index pro Serltnum do ciselniku zbozi + reindexace po stazeni ciselniku
- uprava hlasek pri nenalezeni caroveho kodu (zdali nebyl nalezen i v sarzich nebo online funkce pro doporucene palety nevratila zadny zaznam)

[verze "4.67"] PeV (6.10.2015)
-------------
Prijem
- do tisku se odesila aktualni datum na terminalu ("DATUMPRIJMU", DateTime.Now.ToShortDateString())
- online procedura DetailItem a Detail se pri tisku vola pokazde, pokud je zapnuta

[verze "4.66"] PeV (6.10.2015)
-------------
Prijem
- pri stahovani davky se pridava davka do seznamu hlavicek

[verze "4.65"] PeV (6.10.2015)
-------------
Prodej
- online overeni zdrojove lokace probiha ihned po zadani lokace
- pri zadavani mnozstvi se zobrazuje sarze
- pokud jsou zapnuty online doporucene palety a vyhledavani podle sarzi, tak aplikace pri nenalezeni caroveho kodu se pokusi online dotazat na doporucene palety. Pokud se vrati nejaky zaznam, tak se pouzije
- pridani testovani na null hodnoty pri zadavani lokace
Prijem
- pri zadavani mnozstvi se zobrazuje sarze
- prirazena klavesova zkratka '9' pro otevreni seznamu nezrealizovanych prijemek
- po zalokovani probiha kontrola, zdali jsou vsechny polozky zalokovany a prijemka dokoncena. Pokud ano, zobrazi se dotaz na odeslani davky a je mozne ji rovnou odeslat.

[verze "4.64"] PeV (1.10.2015)
-------------
Prodej
- u vypisu zbozi se zobrazuje sarze (serltnum)
- u online kontroly lokace byl pridan dalsi navratovy stav (2), ktery zobrazi zpravu, kterou vraci procedura
- online funkci, ktera vraci seznam doporucenych palet se posila sarze (serltnum) vybraneho zbozi
Prijem
- u online kontroly lokace byl pridan dalsi navratovy stav (2), ktery zobrazi zpravu, kterou vraci procedura

[verze "4.63"] PeV (17.9.2015)
-------------
Prodej
- pokud je v konfiguraci vyplneno SkladID a jsou povoleny sklady, dojde pri otevirani davky k vyhledani pozadovaneho skladu (s timto skladem se nasledne bude pracovat a jeho ID se bude odesialt proceduram a do vystupnich dat)
- konfiguracni moznost filtr na sklad se nove stara pouze o filtr, o nic vic

[verze "4.62"] PeV (17.9.2015)
-------------
Prijem
- upravy prace se skladem v seznamu polozek
- pri zalokovavani se zobrazuje id skladu
- u zalokovani byly pridany klavesove zkratky mezi polozkami s nezadanou lokaci a vsemi polozkami
- pridana konfiguracni moznost kontrolovat, zdali jsou vyplneny veskere lokace v prijemce (kontrola probiha pri odesilani prijemky)
Prodej
- pri vyberu palety se zobrazuje id skladu (toto id skladu se momentalne nepredava do vystupnich dat czmst_di)

[verze "4.61"] PeV (15.9.2015)
-------------
Prijem
- rozsirovani o moznost zadani skladu pred otevrenim prijemky (konfiguracne mozno zapnout/vypnout)
- rozsireni o moznost "Pouze jeden sklad na davku"
- online metodam Online_OverLokace, Online_GetPalety se posila id skladu
- mensi upravy pri odesilani fotek
Prodej
- online metodam Online_GetDoporuceneLokace, Online_OverLokace, Online_GenerateSerltnum se posila id skladu

[verze "4.60"] PeV (14.9.2015)
-------------
Prijem
- pokud neni nalezena polozka pri zalokovavani podle sarzne, probehne vyhledani podle caroveho kodu
- oprava nezobrazovani scrollbaru pri zalokovani
- pridano zobrazeni aktualni lokace do vypisu zalokovavani
- pridana klavesova skratka na zalokovani (F7)
Prodej
- pokud se sleduje pouze na mnozstvi (CZ_SerNum_Track = 0) a jsou zapnute doporucene palety, dochazi jiz k predvyplneni mnozstvi
- pridany kontroly na NULL u cfg_onl_dop_pal na mistech, kde chybely

[verze "4.59"] PeV (9.9.2015)
-------------
Prodej
- pridana konfiguracni moznost stahnuti ciselniku zbozi (bez dotazu na export) pred vyberem davky
Prijem
- pridana tabulka CZMST_PI_F uchovavajici nazvy fotografii a GUID zaznamu ke kteremu patri + rozsireni datasetu
- pri vraceni davky se mazou fotky a data z CZMST_PI_F (po uspesnem odeslani)
- pri mazani nasnimanych dat dochazi ke smazani fotek a dat z CZMST_PI_F
- pridana moznost konfiguracne zapnuti/vypnuti tisku vsech predloh pri otevreni davky
- pokud je zapnute foceni a mazou se data z CZMST_PI, dojde take k odstraneni dat z CZMST_PI_F a souvisejicich fotek
- oprava, kdy nedochazelo k automatickemu otevreni davky v pripade, ze terminal neobsahoval zadnou stazenou davku

[verze "4.58"] PeV (20.8.2015)
-------------
Prijem
- do online metody pro overeni lokace se posila itemnmbr
- procedura pro online kontrolu lokace navraci 3 ruzne stavy ( 0 - vse OK, 1 - poruseno doporucene poradi, mozno pokracovat, jinak neni mozne pokracovat)
- pridano volani udalosti obsluhy pri poruseni poradi doporucene lokace
- v seznamu doporucenych lokaci je mozno nasnimat i lokaci, ktera neni v ciselniku (a tak ji zvolit) ... pokud je zapnute online overeni, tak probehne
- moznost konfiguracniho zapnuti/vypnuti overovani sarze se seznamem predloh
- vytvoren formular PrijemNezrealizovanePrijemkyList pro zobrazeni nezrealizovanych prijemek (vola se online funkce)
- pridana konfiguracni moznost po sejmuti CK v listu prijemek (pokud prijemka nebyla nalezena) vygenerovat a stahnout danou prijemku (po potvrzeni uzivatelem)
Prodej
- do online metody pro overeni lokace se posila itemnmbr
- oprava, kdy se pri zadavani cilove lokace posilala do online funkce pro overeni lokace zdrojova lokace
- zdrojova lokace se jiz nepredvyplnuje
- pridana konfiguracni moznost online overeni doporucene a cilove lokace
Tabulka CZMST092 rozsirena o:
	[cfg_onl_dop_pal] [tinyint] NULL			-- zobrazovat seznam doporucenych palet
	[cfg_onl_over_lokace] [tinyint] NULL	-- online overeni zdrojove lokace
	[cfg_onl_over_lokace_dest] [tinyint] NULL	-- online overeni cilove lokace
- pri zadavani cilove lokace je  mozno konfiguracne zapnout zobrazeni doporucenych lokaci
- v seznamu doporucenych lokaci je mozne zadat lokaci, ktera neni v seznamu
- pokud carovy kod neni nalezen v seznamu doporucenych palet, tak je zobrazeno upozorneni, ale je mozne pokracovat (dojde k predvyplneni sarze)
- uprava predvyplnovani lokace, mnozstvi a sarze po vyberu palety ze seznamu doporucenych palet
	
[verze "4.57"] PeV (17.8.2015)
-------------
- zmena referenci na Motorola EMDK v2.9
Prijem
- vytvoren formular PrijemZalokovaniList pro postupne zadavani lokaci (snima se serltnum) + pridana moznost online overeni lokace
- vytvoren formular PrijemVyberLokaceList pro nacteni diporucenych lokaci se serveru a nasledny mozny vyber
Prodej
- ProdejVyberPaletyList si jiz pamatuje usporadani sloupcu
- ProdejVyberPaletyList pridano volani online funkce pro overeni sarze (mozno konfiguracne vypnout)
- pridano volani online funkci pro overeni lokace, navratu doporucenych lokaci, navratu doporucenych palet
- po vyberu doporucene palety probiha predvyplneni sarze, lokace a mnozstvi

[verze "4.56"] PeV (3.8.2015)
-------------
Prijem
- konfiguracni moznost zobrazeni dialogu pro zadani/nezadani skladu pri generovani davky
- konfiguracni moznost pro automaticke otevreni davky ihned po stazeni (momentalne se automaticky neotevre, pokud se da "Zpracuj davku" a na disku zadne nejsou -> probehne stazeni)
- konfiguracni moznost pro foceni pri zpracovavani davky (jako nazev sóuboru se pouziva SERLTNUM_X, kde X je cislo od jedne vys) ... fotky se zatim neodesilaji
- pridano volani online metody pro generovani cisla palety (nahradi se zadane SERLTNUM) - moznost konfiguracne zapnout/vypnout
- pridano volani online metody pro overeni lokace - moznost konfiguracne zapnout/vypnout

Prodej
- 095 (zbozi) rozsireno o sloupec SERLTNUM
- CZMST_DI rozsireno o sloupec LOCNCODEDEST
- struktiry 092 rozsireny o:
	[SKL_ID] [varchar] (20) NULL, 		-- !! pouzit misto LOCNCODE !!
	[cfg_lokace] [tinyint] NULL,		-- zadavat zdrojovou/cilovou lokaci
	[cfg_lokace_ciselnik] [tinyint] NULL,	-- oprit zadavani lokace o ciselnik lokaci (!!neni implementovano!!)
	[cfg_lokace_dest] [tinyint] NULL	-- pozadovat zadani cilove lokace (1 - ano, 0 - ne)
- pridano volani online metody pro overeni lokace 
- pridano volani online metody pro navrat doporucenych lokaci (vrati pouze seznam, jinak se s nimi zatim nepracuje)
- pridana moznost zadani cilove lokace (locncodedest)
- pridana moznost vyhledavani podle sarze v tabulce 095 - 
- pridan ProdejVyberPaletyList formular pro zobrazeni seznamu doporucenych lokaci palet, ktery vraci procedura na serveru

Servis
- ServisDynamickaTabulka - scanner se jiz vypina pri sejmuti kodu pouze u jedne polozky a ukoncovani formulare

[verze "4.55"] PeV (28.7.2015)
-------------
- tabulka CZMST095 rozsirena o sloupec [SERLTNUM] [char] (21) NULL + uprava datasetu
- tabulka CZMST_DI rozsirena o sloupec [LOCNCODEDEST] [char] (11) NULL + uprava datasetu

[verze "4.54"] PeV (28.7.2015)
-------------
- pridano chybejici dispose u prepinani klavesnice
- do formularu, kde se zadavaji pouze ciselne hodnoty (vcetne zadavani poctu vytisku) dochazi k automatickemu prepnuti na numerickou klavesnici (pokud je nastaven typ v Settings.xml)
- povoleno scrollovani na nekolika mistech v konfiguracnim formulari (nevesly se veskere parametry na mensim displeji)
- prepinani klavesnice doplneno do vsech formularu, kde se zadava nejaka hodnota

[verze "4.53"] PeV (14.7.2015)
-------------
Vydej
- u vypisu polozek se vypisuje sloupec Note
- pri zadavani mnozstvi se zobrazuji data ze sloupce Note

[verze "4.52"] PeV (13.7.2015)
-------------
KeyboardManager
- implementace tridy KeyboardManager pro prepinani mezi numerickou a alfanumerickou klavesnici (vyuziva se pouze v ServisSejmiKodForm)
- do Settings.xml je treba ke klici "KeyboardType" pridat jednu z techto hodnot: Unknown, Symbol_QWERTY, Symbol_NoQWERTY

Servis
- prepsana funkcionalita tlacitka zpet (jde se vratit o libovolny pocet kroku az do puvodniho stavu, kdy doslo k vyberu Zdroje)
- odesilani dat probiha formou odeslani celeho ciselniku (resp. tmp ciselniku) v jinem vlakne - styl Vyroba_P. 
- databaze zdrojpohyb se odesila pouze v pripade, ze je alespon jeden zaznam na odeslani
- behem automatickeho odesilani ZdrojPohyb dochazi ke kontrole, zdali je momentalne vybrany nejaky ZdrojStav. Pokud je tak dojde k odeslani dat starsich, nez aktualne vybrany ZdrojStav
- po vyberu ZdrojeStavu ze seznamu zustava otevrene spojeni s databazi pro adaptery ZdrojPohyb a ZdrojStav kvuli zrychleni aplikace
- predelani vzhledu vsech formularu + pridani "Krok zpet" do menu
- pri stahovani ciselniku ZdrojStav dojde k nacteni neodeslanych dat ze ZdrojPohyb a pripadnemu updatu zaznamu
- po aktualizaci veskerych ZdrojStav ze serveru (stazeni ciselniku) dojde k porovnani s neodeslanymy daty ze ZdrojPohyb a pripadne aktualizaci na nejnovejsi data
- oprava, kdy nefungovalo korektne tlacitko zpet pri vyberu z nekolika cinnosti

[verze "4.51"] PeV (3.7.2015)
-------------
Servis
- pridany konfiguracni moznosti pro zapnuti/vypnuti zobrazeni potvrzujiciho dialogu pro zmenu stavu/cinnosti
- pridan statusbar do formularu zadani textu/cisla, potvrzeni ANO/NE, zmena stavu a cinnosti
- rozsirena struktura a dataset CZMST_Servis_Zdroj o sloupec "Type" - text, zdali je suchy/chlazeny
- rozsirena struktura a dataset CZMST_Servis_Cinnost o sloupec "Mandatory" (povinne/nepovinne zadani hodnoty) 1 - povinne, 0 - nepovinne
- rozsirena struktura a dataset CZMST_Servis_Cinnost o sloupec "RequiredLength" (pocet znaku, ktery se musi zadat pri zadavani cisla/textu). Pokud je null, tak se nekontroluje
- do konfigurace aplikace pridana moznost zobrazeni ve statusbaru: nazev stavu, id zdroje, oznaceni zdroje, typ zdroje
- pridana moznost vypnout/zapnout hlasku o dokonceni aktualniho stavu

[verze "4.50"] PeV (29.6.2015)
-------------
- odstraneno logovani zapnuti/vypnuti scanneru v LoginForm
Prodej
- rozsirena struktura czmst092 (typdokladu) o cfg_tisk_soupis (povoleni tisku soupisu)
- uprava datasetu typdokladu
- pri tisku soupisu probiha kontrola, zdali je povolen tisk soupisu v typu dokladu
- pri tisku soupisu pomoci klavesy "3" se jiz kontroluje, zdali je povolen print server a tisk soupisu
- do tisku soupisu se pridava QTYPACK a QTYSHPPDMJ z czmst_di

[verze "4.49"] PeV (23.6.2015)
-------------
- v konfiguraci aplikace presunuty veskera nastaveni tisku pro dany modul do nove vytvorene zalozky Tisk

[verze "4.48"] PeV (23.6.2015)
-------------
Prodej
- do tisku soupisu se pridava str_desc a str_carcode z czmst091

[verze "4.47"] PeV (18.6.2015)
-------------
Prodej
- zvetsen maximalni rozsah cisla davek pri automatickem generovani v formConfig
- do tisku soupisu se pridava CZ_CarKod a VNDITNUM z czmst_di

[verze "4.46"] PeV (18.6.2015)
-------------
Prodej
- uprava konfigurace tisku (pridana moznost PovolitTiskEtiket - vyuziva puvodni atribut "PovolitTisk" v ConfigModules, PovolitTiskSoupisu, TiskEtiketyPoPridaniZbozi, TiskSoupisuPriUzavreniDavky)
- oprava, kdy po opetovnem najeti do konfigurace aplikace se nezobrazoval usercontrol pro webservice
- do dat pro tisk soupisu se pridava ITEMCODE
- tisk soupisu se jiz neposila po jednom vytisku (jak pro bluetooth tiskarnu tak pro webservice)
- pridana konfiguracni moznost predvyplnit pocet etiket/soupisu pri tisku

Servis
- pridana konfiguracni moznost pro zapnuti/vypnuti synchronizace zaznamu ihned po pridani noveho zaznamu

[verze "4.45"] PeV (16.6.2015)
-------------
Prodej
- oprava podminky pro zobrazeni dialogu pri overovani pohybu (OverPohyb)
- uprava popisu konfigurace pro pozadovani hesla pro otevreni rozpracovane davky

[verze "4.44"] PeV (12.6.2015)
-------------
Prodej
- do konfigurace prodeje pridana zalozka Tisk, kam byly presunuty veskere tisky pro prodej
- pridana moznost tisku soupisu pri ukoncovani davky (konfiguracne jde zapnout/vypnout - je treba mit take povolen tisk v prodeji)
- hlaska z funkce OverPohyb zobrazuje zadane mnozstvi, jiz nactene mnozstvi a mnozstvi na sklade
- OverPohyb odesila soucet nove zadaneho a jiz ulozeneho mnozstvi v databazi
- rozsiren dataset Uzivatele (czmstpwd) o jmeno a prijmeni
- pri tisku soupisu se nacita jmeno a prijmeni prihlaseneho uzivatele

[verze "4.43"] PeV (8.6.2015)
-------------
Prodej
- konfiguracne moznost zakazani otevreni zpracovane davky (pri zadani spravneho hesla je mozno davku otevrit)
- uprava funkce OverPohyb
- konfiguracne umozneno povoleni/zakazani zadani mnozstvi scannerem

[verze "4.42"] PeV (4.6.2015)
-------------
Vydej
- VlozTypyPaletForm nacita defaultne zvolenou pozici z Settings.xml (klic VlozTypyPaletFormDefault)

[verze "4.41"] PeV (3.6.2015)
-------------
Vydej
- scanner jiz funguje po chybe pri tisku paletoveho listku

[verze "4.40"] PeV (3.6.2015)
-------------
- NaplnPolozkuSN zobrazuje ITEMNMBR

[verze "4.39"] PeV (1.6.2015)
-------------
PrinterProviderWebService
- implementace tisku soupisu (kazdy vytisk se momentalne posila zvlast)
- obraceni podminek pri "one way" tisku
Inventura
- ListInventury_sqlce si jiz pamatuje rozlozeni datagridu
- NaplnPolozku_sqlce zobrazuje ITEMNMBR

[verze "4.38"] PeV (28.5.2015)
-------------
Prodej
- rozsireni databaze Prodej.sdf o sloupec ITEMDESC.
- pri vkladani do CZMST_DI se vklada i ITEMDESC
- pri tisku soupisu se prvne bere ITEMDESC z czmst_di. Pokud je null, pokusi se nacist z databaze Zbozi.sdf
- tisk soupisu se posila po castech

[verze "4.37"] PeV (11.5.2015)
-------------
Servis
- pri vyberu cinnosti a stavu je mozne vybirat nactenim caroveho kodu
- pri vyberu cinnosti a stavu je mozne vybirat zadanim cisla radku (focus musi byt na datagridu)
- v statusbaru se vypisuje cislo navesu
- po dokonceni vsech cinnosti zvoleneho stavu dojde k automatickemu navratu do seznamu (mozno konfiguracne zmenit)

[verze "4.36"] PeV (17.4.2015)
-------------
- pridan ConfigAppForm, pomoci ktereho je mozne pri spusteni aplikace zvolit jinou slozku, odkud se budou brat data
- oprava nacitani konfigurace z ConfigModules (na nacitani se pouzival "ServerAccessType" a na ukladani "ServerAccess")
- pri kontrole licence na serveru jiz dochazi k UpdateWebServiceCredentials

[verze "4.35"] JiS (5.3.2015)
-------------
Prodej
- Vyber davky upraveno/opraveno zobrazeni hodnota v data gridu
- Vyber davky optimalizace prochazeni davek a dohledavani dat v ciselnicich 
- Doplnena reindexace ciselniku zbozi a jejich MEN (CZMST095M)

[verze "4.34"] JiS (5.3.2015)
-------------
Inventura1
- rozsireni o vyber skladu => dodelat 
Prodej
- Cenove hladiny v cizich menach
- tisk bluethoot


[verze "4.33"] JiS (5.3.2015)
-------------
Prodej
- pri mazani polozky je nove dotaz, zda smazat polozku
BluetoothPrinterSymbolWPAN
- pokud je zadana Mena sablony, tak se zjisti, zda existuje soubor s priponou idmeny ([.EUR|.CZK])
	= jestlize existuje, tak se pouzije tento
	= jestlize neexistuje, tak se pouzije vychozi nastaveny
	=> alternativy sablon pro ruzne meny...

[verze "4.32"] JiS (2.3.2015)
-------------
Printer factory: podpora pro ruzne typy tisku
- predelano nastavovani
- konfigurace nastaveni tisku v tiskovem modulu samostatne ... 

Zmeny struktur ciselniku


[verze "4.31"] PeV (14.1.2015)
-------------
Prodej
- vracena puvodni metoda tisku (uz se ceka na dokonceni)

[verze "4.30"] JiS (23.12.2014)
-------------
Inventura1
- dialogy zadani mnozstvi a SN rozsireny o zobrazeni poli "Kod polozky" a "Merna jednotka"

[verze "4.29"] PeV (16.12.2014)
-------------
Prodej
- zmenen tisk na tisk bez navratove hodnoty (kvuli urychleni tisku)

[verze "4.28"] PeV (4.12.2014)
-------------
Prodej
- opraveno vyhledavani podle ITEMCODE (pri vyhledavani byl prohozen sklad_id a ITEMCODE)

[verze "4.27"] PeV (28.11.2014)
-------------
Servis
- ServisCinnostZmena.FillCinnosti() nepadá do exception, když je pouze jedna navazujici cinnost s hodnotou NULL

[verze "4.26"] PeV (28.11.2014)
-------------
Servis
- vyhledávání ve výpisu zdrojů podle ZdrojID

[verze "4.25"] PeV (26.11.2014)
-------------
Servis
- pridana moznost vyuziti dynamicke databaze a vyplnovani textu (s moznosti vlozit prazdny text)
- opravena chyba, kdyz se nedalo prejit z CinnostNext = NULL do dalsiho stavu
- opravena chyba filtru, kdy se dalo z nejake cinnosti tlacitko zpet a seznam byl filtrovan (ikdyz nemel byt)
- vytvoren formular pro volbu dat z dynamicke databaze (mnoznost vyhledavani, aktualizace, razeni, vyhledavani scannerem)
- vytvoren formular pro volbu ano/ne
- uprava datasetu a struktur (do CZMST_Servis_CinnostNext pridan sloupec)
- moznost aktualizace dynamickych tabulek (soucasti aktualizace ciselniku)

[verze "4.24"] PeV (13.11.2014)
-------------
- pridan CountEntries do CZMST_DIH kvuli prevodu mezi sklady
- uprava datasetu CZMST_DIH
- presun nastaveni zapnuti online funkce overeni pohybu (Prodej) do Online v formConfig.Prodej
- opraveno znovu dotazani pri prevodu skladu (pokud byl jiz drive zvolen)

[verze "4.23"] PeV (11.11.2014)
-------------
- pridany do formConfig moznosti zapnout tisk v prodejnim modulu, overovani pohybu
- pridany do formConfig - Print server moznosti zvolit sablonu pro Prodej a Inventuru
- uprava datasetu podle zmen v DB
- pridan ProdejTisk do Prodej_3 a InventuraTisk do Inventura1_sqlce
- pridana moznost tisku do formularu u Prodej_3 a Inventura1_sqlce
- V Prodej_3.ProdejList pridana moznost overeni pohybu a moznost tisku v PridatPolozku
- V Prodej_3.ProdejMain pridana moznost prevodu mezi sklady

[verze "4.22"] JiS (20.10.2014)
-------------
Scanner
- pridana podpora pro PSION WorkAbout G2

[verze "4.21"] JiS (11.9.2014)
-------------
Inventura1
- List variant I3 rozsiren o zobrazeni názvu položky

[verze "4.20"] JiS (11.9.2014)
-------------
Prijem, Vydej, Inventura1
- rozsireni o dohledavani polozky, pokud neni nalezena dle caroveho kodu, tak dohledanim do predlohy SN(Sarzi)
Inventura1
- rozsireni o dialog zadani SN (Dropdown s predlohou SN)

[verze "4.19"] JiS (10.9.2014)
-------------
Servis
- rozsireni funkci a impelmentace Cinnosti
- novy dialog SejmiImageForm pro fotky imagerem
- konfigurace timeoutu modulu

[verze "4.18"] JiS (29.8.2014)
-------------
Oprava Vydej
- dialog zadani SN (Spomalovani procesu vydeje)
Rozsireni
- Prijem, Vydej o cislo skladu (SKL_ID)
Novy modul "Servis"

[verze "4.17"] JiS 
-------------
Prijem 
- datum prijmu : dialog upraveny design tlacitek

[verze "4.16"] JiS 
-------------
Vydej 
- Priority : povolit stazeni davky, pokud je davka jiz rozpracovana. Pokud neni rozpracovana, tak se kontroluji priority
	=> pouze pokud je nastaveno stahovani nejvyssich priorit ...
- ListPolozek : nove klavesove zkratky pro vyvolani tisku, pouze pokud je povolen tiskovy server
	=> F9 : tisk etikety
	=> F10 : tisk pal. listku
Ukolovani : dalsi rozsireni a management pripomenuti
- upozoreni na novy ukol je formou MessageBoxu, ktery je zobrazen dokud se rucne nezrusi
- pripomenuti se zobrazuje casovym dialogem a tak dlouho, dokud uzivatel nezmeni v editaci ukolu nebo dokonci a zmeni stav na dokonceno
	=> pri zmene na konecny stav se pripomenuti automaticky rusi aby se dale nezobrazovalo.
	=> uzivatel si muze pripomenuti nastavit dle sve potreby
	=> pripomenuti se zobrazi pokud je cas mensi nez aktualni cas na terminalu a interval zjistovani je stejny jako interval synchronizace (tedy muze byt nejaka prodleva zobrazeni ukolu)
- Prehled novych a aktivich(nedokoncenych) ukolu je zobrazen ve stavove liste hlavni nabidky MST
	=> U*:1N, 2A = U=ukolovani, [*=probiha synchronizace, -=neprobiha], 1N=jeden novy ukol, 2A=2 aktivni(nedokoncene)

[verze "4.15"] JiS 
-------------
Graphics.DataField
- pridana maximalni delka vstupniho pole
MessageBoxBigTimeout
- parametr, zda prehravat defaultni zvuk. Slouzi k potlaceni, pokud se drive prehrava jiny, protoze to jde asynchronne a pozdejsi pretlaci drivejsi. Nicmene i tak mohou nastavat kolize(mozna upravit jeste predani zvukoveho souboru, pokud je jiny nez default...) 
Ukolovani (Main)
- pridana metoda pro vyvolani dialogu se soupisem novych ukolu
- pokud je novy ukol prave jeden, tak moznost prime editace ukolu (Ano=Otevrit, Ne=pokracovat)
- pokud je ukolu vice, tak jen zobrazeni seznamu (OK)
- pridan zvuk "notify.wav" pro upozorneni na novy ukol pomoci checkeru
- opraveno zobrazeni hodnoty DatumDo v editacnim okne
- nastavena maximalni delka pro zadani pole "note" (Poznamka) pri editaci na 200 znaku(velikost datoveho pole)

[verze "4.14"] JiS 
-------------
Prodej
- oprava zobrazeni hodnot v listovani nasnimanymi polozkami
- rozsireni di o itemcode
Prijem
- pri ukonceni davky volba data vzniku dokladu
- tisk etikety predloha
	=> rozsireno o dotazeni detailu polozky objednavky => musi se implementovat v providerovi		

[verze "4.13"] JiS 
-------------
Ukolovani
- rozsireni funkcnosti
- opravy

[verze "4.12"] JiS 
-------------
Ukolovani
- zakladni funkcnost
Vydej
- VydejDavkyVyberJenScannerem : Novy parametr povoluje/zakazuje vyber davky rucne
- VydejPovolitKontrolaSNPredloha : Novy parametr povoluje/zakazuje kontrolu SN z predlohy SE_SN
- oprava kontrola sarze vuci listu zbozi (Zobrazit alternativy)
Prodej
- oprava : pokud neni nastaven parametr cfg_mena_id (resp. je NULL), tak aplikace spadla


[verze "4.11"] JiS 
-------------
Prijem
- pridan parametr ViceTerminaly, ktery zajistuje, ze pri odeslani davky se algoritmus nepta na "Uzavrit davku"
	=> pokud se davka zpracovava vice terminaly, tak toto je akce, ktera uzamkne data
	=> bylo reseno pro HeO, kde se data pouze predavaji na server a finalni import do dokladu se provadi az na strane pluginu HeO

[verze "4.9"] JiS 
-------------
Prijem (4)
- tisk etiket predlohy po otevreni davky na dotaz(y)
- predloha ser.cisel/sarzi 
	=> nova struktura
	=> novy dialog pro vyber sarze pri vyberu polozky
- konfigurace "Povoleni slucovani davek"

Vydej (3)
- predloha ser.cisel/sarzi 
	=> rozsirena o mnozstvi pro sarzi
	=> novy dialog pro vyber sarze pri vyberu polozky


[verze "4.8"] JiS 
-------------
Prijem
- tisk etiket nasnimanych pred odeslanim ... 


[verze "4.7"] JiS 
-------------
???

[verze "4.3"] JiS 
-------------
Sklady
- Pridana metoda do webservices : Export
- uprava stahovani skladu na terminalu
- uprava zobrazeni nazvu skladu v Prodej, Inventura, Zbozi(Eurom)
	=> dotazenim nazvu skladu z ciselniku skladu ... 
	=> sklady je nutne synchronizovat ... 
Prijem
- doplneny Merne jednotky MJ ...
- opraveno slucovani 
Prodej
- cizi meny
- vyhledavani polozek dle CK a ODBeratele/DODavatele
- rozsireni odberatelu o menu 
- rozsireni zbozi o ODB_ID

Obecne
- opraveno padani aplikace pri odhlaseni uzivatele a ukonceni


[verze "4.2"] JiS v Eurom 26.11.2013
-------------
- loginform : scannerfinalize nebyl volan pri keydown key=esc (pouze dialogresult) 
	=> upraveno na volani konec_but_click(null, null)
	? tak toto neni ten duvod ... 
	- snaha zachytit a zalogovat vyjimku abyse nezobrazovala => nutno doresit ... 
	- 
- odeslani davky prijemky nezavreni cekaciho dialogu (existuji 2 ???)
	=> oprava - do funkce odesli davku pridano zobrazeni cekaciho dialogu a jeho korektni ukoncovani pri vyjimce a ve finally ...

[verze "4.1"]
-------------
- pridana moznost prihlaseni uzivatele nactenim caroveho kodu
Prodej
- do konfigurace pribyla moznost vypnuti/zapnuti pridavani nove polozky, ktera neni v seznamu
- indexace po stazeni ciselniku zbozi ...
- rozdeleni exportu a stazeni ciselniku zbozi 

[verze "3.50"]
--------------
Vydej : upravy z testu
- Sloucena davka : zobrazovani souctu za davky ve sloucene
	=> celkem polozek
	=> suma polozek
	=> objednavka = pocet davek ve sloucene
- Hromadne plneni 
	=> serazeni dle cisla objednavky (SOPNUMBE)
	=> uprava dialogu (design)
	=> zmena popisu na "Zadejte mnozstvi"
	=> pridan titulek okna "Hromadne plneni"
	=> zmena algoritmu vyberu polozky k zadani
		1) hledani nezadane/nevyplnene
		2) pokud je vse nejak zadane, tak se hleda prvni neuplna

[verze "3.49"]
--------------
Vydej
- Vyber pracovnika : oprava zobrazeni pokud je nejaka hodnota NULL
- Zmena algoritmu hromadneho plneni
	=> upraven dialog pro zadavani mnozstvi hromadneho plneni 
- oprava hromandeho plneni [20.6.2013 JiS]
	=> zobrazuje se mnozstvi zbyvajici pro danou polozku (aktivni vybranou v seznamu)
	=> oprava kontroly vyplnenosti hromadneho seznamu po polozce (Zbyva > 0 => nekompletni)
	=> prvek "Nacist" obsahoval hodnotu "Zbyva", nyni zobrazuje hodnotu kolik se ma nacist dle predlohy, tedy zobrazuje hodnotu "Mnozstvi"

[verze "3.48"]
--------------
- Uprava/Oprava komunikace se scannerem rady T+T Netcomm 8000
	=> IScannerFactory rozsiren o nastaveni topForm
	=> Inicializace scanneru v hlavni aplikace(presunuto z modulu)
Vydej
- FormPracovnici : oprava prace s praci/ukoncovanim scanneru

[verze "3.47"]
--------------
Konfigurace
- uprava ulozeni neexistujicich elementu 3.urovne(/Config/Modules/<ModulX>). Pokud <ModulX> neexistuje, je automaticky vytvoren ...
- doplnena moznost akutalizce dokladovych tiskaren a pracovniku z menu Konfigurace
Vydej
- doplnena volba vyberu pracovnika pred odeslanim davky (dle volby dokladove tiskarny)
- rozsiren soubor Vydej.sdf predlohy o CZMST_SIH.PRAC_ID
Forms
- Pridan obecny formular pro vyber pracovnika (dle vzoru prodeje)
- Konfigurace tohoto modulu mozna v Settings.xml 
	=> [VyberPracovnikaRowsCount] : pocet polozek k zobrazeni v seznamu
	=> [VyberPracovnikaLastSort] : zde se uklada posledni nastavene razeni
Events
- upravena logika synchronizace(odesilani), pokud nejsou v udalostech zadna data, tak se soubor smaze a neprenasi se
Inventura2(Evidence majetku)
- upraveno zobrazovani hodnot aktivnich filtru lokaci na hodnoty Lokace1, Lokace2, Nazev, EANL
 
[verze "3.46"]
--------------
Prodej
- oprava prenosu nastaveni cenových hladin ze serveru
- úprava zobrazení celkových cen sDPH a bezDPH
- úprava aktualizace formu nasnímané při smazání položky

[verze "3.45"]
--------------
Vydej
- pridana moznost slucovani davek, rozsireny hlavicky
- pridana moznost hromadneho vydeje polozek (konfiguracne)
- pridana moznost vyberu tiskarny dokladu pred odeslanim davky

Obecne
- upraven graphicbutton
- pridan do vsech modulu...

[verze "3.44"]
--------------
Vydej
- doplnen dotaz na tisk pal. listku pri otevření dávky a dávka je kompletní při potvrzení odeslání bez otevření dávky

[verze "3.43"]
--------------
Upravy NEKUPTO + opravy
Udalosti obsluhy
- pridany do hlavniho menu
- doplneny udalosti tisku na Prijmu a Vydeji 
upravy z testu 20130322
- oprava cyklu
- optimalizace dotazu do ciselniku zbozi
upravy z 20130324 a testu z 20130322
- pokud se ve vyberu nalezne vice polozek pomoci car.kodu, tak se zobrazi ListMNForm
	=> zde se prebira nastaveni zakazni vyberu polozky enterem nebo tlacitkem OK dle nastaveni hlavniho okna seznamu polozek
	=> Pokud bude nalezeno vice variant zbozi v tomto rezimu, tak neni mozne polozku vydat => !!! varianty baleni nemohou fungovat, pokud maji stejny car.kod(vnditnum X cz_carkod)
- pokud je zakazano preplneni polozky v nastaveni modulu na terminalu, tak se pred zobrazenim variant v okne ListMNForm odstrani z nalezenych zaznamu ty polozky, ktere jsou nasnimane (nasnimano>=qtyshppd)


[verze "3.42"]
--------------
Inventura2 (Evidence majetku)
- oprava "zmena strediska" v dialogu zadani mnozstvi

[verze "3.41"]
--------------
Prodej
- novy ciselnik "Pracovnici"
- novy sloupec CZMST_SI.PRAC_ID
- novy prvek k vystupu na prodeji

Obecne
- upraveny vyhledavaci dotazy dle caroveho kodu pomoci t-sql union
	=> vyuziti indexu vnditnum a cz_carkod pro vyhledavani pomoci indexu (misto OR)


[verze "3.40"]
--------------
Inventura
- pridany dva nove sloupce (REZ1, REZ2) do listu polozek a detailu polozky (nazev techto sloupcu se nacita z konfiguracniho souboru pro
rozlozeni sloupcu u gridu)

Vydej
- moznost stahovani pouze davky s nejvyssi prioritou (konfiguracne zavisle)
- moznost nastaveni ruzne barvy radku pro ruzne priority vydejek (priority columns) 

[verze "3.39"]
--------------
Prijem4, Prijem3 (Memory)
- oprava vyhledavani dle cz_carkod a vnditnum (chyba v dotazu bylo AND misto OR v datasetu czmst_pe.fillbybarcode)

[verze "3.37"]
--------------
Vydej
- odeslani davky : pri pramateru VydejPokracovatNaJinemTerminalu umoznen dotaz zda pokracovat na jinem terminalu nebo davku dokoncit...

[verze "3.37"]
--------------
Inventura1
- oprava chyceni vyjimky pri vyhledavani polozky rucnim vyberem v pripade ze existuji duplicity v I3
- pridan do klice I3 sloupec DEX_ROW_ID
- uprava pouzivani ciselniku skladu, pokud neexistuje datovy soubor skladu, tak se neprovadi dotahovani nazvu skladu

[verze "3.36"]
--------------
Vydej
- oprava predavani LOCNCODE na vystup pokud neni nastavena prace s lokacemi
- uprava zobrazeni obderatele pokud se nimi nepracuje na "Nezadáno"

[verze "3.35"]
--------------
Inventura1
- vyhledavaci dialog dle nazvu polozky upraven o nastaveni "Fultextoveho" vyhledavani
- vyhledavaci dialog rozsiren o pamatovani posledni zadane hodnoty a pamatovani nastaveni volby fultextoveho vyhledavani

[verze "3.34"]
--------------
Vydej
- Zobrazeni hodnoty "Sarze" na dialogu "Zadani mnozstvi", ktera byla nasnimana v predchozim kroku
  => obecna uprava k zobrazeni nasnimanych hodnot(aktualne pouze "Sarze"/"Seriove cislo")
  => hodnota se zobrazuje pouze v pripade ze je nastavena a neni prazdny text("")

[verze "3.33"]
--------------
Vydej
- stazeni davky: pri odstanovani existujicich opraven cyklus foreach na for(int...) pri vice vznikala vyjimka...
- tisky etiket: predloha, nasnimane, pal.listek
	> predloha : na vyzadani z listu polozek
	> nasnimane : dle parametru "po vlozeni polozky do vystupu" - nutne nastavit konfigurace vydeje na terminalu (Hlavni parametry)
	> pal.listek : na vyzadani z listu polozek (sablona serveru se nastavuje v tiskovem serveru)
Informations.DetailPolozky : rozsireno o druhy paremetr "Doklad"
	> Prijem = predava se hodnota "PONUMBER"
	> Vydej = predava se hodnota "SOPNUMBE"
	> Prodej = predava se prazdny retezec ("")

[verze "3.32"]
--------------
ScannerFactory.dll
- upravena inicializace scanner objektu na relativni cestu v konfiguraci scanneru ("Path" v scannerfactory.xml)

MST_W
- inicializace: uprava nacitani externich modulu ve spravne vetvi
- oprava prace se scannerem ve vsech modulech (prechod vsude na dynamicke volani)

[verze "3.31"]
--------------
Vydej
- pridana moznost pri vyzadovani cisla sarze (cz_sernum_track) overit hodnotu vuci ciselniku zbozi (CZMST095)
- konfigurace ke kazde davce (vydejce), vydejparams.xm, posledni 3 parametry  
- form pro zobrazeni

[verze "3.30"]
--------------
Obecne
- pridano dynamicke nacitani scanneru (nastaveni v ScannerFactory.xml)

Prodej
- Volba razeni v seznamu obderatelu, stredisek a seznamu polozek se uklada do nastaveni uzivatele a pri dalsim spusteni je pouzito posleni nastaveni razeni
- Pridana konfiguracni volba zobrazeni maximalniho poctu polozek v gridech (odberatele, strediska, polozky)
- V seznamu polozek pri vyhledavani a prekroceni nastaveneho maximalniho poctu zobrazenych polozek se zobrazi hlaska (tato hlaska je misto prekroceni 100 polozek)

[verze "3.29"]
--------------
Prodej
- hodnota LOCNCODE se prednastavuje a prenasi do vystupu z vybraneho zaznamu Zbozi

[verze "3.28"]
--------------
Opravy z testu Cxxxx_MST_HO-009
- Vydej: F7-Zmena lokace, Pokud neni funkcionalita aktivovana, tak se akce znepristupni
- Logovani spusteni/ukonceni aplikace
- Oprava designu Datagrid2.SelectedRows
- Inventura1: oprava aktivace scanneru v poradi - mnozstvi, seriove cislo ...
- Inventura1: uprava pripravy dat pro zobrazeni - Sklad, pokud neni jeho nazev definovan vypisovalo vyjimku
- Prijem: v seznamu nasnimanych polozek pridan status bar s informaci o celkovem poctu polozek a indexu aktualni polozky
- Vydej: v seznamu nasnimanych polozek pridan status bar s informaci o celkovem poctu polozek a indexu aktualni polozky
- Prodej: upraveny datagridy
- Prodej: seznam davek - zruseny buttony, pridano menu a klavesove zkratky
- Obecne: funkcionalita, ktera neni dostupna, je mozna vypnout v nastaveni (disable)

- upraveny datagridy a detaily v Inventure2

Prodej
- MST_HO : doplneno nastaveni typu prodejnich cen(hladin) z tabulky externi konfigurace (CZMSTCFG)
		 : aktualizace probiha z hlavniho menu prodejniho modulu. 
		 : aktualizuje se globalni nastaveni prodejniho modulu

- Doplnena moznost konfiguracne zpristupnit online funkce (pocet kusu polozky,
pocet kusu polozky na sklade a detail polozky)

[verze "3.27"]
--------------
Inventura1, Inventura2
- oprava CFG_PovolitZaporneMnozstvi, pokud je aktivni, tak neumoznilo zadani hodnoty 0

[verze "3.26"]
--------------
Evidence majektu(Inventura2)
- Oprava RFID/rucni potvrzeni
- Oprava zobrazeni nazvu sloupce "Kategorie" v zakladnim prehledu
- RFID: oznaceni posledniho vlozeneho zaznamu podbarvenim celeho radku
- Nasnimane: doplneno vyhledani nasnimanych zaznamu nacteni ČK.

[verze "3.25"]
--------------
Evidence majektu(Inventura2)
- uprava vyhledani klice dle I_CISLO, KATEGORIE (respektive vystupni struktura terminalu rozsirena o INVENTUR.ID_MAJETEK, kterym se dohledava predloha)
- optimalizace INDEXU databaze pro rychlejsi provadeni dotazu/trideni zaznamu
- implementace skaneru v nasnimanych(po nasnimani c.k. se vyhledaji zaznamy s uvedenym c.k.)

[verze "3.24"]
--------------
Vydej
- zobrazení stavu výdeje ve statusBaru (zobrazuje se pouze pokud je aktivní kontrola dokončenosti)
- zobrazení stavu výdeje v dialogu pro dotaz na odeslání dat, pokud je dávka úplná
- oprava nenactenych konfiguracnich parametru a updatestatusbar pri odesilani nekomplentni davky z hlavniho menu modulu vydej
- oprava nacteni souboru davek, ktere nejsou v seznamu

[verze "3.23"]
--------------
Tisky upravy/opravy
- input box: selectall()
- tisk nepredvyplnuje mnozstvi etiket k tisku
- oprava reaktivace scanneru po tisku
- predloha nasnimane obsahuje vsechny informace + i z predlohy (provadi se dohledanim zaznamu v PI, PE)
- oprava orezavani textovych hodnot zasilanych tiskovemu serveru

[verze "3.22"]
--------------
Inventura1
- Potlaceni zobrazeni mnozstvi k nasnimani
- Online funkce BYZNYS "Novy EAN" - mozne nastavit defaultniho partnera pro vsechny polozky
Main
- Online funkce BYZNYS "Novy EAN" - s online vyberem polozek
Konfigurace
- Povoleni Online funkci BYZNYS
- Nastaveni pro tiskovy server
	> nacteni dostupnych tiskaren na serveru
	> nacteni dostupnych sablon etiket na serveru
	> Prijem : nastaveni sablon etiket pro tisk predlohy x nasnimanych (z dialogu mnozstvi se tiskne predlohova sablona)
Prijem_4
- podpora tisku pres tiskovy server (ListPolozek, Nasnimane, Vkladani mnozstvi)

[verze "3.21"]
--------------
Inventura1
- Rozsireni o Online Funkci "Vlozeni noveho EAN kodu materialu do IS"

[verze "3.20"]
--------------
Vydej
- shodny/neshodny vyrobek pomoci rez_2 na vystupu (konfiguracne zavisle pri zadavani mnozstvi)

[verze "3.19"]
--------------
Oprava nacitani settings.xml
- logovani chyby
- oprava parametru prijmu PrijemZobrazitVseNeuplne (zmena na true/false) => zpusobovalo chyby TypeLoadException, protoze xml soubor nebyl korektne formatovan...

[verze "3.18"]
--------------
Prijem + konfigurace PrintServer
- doplnena moznost tisku etikety pomoci printserveru
- konfigurace nastaveni printserveru v konfiguracnim dialogu

[verze "3.17"]
--------------
Vydej
- nova funkcionalita : predloha seriovych cisel (tabulka CZMST_SE_SN, podle implementace na Inventura1)
- Zadavani hodnoty LOCNCODE (globalni parametry vydeje ze serveru)
	> neumoznit zmenu prednastaveneho kodu
	> zakazat rucni potvzeni, umoznit pouze nactenim caroveho kodu scannerem

[verze "3.16"]
--------------
[20.10.2011]
Inventura2 (Evidence majetku)
- oprava deaktivace scanneru dialogu mnozstvi pri funci "Zpracuj vse"
- oprava prace se scannerem pri funkci "Zpracuj polozku"
- uprava reaktivace rfid scanneru tt8000 po chybe komunikace s hardwarem (latence cca 5s)

[16.9.2011]
Prodej
- Vyhledavani defaultni MJ upraveno (optimalizovano)
- Vyhledavani defaultni MJ vyvedeno do konfigurace prodeje, je mozne aktivovat/deaktivovat
Prijem_4
- opravy vnitrniho kolecka
- uprava managementu davek(stahnout, zpravovat) dle vydeje
RFID
- uprava vylouceni konkretnich nastavenych tagu


[verze "3.15"]
--------------
[16.9.2011]
Prodej
- Zbozi rozsireno o prvek REZ1 nvarchar(10)
- Konfiguracni parametr "Mnozstvi nastav ze zbozi REZ1"
	> je-li aktivni, tak do hodnoty "mnozstvi" predvyplni hodnotu uvedenou v REZ1 u zbozi, pokud je vyplnena
- Umozneno nastaveni dodatecneho filtru na nazev polozky pro zobrazene radky
	> filtr se deaktivuje v okamziku vyhledavani nebo pri jeho zmene a nastaveni na prazdny retezec

[verze "3.14"]
--------------
[11.6.2011]
RFID
- Pridana podpora pro rfid terminal motorola-MC319Z

[verze "3.13"]
--------------
[27.5.2011]
Prodej
- konfiguracne mozne nastavit automaticke vkladani polozky s mnozstvim 1, pokud je sledovano pouze na mnozstvi

Inventura2 (Evidence majetku)
- implementovano rozhrani pro RFID reader terminalu Motorola-MC9090G

[verze "3.13"]
--------------
[25.5.2011]
Prodej
- Konfigurace: vlozit mnozstvi "1" automaticky. Automaticky vlozi mnozstvi 1 nasnimaneho(vybraneho) zaznamu do nasnimanych.

[16.5.2011]
Prijem_3 (CODEBOOK)
- Vyhledani polozky dle CK prevedeno zpet na zobrazeni seznamu nalezenych do hlavniho seznamu polozek

[7.4.2011]
Inventura2
- Opravy uvolnovani scanneru car.kodu v dialozich vyberu z ciselniku
- Uprava vlastnosti RFID dialogu
- Rozsireni RFID dialogu o podporu scanneru car.kodu = chovani stejne jako pri snimani RFID tagu
- Rozsireni o kontroly uplnosti polozky, preplnenosti a zapornych zadanych dat mnozstvi

[28.3.2011]
Licencovani
- pridano licencovani instalaci. Pro licencovanou verzi je nutne dodat licencni soubor "mst_w.ini" na uroven mst_w.exe
- obfuskace kodu

[20.3.2011]
Inventura2 (Evidence majetku)
- podpora pro RFID scaner TT8800
	> tagy UHF
	> konverze hexa => text, hexa => hexa
	> orez pocatecnich nulovych znaku

[verze "3.12"]
--------------
[16.3.2011]
Prodej
- Uprava funkci vyhledavani:
	> pridano vyhledavani dle cisla polozky (LIKE %cislo)
	> doplneny zkratkove klavesy pro vyhledavani (F5-F8) 
	> zkraktova klavesa online kontroly stavu na skladu presunuta z F8 na F10

[15.3.2011]
Prijem, Vydej
- Oprava zobrazeni a pamatovani nastavenych dynamickych sloupcu v prehledu davek ke stazeni a ke zpracovani

[8.3.2011]
Vydej
- Oprava dohledavani v seznamu davek ke zpracovani dle SOPNUMBE

[25.2.2011]
Inventura1
- Oprava zobrazeni sloupce DMJ, projevilo se pri pokusu o zmenu poradi sloupcu

[14-22.2.2011]
Vydej, Prijem
- Rozsireni o ukladani doplnujicich informaci do hlavicek ke stazeni a ulozenych davek na terminalu

Prijem
- rozsireni o online funkci : "Detail objednavky", "Pocet kusu", "Pocet kusu lokace", "Detail polozky"
- listpolozek rozsireno o vyhledani zaznamu dle "PONUMBER" pomoci caroveho kodu scannerem

[8.2.2011]
Prodej
- oprava ukladani null hodnot (zejmena SKL_ID, pokud neni vyzadovano zadani cisla skladu z ciselniku)

[3.2.2011]
Synchronizace casu terminalu se serverem

[28.1.2011]
Inventura1
- oprava prace s mernym jednotkami
- prehled polozek obsahuje sloupec "DMJ":doporucena merna jednotka
- prehled polozek i3 (dodavatele) obsahuje zobrazeni sloupce "MJ":merna jednotka
	> jestlize je tento dialog vyvolan po nacteni ck, nebo pri rucnim vyberu, tak se aktivuje polozka, ktera odpovida klici ITEMNMBR a DMJ pro tento itemnmbr obsahuje retezec obsazeny v MJ
- prehled nasnimanych polozek rozsiren o nove sloupce MJ, QUANTITYMJ i v detailu

[21.1.2011]
Prodej
- vyber dokladu automaticky s existujicim zaznamem (melo by fungovat???)
- vyber skladu automaticky
- vyber odberatele automaticky (melo by fungovat???)
- vyber strediska automaticky 1x stredisko na doklad => konfigurace

Inventura1
- znova stazeni davky ovetvrene inventury opravit ... prepsani...
- inventura zadani MJ neni naplneno MJ v loadu polozek do pohledu ... opravit ...

[19.1.2011]
Prodej
- oprava chyby v dialogu "Vyber odberatele", kdy bylo mozne pokracovat potvrzenim vyberu i pokud odberatel nebyl nalezen...

[12.1.2011]
Prodej, Inventura1 
- merne jednotky
- designove upravy

[verze "3.11"]
--------------
[12.1.2011]
Prijem_4
- oprava zobrazeni panelu nad statusbar (fill) nebylo videt posuvniky
- oprava razeni dle nazvu - pouzit spatny datovy sloupec


[6.1.2011]
Vydej_3
- designove upravy
- oprava zobrazeni aktivniho zaznamu v listu pri zadani uplneho mnozstvi polozky
- zobrazeni casoveho dialogu po vlozeni podmineno nastavenou vlastnosti CONFIG_MNOZSTVI_ZADAVAT
- zobrazeni textu "Zadej mnozstvi baleni" pri pozadavku na zadani poctu baleni
- pamatovani posledniho nastaveneho fitlru - Vse/Neuplne
- pridan konfiguracni parametr na terminalu "Auto 1. neúplný dle č.k." : pokud je aktivni, pri vyberu carovym kodem automaticky vybira 1.neuplnou polozku dle ORD

Prijem_4
- castecne sjednoceni designu s Vydej_3
- sjednoceni algoritmu a parametru chovani pri zadavani mnozstvi s Vydej_3
	> moznost predvyplneni hodnot
	> moznost deaktivace scanneru pri zadavani mnozstvi
- sjednoceni klavesovych zkratek s Vydej_3
- doplneni statusbaru pro zobrazeni aktualniho filtru
- pridani loginu uzivatele do textu hlavniho okna
- zobrazeni casoveho dialogu po vlozeni podmineno nastavenou vlastnosti CONFIG_MNOZSTVI_ZADAVAT
- zobrazeni textu "mnozstvi baleni" pri pozadavku na zadani poctu baleni
- pamatovani posledniho nastaveneho filtru 
- pridano menu Razeni
- pridan konfiguracni parametr na terminalu "Auto 1. neúplný dle č.k." : pokud je aktivni, pri vyberu carovym kodem automaticky vybira 1.neuplnou polozku dle ORD


[verze "3.10"]
--------------
[27.12.2010]
Prijem_4 - novy modul
- pametovy, data jsou vsechny natazena do pameti (funguje jako Vydej_3)
- Prijem_3 lze pouzit, ale projekt se musi prekompilovat s jinym nastavenim > TODO konfiguracne 
- nutne dopracovat a sjednotit designy s modulem Vydej_3

[20.12.2010]
Hlavni menu
- opraveno bug pri zobrazeni a spousteni modulu pres focus a klavesu Enter
Inventura1
- opraveno zobrazeni informace o skladu v dialogu nasnimanych polozek
- oprava zobrazeni poctu nasnimanych pri refreshi dat
	> jestlize je polozka vedena na sklad(skl_id), pak se pri obnoveni dotahuje informace z vystupu na uroven skladu
	> skl_id nesmi byt NULL nebo prazdny retezec. V opacnem pripade bude mnozstvi u vsech polozek stejne
- uprava hromadneho odmazani nasnimanych
	> jestlize je polozka vedena na sklad(skl_id), pak se pri hromadnem odmazu aplikuje pouze na data se stejnym ITEMNMBR a SKL_ID
	> skl_id nesmi byt NULL nebo prazdny retezec aby se aplikoval tento mechanismus. V opacnem pripade se odmaze vse dle ITEMNMBR
- opraveno bug pri stahovani davky(test na rychlost downloadu)
Inventura2
- opraveno bug pri stahovani davky(test na rychlost downloadu)

[16.12.2010]
Inventura1 
- doplneno skladid a skladnazev do 
	> dialogu mnozstvi
- upraven design zakladniho menu

[14.12.2010]
Prijem_3
- uprava aktualizace zobrazeni nasnimaneho mnozstvi(zbyva) u zobrazenych polozek 
	> po uspesnem ulozeni zadaneho mnozstvi

[verze "3.9"]
-------------
[6.12.2010]
- konfiguracne mozne skryt taskbar 
- konfiguracne mozne skryt nadpis okna
- jednotne zobrazovani cisel dle konfigurace aplikace ( priklady formatovani: N, N2, N0, G, 0,0.00, #,#.#, 0.00#, ...)
  > aplikovano v modulech Prijem, Vydej, Prodej, Inventura1, Inventura2
- Vydej, Prijem - rozšířeny zobrazované sloupce v přehledech
- Vydej - rozsireni o moznost odeslat davku a pokracovat stazenim na jinem terminalu. 
	    - Tato konfigurace neumoznuje pouziti vychystavani jedne obejdnavky vice terminaly
	    - Zapina se konfiguracne na terminalu : Vydej/PokracovatNaJinemTerminalu (default = Ne)
	    - Pokud je aktivni volba Vydej/ItemTypeQuestion, tak je neaktivni a nelze tuto funkci vyuzivat
	    - Je nutne aby byl nastaven i komunikacni server s timto parametrem(MST_W_Server->VydejPokracovatNaJinemTerminalu=True), jinak bude dochazet ke kolizi dat v tabulce CZMST_SI

[3.12.2010]
- novy datagrid2 (vsechny moduly)
- pridan DataGrid2NumberBoxColumn pro formatovani cisel

[verze "3.8"]
------------------------
[29.11.2010]
Vydej_3
- parametry : pridan novy funkcni parametr "CONFIG_MNOZSTVI_SCANNEREM", ktery urcuje, zda je mozne na terminalech snimat mnozstvi scannerem(car.kodem)

[verze "3.7"]
------------------------
[25.10.2010]
Inventura2 - Evidence majetku
- rozsireno o funkci "Zbyvajici" : filtr, ktery zobrazuje pouze zbyvajici polozky (NACTENO<>KUSU)
- rozsirena datova struktura sqlce databaze o sloupec MAJETEK.NACTENO.
[21.10.2010]
Inventura2 - Evidence majetku

[verze "3.6"]
------------------------
[19.10.2010]
Vydej_3
- Opravena aktivace scanneru pri dotazu na cislo expedicniho prikazu
- Upraveno vyhledavani cisla "objednavky" v seznamu polozek ke zpracovani podle sloupce SOPNUMBE

[4.10.2010]
Vydej_3
- Generovani dat predlohy pomoci online fce v seznamu davek pro stazeni
- Seznam davek pro stazeni aktivuje scanner a je mozne dohledat polozku dle prvku SOPNUMBE


[16.8.2010, verze "3.5"]
------------------------
Prodej_3, Prijem_3
Plneni rezervnich hodnot rozsireno o moznost urceni, zda se jedna o:
- cislo nebo text
- zda je tento udaj povinny
- zda se ma posledni zadana hodnota pamatovat

Prodej_3
- Zadani strediska je mozne zvolit v konfiguraci terminalu, zda se bude vybirat z ciselniku nebo se bude zadavat pouze jako textova hodnota bez vazby na ciselnik

[16.6.2010, verze "3.5"]
------------------------
Vydej3
- StatusBar
	- zobrazeni Nazvu lokace aktualni polozky misto vybrane id lokace
	- L=:<Nazevlokace> : id lokace aktualni polozky je stejne jako id lokace vybrane
	- L!:<Nazevlokace> : id lokace aktualni polozky se lisi od id lokace vybrane
- Pridana funkce Zobrazeni/F8 Lokace vše 
	- zobrazí všechny položky na všech lokacích	
	- indikace stavu je zobrazena pouze v menu Zobrazení/F8-Lokace zaškrtnutím této položky
	- tuto funkci lze kombinovat s Filtrem Vše/Neúplné

[8.6.2010, verze "3.5"]
------------------------
Vydej3
- Rozsireni o ciselnik Lokaci
- Nove konfiguracni parametry na vydeji => zalozka Lokace
- Lokace je mozne konfiguracne vybirat z ciselniku nebo je "pouze" zadavat resp. potvrzovat

[1.6.2010, verze "3.5"]
------------------------
Inventura1
- Online kontroly
- upravy pro zpracovani davky vice terminaly
- nataveni timeoutu stahovani dat a online kontrol

[14.5.2010, verze "3.5"]
------------------------
Vydej_3
- dotazeni neimplementovanych casti
- designove upravy
- oprava vyhledavani obderatele podle caroveho kodu

[16.4.2010, verze "3.4"]
------------------------
- Uprava modulu Inventura1
	- zmena vzhledu Nasnimanych polozek
	- vyberu dodavatele
	- pamatovani rozlozeni datovych sloupcu v seznamech
- Pridana podpora pro autentifikaci uzivatele na zaklade loginu, hesla, domeny vuci serveru
- Pridana podpora pro zabezpecenou komunikaci na urovni certifikatu
- Inventura1 locncode mozne zadat pouze jednou ve vnitrnim kolecku, pokud neni zadan locncode na vybrane polozce

