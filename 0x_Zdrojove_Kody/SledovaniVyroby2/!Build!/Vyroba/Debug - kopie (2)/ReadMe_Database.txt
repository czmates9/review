
Jedná se o dokument, který popisuje "nový" způsob komunikace s databází.

Projekt "Database" zastřešuje komunikaci.
- Obsahuje třidy: 
	- Vyroba_Local : ICommDatabase.ICommDatabase
	- Vyroba_Remote : ICommDatabase.ISQLDatabase
Každá táto třída obsahuje metody které volají Interface metody.


Projekt "APIRemoteLib" implementuje interface "ICommDatabase.ISQLDatabase"
který slouží pro komunikace přes REST API s komunikačním serverm, a následně do SQL Databáze.

Projekt "SQLRemoteLib" implementuje interface "ICommDatabase.ISQLDatabase"
který slouží pro komunikaci přímo do SQL Databáze.

Projekt "SQLCommLib" implementuje interface "ICommDatabase.ICommDatabase"
který slouží pro komunikaci přímo do SQL Databáze.

Projekt "SQLiteCommLib" implementuje interface "ICommDatabase.ICommDatabase"
který slouží pro komunikaci do lokalného SQLite Vyroba.prd souboru.