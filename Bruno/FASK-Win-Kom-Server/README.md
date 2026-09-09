# Testování Win_Kom_Serveru v Brunu

Tato složka je hotová Bruno kolekce pro lokální REST API projektu `Win_Kom_Server`. URL, přihlašovací údaje, JSON tělo i kontrola odpovědi jsou již připravené. V Brunu je nemusíte vytvářet ručně.

## Co se bude testovat

Kolekce odešle tento požadavek:

```text
POST http://localhost:8000/api/Informations/MnozstviNaSklade
```

Metoda vyhledá množství zboží na zadaném skladu. Výchozí test používá:

```json
{
  "idZbozi": "TEST_ITEM_01",
  "idSklad": "TEST_LOC_01"
}
```

Pro tento záznam je očekávaná odpověď `200 OK` a hodnota `1.0`.

## Jednorázová příprava databáze

Tento krok stačí provést jednou. V PowerShellu otevřeném v kořeni repozitáře spusťte:

```powershell
sqlcmd -S localhost -E -i .\setup-fask-api-stock-procedure.sql
```

Příkaz vytvoří nebo aktualizuje proceduru `dbo.fask_mnozstvinasklade2`, kterou testovaná metoda používá. Databáze `FASK` a testovací záznamy musí být připravené podle hlavního README repozitáře.

## 1. Spuštění API ve Visual Studiu

1. Otevřete solution `0x_Zdrojove_Kody\Win_Kom_Server\MST_Win_Kom_Server.sln`.
2. V **Solution Exploreru** klikněte pravým tlačítkem na webový projekt `MST_Win_Kom_Server` a vyberte **Set as Startup Project**.
3. V horní liště Visual Studia vyberte profil **IIS Express**.
4. Spusťte projekt klávesou **F5**.
5. Zkontrolujte, že se služba otevře na adrese `http://localhost:8000/`.

Visual Studio musí zůstat spuštěné po celou dobu testování v Brunu.

## 2. Otevření připravené kolekce v Brunu

1. Spusťte aplikaci **Bruno**.
2. Na úvodní obrazovce klikněte na **Open Collection**. Pokud už máte otevřenou jinou kolekci, použijte nabídku **File → Open Collection**.
3. Vyberte přesně tuto složku:

   ```text
   C:\Users\ratho\OneDrive\Plocha\work_2026\FASK_2026\Bruno\FASK-Win-Kom-Server
   ```

4. Potvrďte otevření složky. Bruno pozná kolekci podle souboru `bruno.json`.
5. V levém panelu se zobrazí složka **Informations** a v ní požadavek **Mnozstvi na sklade**.

Nevybírejte nadřazenou složku `Bruno`; je potřeba otevřít přímo složku `FASK-Win-Kom-Server`.

## 3. Výběr lokálního prostředí

1. Vpravo nahoře klikněte na nabídku prostředí, která může zobrazovat **No Environment**.
2. Vyberte **Local IIS**.
3. Prostředí automaticky nastaví:

   | Proměnná | Hodnota | Význam |
   | --- | --- | --- |
   | `baseUrl` | `http://localhost:8000` | Adresa IIS Express. |
   | `username` | `0` | Vývojový uživatel API. |
   | `password` | `1` | Vývojové heslo API. |
   | `itemNumber` | `TEST_ITEM_01` | Testované ID zboží. |
   | `warehouseId` | `TEST_LOC_01` | Testované ID skladu. |

Pokud v URL nebo JSON těle zůstane text jako `{{baseUrl}}`, prostředí **Local IIS** není vybrané.

## 4. Odeslání požadavku

1. V levém panelu otevřete **Informations → Mnozstvi na sklade**.
2. Nahoře u požadavku uvidíte metodu **POST** a URL `{{baseUrl}}/api/Informations/MnozstviNaSklade`.
3. Na kartě **Body** je připravený JSON s proměnnými `{{itemNumber}}` a `{{warehouseId}}`.
4. Na kartě **Auth** je nastavený typ **Basic Auth** a hodnoty `{{username}}` a `{{password}}`.
5. Klikněte na **Send** nebo stiskněte **Ctrl+Enter**.

Basic Auth nepoužívá šifrování hesla. Bruno automaticky sestaví text `0:1`, převede jej do Base64 a odešle v hlavičce:

```text
Authorization: Basic MDox
```

Tuto hlavičku do části **Headers** nepřidávejte ručně.

## 5. Vyhodnocení výsledku

Ve spodní části Bruna se zobrazí HTTP stav a tělo odpovědi:

| Výsledek | Význam |
| --- | --- |
| `200 OK` a `1.0` | Testovací zboží bylo nalezeno a na skladu je množství 1. |
| `204 No Content` | Zadaná kombinace zboží a skladu v databázi není. |
| `401 Unauthorized` | Nesouhlasí uživatel, heslo nebo oprávnění API. |
| `404 Not Found` | Nesouhlasí URL, port nebo route metody. |
| `500 Internal Server Error` | Server běží, ale selhala konfigurace provideru, databázové připojení nebo procedura. |

Součástí požadavku je test, který považuje odpovědi `200` a `204` za platné. Výsledek testu se zobrazí na kartě **Tests**.

## Test jiného zboží nebo skladu

1. Vpravo nahoře otevřete prostředí **Local IIS**.
2. Změňte hodnotu `itemNumber` nebo `warehouseId`.
3. Uložte prostředí.
4. Požadavek odešlete znovu.

Tím zůstanou tělo požadavku i autentizace společné a mění se jen testovací data.

## Nejčastější potíže

### Bruno hlásí `ECONNREFUSED`

IIS Express na portu 8000 neběží. Spusťte `MST_Win_Kom_Server` ve Visual Studiu pomocí **F5** a zkuste požadavek znovu.

### Port 8000 je obsazený

Ukončete starou instanci IIS Express a znovu spusťte projekt ve Visual Studiu. V kolekci i projektu je nastavený port 8000.

### Odpověď je `401 Unauthorized`

Zkontrolujte, že je vybrané prostředí **Local IIS** a na kartě **Auth** vidíte typ **Basic Auth**, uživatele `0` a heslo `1`. Uživatel také musí mít v databázi oprávnění pro serverové API.

### Odpověď je `500 Internal Server Error`

Ověřte připojení projektu k lokální databázi `FASK` a znovu spusťte `setup-fask-api-stock-procedure.sql`. Podrobnost chyby bývá také v logu `Win_Kom_Serveru`.
