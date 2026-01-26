/************************************************************************************/

--Jedné se o mapovací mechanizmus, který projde celu tabulku CZMST_SkladLokace_Stav 
-- a najde položky ktere jsou na učitem skladu, ale ID položky neodpovida ID Skladu
-- Sklad je hlavny, sklad a lokace je rouhodujici

--nasledne dotahne na zaklade podminek spravne ID karty pro žadaný sklad + nazev položky a provede update v CZMST_SkladLokace_Stav

-- Veci na ktere dat POZOR
-- jsou zde 2 zakomentovane update, pustit až ked sisi jisty co chceš udelat!!!

-- Nazev POHODA db v tady je StwPh_28280725_2020, jedná se o TierraVerde


/******************************************************************************************/
DECLARE @ITEMNMBR_Stav int;
DECLARE @SKL_ID_Stav int;
DECLARE @ITEMDESC_Stav nvarchar(200)
DECLARE @QTYSHPPD numeric(19,5)
DECLARE @SERLTNUM nvarchar(50)
DECLARE @LOCNCODE nvarchar(11)


DECLARE @List_ORD TABLE
(
Poznamka nvarchar(500),
itemnmbr_Stav int,
skl_id_Stav int,
itemdesc_Stav nvarchar(200),
skl_id_map int null default(null),
itemnmbr_map int null default(null),
itemdesc_map nvarchar(200) null default(null),
QTYSHPPD numeric(19,5),
SERLTNUM nvarchar(50),
SKL_ID nvarchar(20),
LOCNCODE nvarchar(11)
)


DECLARE curPol CURSOR FOR
SELECT 
ITEMNMBR, 
SKL_ID,
ITEMDESC,
QTYSHPPD,
SERLTNUM,
LOCNCODE
FROM
CZMST_SkladLokace_Stav

