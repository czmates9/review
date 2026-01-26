v 1.25 MaR 15.1.2024
==================
-nulovani promenne fujtokod jen kdyz carovy kod odpovida scanovanemu
-zvyseni verze

v 1.24 MaR 27.10.2023
==================
-vypnuti prohazovani pri zmene vyrobku
-oprava NOREAD ukladani kontrola kodu do konfigurace
-zvyseni verze

v 1.23 MaR 13.10.2023
==================
-Kontrola kodu 1, zakomentovani spusteni houkacky, pro READ a NOREAD
-zvyseni verze


v 1.22 MaR 1.08.2023
==================
-Zmena prohaz, pri ON/OFF zapis hodnoty senzor 1
- automaticke ulozeni neresetuje hodnoty qty, qtyreal a senzor 1
-zvyseni verze

v 1.21 MaR 11.07.2023
==================
-Zmena prohaz, ukladani do Fask_Event pri zmene rezimu:  prohaz Zapnuto/Vypnuto
-logika zmena prohaz pocitani pytlu z rezimu prohazovani, ukladani do Fask_Events sloupec senzor
-zmena prohaz text barevne pokud je zapnut tak cervene, jinak zelene
-zakomentovana hlaska zapnout prohaz v metode TimerTestyCallback()
-zvyseni verze

v 1.2 TaD 26.01.2023
==================
-Uprava, vyčistení počtu na obrazovke pri odhlaseni


v 1.1 TaD 11.01.2023
==================
-upravy v konfiguraci
-sestaveni do AGRA první verze


v 1.0 TaD 04.11.2022
==================
- Vytvořena kopie z AGRO2 pro substrady do AGRO2_ModBus pro roboty

============================================================================================================
============================================================================================================

v 2.10 TaD 24.08.2022
==================
-Oprava dotahovani poriznaku BarcodeT do VPP


v 2.9 TaD 21.03.2022
==================

-AGRO linky
*Rozdelena konfigurace pro kontrolu kodu
-rozdelene na Read a NORead

-ukldani fo fask_events pridane do konstatt, jako text

v 2.8 TaD ???????
==================
- do Task Listu pridat : AGRO_HOUKACKA
-Ten ukazuje kde všude se zakomentovala puvodni prace s houkačkou


v 2.7 TaD 9.10.2021
==================
-Mraky uprav

-ale ted je pridane ovladani houkačky pomoci tlačitka Houkačka

v 2.6 TaD 8.7.2021
==================
-Pridan novz stav, kdy se zadava BarcodeP z VPP položky
-kontroluje se zda existuje

-Pri zmene matzerialu se zadava vyrobny prikaz a polozka vyrabana

- na scanner pridane kontroly zda se snima polozka zadana z vyrobneho prikazu
- dle logiky: 
- 1 neshodný - blikne maják – NE = jedno bliknutí nikdo nemusí ani postřehnout
- 3 neshodné - blikne maják + hlášení na obrazovce - ANO
- 5 neshodných - blikne maják + hlášení na obrazovce + zastavit stroj - ANO

-Vse je konfiguracne nastavitelne 

-v treti variante, je novy stav, stav Zombie, že se zastaví linka, a je to žive, ale zaroven mrtve...


v 2.5 TaD 8.7.2021
==================
Z duvodu AGRO modulu, rozšiřeno o struktury MES7 - Odvadeni vyrovy VPP a VPH
-vytvořena synchronizace

-Pridany novy stav, kde se po přihlašení zadavá číslo vyrobniho přikazu, a to se nasledne pridava do FASK_Events pro odvedenz zaznam



v 2.5 JiS 8.7.2021
==================
- ulozeni odvodu vyroby
	: nove se provede ulozeni vyroby i po prichodu impulsu palety

v 2.5 JiS 29.6.2021
==================
- oprava odesilani souhrnnych informaci po ukonceni smeny (prehledy hodnot)

v 2.4 JiS 22.6.2021
==================
ok - změna vzhledu aplikace
ok - přidat hodnotu počet pytlů : za výrobek / za směnu
ok - při změně výrobku vynulovat počet palet 
-> tato informace se pouze dopocitava z ulozenych dat v aplikaci. Celkovy pocet v adam se bude nulovat pouze pri ukonceni smeny, protoze toto jsou kontrolni hodnoty a jejich ztrata by byla spatna
ok - do Fask_Events scaner1,2,3 ukládat průběžné hodnoty čítačů (vstup 1,2,3) : "inkjet", "cidlo count", "paletizator" 
-> ukladaji se prubezne inkrementalni prirustky nehlede k vyrobku za smenu
-> tyto hodnoty jsou pocitany z prichozich udalosti z P2P komunikace z Adama
-> jsou explicitne pouzity vstupy adama DI0, DI1, DI2

