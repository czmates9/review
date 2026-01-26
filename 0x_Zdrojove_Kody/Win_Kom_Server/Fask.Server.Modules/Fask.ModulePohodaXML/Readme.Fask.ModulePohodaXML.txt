
26.11.2021 v2.25 TaD
=============
-Mraky uprav

-nejnovejší InnovaMedical

Volitelne parametry : VPrSarzeKSN a VPrExspiraceKSN

-Prodej
* Vytvoření nový typ dokladu FV (faktura vydana)
* pridan zapis Expirace nativně pohoda
* pridane zapisy do volitelnych parametrů
* prijemka, vklada nové zadane hodnoty
* FV, vydej, prevod
 - dotahuje informace

-Prijem
* pridan zapis Expirace nativně pohoda
* pridane zapisy do volitelnych parametrů
* prijemka, vklada nové zadane hodnoty
* Generovani predlohy, když je VPr zapnut a je na SN tak se cz_sernumtrack = 10

-Vydej
* FV, vydej, prevod
 - dotahuje informace
* na OBJ definovan doklad který vznikne
* pokud neni definovan tak se berou konfiguračne parametry
* Generovani predlohy, když je VPr zapnut a je na SN tak se cz_sernumtrack = 10


21.10.2019 v2.24 TaD
=============
-Uprava pri importu přijemky a naslednem  našem programovem provazovani 
Objednavky Vydanej a Přijemky uprava Update SKz


17.10.2019 v2.23 JiS
=============
-Uprava xml requestu import faktury vydane "classificationVAT"
	: jeli objednavka na registraci DPH v EU (DICRegDPHEU je nastaveno) => classificationVATType = "nonSubsume" //Nezahrnovat do DPH.
	: jeli objednavka bez registrace DPH v EU (DICRegDPHEU neni nastaveno) => classificationVATType = "inland" //Tuzemske plneni.

10.07.2019 v2.22 TaD
=============
-Prodej, tvorba dokladu
-Prijemka, Prevodka, Vydejka: Pridana možnost tisk dokladu přimo z pohody na jejich tiskove sestavy

-Prodej, přijemka pri vytvařeni dokladu v cizi mene tak se dotahuje i kurz

-Premenovani ConnetionStringy na FASK_ConnectionString_ProDesignery kde se jedná o defaultne connetionstringy vygenerovane VS
-v kode by se mnely tyto connectionstringy nahrazovat korektnimy connectionstringem


06.05.2019 v2.21 TaD
=============
-POHODA, přijemka z volného pohybu rozšířena o variantu kdy v Adresáší ma Dodavatel definovanou Cizí měnu (CM), tak se nasledně vytvoří příjemka v CM


10.4.2019 v2.20 TaD
=============
-Ostraneni z XML requestu, vazbu na typy dokladu, predelano na SQL server
-na prijemku cez volny pohyb pridane, Stredisko, Cinnost a Zakazka 
-Opraveno, dotahovani delky stloupcu pri generovani CZMST090

-Dotahovani odběratee z AD dle ICO


14.12.2018 v2.19 TaD
=============
-Uprava na strane Vydeje a Prijmu
-Odstraneni dalších TableAdapteru


13.12.2018 v2.18 TaD
=============
-Zbaveni sa TableAdapteru 
*SKzParametry
*OBJCislo

-Pridano logovani chyb Datasetu a DataTable
-Opraveno : Update Prijem tabulky OBJ a OBJpol



12.12.2018 v2.17 TaD
=============
-Pridana možnost konfiguračne povolit/zakazat dotahovani už vykritych položek na Vydeji
-Volny pohyb, rozširena trida Doklady, kde je možno nadefinovat vazby skrz typy dokladu
-Zbaveni sa TableAdapteru pro sCRady


05.12.2018 v2.16 TaD
=============
-Uprava Datasetu pohoda, postupne vyhadzovani TableAdapteru a predelavani na natvrdo napsane SQL commandy
-predelane OBJ a OBJpol
-Uprava modulu Prodej, online generovani Odberatele podle položek
-Predelani modulu Prodej na "Novy" spusob komunikace s pohodu a na "Objektove" skladani XML requestu

26.10.2018 v2.15 TaD
==============
-Modul Vydej, pridana konfigurane moznost generovat Fakturu
-Pridana Validace položek vuči Pohode zda je mozne provest import


22.10.2018 v2.14 TaD
==============
-Uprava import Vydejky, prenašeni Registrace Sazby DPH v EU


17.10.2018 v2.13 TaD
==============
-Uprava import vydejky, dotahovani DPH sazby
-Uprava generovani vydejky, dotahovani dat z objednavky



05.10.2018 v2.12 TaD
==============
-Inventura, uprava selectu dotahovani volitelnych parametru na typy sledovani
-Vydej, dotahovani text SText a poznamky + Odberatele spravne + dodaci adresa
-Vydej, pri grupovani pridany GUID jak nemenší s seznamu
-Prijem, pri grupovani pridany GUID jak nemenší s seznamu



