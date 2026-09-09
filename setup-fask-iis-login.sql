USE [master];
GO

IF SUSER_ID(N'IIS APPPOOL\MST_W_Server') IS NULL
    CREATE LOGIN [IIS APPPOOL\MST_W_Server] FROM WINDOWS;
GO

USE [FASK];
GO

IF USER_ID(N'IIS APPPOOL\MST_W_Server') IS NULL
    CREATE USER [IIS APPPOOL\MST_W_Server] FOR LOGIN [IIS APPPOOL\MST_W_Server];
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.database_role_members
    WHERE role_principal_id = DATABASE_PRINCIPAL_ID(N'db_datareader')
      AND member_principal_id = DATABASE_PRINCIPAL_ID(N'IIS APPPOOL\MST_W_Server')
)
    ALTER ROLE [db_datareader] ADD MEMBER [IIS APPPOOL\MST_W_Server];

IF NOT EXISTS (
    SELECT 1
    FROM sys.database_role_members
    WHERE role_principal_id = DATABASE_PRINCIPAL_ID(N'db_datawriter')
      AND member_principal_id = DATABASE_PRINCIPAL_ID(N'IIS APPPOOL\MST_W_Server')
)
    ALTER ROLE [db_datawriter] ADD MEMBER [IIS APPPOOL\MST_W_Server];

GRANT EXECUTE TO [IIS APPPOOL\MST_W_Server];
GO
