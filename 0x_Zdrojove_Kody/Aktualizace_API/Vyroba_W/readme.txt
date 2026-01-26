[25.04.2023] v4.19 MaR
=================

- vytvorena verze E

*********************************************************************************************************


[09.03.2023] v4.18 TaD
=================
- Rozšíøení struktur tabulek
  - CZPRO_VPP
  - CZPRO_VPH
  - Production
  
  

[03.11.2021] v4.17 TaD
=================
-API odesilaní souboru, uprava prace s certifikatama


[28.06.2021] v4.16 TaD
=================
-Uprava odesilani souboru pomoci REST sharp API
-Zmena settings souboru, upravene parametry kvuli komunikace cez API

[06.05.2021] v4.15 TaD
=================
-Pouze sestavení Dašenky

[25.02.2021] v4.15 TaD
=================
-Pouze sestavení Dašenky

[09.02.2021] v4.15 TaD
=================
-Uzavøení verze Cipisek


[19.11.2020] v4.14 TaD
=================
-Production_SN
-Pridáno sledovaní SN a šarže
-od JiS pridana uprava *[guid].prd souboru odmayavani pri synchronizaci


[16.10.2020] v4.13 TaD
=================
-Pridana možnost zmìnit zakazku bez nutnosti sa odhlasit, pomoci klavesi 4
-pri stlaèení ESC se uživatel odhlasí

-pøidane refiony ve FormOdvadeni,  pro lepší pøehlednost


[15.9.2020] v4.12 TaD
=================
-Uprava, pri synchrnizaci a odmazavani podle GUID, tak v parametru nebyl typ Guid ale string, proto to neodmazavalo
-Uprava na sql view na vpp
-tym že se opravilo odmazavani, tak se opravyly vypoèty odvadeni


[11.9.2020] v4.11 TaD
=================
-JiS delal upravy s èasama, pøetypovavanim... a praci se synchronizaci asi...

-Pridana funkcionalita pro generovani šarže/SN, pomocou online dotazu na server, kde sa volá procedura
-pouze u položek, ktere jsou na šarže/SN

-Pøidane zdilene tøídy pro tisk, a pro Logovani
-Proto pøedenale Logovani namespace...

-Tiskove tøidny rozšíøeny o možnost tisku na Vyrobe pøi odvadení...




[28.7.2020] v4.10 TaD
=================
-TaD dle Zadani od JaS dne 27.7.2020 je s toho vytvoøek koèkopes... kde èas je zadavan max 1 den...
-Uprava ve Odvadeni\FormOperacePotvrzeni.cs

[21.7.2020] v4.9 JiS
=================
- Revize algoritmu odesilani > SVN:\Vyroba_2\!!!_Podklady_!!!\Schemata\01_Proces_Synchronizace_Databazi_ServerXClient.jpg 

[23.6.2020] v4.8 TaD
=================
-Premenovani projektu na Cipisek ALFA
-Pøeneseni prace s certifikatama na serveruz MSTW
-ted je možnost pracovat i pod HTTPs šifrovana komunikace

-Pøidany projekt pro parsovani èaroveho kodu
-Pøi naèitavani kodu pøidana možnost naèitavani vahoveho kodu po ktere nemuzsi zadavat množstvi/vahu...

-Preneseni projektu Fask.Graic sem k Vyrobe...

-Pøedelani na SQLite


[07.10.2019] v4.4 TaD
=================
-Uzavøeni verze AMALKA


[03.10.2019] v4.3 TaD
=================
-AMALKA alfa, pøejmenovano z duvodu ladeni chyb
-Uprava chyby, když je v SQL ID Logins s medzerama tak se uživatel nepøihlasil
-Pøidano Trim na heslo

[03.10.2019] v4.2 TaD
=================
-Uzavøeni verze AMALKA
-Uprava umisteni release a debug k Konzole

[10.9.2019] v4.2 TaD
=================
-Zjednoceni verze s Serverem, sestaveni pro Konferenci2019


