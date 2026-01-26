
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
ADD productionGuid uniqueidentifier null;

GO


ALTER TABLE CZPRO_VPP
ADD BarcodeT [tinyint] NOT NULL DEFAULT ((1));

GO


