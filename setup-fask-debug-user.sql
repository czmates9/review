SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRANSACTION;

DECLARE @UserId nvarchar(20) = N'0';
DECLARE @Password nvarchar(50) = N'1';

IF EXISTS (SELECT 1 FROM dbo.FASK_Logins WHERE USERID = @UserId)
BEGIN
    UPDATE dbo.FASK_Logins
       SET firstname = N'Local',
           surname = N'Debug',
           psswd = @Password,
           VALIDFROM = GETDATE(),
           VALIDTO = DATEADD(year, 10, GETDATE())
     WHERE USERID = @UserId;
END
ELSE
BEGIN
    INSERT dbo.FASK_Logins (USERID, firstname, surname, psswd, CREATED, VALIDFROM, VALIDTO, RFID)
    VALUES (@UserId, N'Local', N'Debug', @Password, GETDATE(), GETDATE(), DATEADD(year, 10, GETDATE()), NULL);
END;

DECLARE @Rights TABLE
(
    AGENDAID nvarchar(100) NOT NULL PRIMARY KEY,
    NAME nvarchar(100) NOT NULL
);

INSERT @Rights (AGENDAID, NAME) VALUES
    (N'_',             N'Veškerá oprávnění'),
    (N'API_',          N'API'),
    (N'K_',            N'Konzola'),
    (N'K_Admin_',      N'Konzola administrátor'),
    (N'K_E_',          N'Konzola editace'),
    (N'K_Imp_',        N'Konzola import'),
    (N'K_IT_',         N'Konzola IT'),
    (N'K_IT_Arch',     N'Konzola IT archivace'),
    (N'K_P_Approval_', N'Konzola schvalování'),
    (N'K_P_ZP',        N'Konzola plánování'),
    (N'M_',            N'Mobilní klient'),
    (N'M_Admin_',      N'Mobilní administrátor'),
    (N'SV_',           N'Sledování výroby'),
    (N'V_',            N'Výroba'),
    (N'V_Admin_',      N'Výroba administrátor'),
    (N'V_VS_',         N'Výroba vedoucí směny');

UPDATE agenda
   SET agenda.NAME = rights.NAME,
       agenda.DESCIPTION = N'Lokální vývojový účet Konzola',
       agenda.AUTH = 1
  FROM dbo.FASK_AGENDA agenda
  JOIN @Rights rights ON rights.AGENDAID = agenda.AGENDAID;

INSERT dbo.FASK_AGENDA (AGENDAID, NAME, DESCIPTION, AUTH)
SELECT rights.AGENDAID, rights.NAME, N'Lokální vývojový účet Konzola', 1
  FROM @Rights rights
 WHERE NOT EXISTS (SELECT 1 FROM dbo.FASK_AGENDA agenda WHERE agenda.AGENDAID = rights.AGENDAID);

DELETE FROM dbo.FASK_Logins_Auth WHERE USERID = @UserId;

INSERT dbo.FASK_Logins_Auth (USERID, AGENDAID, AUTH)
SELECT @UserId, AGENDAID, 1 FROM @Rights;

IF NOT EXISTS
(
    SELECT 1
      FROM dbo.FASK_Logins_View_Prava
     WHERE USERID = @UserId AND AGENDAID = N'K_Admin_'
)
    THROW 51000, 'Debug user permission verification failed.', 1;

COMMIT TRANSACTION;

SELECT USERID, firstname, surname, VALIDFROM, VALIDTO
  FROM dbo.FASK_Logins
 WHERE USERID = @UserId;

SELECT USERID, AGENDAID, AUTH
  FROM dbo.FASK_Logins_Auth
 WHERE USERID = @UserId
 ORDER BY AGENDAID;
