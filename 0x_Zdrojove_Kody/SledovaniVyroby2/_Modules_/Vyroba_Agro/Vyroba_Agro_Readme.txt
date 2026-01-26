v 1.9 TaD 20.10.2017
==================
- rozsireni logovani po odhlaseni o Material(sarzi) dos souboru a emaile v NotoficationMail.cs metode SendEmailOdhlaseniSmeny


v 1.8 TaD 17.10.2017
==================
InformationUC.cs :
-pridane pri zadavani hesla u zmenz sarze misto textu se zobrazuji *
-pri prechodu z stavu VyrobaStavy.SarzeID do stavu VyrobaStavy.SarzeHeslo se zapnou hviezdičky
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
