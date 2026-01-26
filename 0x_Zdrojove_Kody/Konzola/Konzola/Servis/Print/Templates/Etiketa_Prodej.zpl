^XA
^MMC
^PW600
^LL304
^LS0
^CI31

^FT250,40
^A0N,20,20
^FDREF : 
^FS


^FT250,90
^A0N,20,20
^FB300,2,,C,
^FD$ITEMCODE$
^FS

^FT250,130
^A0N,20,20
^FDLOT : 
^FS

^FT300,160
^A0N,25,25
^FD$SERLTNUM$
^FS

^FT250,190
^A0N,20,20
^FDUse by date :
^FS

^FT300,220
^A0N,25,25
^FD$EXPIRACE_RRMMDD$
^FS

^FT250,250
^A0N,20,20
^FDGTIN :
^FS

^FT300,280
^A0N,25,25
^FD$VNDITNUM$
^FS

^FX jedna se o graficky 2D KOD ktery spracovava FASK server
#QR,20,58,200,200,0,$GS1_KOD$#

^PQ$PocetVytisku$,0,1,Y
^XZ