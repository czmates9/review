

ALTER TABLE CZMST092
ADD cfg_AttributeToSN_ONOFF tinyint default(0) NULL;


ALTER TABLE FASK_Events
ADD QTYPACK numeric(19,5) not null DEFAULT((0));

GO

ALTER TABLE FASK_Events
ADD PackType nvarchar(50) null;

GO

ALTER TABLE FASK_Events
ADD WEIGHT numeric(19,5) null;

GO


ALTER TABLE FASK_Events
ADD VPH nvarchar(30) null;

GO

ALTER TABLE FASK_Events
ADD VPPol int null;

GO

ALTER TABLE FASK_Events
ADD EAN_IS nvarchar(31) null;

GO

ALTER TABLE FASK_Events
ADD IS_ID nvarchar(40) null;

GO

ALTER TABLE FASK_Events
ADD NMBRPAL nvarchar(50) null;

GO

ALTER TABLE FASK_Events
ADD status int null;

GO


ALTER TABLE CZPRO_VPP
ADD BarcodeT tinyint default(1) not NULL;

GO

ALTER TABLE CZMST_PI
ADD AttributeToSN nvarchar(50) NULL;

GO 

ALTER TABLE CZMST_PI_HISTORY
ADD AttributeToSN nvarchar(50) NULL;

GO

ALTER TABLE CZMST_DI
ADD AttributeToSN nvarchar(50) NULL;

GO 

ALTER TABLE CZMST_DI_HISTORY
ADD AttributeToSN nvarchar(50) NULL;

GO

ALTER TABLE FASK_Logins
ADD [RFID] [nvarchar](20) NULL

GO