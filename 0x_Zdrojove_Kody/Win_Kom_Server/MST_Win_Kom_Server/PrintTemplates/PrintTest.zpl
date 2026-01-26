^FX*************************************
^FX************HEAD**********************
^FX*************************************

^XA
^XB
^MMD
^POI
^PW799
^LL0799
^CI31
^FWN,0


^FO16,24^GB767,759,8^FS
^FO26,195^GB747,0,8^FS
^FO16,463^GB759,0,8^FS

^FT83,700^A0N,32,28^FD00001234560000000889^FS
^BY2,3,126^FT51,664^BCN,,N,N,N,N
^FD>;00001234560000000889^FS

^FT37,234^A0N,23,24^FDČíslo balící jednotky:^FS
^FT37,340^A0N,23,24^FDPoložek v jednotce:^FS
^FT429,345^A0N,23,24^FDRozměr mm (šířka/výška/hloubka):^FS
^FT431,416^A0N,23,24^FDBrutto váha (kg):^FS

^FT37,298^A0N,56,55^FD00001234560000000889^FS
^FT42,380^A0N,28,28^FD40.00 pcs^FS

^FT431,380^A0N,28,28^FD10 x 20 x 30^FS 


^FT431,453^A0N,28,28^FD40.00^FS

^FT223,71^A0N,25,24^FDi-tec Technologies s.r.o., Kalvodova 2, 709 00 Ostrava^FS
^FT50,125^A0N,56,55^FDBALIK^FS
^FT50,155^A0N,25,24^FDČíslo Objednávky: 123456789,123456789,123456789^FS


^PQ1,0,1,Y
^XZ


^FXHlavicka polozek
^XA
^POI
^XB
^CI31
^LL0050
^FO25,25^A0N,25,24^FDPol.č.^FS
^FO125,25^A0N,25,24^FDNázev^FS
^FO650,25,1^A0N,25,24^FDMnožství^FS
^FO700,25,1^A0N,25,24^FDMJ^FS
^XZ



^FXZacatek polozek
^XA
^DFR:ROW.ZPL
^POI
^XB
^CI31
^LL0125
^FO25,0^A0N,25,24^FN1^FS
^FO125,0^A0N,25,24^TBN,300,50^FN2^FS
^FO650,0,1^A0N,25,24^FN3^FS
^FO700,0,1^A0N,25,24^FN4^FS
^FO790,0,1^A0N,25,24^FN5^FS
^FO550,25,0^BEN,25^FN6^FS

^FO125,58^A0N,25,24^FDKód :^FS
^FO225,58^A0N,25,24^TBN,200,25^FN7^FS

^XZ

^FX*************************************
^FX************ROW 1 **********************
^FX*************************************

^XA
^XFR:ROW.ZPL
^PN0
^FN1^FD16363^FS
^FN2^FD1 Dicota BacPac Traveler 13" - 14,1" / EOL!^FS
^FN3^FD3.00^FS
^FN4^FDks^FS
^FN6^FD7332752000186^FS
^FN7^FD30033^FS
^XZ

^XA
IF TRUE THEN
^DFR:SN.ZPL
^POI
^XB
^CI31
^LL200
^FO25,0^A0N,25,24^FDVýr.č.:^FS
^FO125,0^A0N,25,24^TBN,550,200^FN1^FS
END IF
^XZ

^XA
IF TRUE THEN
^XFR:SN.ZPL
^PN0
^FN1^FD1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234^FS
END IF
^XZ


^FX*************************************
^FX************ROW 2 **********************
^FX*************************************

^XA
^XFR:ROW.ZPL
^PN0
^FN1^FD16363^FS
^FN2^FD2 Dicota BacPac Traveler 13" - 14,1" / EOL!^FS
^FN3^FD3.00^FS
^FN4^FDks^FS
^FN6^FD7332752000186^FS
^FN7^FD30033^FS
^XZ

^XA
IF FALSE THEN
^DFR:SN.ZPL
^POI
^XB
^CI31
^LL200
^FO25,0^A0N,25,24^FDVýr.č.:^FS
^FO125,0^A0N,25,24^TBN,600,200^FN1^FS
ELSE
^DFR:SN.ZPL
^POI
^XB
^CI31
^LL1
END IF
^XZ

^XA
IF FALSE THEN
^XFR:SN.ZPL
^PN0
^FN1^FD^FS
ELSE

END IF
^XZ


^FX*************************************
^FX************ROW 3 **********************
^FX*************************************

^XA
^XFR:ROW.ZPL
^PN0
^FN1^FD16363^FS
^FN2^FD3 Dicota BacPac Traveler 13" - 14,1" / EOL!^FS
^FN3^FD3.00^FS
^FN4^FDks^FS
^FN6^FD7332752000186^FS
^FN7^FD30033^FS
^XZ

^XA
IF TRUE THEN
^DFR:SN.ZPL
^POI
^XB
^CI31
^LL200
^FO25,0^A0N,25,24^FDVýr.č.:^FS
^FO125,0^A0N,25,24^TBN,550,200^FN1^FS
END IF
^XZ

^XA
IF TRUE THEN
^XFR:SN.ZPL
^PN0
^FN1^FD1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234,1234^FS
END IF
^XZ



^FX*************************************
^FX************FOOT **********************
^FX*************************************

^XA
^PN1
^MMC
^POI
^LL0030
^FO0,8
^GB800,8,8
^XZ
