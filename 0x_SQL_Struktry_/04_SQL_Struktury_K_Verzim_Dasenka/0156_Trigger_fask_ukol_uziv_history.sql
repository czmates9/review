
/****** Object:  Trigger [dbo].[fask_ukol_uziv_history]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***** Trigger pro ukladani historie pri zmene stavu ukolu uzivatele *****/
-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 
-- Description:	historie a aktualizace
-- =============================================
CREATE TRIGGER [dbo].[fask_ukol_uziv_history] 
   ON  [dbo].[CZ_UKOL_UZIV] 
   AFTER INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	declare 
           @ID int
           ,@UkolID int
           ,@UserID int
           ,@State nvarchar(1)
           ,@DateChanged datetime
           ,@UserIDChanged int
           ,@Note nvarchar(200)
           ,@DateNotify datetime
           ,@DateFinished datetime

INSERT INTO [CZ_UKOL_UZIV_HIST]
           ([ID]
           ,[UkolID]
           ,[UserID]
           ,[State]
           ,[DateChanged]
           ,[UserIDChanged]
           ,[Note]
           ,[DateNotify]
           ,[DateFinished])
           ( select 
				ID 
				,UkolID
				,UserID
				,[State]
				,DateChanged
				,UserIDChanged
				,Note
				,DateNotify
				,DateFinished
				from inserted
           )
END

GO
/**************************************************************************************/