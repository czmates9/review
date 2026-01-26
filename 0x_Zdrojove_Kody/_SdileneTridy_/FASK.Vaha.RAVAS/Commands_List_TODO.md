
# Commands List TODO #

## v0.1 03.08.2022 TaD ##

* **DONE** SZ\<CR> OK\<CR>/ERR\<CR> Set zero value
* **DONE** RZ\<CR> OK\<CR>/ERR\<CR> Reset zero value
* **DONE** SP\<value>\<CR>*1 OK\<CR>/ERR\<CR> Set preset tare value
* **DONE** RP\<CR> OK\<CR>/ERR\<CR> Reset preset tare
* **DONE** RT\<CR> OK\<CR>/ERR\<CR> Reset tare
* **DONE** ST\<CR> OK\<CR>/ERR\<CR> Set tare
* **DONE** SR\<CR> OK\<CR>/ERR\<CR> Set tare (also with a previous tare) *3
* **DONE** SG\<CR> G+0001.0\<CR> Send gross mode (continuously)
* **DONE** SN\<CR> N+0001.0\<CR> Send net mode (continuously)
* **DONE** SW\<CR> W+00010+000103805\<CR>*2 Send weights mode (continuously)
* SA\<CR> A;+000.0;+000.0\<CR> Send angle positions X and Y (continuously)
* SL\<CR> See chapter SL command Similar to SW but including errors
* **DONE** GP\<CR> P+0001.0\<CR> Get preset tare
* **DONE** GT\<CR> T+0001.0\<CR> Get tare
* **DONE** GG\<CR> G+0001.0\<CR> Get gross
* **DONE** GN\<CR> N+0001.0\<CR> Get net
* **DONE** GW\<CR> W+00010+000103805\<CR> Get net, gross, status and checksum
* GA\<CR> A;+000.0;+000.0\<CR> Get angle positions X and Y
* GE\<CR> See chapter GE command Read out of last 50 messages
* GI\<CR> See chapter GI command Read out of general info and parameters
* GS\<CR> See chapter GS command Read out of status and calibration
* GL\<CR> See chapter GL command Read out total log file
* RE\<CR> See chapter RE command Reset the ERRORs database (passcode required)
* MN\<CR> N+0001.0\<CR>/ERR\<CR>*4 Get net, wait for no motion
* MG\<CR> G+0001.0\<CR>/ERR\<CR>*4 Get gross, wait for no motion
* RS\<CR> S+0001.0;-01-\<CR> Send and Reset Subtotal,
* AN\<CR>*5 N+0001.0;0001\<CR>/ERR\<CR>*4 Get net and alibi nr., wait for no motion
* AG\<CR>*5 G+0001.0;0001\<CR>/ERR\<CR>*4 Get gross and alibi nr., wait for no motion
