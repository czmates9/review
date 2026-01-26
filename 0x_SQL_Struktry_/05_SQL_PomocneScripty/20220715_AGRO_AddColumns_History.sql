

ALTER TABLE FASK_Events_HISTORY
ADD BarcodeT tinyint not null default(1);

GO

ALTER TABLE FASK_Events_HISTORY
ADD REZ_1 nvarchar(100) null;

GO

ALTER TABLE FASK_Events_HISTORY
ADD REZ_2 nvarchar(100) null;

GO

ALTER TABLE FASK_Events_HISTORY
ADD REZ_3 nvarchar(100) null;

GO

ALTER TABLE FASK_Events_HISTORY
ADD REZ_4 nvarchar(100) null;

GO

ALTER TABLE FASK_Events_HISTORY
ADD REZ_5 nvarchar(100) null;

GO