[19.3.2019] v4.0 TaD
=================
-Vytvoreni Vyroba v4.0


[6.3.2019] v3.72 JiS
=================
tisky
- rozsirena funkcnost o tisky baleni dle pozadavku z S4S
- online funkcionalita

[18.1.2019] v3.71 JiS
=================
- online informace o operaci a odvod vyroby online pro partnera S4S
- uprava odvadeni "vyroba_online"

[23.7.2018] v3.70 TaD
=================
-uprava vzhledu Formu zadavat  Lokace pri rozpadu materialu



[18.7.2018] v3.69 TaD
=================
-Uprava rozkopirovani sdf souboru pokud nejsou na zaøizeni
-uprava kontroly procesu již zapnute aplikace



[19.6.2018] v3.68 TaD
=================
- Uprava stahovani sdf souboru pomocu requestu a zipovani na strane serveru


[21.3.2018] v3.67 TaD
=================
-Rozšireni struktury Production o ITEMDESC

-Pridano konfuguracne vyplnovani hodnoty mnozstvi 0 pri zapornem mnozství
-Uprava zobrazeni Prehledu odvadeni, Namisto ITEMNMBR se zobrazi ITEMDESC
-Zobrazeni nazvu ITEMDESC cez dva radky
-Pridana moznost konfuguracne potlacit dialogove okno  ktere upozornuje na vetsi zadane mnozstvi


[5.12.2017] v3.66 TaD
=================
-Pridan rozpad Polotovaru(nacteneho jak material) na jednotlive materialy


[29.11.2017] v3.65 TaD
=================
- pridane automaticke naèteni materialu pomoci vazebne tabulky
-autmaticke nacteni probika v pribehu odvadeni až po zadani mnozstvu, skladu a lokace
- pro každy automaticky naèteny materialy je potrebne zadat sklad(pokud neni) a Lokaci(pokud neni)
- mnozsvi se automaticky vypocita s koeficientu ve vazebne tabulce

- pokud ma material alternativy bude uzivatelovu nabudnut na vyber z alternativ

-pridane nacteni poltvaru jak materialu, ale take rozpad poltvaru na jednotlive materialy
-koeficient pri rozbalivani polotvaju je nasoben mnoztvim vyrobku a mnozstvim materialu





[26.10.2017] v3.64 TaD
=================
- v Data/InternalState.sdf pridany pouze odkaz na soubor ktery je presunut do serveru

v main_load pridana kontrola zda v zarizeni sou soubory InternalState.sdf a Production.sdf
-pokud se zde nenachazeji tak se prazna a aktualna verze stahne z serveru
pridane webreference na prepare metody na strane serveru


[16.10.2017] v3.63 TaD
=================
- pridana moznost konfiguracne povolit/zakazat zobrazzovani formu FormOdvadeniPrehled
-pridane/premenovane do config, Vyroba/Hlavni s oznacenim 
*Zobrazovat potvrzení Operace
*Zobrazovat pøehled

-pridana možnost konfiguracne povolit/zakazat zobrazzovani èasove informace o neèinnosti na poèatku odvadeni
-pridane/premenovane do config, Vyroba/Hlavni s oznacenim 
*Dotazovat se na neèinnost



[25.9.2017] v3.62 TaD
=================
-form odvadeni pridana podminka pro konfiguracne povoleni/zakazani zobrazeni OdvadeniPrehled
- provedena uprava, v konfiguraci pridana možnost povolit nebo zakazat zobrazovani FormOperacePotvrzeni
- v settings pridane ukladani nastaveni
- materialvyber zmeneny QTYSHPPD na QTY


[25.9.2017] v3.61 TaD
=================
-uprava konfigurace, možnost povolit/zakazat pridavani materialu pred, v prubehu nebo po vyrobe
- upraven dialog FormMaterialy s tlaèitky OK a Storno
- pridana možnost volby stavu FormMaterialy kdy se zobrauzuji tlacitka OK a Storno nebo tlacitko Pokraèuj



