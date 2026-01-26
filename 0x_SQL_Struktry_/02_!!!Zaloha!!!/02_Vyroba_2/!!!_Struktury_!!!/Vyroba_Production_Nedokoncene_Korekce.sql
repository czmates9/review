select * from Production where 
CORRGUID not IN (
    select distinct CORRGUID from Production
    where
    CORRGUID is not null and TIMECORSTOP is not null	-- doplnen SOUBEHGUID kvuli zakazkam, ktere puvodne nemeli soubeh
    )