
ALTER TABLE Production
ADD NMBRPAL nvarchar(50) null;

GO

ALTER TABLE Production
ADD TYPEPAL nvarchar(10) null;

GO

ALTER TABLE Production
ADD PackType nvarchar(50) null;

GO


ALTER TABLE Production
ADD status int null;

GO

ALTER TABLE Production
ADD WEIGHT numeric(19,5) null;

GO

ALTER TABLE Production
ADD STORNOGUID uniqueidentifier null;

GO
