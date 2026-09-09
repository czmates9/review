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
| [`Instalace`](Instalace/) | Instalační balíčky nástrojů potřebných pro vývoj a testování. |

## Požadavky pro lokální vývoj

- Windows 10 nebo 11.
- Visual Studio 2026 nebo kompatibilní verze Visual Studia s podporou desktopových aplikací .NET a ASP.NET.
- .NET Framework 4.7 Developer Pack.
- Microsoft SQL Server 2017 nebo novější; databázi `FASK` lze založit dodaným skriptem.
- IIS Express pro projekt `Win_Kom_Server`.
- Přístup k NuGetu pro obnovení balíčků.
- Git LFS pro stažení velkých instalačních souborů ze složky `Instalace`.

Některé starší části mohou navíc vyžadovat Microsoft Report Viewer 2010 SP1 nebo nativní knihovny SQLite.

## Založení a příprava databáze FASK

Aktuální zakládací skript je [FASK_SQL2017.sql](0x_SQL_Struktry_/00_SQL_Zakladaci_Script_Cele_DB/FASK_SQL2017.sql). Je určený pro SQL Server 2017 nebo novější, používá výchozí datové cesty SQL instance a vytvoří databázi `FASK` se 146 tabulkami a dalšími databázovými objekty.

Skript spusťte pod účtem, který může vytvářet databáze. Z kořene repozitáře použijte:

```powershell
sqlcmd -S localhost -E -i ".\0x_SQL_Struktry_\00_SQL_Zakladaci_Script_Cele_DB\FASK_SQL2017.sql"
```

Pokud databáze `FASK` už existuje, musí být prázdná a spouštěcí účet nad ní musí mít oprávnění `CONTROL`. Skript existující databázi nemaže a při nalezení uživatelských objektů skončí bez jejich změny. Zakládá pouze schéma bez počátečních dat; integrační objekty závislé na externích databázích, například Pohodě, nejsou aktivovány.

Po úspěšném založení připravte vývojového uživatele s přihlášením `0`, heslem `1` a potřebnými oprávněními:

```powershell
sqlcmd -S localhost -E -d FASK -i .\setup-fask-debug-user.sql
```

Nakonec lze doplnit testovací data pomocí:

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

### Test REST API v Brunu

Připravená kolekce [FASK Win Kom Server](Bruno/FASK-Win-Kom-Server/) obsahuje lokální prostředí pro IIS Express a požadavek `POST /api/Informations/MnozstviNaSklade`. Podrobný návod popisuje spuštění API, otevření správné složky v Brunu, výběr prostředí, odeslání požadavku i řešení běžných chyb. Před prvním testem doplňte podpůrnou proceduru do vývojové databáze:

```powershell
sqlcmd -S localhost -E -i .\setup-fask-api-stock-procedure.sql
```

V Brunu otevřete přímo složku `Bruno\FASK-Win-Kom-Server`, vyberte prostředí `Local IIS` a odešlete požadavek `Informations / Mnozstvi na sklade`. Kolekce používá Basic autentizaci s vývojovým uživatelem `0` a heslem `1`; Base64 hlavičku vytvoří Bruno automaticky.

## Čištění pracovního adresáře

Interaktivní čištění dočasných výstupů spustíte souborem:

```bat
Clear_VSE.bat
```

Skript odstraní generované adresáře a soubory, například `.vs`, `bin`, `obj`, sestavení v `!Build!`, soubory `.user` a `.suo`. Zdrojové kódy ani dokumentaci nemaže.

## Git a sestavení

Balíčky NuGet, sestavení, logy, cache a uživatelská nastavení Visual Studia jsou v `.gitignore`. Po novém klonování proto nejprve obnovte NuGet balíčky a proveďte čisté sestavení požadované solution.

Lokální přístupové údaje, adresy služeb a vývojové licence jsou určené pro ladění. Před nasazením vždy nastavte hodnoty odpovídající cílovému prostředí.
