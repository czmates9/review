create table [Users]
(
    [ID] INTEGER CONSTRAINT Users_ID_PK PRIMARY KEY,
    [Login] TEXT not null,
    [Pwd] TEXT,
    [Hash] TEXT,
    [EAN] TEXT,
    [FIRSTNAME] TEXT,
    [SECONDNAME] TEXT
);
create unique index UQ__Users__0000000000000010 on [Users] ([Login]);

