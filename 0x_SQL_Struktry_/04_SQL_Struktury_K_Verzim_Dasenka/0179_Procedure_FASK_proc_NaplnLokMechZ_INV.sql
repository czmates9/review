/****** Object:  StoredProcedure [dbo].[FASK_proc_NaplnLokMechZ_INV] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bc Tadeas Divacky
-- Create date: 
-- Description:	Procedura Pro naplneni LokMech z Inventury
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_NaplnLokMechZ_INV] 
	@CountEntries int = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @cnt int;

	select @cnt = COUNT(*) from [CZMST_SkladLokace_Stav];

	if @cnt > 0
		BEGIN
			PRINT 'Tabulka [CZMST_SkladLokace_Stav] již obsahuje ' + CAST(@cnt AS NVARCHAR(50)) + ' záznamů. Záznamy z inventury nebudou přidány.'
			return -1;
		END

		INSERT INTO [CZMST_SkladLokace_Stav]
			([ITEMNMBR],
			[ITEMDESC],
			[QTYSHPPD_DEF],
			[QTYSHPPD],
			[SERLTNUM],
			[SKL_ID],
			[LOCNCODE],
			[DATECHANGE],
			[EXPIRATION],
			[QTYSHPPD_DEF_DATE],
			[QTY_OWNER],
			[PRAC_ID_OWNER])
		SELECT
			i4.ITEMNMBR as ITEMNMBR,
			ISNULL(i1.ITEMDESC, '') as ITEMDESC,
			SUM(i4.QUANTITY) as QTYSHPPD_DEF,
			SUM(i4.QUANTITY) as QTYSHPPD,
			ISNULL(i4.SERLNMBR, '') as SERLTNUM,
			ISNULL(i4.skl_id, '') as SKL_ID,
			ISNULL(i4.LOCNCODE, '') as LOCNCODE,
			GETDATE() as DATECHANGE,
			i4.Expirace as EXPIRATION,
			GETDATE() as QTYSHPPD_DEF_DATE,
			0 as [QTY_OWNER],
			null as [PRAC_ID_OWNER]
			from [CZMST_I4] i4
			left join [CZMST_I1] i1 on i1.ITEMNMBR = i4.ITEMNMBR
			WHERE  I4.CountEntries = @CountEntries
			GROUP BY i4.ITEMNMBR, i1.ITEMDESC, i4.SERLNMBR, i4.skl_id, i4.LOCNCODE, i4.Expirace


		select @cnt = COUNT(*) from [CZMST_SkladLokace_Stav];

		PRINT 'Lokační mechanismus úspešně naplněn inventurními daty dne ' + CAST(GETDATE() AS NVARCHAR(50))  + '. Do lokačního mechanismu bylo přidáno ' + CAST(@cnt AS NVARCHAR(50)) + ' nových záznamů.';

END

/*********************************************************************************************************/
