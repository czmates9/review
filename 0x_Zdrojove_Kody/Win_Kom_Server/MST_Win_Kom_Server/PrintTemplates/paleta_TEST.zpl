^XA
^MMT
^PW1183
^LL1654
^LS0
^CI31

^SLT



^FX -- LOGO AGRO (nahore) --
^FO39,59^GFA,1581,6080,64,:Z64:eJzF2E2OpDYUAGAjpDiLUfsCUXyFLGcxknOUzA16OYtIMGopfYwcJW7NIscYj3IBpGy8QLz4/fivgJpIQ2tQd0EBn8t+PGyMgrrMql1GCN13szWHlvRJH2rnLfABByt9QmQEtTwHqSwNVGD9kZH9JIILxHID/yaVJ5sblezveM0Q/SrVyg0wtAlcz+r1LPUkb3hNDeM6lwBRUeM1fgiA7bBB2d5brjjkAJRA0OZG7UP/D3kX1PTceQe5lXTeWP1AR0zjZwV+gD/K0ahKxWkVOZx8XamojeqH/okbk/46P7AfsjeylshseJmu8ivmpoYfWk/txVYBx8GWRKDNzkdlUxHix6a9ENgDZVX2HFmoflFTTHs7b4885ASFdWh8qmVMew/8Ujy8op/xVgfxhtvpOj8PjadI5HwantNeQ4F++LqfpYKdx1zy+sZzvGJ246034sdnwNjPxVuo903xvvH6ap8uP8zp+5sDL84bWUsDG68pSGrvt2RD+l8m9nGqfljTaWFijyche1Oyc6XeEnejT4d8+l8ce4vHf8edhr1nv9341HZaL4Z9qD6qt+j1RV6nE13vNXlfvOMYeE7foH602Dj2KSKNn7JfhlWdeE+1UINHv536pfMY+MAnzFRN9qnrcezfNd5IZ4nXEP1UPdBtRMH49Qq/JhIxX8inXPrM3n/NBzrhyG/3vYzrVEGTPJx7zX6eeF/nI/uY/Vv5wc88uhSftj9C8QN7K8P56/n51OfhGU94MGFgH7P/k/1HvP/RT7S97v184PXe4/1+6H3xmD9n/tOpXy7x2HXvfGrzk/i0bI0f2cnjkXqwxX9QvN+yHxs/QsnpE2+PvS3d8I0fuZsVD/f98nr+Z/FGvOv8Un3sPT/Edf6RPQ4Nc+/93qvep/HvjqfuXB64s584gX+yNLYlvzzy7r3fXt+n549zvx55W7zufRSvi4/Fz9WbzqfnJ/bQ+XDutfiJfNz5jbxr/NT58WI/Zb+IH4tfj7zqfBpElpyW0PT3N55/lx/jubh3Zx7u+jT0ut67xe89jtt/cf6sMvBd7gPfiux14+3OryrPY1/CzodjvznxQ+OH4n859DReO/onP3QelH5Zio+q8ab6uXiahmE+RfHePmEXhf3HK3psQzrJyjTQsucr9AQKip/2Pp38L8/yiofacfLgxB5Tr3pbPVTv73tH/sver9nLdCbIfSN+vcA/It17U735qqf3IRN5d+iD+Ln3kTwoGoCPfCzeVU9FjQd+brxMv7E7b7wqvryO+Fb/Wx4dp5c5N67xPFNbOK78/sjeenmAE8/PnvL6QfE07MR79jh/DL0Po8Qre5Ovl6S4zuEk7zBD0Q/8KEQVB572r6MMALnf1sWrC/x7en2y9xPINLbxlH78+kjncLznJ+s0/v+dvc4XLFY/5HjxNdq0pI/6AnR7pyLQj8Vbri/3e5vie1zlBCC/ZD+rtfqFPQ/qil8BbDJr5sWJD1d5rz6k470fOL7F5wkhJwC9V1DVY/nPqvXy+lWmNnREwodF041BX/LOdF1Vt+ib18Eulk1L5fn7/n8u39tDXb6L/9blPz1oWuo=:2448


^FX -- 2D / DATAMATRIX KOD SSCC palety --
^FT750,500^BXN,28,200,0,0,1,_,1
^FH\^FD$SSCC$^FS

^FX -- NAZEV PRODUKTU NA PALETE - automaticky rozdeli text na dva radky po cca 30 znacich / prikaz FB (field box) --
^FT39,315
^A@N,62,61,E:ARI001.TTF
^FB700,2,0,L,0^FD$ITEMNMBR$^FS

^FX -- BATCH/LOT dle orig. normy GS1, ale dle pozadavku AGRO/p.Slavika se tiskne kód vzniklý spojením hodnoty $UserID$ (=číslo pracovníka) a aktuálního datumu ve formátu YYMMDD --
^FT39,375
^A@N,45,46,E:ARI001.TTF
^FX -- striskaFC%striskaFD$UserID$/%y%m%dstriskaFS
^FC%^FD$UserID$/%y%m%d^FS

^FX -- POPISEK 1 --
^FT39,700
^A@N,62,61,E:ARI001.TTF
^FB700,2,0,L,0^FD$ITEMDESC_1$^FS


^FX -- PRVNI horizontalni cara --
^FO9,750^GB1220,0,8^FS

^FX -- POPISEK 2 --
^FT39,950
^A@N,62,61,E:ARI001.TTF
^FB700,2,0,L,0^FD$ITEMDESC_2$^FS

^FX -- DRUHA horizotalni cara --
^FO9,1050^GB1160,0,8^FS

^FX -- POPISEK 2 --
^FT39,1250
^A@N,62,61,E:ARI001.TTF
^FB700,2,0,L,0^FD$SOPDESC$^FS



^PQ$PocetVytisku$,0,1,Y
^XZ