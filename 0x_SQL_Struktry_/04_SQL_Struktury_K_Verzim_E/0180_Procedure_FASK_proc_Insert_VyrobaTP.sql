/****** Object:  StoredProcedure [dbo].[FASK_proc_Insert_VyrobaTP] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Tadeas Divacky
-- Create date: 26.10.2020
-- Description:	Procedura pro dotažení vazev z IS pohoda do FASK
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_Insert_VyrobaTP] 
@ITEMNMBR nvarchar(31) = null,
@ITEMDESC nvarchar(100) = null,
@MJ nvarchar(50) = null,
@ID_H int,
@TypyMaterialu nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;	

	DECLARE @PolozkyMat TABLE
	(
		ID INT PRIMARY KEY IDENTITY,
		Klic int,
		ITEMDESC nvarchar(100),
		MJ nvarchar(50),
		QTY numeric(19,5)
	);

	DECLARE @AllCount INT,
			@Inkrement INT = 0,
			@ITEMNMBR_pol nvarchar(31) = 0,
			@ITEMDESC_pol nvarchar(100) = 0,
			@MJ_pol nvarchar(50) = 0,
			@QTY numeric(19,5),
			@ID_L_Max int;

	INSERT  @PolozkyMat
	SELECT
		S.ID as Klic,
		S.Nazev as ITEMDESC,
		S.MJ as MJ,
		P.Mnozstvi as QTY
	FROM StwPh_04535667_2020.dbo.SKzPol as P
	left join StwPh_04535667_2020.dbo.SKz as S ON S.ID = P.RefSKz
	where P.RefAg = @ITEMNMBR AND 
	S.RelSkTyp in (SELECT Name FROM dbo.splitstring(@TypyMaterialu, default))


	SET @AllCount = (SELECT COUNT(1) FROM @PolozkyMat)

		WHILE (@AllCount > @Inkrement)
		BEGIN

			SET @Inkrement = @Inkrement + 1;

				SET @ITEMNMBR_pol = (SELECT Klic
							FROM    @PolozkyMat
							WHERE   ID = @Inkrement);

			SET @ITEMDESC_pol = (SELECT ITEMDESC
							FROM    @PolozkyMat
							WHERE   ID = @Inkrement);

			SET @MJ_pol = (SELECT MJ
							FROM    @PolozkyMat
							WHERE   ID = @Inkrement);

			SET @QTY = (SELECT QTY
							FROM    @PolozkyMat
							WHERE   ID = @Inkrement);

		SET @ID_L_Max = (SELECT MAX(ID_L) FROM FASK_Vyroba_TP)

		SET @ID_L_Max = @ID_L_Max + 1;

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
		@ID_H as [ID_H],
		@ID_L_Max as [ID_L],
		@ITEMNMBR as [ITEMNMBR_Def],
		@ITEMDESC as [DESC_Def],
		@MJ as [MJ_Def],
		@ITEMNMBR_pol as [ITEMNMBR_Fol],
		@ITEMDESC_pol as [DESC_Fol],
		@MJ_pol as [MJ_Fol],
		@QTY as [koef],
		99 as [ID_USER],
		GETDATE() as [dateedit],
		0 as [alter],
		0 as [PUO]

		END		
END


/*****************************************************************************/