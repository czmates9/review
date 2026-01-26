/****** Object:  View [dbo].[FASK_Logins_View_Prava]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[FASK_Logins_View_Prava]
AS
SELECT        L.USERID, L.firstname, L.surname, L.psswd, L.CREATED, L.VALIDFROM, L.VALIDTO, A.AGENDAID
FROM            dbo.FASK_Logins AS L LEFT OUTER JOIN
                         dbo.FASK_Logins_Auth AS A ON A.USERID = L.USERID

GO
/**************************************************************************************/