[22.9.2017] v3.60 TaD
=================
- uprava tabulka Production_Sources, USER_ID zmeneno z int na string


[19.9.2017] v3.59 TaD
=================
-v MainForm zmena velkosti pisma aby sa vešlo na obrazovku
-v FormMaterial : 
                    -odtranene tlacitka OK a Strno
                    -zmazani metody PerformCancel
                    -uprava metody PerformOK
                    -uprava naèteny dat do datasetu
- v FormOperacePotvrzeni : pridana možnost editace materialu a nacteni datasetu s kterym se pracuje
- uprava v FormOdvadeni v metode StopOdvod() 
zmeneno poradi : cislo operace, prehled, material(Material Vyber), potvrzeni operace, Mnozstvi kusu, cilovy sklad, cilova lokace
- Opraveno v FormMarialVyber vykreslovani datagrid, spatne pomenovani stloupcu
                 


[1.9.2017] v3.58 JiS
=================
Odvadeni
- Material : pokud je na polozce nastaven sklad (SKL_ID) a je pozadovano zadani skladu, pak se prebira id skladu vybrane polozky, bez dotazu na zadani skladu
- SOUBEHGUID : prevzeti z vyroba_p

[30.8.2017] v3.57 JiS
=================
Odvadeni
- StartOdvod, pokud je stejny odvod, tak vklada start aktualni ...
- Dialog prehledu vyroby pred zahajenim - barvne oznaceni stavu

[25.8.2017] v3.56 JiS
=================
Odvadeni
- novy dialog se zobrazenim prehledu informace o hlavicce a prikazu vybrane operace po zadani cisla prikazu

[22.8.2017] v3.55 JiS
=================
Odvadeni
- StopOdvod, FormVyberMaterialu : overeni existence id lokace(dle barcode[Kod]) a id skladu(dle skl_carcode[SKLID])
- pri neupesnem start/stop se zobrazi informace o predchozi vyrobe : sopnumbe, itemnmbr, barcodep
Konfigurace
- VyberZakazkyPoPrihlaseni : doplneno do nastaveni aplikace
- StartVyrobyPoStopVyrobyIhned : po start operaci se inhned spusti stop operace


[27.6.2017] v3.54 JiS
=================
Odvadeni
- FormVyberMaterialu : Vyber materialu pokud existuje vice variant s car.kodem


[17.5.2017 - JiS] (v3.53)
Pouzita komponenta Datagrid2 pro moznost ukladani konfigurace nastaveni gridu
=> knihovna fask.graphic.dll
=> predelany vsechny gridy na tuto komponentu
=> rozlozeni se uklada nove do adresare "Config" na urovni "SOUNDS"... (tedy v pracovnim adresari aplikace)


[13.4.2017 - JiS] (v3.52)
Rozsireni o zadavani skladu a lokaci vyrobku
Rozsireni o zadavani vstupnich materialu a jejich skladu a lokaci 

...
...
...

[17.4.2015 - PeV] (v3.3)
online kontrola posledni akce uzivatele se provadi vzdy

[6.11.2014 - JiS] (v3.2)
Odvod vyroby:
0) !!! Production.sdf a VyrobaCE.sdf jsou definovany na serveru, ale ten byl rozsiren, proto se museji(meli by) pouzit tyto soubory z predchozich verzi
- Production.sdf a VyrobaCE.sdf nejsou soucasti updatu
- teoreticky by nemelo vadit nove struktury ... 

1) Odstraneni online kontroly korekci ... 
	// Check korekce neni treba, protoze se aktualne vkladaji korekce vzdy parove ...
	// TODO : pokud se zmeni zpusob zadavani korekci, tak se musi kontrolovat ... 
	//if (CheckCorrectionsOpenedNoProduction())
	//{
	//    return;
	//}