03.10.2018 v2.11 TaD
==============

- pridane konfiguračne grupovani pri importu do pohody  
-Vydejka a Prijemka
-Pridane grupovani pred provazovanim dokladu


19.9.2018 v2.10 TaD
==============
-Uprava dotahovani CZ_SerNum_Track podle nove logiky

-Vydej - OK SE
-Prijem - OK PE
-Prodej - OK 095
-Inventura - OK I1




10.9.2018 v2.9 TaD
==============
-Uprava SQL dotazu pro generovani do predlohy vydeje s FA i s databaze access

5.9.2018 v2.8 TaD
==============
-Uprava dotaženi na vydej primim pristupem do DP z Faktury ( pridani do poznamky dotazeni SText s SKz) 
a Objednavky vydanej(jeste se nepouziva)




5.9.2018 v2.7 JiS
==============
Objednavky vydane
- aktualizace hodnoty SKz.ObjedV, SkzBuf.ObjedV, pokud se nastavuje priznak "Vyrizeno" se ponizuje o celkove mnozstvi na objednavce (resp. vydano + zbyvajici[mnozstvi-dodano ::: maximum dodano])	
- priznak "Vyrizeno" nastavuje se pouze pokud:
	: je "NeVyrizeno"
- pokud je objednavka "Trvaly doklad", tak se zadne dopocty stavu nedeji, pouze dojde k provazani polozek prijemky a objednavek
	

9.5.2016 v2.5 JiS
==============
Provider.pohoda
Prijem, Vydej
- upravy lokacniho mechanismu
- rozsireni o definici vazby polozkalokacevychozi
- trace modulu a zpracovani pro dohledani uzkych mist

28.4.2016 v2.4 JiS
==============
Prijemka, Vydejka
- Generovani cisla davky preneseno do transakce pri Update
- Opravy zpracovani status objectu ...

15.4.2016 v2.3 JiS
==============
Konfigurace (plati pro vsechny moduly - prijem, vydej, zbozi, inventura)
- Zbozi_DotahovatAlternativniDodavatele : dotahovat alternativni dodavetele pro zbozi
- MerneJednotky_DotahovatDalsiVarianty : dotahovat dalsi merne jednotky pro zbozi (False=pouze zakladni MJ)
            
31.3.2016 v2.2
=============
JiS
Obecne
parametry v settings rozsireny o paramtery, ktere museji byt nastaveny dle pohody (Nastaveni/Globalni nastaveni/Sklady[<Evidence vyrobnich cisel>|<Evidence sarzi>])
	=> EvidovatVyrobniCisla : Ano/Ne 
	=> EvidovatSarze		: Ano/Ne
	... pokud neni evidovano, pak je automaticky nastavena v tomto pripade hodnota na mnozstvi (CZ_SerNum_Track = 0)
Inventura, Prijem, Vydej, Prodej
- Oprava sledovani na SN/Sarze
Prijem
- prirava XML pro import prijemky dle vydane objednavky doplneny vazby dotazeni ze zdrojoveho dokladu
	hlavicka
	--------
	=> stredisko
	polozka
	-------
	=> stredisko 
	=> cinnost
	=> zakazka
	=> s DPH / bez DPH
	=> Sazba DPH (none, high, low, third)
	=> Sleva
	=> CenaMJ
	=> CenaMJ cizi mena
	=> Koeficient
	=> ...
Login
- Uprava online prihlaseni k terminalu, pri neuspechu zaloguje do logu MST Serveru

8.2.2016
========
JiS
Settings - HlavniSkladID(int)
=> Pokud neni uveden sklad pri generovani dokladu, je pouzit tento jako vychozi sklad pro generovani dokladu
=> neni umozneno generovat polozky dokladu, ktere obsahuji polozky z ruznych skladu


7.7.2012 JiS
============
Prijem 
- datum prijmu : oprava

12.5.2014 JiS
=============
Doplena funkcionalita FinishPrijemka
- bez hesla, akce aktualizuje hodnotu cz_doslo na cislo terminalu, ktery data odesila


19.11.2013 JiS
==============
Provider.LoadPrijemImportResponseXMLAndMakeUpdateDB(file, countentries);
doplnena prvni impelementace vazby na parovani(dokladovou vazbu) mezi vydanou objednavkou a novou prijemkou
algoritmus:
                //cislo dokladu nalezeno => aktualizace dokladu v pohode...
                //1) zjistit cislo dokladu prijemky
                //2) vytahnout polozky z pe s ord, itemnmbr, sopnumbe?
                //2a) cislo dokladu objednavky
                //3) vytahnout polozky z skpp a skppol pro konkretni novy doklad
                //4) vytahnout polozka z obj a objpol pro doklad
                //5) sparovat polozky obj(pe) s skpp
                // - postup ...
                //6) update v transakci ...



