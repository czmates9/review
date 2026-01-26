create table [CZMST_SSCC_PARAMETERS]
(
	[ID_SSCC] INTEGER not null,
	[DESC_SSCC] TEXT null,
	[LV] NUMERIC null,
	[GCP] NUMERIC null,
	[GCP_count] NUMERIC null
);

create table [CZMST_SSCC_SEQUENCE]
(
	[seq_id] INTEGER not null,
	[sequence_count] INTEGER not null
);