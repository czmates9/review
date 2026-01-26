/****** Object:  Table [dbo].[CZMST092]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CZMST092](
	[doc_id] [nvarchar](12) NOT NULL,
	[doc_id2] [nvarchar](12) NOT NULL DEFAULT (''),
	[doc_desc] [nvarchar](31) NULL,
	[doc_typ] [nvarchar](3) NULL,
	[doc_carcode] [nvarchar](21) NULL,
	[DEX_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[LOCNCODE] [nvarchar](11) NOT NULL DEFAULT (''),
	[cfg_odb] [tinyint] NOT NULL DEFAULT (0),
	[cfg_str] [tinyint] NOT NULL DEFAULT (0),
	[cfg_prac] [tinyint] NOT NULL DEFAULT (0),
	[cfg_mn2sn] [tinyint] NOT NULL DEFAULT (0),
	[cfg_disp] [tinyint] NOT NULL DEFAULT (0),
	[cfg_disp_dest] [tinyint] NOT NULL DEFAULT (0),
	[cfg_palety] [tinyint] NOT NULL DEFAULT (0),
	[cfg_paleta_id] [tinyint] NOT NULL DEFAULT (0),
	[cfg_zakazka_id] [tinyint] NOT NULL DEFAULT (0),
	[cfg_mena_id] [tinyint] NULL,
	[cfg_tisk] [tinyint] NOT NULL DEFAULT (0),
	[cfg_prevod_sklad] [tinyint] NOT NULL DEFAULT (0),
	[cfg_tisk_soupis] [tinyint] NULL,
	[SKL_ID] [nvarchar](20) NULL,
	[cfg_lokace] [tinyint] NULL,
	[cfg_lokace_ciselnik] [tinyint] NULL,
	[cfg_lokace_dest] [tinyint] NULL,
	[cfg_onl_dop_pal] [tinyint] NULL,
	[cfg_onl_over_lokace] [tinyint] NULL,
	[cfg_onl_over_lokace_dest] [tinyint] NULL,
	[cfg_mnozstvi_ze_zbozi] [tinyint] NULL,
	[cfg_predvyplnit_mnozstvi] [tinyint] NULL,
	[cfg_skl_id_dest] [tinyint] NULL,
	[predvyplnit_skl_id_dest] [nvarchar](20) NULL,
	[cfg_lok_mech] [tinyint] NULL,
	[cfg_lok_mech_pohyb_type] [nvarchar](1) NULL,
	[cfg_skl_id_dest_prevzit] [tinyint] NULL,
	[cfg_lokace_dest_ciselnik] [tinyint] NULL,
	[predvyplnit_locncodedest] [nvarchar](11) NULL,
	[cfg_sklady] [tinyint] NULL,
	[cfg_onl_dop_lokace_dest] [tinyint] NULL,
	[cfg_generovat_sn] [tinyint] NULL,
	[cfg_parsovat_ck] [tinyint] NULL,
	[cfg_sn_na_davku] [tinyint] NULL,
	[cfg_lok_mech_online_pohyby] [tinyint] NULL,
	[cfg_onl_palety_generovat] [tinyint] NULL,
	[cfg_tisk_palety] [tinyint] NULL,
	[cfg_sklady_zmena] [tinyint] NULL,
	[cfg_delka_SN] [int] NULL,
	[cfg_Navrh] [tinyint] NULL,
	[cfg_FIFO_FEFO_check] [tinyint] NULL DEFAULT(0),
	[cfg_sarze_ONOFF] [tinyint] NULL DEFAULT (1),
	[cfg_sn_ONOFF] [tinyint] NULL DEFAULT (1),
	[cfg_expirace_ONOFF] [tinyint] NULL DEFAULT (1),
	[cfg_AttributeToSN_ONOFF] [tinyint] NULL DEFAULT (0)
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/