OPEN curPol
WHILE (1=1) 
	BEGIN
	
	FETCH NEXT FROM curPol INTO @ITEMNMBR_Stav, @SKL_ID_Stav, @ITEMDESC_Stav, @QTYSHPPD, @SERLTNUM, @LOCNCODE
	IF @@FETCH_STATUS <> 0
	BEGIN
		BREAK
	END

	declare @cnt int;

	SELECT @cnt = count(*) from FASK_ZASOBY where ITEMNMBR = @ITEMNMBR_Stav AND SKL_ID = @SKL_ID_Stav

	IF @cnt = 0
		BEGIN
			declare @cnt2 int

			SELECT @cnt2 = count(*) from FASK_ZASOBY where ITEMNMBR = @ITEMNMBR_Stav

			IF @cnt2 = 0
				BEGIN

			declare @cnt3 int
			SELECT @cnt3 = count(*) from StwPh_28280725_2020.dbo.SKz where ID = @ITEMNMBR_Stav

			IF @cnt3 = 0
				BEGIN
					insert into @List_ORD(Poznamka, itemnmbr_Stav, skl_id_Stav, itemdesc_Stav , QTYSHPPD, SERLTNUM, LOCNCODE) values ('!!!Nenalezena!!', @ITEMNMBR_Stav, @SKL_ID_Stav, @ITEMDESC_Stav, @QTYSHPPD, @SERLTNUM, @LOCNCODE)
				END
			ELSE IF @cnt3 = 1
				BEGIN
					--insert into @List_ORD(Poznamka,itemnmbr_Stav, skl_id_Stav, itemdesc_Stav ) values ('Nalezen v IS POHODA dle ID!!', @ITEMNMBR_Stav, @SKL_ID_Stav, @ITEMDESC_Stav)

					declare @SKL_ZdrojP int

					SELECT @SKL_ZdrojP= StwPh_28280725_2020.dbo.SKz.RefSklad from StwPh_28280725_2020.dbo.SKz where ID = @ITEMNMBR_Stav

					declare @ITEMNMBR_mapP int
					declare @ITEMDESC_mapP nvarchar(100)

					SELECT @ITEMDESC_mapP = ITEMDESC, @ITEMNMBR_mapP = ITEMNMBR FROM FASK_Get_POHODA_LokMechMapingID( @ITEMNMBR_Stav,@SKL_ZdrojP, @SKL_ID_Stav)  


					insert into @List_ORD(
					Poznamka,
					itemnmbr_Stav, 
					skl_id_Stav, 
					itemdesc_Stav,
					skl_id_map,
					itemnmbr_map,
					itemdesc_map, 
					QTYSHPPD, 
					SERLTNUM, 
					LOCNCODE
					)
					values (
					'Namapovano_POHODA' ,
					@ITEMNMBR_Stav , 
					@SKL_ID_Stav , 
					@ITEMDESC_Stav ,
					@SKL_ZdrojP ,
					@ITEMNMBR_mapP, 
					@ITEMDESC_mapP , 
					@QTYSHPPD, 
					@SERLTNUM, 
					@LOCNCODE)

				 --UPDATE [dbo].[CZMST_SkladLokace_Stav]
				 --SET [ITEMNMBR] = @ITEMNMBR_map
			     --,[ITEMDESC] = @ITEMDESC_map
				 --WHERE 
				 --SERLTNUM = @SERLTNUM AND SKL_ID = @SKL_ID_Stav AND LOCNCODE = @LOCNCODE AND QTYSHPPD = @QTYSHPPD


				END
			ELSE
				BEGIN
					insert into @List_ORD(Poznamka,itemnmbr_Stav, skl_id_Stav , itemdesc_Stav, QTYSHPPD, SERLTNUM, LOCNCODE) values ('Nesmí nastat 3', @ITEMNMBR_Stav, @SKL_ID_Stav, @ITEMDESC_Stav, @QTYSHPPD, @SERLTNUM, @LOCNCODE)
				END

				END
			ELSE IF @cnt2 = 1
				BEGIN
					--insert into @List_ORD(Poznamka,itemnmbr_Stav, skl_id_Stav , itemdesc_Stav) values ('Je jeden ale nespravne', @ITEMNMBR, @SKL_ID, @ITEMDESC)

					declare @SKL_Zdroj int

					SELECT @SKL_Zdroj= SKL_ID from FASK_ZASOBY where ITEMNMBR = @ITEMNMBR_Stav

					declare @ITEMNMBR_map int
					declare @ITEMDESC_map nvarchar(100)

					SELECT @ITEMDESC_map = ITEMDESC, @ITEMNMBR_map = ITEMNMBR FROM FASK_Get_POHODA_LokMechMapingID( @ITEMNMBR_Stav,@SKL_Zdroj, @SKL_ID_Stav)  

					insert into @List_ORD(
					Poznamka,
					itemnmbr_Stav, 
					skl_id_Stav, 
					itemdesc_Stav,
					skl_id_map,
					itemnmbr_map,
					itemdesc_map, 
					QTYSHPPD, 
					SERLTNUM, 
					LOCNCODE
					)
					values ( 
					'Namapovano_FASK',
					@ITEMNMBR_Stav , 
					@SKL_ID_Stav , 
					@ITEMDESC_Stav ,
					@SKL_Zdroj ,
					@ITEMNMBR_map , 
					@ITEMDESC_map, 
					@QTYSHPPD, 
					@SERLTNUM, 
					@LOCNCODE
					)
					

					--UPDATE [dbo].[CZMST_SkladLokace_Stav]
				 --  SET [ITEMNMBR] = @ITEMNMBR_map
					--  ,[ITEMDESC] = @ITEMDESC_map
				 --WHERE 
				 --SERLTNUM = @SERLTNUM AND SKL_ID = @SKL_ID_Stav AND LOCNCODE = @LOCNCODE AND QTYSHPPD = @QTYSHPPD



				END
			ELSE
				BEGIN
					insert into @List_ORD(Poznamka,itemnmbr_Stav, skl_id_Stav , itemdesc_Stav, QTYSHPPD, SERLTNUM, LOCNCODE) values ('Nesmí nastat 1', @ITEMNMBR_Stav, @SKL_ID_Stav, @ITEMDESC_Stav, @QTYSHPPD, @SERLTNUM, @LOCNCODE)
				END


		END
	ELSE IF @cnt = 1
		BEGIN
			insert into @List_ORD(Poznamka,itemnmbr_Stav, skl_id_Stav, itemdesc_Stav , QTYSHPPD, SERLTNUM, LOCNCODE) values ('OK', @ITEMNMBR_Stav, @SKL_ID_Stav, @ITEMDESC_Stav, @QTYSHPPD, @SERLTNUM, @LOCNCODE)
		END
	ELSE
		BEGIN
			insert into @List_ORD(Poznamka,itemnmbr_Stav, skl_id_Stav, itemdesc_Stav , QTYSHPPD, SERLTNUM, LOCNCODE) values ('Nesmí nastat 2', @ITEMNMBR_Stav, @SKL_ID_Stav, @ITEMDESC_Stav, @QTYSHPPD, @SERLTNUM, @LOCNCODE)
		END
END
	
CLOSE curPol
DEALLOCATE curPol


SELECT Poznamka, itemdesc_Stav, itemnmbr_Stav, skl_id_map, itemnmbr_map , skl_id_Stav, itemdesc_map , QTYSHPPD, SERLTNUM, LOCNCODE FROM @List_ORD
where Poznamka != 'OK'





