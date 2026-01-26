create table [CZMST_SSCC_PARAMETERS]
(
	[ID_SSCC] int not null,
	[DESC_SSCC] nvarchar(20) null,
	[LV] numeric(1, 0) null,
	[GCP] numeric(9, 0) null,
	[GCP_count] numeric(9, 0) null
);

create table [CZMST_SSCC_SEQUENCE]
(
	[seq_id] int not null,
	[sequence_count] int not null
);