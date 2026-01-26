
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
