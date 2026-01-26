/****** Object:  StoredProcedure [dbo].[FASK_procPlnVyrobaTP]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 26.10.2020
-- Description:	Procedura pro dotažení vazev z IS pohoda do FASK
-- =============================================
CREATE PROCEDURE [dbo].[FASK_procPlnVyrobaTP]
@TypyVyrobku nvarchar(MAX),
@ListVyrobku nvarchar(MAX),
@TypyMaterialu nvarchar(MAX) 
AS
BEGIN
	SET NOCOUNT ON;	

		declare @cnt int;

	select @cnt = COUNT(*) from FASK_Vyroba_TP;

	if @cnt > 0
		BEGIN
			PRINT 'Tabulka [FASK_Vyroba_TP] již obsahuje ' + CAST(@cnt AS NVARCHAR(50)) + ' záznamů. Záznamy z IS POHODA nebudou přidány.'
			return -1;
		END


DECLARE @Polozky TABLE
(
    ID INT PRIMARY KEY IDENTITY,
    Klic int,
	ITEMDESC nvarchar(100),
	MJ nvarchar(50)

);

--Deklarace promennych
DECLARE @sp_count INT,
        @count INT = 0,
		@ITEMNMBR nvarchar(31) = 0,
		@ITEMDESC nvarchar(100) = 0,
		@MJ nvarchar(50) = 0,
		@ID_L_Max nvarchar(50) = '99';


IF @ListVyrobku = ''
	BEGIN
		INSERT  @Polozky
		SELECT
			S.ID as Klic,
			S.Nazev as ITEMDESC,
			S.MJ as MJ
		FROM  StwPh_04535667_2020.dbo.SKz as S
		WHERE S.RelSkTyp in (SELECT Name FROM dbo.splitstring(@TypyVyrobku, default))
	END
ELSE
	BEGIN
		INSERT  @Polozky
		SELECT
			S.ID as Klic,
			S.Nazev as ITEMDESC,
			S.MJ as MJ
		FROM  StwPh_04535667_2020.dbo.SKz as S
		WHERE S.RelSkTyp in (SELECT Name FROM dbo.splitstring(@TypyVyrobku, default))
		AND S.ID in (SELECT Name FROM dbo.splitstring(@ListVyrobku, default))
	END


SET @sp_count = (SELECT COUNT(1) FROM @Polozky)

--Cyklus přes všechny
	WHILE (@sp_count > @count)
	BEGIN
		SET @count = @count + 1;

		SET @ITEMNMBR = (SELECT Klic
						FROM    @Polozky
						WHERE   ID = @count);

		SET @ITEMDESC = (SELECT ITEMDESC
						FROM    @Polozky
						WHERE   ID = @count);

		SET @MJ = (SELECT MJ
						FROM    @Polozky
						WHERE   ID = @count);

		SET @ID_L_Max = (SELECT 
						ISNULL(MAX(ID_L), '99') 
						FROM FASK_Vyroba_TP)

		SET @ID_L_Max = Convert(nvarchar(50), CONVERT(int, @ID_L_Max) + 1);

	INSERT INTO [dbo].[FASK_Vyroba_TP]
			   ([ID_H]
			   ,[ID_L]
			   ,[ITEMNMBR_Def]
			   ,[DESC_Def]
			   ,[MJ_Def]
			   ,[ITEMNMBR_fol]
			   ,[DESC_Fol]
			   ,[MJ_Fol]
			   ,[koef]
			   ,[ID_USER]
			   ,[dateedit]
			   ,[alter]
			   ,[PUO])
			   SELECT 
			   NULL as [ID_H],
			   @ID_L_Max as [ID_L],
			   @ITEMNMBR as [ITEMNMBR_Def],
			   @ITEMDESC as [DESC_Def],
			   @MJ as [MJ_Def],
			   NULL as [ITEMNMBR_Fol],
			   NULL as [DESC_Fol],
			   NULL as [MJ_Fol],
			   NULL as [koef],
			   99 as [ID_USER],
			   GETDATE() as [dateedit],
			   0 as [alter],
			   1 as [PUO]

EXECUTE [FASK_proc_Insert_VyrobaTP] 
   @ITEMNMBR
  ,@ITEMDESC
  ,@MJ
  ,@ID_L_Max
  ,@TypyMaterialu


	END

END
/****************************************************************************/