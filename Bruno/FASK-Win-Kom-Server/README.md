# FASK Win Kom Server – Bruno kolekce

Kolekce obsahuje lokální test REST API projektu `Win_Kom_Server` spuštěného přes IIS Express.

## Použití

1. Spusťte `MST_Win_Kom_Server` ve Visual Studiu a ověřte, že IIS Express naslouchá na `http://localhost:8000`.
2. V Brunu zvolte **Open Collection** a otevřete tuto složku.
3. Vyberte prostředí **Local IIS**.
4. Otevřete požadavek **Informations / Mnozstvi na sklade** a stiskněte **Send**.

Požadavek používá HTTP Basic autentizaci. Bruno z hodnot `username=0` a `password=1` automaticky vytvoří Base64 hodnotu hlavičky `Authorization`; heslo se ručně nekóduje.

Výchozí tělo požadavku používá testovací záznam z databáze:

```json
{
  "idZbozi": "TEST_ITEM_01",
  "idSklad": "TEST_LOC_01"
}
```

Očekávaná odpověď je `200 OK` s číselným množstvím. Pokud kombinace zboží a skladu neexistuje, API vrací `204 No Content`. Hodnoty lze změnit v prostředí `Local IIS`.

