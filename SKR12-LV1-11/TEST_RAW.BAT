REM @ECHO OFF
IF NOT EXIST FTREX%1.exe (
   ECHO --------------------------------------
   ECHO No executable [..\FTREX%1.exe]
   ECHO --------------------------------------
   GOTO END
)

MKDIR OUT%1

@REM --------------------------------------
@REM TEST NEGATE
@REM --------------------------------------
 FTREX%1 /RAW_COMBINE INP.LST OUT.RAW
 FTREX%1 /RAW_SORT_PROB OUT.RAW OUT1.RAW
@REM FTREX /RAW_SORT_ORDER INP.RAW OUT.RAW [TOP_EVENT]
@REM  FTREX /RAW_SPLIT INP.RAW 10000
@REM         When a cutset number exceeds 10000,
@REM         INP.RAW is divided into INP.M.1.RAW, INP.M.2.RAW, ..., INP.M.M.RAW.

:END
