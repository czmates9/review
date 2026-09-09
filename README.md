# FASK 2026

Vývojový repozitář systému FASK/MST pro skladové a výrobní procesy. Obsahuje několik vzájemně propojených aplikací v .NET Frameworku, sdílené knihovny, databázové skripty a projektovou dokumentaci.

## Hlavní projekty

| Projekt | Účel |
| --- | --- |
| [Konzola](0x_Zdrojove_Kody/Konzola/Konzola.sln) | Desktopová administrační a servisní konzola. |
| [Win_Kom_Server](0x_Zdrojove_Kody/Win_Kom_Server/MST_Win_Kom_Server.sln) | Komunikační server a webové služby spouštěné přes IIS Express. |
| [MST_07_Klienti](0x_Zdrojove_Kody/MST_07_Klienti) | Windows a Android klienti pro terminály. |
| [Vyroba_2_Klienti](0x_Zdrojove_Kody/Vyroba_2_Klienti) | Klientské aplikace pro výrobu. |
| [SledovaniVyroby2](0x_Zdrojove_Kody/SledovaniVyroby2/SledovaniVyroby.sln) | Aplikace pro sledování výroby. |
| [Aktualizace_API](0x_Zdrojove_Kody/Aktualizace_API/Aktualizace_API.sln) | API a nástroje pro aktualizace. |
| [_SdileneKnihovny_](0x_Zdrojove_Kody/_SdileneKnihovny_) | Společné knihovny používané více projekty. |
| [_SdileneProjekty_](0x_Zdrojove_Kody/_SdileneProjekty_) | Sdílené projekty řešení. |
| [_SdileneTridy_](0x_Zdrojove_Kody/_SdileneTridy_) | Sdílené zdrojové třídy. |
| [Utility](0x_Zdrojove_Kody/Utility) | Servisní, licenční a databázové nástroje. |

Repozitář není jedna monolitická solution. Pro konkrétní část systému otevřete příslušný soubor `.sln`.

## Další obsah repozitáře

| Adresář | Obsah |
| --- | --- |
| `01_Schema` | Schémata systému a procesů. |
| `02_Pozadavky` | Požadavky na změny a funkce. |
| `03_Analyzy` | Analytické podklady. |
| `04_Navrhy` | Návrhy řešení. |
| `04_Support` | Podklady pro podporu. |
| `05_Pripominky` | Připomínky a zpětná vazba. |
| `06_ToDo` | Rozpracované úkoly. |
| `07_Dokumentace` | Uživatelská a technická dokumentace. |
| `0x_SQL_Struktry_` | SQL struktury a databázové podklady. |

## Požadavky pro lokální vývoj

- Windows 10 nebo 11.
- Visual Studio 2026 nebo kompatibilní verze Visual Studia s podporou desktopových aplikací .NET a ASP.NET.
- .NET Framework 4.7 Developer Pack.
- Microsoft SQL Server s lokální databází `FASK`.
- IIS Express pro projekt `Win_Kom_Server`.
- Přístup k NuGetu pro obnovení balíčků.

Některé starší části mohou navíc vyžadovat Microsoft Report Viewer 2010 SP1 nebo nativní knihovny SQLite.

## Příprava databáze FASK

Skripty předpokládají lokální SQL Server, Windows autentizaci a existující databázi `FASK`.

Vývojového uživatele s přihlášením `0`, heslem `1` a potřebnými oprávněními připravíte z kořene repozitáře příkazem:

```powershell
sqlcmd -S localhost -E -d FASK -i .\setup-fask-debug-user.sql
```

Testovací data lze doplnit pomocí:

```powershell
sqlcmd -S localhost -E -d FASK -i .\seed-fask-all-tables.sql
```

Seedovací skript zapisuje data do databáze. Používejte jej pouze nad vývojovou databází a před spuštěním vytvořte zálohu.

## Spuštění projektu Konzola

1. Otevřete [Konzola.sln](0x_Zdrojove_Kody/Konzola/Konzola.sln).
2. Obnovte NuGet balíčky a sestavte konfiguraci `Debug`.
3. Z kořene repozitáře spusťte:

   ```powershell
   .\setup-konzola-debug.ps1
   ```

4. V projektu nastavte správný spouštěcí projekt a spusťte ladění klávesou `F5`.
5. Do aplikace se přihlaste uživatelem `0` a heslem `1`.

Skript nastaví SQL provider, připojení k lokální databázi `FASK`, vypne synchronizaci Active Directory pro lokální vývoj a připraví vývojovou licenci v adresáři sestavení. Licence Konzoly je platná jeden rok od vytvoření; při pozdějším vývoji spusťte skript znovu.

## Spuštění projektu Win_Kom_Server

1. Otevřete [MST_Win_Kom_Server.sln](0x_Zdrojove_Kody/Win_Kom_Server/MST_Win_Kom_Server.sln).
2. Obnovte NuGet balíčky a sestavte řešení.
3. Nastavte webový projekt jako spouštěcí projekt a spusťte jej přes IIS Express.
4. Lokální adresa služby je [http://localhost:8000/](http://localhost:8000/).

Vývojová konfigurace používá SQL provider `Fask.ModuleSql`, databázi `FASK` přes Windows autentizaci a lokální vývojovou licenci uloženou ve výstupním adresáři aplikace.

Pokud je port 8000 obsazený nebo IIS Express hlásí duplicitní registraci URL, ukončete běžící instanci IIS Express a spusťte řešení znovu.

## Čištění pracovního adresáře

Interaktivní čištění dočasných výstupů spustíte souborem:

```bat
Clear_VSE.bat
```

Skript odstraní generované adresáře a soubory, například `.vs`, `bin`, `obj`, sestavení v `!Build!`, soubory `.user` a `.suo`. Zdrojové kódy ani dokumentaci nemaže.

## Git a sestavení

Balíčky NuGet, sestavení, logy, cache a uživatelská nastavení Visual Studia jsou v `.gitignore`. Po novém klonování proto nejprve obnovte NuGet balíčky a proveďte čisté sestavení požadované solution.

Lokální přístupové údaje, adresy služeb a vývojové licence jsou určené pro ladění. Před nasazením vždy nastavte hodnoty odpovídající cílovému prostředí.
