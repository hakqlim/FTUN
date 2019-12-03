@ECHO OFF

SET TOOL=BeEAST.exe
IF NOT EXIST %TOOL% (
   ECHO --------------------------------------
   ECHO No executable [%TOOL%]
   ECHO --------------------------------------
   GOTO END
)

BeEAST.exe S015\#Result.RAW S015\#Result.TXT /C=2000 /IMP=1 /N=#S1-SEISMIC-
BeEAST.exe S03\#Result.RAW S03\#Result.TXT /C=2000 /IMP=1 /N=#S1-SEISMIC-
BeEAST.exe S05\#Result.RAW S05\#Result.TXT /C=2000 /IMP=1 /N=#S1-SEISMIC-
BeEAST.exe S07\#Result.RAW S07\#Result.TXT /C=2000 /IMP=1 /N=#S1-SEISMIC-
BeEAST.exe S09\#Result.RAW S09\#Result.TXT /C=2000 /IMP=1 /N=#S1-SEISMIC-

:END
