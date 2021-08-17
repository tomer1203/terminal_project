//          writing file        //
//////////////////////////////////
$[Wf]_|000|          --- start writing file

$[Na]_|___|          --- Name of the writing file
$[Sz]_|___|          --- Size of the writing file

$[Wd]_|___|DATA      --- DATA
$[Wd]_|___|DATA
.
.
.
.
$[Wd]_|___|DATA

--board to pc
$[Fe]_|001|_          --- File Status


//          sending text        //
//////////////////////////////////
$[Tx]_|___|TEXT      --- send text to board or from board to pc

-- board to pc
$[St]_|001|Status    --- a reply if message was received and checksum is ok.


//      change baud rate        //
//////////////////////////////////
$[Br]_|___|BaudRate   --- update baud rate

-- board to pc
$[St]_|001|Status    --- a reply if message was received and checksum is ok.

