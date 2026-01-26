create table [CZMST_EventsUser]
(
    -- k cemu id identity, kdyz je primary key eguid>??? [id] int identity not null,
    [eguid] uniqueidentifier CONSTRAINT CZMST_EventsUser_eguid_PK PRIMARY KEY,
    [eid] TEXT not null,
    [etype] TEXT not null,
    [etime] TEXT not null,
    [termid] INTEGER not null,
    [userid] INTEGER not null,
    [loginid] TEXT,
    [machineid] TEXT,
    [modul] TEXT,
    [countentries] INTEGER,
    [docnmbr] TEXT,
    [itemnmbr] TEXT,
    [REZ1] TEXT,
    [REZ2] TEXT
);