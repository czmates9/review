/*
-- Stara verze
select * from Production where 
SOUBEHGUID not IN (
    select distinct SOUBEHGUID from Production
    where
    SOUBEHGUID is not null and TIMESTOP is not null	-- doplnen SOUBEHGUID kvuli zakazkam, ktere puvodne nemeli soubeh
    )
*/
-- nova verze 20150713
Select p.*
From Production p with (nolock) 
Where not exists ( 
	select SOUBEHGUID 
	from Production with (nolock) 
	where 
	(TIMESTOP is not null or TIMEPREPSTOP is not null) 
	and 
	SOUBEHGUID=p.SOUBEHGUID 
	) 
and SOUBEHGUID is not null 
order by id


-- index
CREATE NONCLUSTERED INDEX [IX_timestop_timeprepstop] ON [dbo].[Production]
(
	[SOUBEHGUID] ASC
)
INCLUDE ( 	[TIMESTOP],
	[TIMEPREPSTOP]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO