
create PROCEDURE FASK_procGetPolozkaFloat_SQL
    @itemnmbr NVARCHAR(50),
    @location NVARCHAR(50)
AS
BEGIN
    DECLARE @vysledek FLOAT = 1.5;
    SELECT [QTYPACK] 
	from [FASK_ZASOBY]
	where @itemnmbr = [ITEMNMBR]                            

END;
