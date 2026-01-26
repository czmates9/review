-- ================================================
-- Template generated from Template Explorer using:
-- Create Trigger (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- See additional Create Trigger templates for more
-- examples of different Trigger statements.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiri Skrivanek
-- Create date: 
-- Description:	historie a aktualizace
-- =============================================
CREATE TRIGGER fask_ukol_uziv_history 
   ON  CZ_UKOL_UZIV 
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

-- TODO : pridat volani ulozene procedury pro aktualizaci pro kazde ID ukolu uzivatele                                 

END
GO
