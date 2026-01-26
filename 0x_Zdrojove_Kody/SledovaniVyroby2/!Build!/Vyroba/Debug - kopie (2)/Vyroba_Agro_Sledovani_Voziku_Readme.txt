v 1.12 MaR 17.7.2023
-prejmenovani bocedi na strech
-zvyseni verze

v 1.11 MaR 29.5.2023
-logovani výpadku signálu na ADAM

v 1.10 MaR 23.3.2023
-logovani online nakladka, nedoslo k vykladce
-uprava RFID vykladka, nevymazava se interni promenna

v 1.9 MaR 1.3.2023
-RFID zapis do ErrLogu pri zalozni logice RFID, nekorektni vykladka, nefunkcnost RFID TAG, zalozni system RFID OK 
-vykladka vycteni z RFID vyhodnoceni nekorektni vykladky a dochazi k nastaveni interni promenne na 0 + logovani online

v 1.8 MaR 1.3.2023
-RFID zapis do ErrLogu pri zmene barvy prvku RFID funkcnost TAG
-RFID zmenena logika pri vykladce, pokud se vycete TAG a chybi interniCisloLinky, tak se bere z tagu hodnota jen kdyz je prvek
RFID funkcnost tag zeleny a pak se kontroluje zda-li neni linka==99, pokud to selze nedojde k vykladce

v 1.7 MaR 24.2.2023
-oprava RFID Bocedi 2 + dopneni konstanty na falsak pri uplnem selhani logiky RFID a interni promenne
-logovani chyby tisku do FASK_Event_Err pri nefunkcni sluzbe
-uprava logiky RFID, pri lince 99 se nebere internicisloMatice, ale interni promenna!

v 1.6 MaR 23.2.2023
-hlubsi logovani chyb RFID
-logovani chyby tisku

v 1.5 MaR 23.2.2023
-logovani do ERR_log neni nalezen zaznam status 0 a kdyz nejede RFID
-Deaktivace starych zaznamu, propisuje se machineId bocedi 1 a 2 a na zaklade toho se pak posila na server


v 1.4 MaR 20.2.2023
-globalni promenne pro konstanty linek
-designpatern provideroveho reseni
-vytazeni vnitrni promenne linek do labelu
-panel testy zaheslovany a udelana logika pro aktivaci TAGu

v 1.3 TaD 26.1.2023
-Uprava formatovani kodu

v 1.2 TaD 24.8.2022
- Uprava grafiky a rozlozeni tlacitek
-prohozeni tlacitek dle pozadavku JaS + p.Slavik
-do Verze pridane zaviraci tlacitko velke

v 1.1 TaD 23.8.2022
- Uprava grafiky a rozlozeni tlacitek

v 1.0 MaR 6.4.2022
- AGRO sledovani voziku
- logovani posilanych dat na TISK
- uprava logiky pri posilani dat na TISK