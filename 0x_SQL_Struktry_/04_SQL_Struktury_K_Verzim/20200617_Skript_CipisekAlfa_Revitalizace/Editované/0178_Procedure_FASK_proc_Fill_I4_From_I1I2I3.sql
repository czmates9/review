/****** Object:  StoredProcedure [dbo].[FASK_proc_DobrePodlahy_Fill_I4]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bc. Tadeas Divacky
-- Create date: 12.10.2020
-- Description:	Procedrua pro FAKE naplneni Inventury do I4
-- =============================================
CREATE PROCEDURE [dbo].[FASK_proc_Fill_I4_From_I1I2I3]
	-- Add the parameters for the stored procedure here
	@CountEntries int = 1,
	@LOCNCODE_FAKE nvarchar(20) = '99',
	@SERLTNUM_FAKE nvarchar(20) = '66'	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @cnt int;

	select @cnt = COUNT(*) from [CZMST_I4];

	if @cnt > 0
		BEGIN
			PRINT 'Tabulka [CZMST_I4] již obsahuje ' + CAST(@cnt AS NVARCHAR(50)) + ' záznamů. Záznamy z inventury nebudou přidány.'
			return -1;
		END

	/*************************/
	--Varianta kdy je položka vedena na šarže, ale šarže neni vyplnena
	--Vyplnuje se FAKE šarže 66
	/************************/

			INSERT INTO CZMST_I4 (
				[CountEntries]
				,[CE_Orig]
				,[ITEMNMBR]
				,[CZ_CarKod]
				,[LOCNCODE]
				,[SKL_ID]
				,[VNDITNUM]
				,[MJ]
				,[QUANTITY]
				,[QUANTITYMJ]
				,[QTYPACK]
				,[SERLNMBR]
				,[DATEDONE]
				,[TIMEDONE]
				,[USERID]
				,[GUID]
				,[O_Checked]
				,[INPUT_MODE]
				,[ID_TERMINAL]
				,[ITEMCODE]
				,[REZ_1]
				,[REZ_2]
				,[WEIGHT]
				,[Expirace])
			SELECT 
				I1.CountEntries,
				null as CE_Orig,
				I1.ITEMNMBR,
				I1.CZ_CarKod,
				@LOCNCODE_FAKE as [LOCNCODE],
				I1.SKL_ID,
				I3.VNDITNUM,
				I3.MJ,
				case I3.QTYPACK when 0 then I1.QUANTITY else I3.QTYPACK * I1.QUANTITY END as [QUANTITY],
				I1.QUANTITY as QUANTITYMJ,
				I3.QTYPACK,
				@SERLTNUM_FAKE as [SERLNMBR],
				convert(varchar, getdate(), 112) as [DATEDONE],
				replace(Convert (varchar(8),GetDate(), 108),':','') as [TIMEDONE],
				0 as [USERID],
				NEWID() as [GUID],
				0 as [O_Checked],
				0 as [INPUT_MODE],
				99 as [ID_TERMINAL],
				I1.ITEMCODE,
				'' as [REZ_1],
				'' as [REZ_2],
				null as [WEIGHT],
				null as [Expirace]
				FROM CZMST_I1 as I1
			left join CZMST_I3 as I3 on I3.ITEMNMBR = I1.ITEMNMBR AND I3.CountEntries = I1.CountEntries
			left join CZMST_I2 as I2 on I2.ITEMNMBR = I1.ITEMNMBR AND I2.CountEntries = I1.CountEntries
			WHERE I1.CZ_SerNum_Track = 2
			AND I2.SERLNMBR is null
			AND I1.CountEntries = @CountEntries

			/*************************/
			--Varianta kdy je položka vedena na množství, ale šarže neni vyplnen			
			/************************/

			INSERT INTO CZMST_I4 (
				[CountEntries]
				,[CE_Orig]
				,[ITEMNMBR]
				,[CZ_CarKod]
				,[LOCNCODE]
				,[SKL_ID]
				,[VNDITNUM]
				,[MJ]
				,[QUANTITY]
				,[QUANTITYMJ]
				,[QTYPACK]
				,[SERLNMBR]
				,[DATEDONE]
				,[TIMEDONE]
				,[USERID]
				,[GUID]
				,[O_Checked]
				,[INPUT_MODE]
				,[ID_TERMINAL]
				,[ITEMCODE]
				,[REZ_1]
				,[REZ_2]
				,[WEIGHT]
				,[Expirace])
			SELECT 
				I1.CountEntries,
				null as CE_Orig,
				I1.ITEMNMBR,
				I1.CZ_CarKod,
				@LOCNCODE_FAKE as [LOCNCODE],
				I1.SKL_ID,
				I3.VNDITNUM,
				I3.MJ,
				case I3.QTYPACK when 0 then I1.QUANTITY else I3.QTYPACK * I1.QUANTITY END as [QUANTITY],
				I1.QUANTITY as QUANTITYMJ,
				I3.QTYPACK,
				'' as [SERLNMBR],
				convert(varchar, getdate(), 112) as [DATEDONE],
				replace(Convert (varchar(8),GetDate(), 108),':','') as [TIMEDONE],
				0 as [USERID],
				NEWID() as [GUID],
				0 as [O_Checked],
				0 as [INPUT_MODE],
				99 as [ID_TERMINAL],
				I1.ITEMCODE,
				'' as [REZ_1],
				'' as [REZ_2],
				null as [WEIGHT],
				null as [Expirace]
				FROM CZMST_I1 as I1
			left join CZMST_I3 as I3 on I3.ITEMNMBR = I1.ITEMNMBR AND I3.CountEntries = I1.CountEntries
			WHERE I1.CZ_SerNum_Track = 0
			AND I1.CountEntries = @CountEntries

			/*************************/
			--Varianta kdy je položka vedena na šarže, a ma šaržu
			/************************/


						INSERT INTO CZMST_I4 (
				[CountEntries]
				,[CE_Orig]
				,[ITEMNMBR]
				,[CZ_CarKod]
				,[LOCNCODE]
				,[SKL_ID]
				,[VNDITNUM]
				,[MJ]
				,[QUANTITY]
				,[QUANTITYMJ]
				,[QTYPACK]
				,[SERLNMBR]
				,[DATEDONE]
				,[TIMEDONE]
				,[USERID]
				,[GUID]
				,[O_Checked]
				,[INPUT_MODE]
				,[ID_TERMINAL]
				,[ITEMCODE]
				,[REZ_1]
				,[REZ_2]
				,[WEIGHT]
				,[Expirace])
			SELECT 
				I1.CountEntries,
				null as CE_Orig,
				I1.ITEMNMBR,
				I1.CZ_CarKod,
				@LOCNCODE_FAKE as [LOCNCODE],
				I1.SKL_ID,
				I3.VNDITNUM,
				I3.MJ,
				ISNULL(case I3.QTYPACK when 0 then SUM(I2.QTY) else I3.QTYPACK * SUM(I2.QTY) END,0) as [QUANTITY],
				SUM(ISNULL(I2.QTY,0)) as QUANTITYMJ,
				I3.QTYPACK,
				ISNULL(I2.SERLNMBR,''),
				convert(varchar, getdate(), 112) as [DATEDONE],
				replace(Convert (varchar(8),GetDate(), 108),':','') as [TIMEDONE],
				0 as [USERID],
				NEWID() as [GUID],
				0 as [O_Checked],
				0 as [INPUT_MODE],
				99 as [ID_TERMINAL],
				I1.ITEMCODE,
				'' as [REZ_1],
				'' as [REZ_2],
				null as [WEIGHT],
				null as [Expirace]
			FROM CZMST_I1 as I1
			left join CZMST_I3 as I3 on I3.ITEMNMBR = I1.ITEMNMBR AND I3.CountEntries = I1.CountEntries
			left join CZMST_I2 as I2 on I2.ITEMNMBR = I1.ITEMNMBR AND I2.CountEntries = I1.CountEntries
			left join (SELECT SUM(I2.QTY) as QTY_SUM, I1.ITEMNMBR, I1.CountEntries FROM CZMST_I1 as I1
			left join CZMST_I2 as I2 on I2.ITEMNMBR = I1.ITEMNMBR AND I2.CountEntries = I1.CountEntries
			group by I1.ITEMNMBR, I1.CountEntries
			) as I_SUM ON I_SUM.ITEMNMBR = I1.ITEMNMBR AND I_SUM.CountEntries = I1.CountEntries
			where I1.CZ_SerNum_Track = 2 AND I2.SERLNMBR is not null AND I1.CountEntries = @CountEntries
			Group by  I3.VNDITNUM, I1.ITEMDESC, I1.ITEMCODE, I1.CountEntries, I1.ITEMNMBR, I1.CZ_CarKod, I1.SKL_ID, I3.MJ, I3.QTYPACK, I1.CZ_SerNum_Track, I2.SERLNMBR,  I1.QUANTITY, I_SUM.QTY_SUM
			order by I3.VNDITNUM

			/***************************/
			/*** Experiment s prayzdnim stanem a fake sarži***/

			--			INSERT INTO CZMST_I4 (
			--	[CountEntries]
			--	,[CE_Orig]
			--	,[ITEMNMBR]
			--	,[CZ_CarKod]
			--	,[LOCNCODE]
			--	,[SKL_ID]
			--	,[VNDITNUM]
			--	,[MJ]
			--	,[QUANTITY]
			--	,[QUANTITYMJ]
			--	,[QTYPACK]
			--	,[SERLNMBR]
			--	,[DATEDONE]
			--	,[TIMEDONE]
			--	,[USERID]
			--	,[GUID]
			--	,[O_Checked]
			--	,[INPUT_MODE]
			--	,[ID_TERMINAL]
			--	,[ITEMCODE]
			--	,[REZ_1]
			--	,[REZ_2]
			--	,[WEIGHT]
			--	,[Expirace])
			--SELECT 
			--	2 as CountEntries,
			--	null as CE_Orig,
			--	I1.ITEMNMBR,
			--	I1.CZ_CarKod,
			--	'99' as [LOCNCODE],
			--	I1.SKL_ID,
			--	I3.VNDITNUM,
			--	I3.MJ,
			--	0 as [QUANTITY],
			--	0 as [QUANTITYMJ],
			--	0 as [QTYPACK],
			--	'66' as [SERLNMBR],
			--	convert(varchar, getdate(), 112) as [DATEDONE],
			--	replace(Convert (varchar(8),GetDate(), 108),':','') as [TIMEDONE],
			--	0 as [USERID],
			--	NEWID() as [GUID],
			--	0 as [O_Checked],
			--	0 as [INPUT_MODE],
			--	99 as [ID_TERMINAL],
			--	I1.ITEMCODE,
			--	'' as [REZ_1],
			--	'' as [REZ_2],
			--	null as [WEIGHT],
			--	null as [Expirace]
			--	FROM CZMST_I1 as I1
			--left join CZMST_I3 as I3 on I3.ITEMNMBR = I1.ITEMNMBR AND I3.CountEntries = I1.CountEntries
			--left join CZMST_I2 as I2 on I2.ITEMNMBR = I1.ITEMNMBR AND I2.CountEntries = I1.CountEntries
			--WHERE I1.CZ_SerNum_Track = 2
			--AND I2.SERLNMBR is not null
			--AND I1.CountEntries = 1
			--Group by I1.CountEntries, I1.ITEMNMBR, I1.CZ_CarKod, I1.SKL_ID, I3.VNDITNUM, I3.MJ, I1.ITEMDESC, I1.ITEMCODE


SELECT Count(*) FROM CZMST_I4 where CountEntries = @CountEntries

END


/******************************************************************************************/