2) Online kontrola posledni akce uzivatele jen pokud se nedela aktualne stop
a presunuto az za cast dohledani vyrobni operace
    // Pokud se zadava timemode==STOP, tak se neprovadi online kontroly...
    // 6.11.2014 -> telefonicky pozadavek JaS ???
    if (rowvpp.TIMEMODE == (int)TIMEMODES.Stop)
    {
        //Posledni datum a cas akce uzivatele
        DateTime? userLastAction = UserLastAction();
        if (userLastAction != null && userLastAction < DateTime.Now - Settings.Production_UserMaxTimeSpanNoAction)
        { // uzivatel neco delal dele nez je nastavena necinnost, => musi zadat korekci mimo vyrobu ???
            DialogResult drNecinnost = MessageBox.Show(
                "Necinnost trvala dele nez " + Settings.Production_UserMaxTimeSpanNoAction.ToStringHHmm() + "\n" +
                "Celkem " + (DateTime.Now - userLastAction.Value).ToStringHHmm(),
                "Pokraèovat dále nebo ukonèit pro zadání korekce mimo výrobu?",
                 MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Exclamation,
                 MessageBoxDefaultButton.Button1
                );
            if (drNecinnost == DialogResult.Cancel)
                return;
        }
    }

3) Zjisteni posledni akce uzivatele online, jen pokud se nedela STOP
	// Zjisteni posledni akce uzivatele...
    WebServiceVyroba.VyrobaDataSet dsR_LastProductionUserMachine = null;
    // Pokud se zadava timemode==STOP, tak se neprovadi online kontroly...
    // 6.11.2014 -> telefonicky pozadavek JaS ???
    if (rowvpp.TIMEMODE == (int)TIMEMODES.Stop)
    {
        try
        {
            dsR_LastProductionUserMachine = vyrobaS.ProductionLastAction(idpracovnik.id, idmachine.id, false, 1);
        }
        catch (Exception ews)
        {
            Logging.Log.Write(ews);
        }
        //nepovedlo se stazeni ... 
        if (dsR_LastProductionUserMachine == null)
        { // TODO : osetrit nejak ... 
            if (DialogResult.No == MessageBox.Show("Nezdarilo se zjisteni posledniho stavu uzivatele ze serveru. Pokracovat?", "Online stav", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1))
            {
                return;
            }
        }
    }

[8.8.2014 - JiS] (v3.1)
rezimy: Start,Start/Stop,Start/Start/Stop
online: nove funkce pro dohledani produkce, uzivatele ... 

[29.7.2014 - JiS] (v2.2)
- Rozsireni o start/stop casy (v pripravne fazi)

[21.7.2014 - JiS] (v2.1)
- Optimalizace stahovani predlohy (novy server)
- Optimalizace vlaken stahovani
- Optimalizace prace s databazi
- Rozsireni datovych struktur o nove prvky, optimalizace pro beh nad starsim rozhranim databaze

[22.9.2009 - JiS]
- konfiguracne vypinatelne pozadavek na prihlaseni smeny(negeneruje se udalost prihlaseni a odhlaseni smeny)
- zobrazovni hodnot celociselne, pokud neobsahuje destinne cisla
- upraven dialog pro vyber typu udalosti, pridan seznam s moznosti rucniho vyberu nebo nacteni carovym kodem
- zvestseno a preusporadano poradi informacnich poli v dialogu potvrzeni operace, celkovy cas vcetne korekce casu
- povoleno vlozeni nuloveho mnozstvi rezijni zakazky nebo dalsi korekce k vyrobe
- uzivatelske nastaveni rozlozeni dialogu udalosti obsluhy
- nastaveni Settings se uklada automaticky pri ukonceni aplikace
- vytvoren konfiguracni parametr timeoutprihlaseniuzivatele. Pokud doba od posledniho odvodu uzivatele je vetsi nez timeout, tak je vyzadovano nove prihlaseni uzivatele se zadanim data a casu prihlaseni

