/****** Object:  Index [idx_fask_czmst_se_getvydejky]     ******/

CREATE NONCLUSTERED INDEX [idx_fask_czmst_se_getvydejky] ON [dbo].[CZMST_SE]
(
	[QTYPACK] ASC,
	[CZ_Doslo] ASC
)
INCLUDE ( 	[CountEntries],
	[SOPNUMBE],
	[ITEMTYPE],
	[SKL_ID],
	[QTYSHPPD]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


CREATE NONCLUSTERED INDEX [idx_fask_czmst_se_getvydejky2] ON [dbo].[CZMST_SE] 
(
	[CZ_Doslo]
)
INCLUDE (
	[CountEntries],
	[SOPNUMBE],
	[ITEMTYPE],
	[SKL_ID],
	[PRIORITY]
)
GO

/**************************************************************************************/