v 2.3 JiS 24.7.2019
==================
- uprava algoritmu prirazeni caroveho kodu ze scanneru
	=> dojde "pouze" ke zmene vyrobku
- vraceni moznosti rucni volby F1 do uzivatelskych stavu
- oprava prace s cisly pracovniku, vymazani seznamu pri odhlaseni smeny (resp v aktivaci stavu zadani smeny)

v 2.2 JiS 30.5.2019
==================
- ladeni stavu, programu, behu, optimalizace behu, volani vlaken, atd ... detail viz SVN


v 2.1 JiS 15.5.2019
==================
- docasna modifikace bez scaneru

v 2.0 JiS 15.5.2019
==================
- modifikace algoritmu

v 1.9 TaD 20.10.2017
==================
- rozsireni logovani po odhlaseni o Material(sarzi) dos souboru a emaile v NotoficationMail.cs metode SendEmailOdhlaseniSmeny


v 1.8 TaD 17.10.2017
==================
InformationUC.cs :
- pridane pri zadavani hesla u zmenz sarze misto textu se zobrazuji *
- pri prechodu z stavu VyrobaStavy.SarzeID do stavu VyrobaStavy.SarzeHeslo se zapnou hviezdičky
- pri prechodu z VyrobaStavy.SarzeHeslo do stavu VyrobaStavy.SarzeID se hviezdicky vypnou
- pri prechodu z VyrobaStavy.SarzeHeslo do stavu VyrobaStavy.SarzeHodnota se hviezdicky vypnou


- do stavu VyrobaStavy.SarzeHodnota pri prechodu do stavu VyrobaStavy.Main 
- odesle se InsertNewUserEvents o zmene sarze,
- InsertNewEvents s datama, CodeReadCnt a CodeNoReadCnt se nastavi na 0 a nastavi sa čas zmeny.

-kontrola prechodu stavu a zapinani a vypinani linky.Pridalo do logu info o zapinani a vypinani linky u zmneny sarze 

v 1.7 TaD 5.9.2017
==================
uprava connectionstringu


v 1.6 TaD 28.8.2017
==================
-Classes: 
=>Database.cs upravene UserEvents pridanim rez_2
=>DatabaseCentral.cs - pridane metody : 
        
    ReturnID(string inID)- slouzi pro online overeni zadaneho zadaneho ID primo na SQL serveru
    funguje pomoci sql dotazu, ktery obsahuje funkci ktera je umistena na sql serveru
    
    ReturnHeslo(string inHESLO,string inID)- slouzi pro online overeni zadaneho hesla podle zadaneho ID primo na SQL serveru
    funguje pomoci sql dotazu, ktery obsahuje funkci ktera je umistena na sql serveru
-obe tyto funkce vraci bool hodnototu true když je nalezena na serveru požadovana informace

=>DataVyroba:
-pridane metody ktere volaji již predchodzi zvolane metody
-enum Vyrobastavy pridana hodnota SarzeID pro identifikaci podle ID pri pridavani/editaci Sarze
-do vsech InsertNewUserEvents pridana rez_2. bud jako prazdna nebo jako relevanti informace

-Configuration/Congif.cs pridana hodnota LOG_SARZE_ZMENA_ID = "11"; ktera sa vypisuje do UserEvents

-SQLCEDatabase/Vyroba.sdf lokalni databaze rozšířena tabulka FASK_UserEvents o rez_2

-InformationUC.sc:
-pridana promenna _sarzeID ktera uchovava hodnot ID pro overovani pri zadavani sarze
-pridan novy stav SarzeID ktery ma nastarosti ziskat a overit online na sql serveru ID uživatele ktery chce menit sarzi
-uprava stavu SarzeHeslo ktery ted online overu podle zadnaho ID take heslo primo na sql serveu

v 1.5 JiS 7.8.2017
==================
Pracovnici
- seznam pro generovani sarze
	=> neprobihalo jeho nulovani pri odhlaseni smeny
	=> pole pracovnik se plnilo pouze pri posledni akci, neukladaly se hodnoty z vice pracovniku => a tim se odesilalo pouze posledni prihlaseni od spusteni aplikace		

v 1.4 JiS 3.7.2017
==================
Sarze
- sarze se generuje automaticky z centralniho serveru ulozenou procedurou "fask_vyroba_GetSarze"
	=> pokud akce neproje online, pak sarze neni pridelena
- sarzi je mozne generovat znovu pres moznost "Sarze" v hlavnim odvadecim cyklu 
	=> sarzi je zde mozne i pridelit rucne
	=> tyto akce jsou podminene pristupovym heslem, ktere je ulozeno v kodovane podobe v konfiguracnim souboru, lze je zmenit v nastaveni po zadani hlavniho pristupoveho hesla

!!! jedna se o online funkce, ktere nemusi vzdy projit, proto je treba toto kontrolovat
??? je otazkou, zda umoznit odvody bez pridelene sarze ...???
