v 1.31 MaR 17.07.2023

- prejmenovani bocedi na strech
- zvyseni verze


v 1.3 TaD 02.02.2023

- Archivace, oprava zasílani na server MachineID -1 pokud se jedna o bocedi...
   - TODO, stejne je to vymyslene špatne, pokud bude víc hal v DB tak to nebude fungovat spravně


v 1.2 TaD 16.01.2023

- Kompletne odstraneni RFID logiky a komunikace + konfigurace
- Kompletne odstraneni ADAM3  + konfigurace
- kompletne odtraneni komunikace s FASK serverem pomoci API komunikace. + konfigurace
  - Predelano na volaní přes providera APIRemoteLib
- Pokus o vyčistení kodu od zbytečnych komentaru, testovacich časti kodu, nepouživanych časti kodu....
- Kompletne odtraneni L1 až L4 založní logiky + konfigurace
- ADAM1 a ADAM2 pročistení kongiruračných parametru ktere se v kodu nepouživaly + konfigurace
- Předelaní všech ID strojů na promenne ktere jsou definovany v konstantach, už žadny ID nekde hardcode
- Předelaní všech Statusu na promenne ktere jsou definovany v konstantach, už žadny Statusy nekde hardcode

- Logika osekana na pouze jednu Linku a Jedednu Streckovacku
- cez REST API je pouze komunikace s Leonardo Tisk

- Kompletne predelany zpusob prace s koniguraci která se uklada do souboru vyroba_agro_SV_Modbus_config.xml

- Kompletne predelana logika okna pro Archivaci
  - Ted je udelano univerzalnější, neobsahuje moc logiky z informacema
  - všechno predavano jak parametr do kontruktoru

- Presunuty všech formu do složky form kromě MainFormu
- Presunuty všech UC do složky UC

-UserControler pro Vahy osekan pouze na jednu Vahu

frmMain:
- Hlavní event "OnRFIDEvent" premenovan na "LogikaZAdama_EventHandler"
  - důvod, RFID už zde nefiguruje
- Odstraneny ADAM3



v 1.1 TaD 07.11.2022

-Vytvořena kopie projektu pro Sledovani voziku pro Roboty
 

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