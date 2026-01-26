USE [master]
GO

ALTER DATABASE [s4s-colorprofi-automotive] SET  OFFLINE
GO

USE [master]
RESTORE DATABASE [s4s-colorprofi-automotive] FROM  DISK = N'D:\!!!_Miccrosoft SQL Server_!!!\MSSQL15.SQL2019\MSSQL\Backup\s4s-colorprofi-automotive.bak' WITH  FILE = 1,  MOVE N's4s-color' TO N'D:\!!!_Miccrosoft SQL Server_!!!\MSSQL15.SQL2019\MSSQL\DATA\s4s-colorprofi-automotive.mdf',  MOVE N's4s-color_log' TO N'D:\!!!_Miccrosoft SQL Server_!!!\MSSQL15.SQL2019\MSSQL\DATA\s4s-colorprofi-automotive.LDF',  NOUNLOAD,  REPLACE,  STATS = 5
GO

ALTER DATABASE [s4s-colorprofi-automotive] SET  ONLINE
GO

