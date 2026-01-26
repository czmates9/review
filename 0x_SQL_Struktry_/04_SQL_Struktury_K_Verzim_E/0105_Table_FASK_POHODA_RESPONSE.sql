/****** Object:  Table [dbo].[FASK_POHODA_RESPONSE]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FASK_POHODA_RESPONSE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Resp_ID] [nvarchar](50) NULL,
	[Resp_Name] [nvarchar](255) NULL,
	[Resp_DateTime] [datetime] NULL,
	[Resp_ICO] [nvarchar](50) NULL,
	[Resp_Key] [nvarchar](50) NULL,
	[Resp_Note] [nvarchar](255) NULL,
	[Resp_State] [nvarchar](20) NULL,
	[Item_ID] [nvarchar](50) NULL,
	[Item_Note] [nvarchar](255) NULL,
	[Item_State] [nvarchar](20) NULL,
	[Item_Type] [nvarchar](50) NULL,
	[Detail_State] [nvarchar](20) NULL,
	[Detail_Type_Errno] [nvarchar](20) NULL,
	[Detail_Type_Note] [nvarchar](255) NULL,
	[Detail_Type_State] [nvarchar](20) NULL,
	[Detail_Type_VProcesed] [nvarchar](255) NULL,
	[Detail_Type_VRequested] [nvarchar](255) NULL,
	[Detail_Type_XPath] [nvarchar](255) NULL,
	[Produced_Detail_ID] [nvarchar](50) NULL,
	[Produced_Detail_ActionType] [nvarchar](50) NULL,
	[Produced_Detail_Code] [nvarchar](50) NULL,
	[Produced_Detail_Number] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/**************************************************************************************/