@echo off
echo Iniciando o Software G Web Clone - Portugues...

set "APP_PATH=%~dp0index.html"
if not exist "%APP_PATH%" (
    echo Erro: arquivo index.html nao encontrado em "%APP_PATH%".
    pause
    exit /b 1
)

set "APP_URL=%APP_PATH:\=/%"

set "EDGE_EXE=%ProgramFiles(x86)%\Microsoft\Edge\Application\msedge.exe"
if not exist "%EDGE_EXE%" set "EDGE_EXE=%ProgramFiles%\Microsoft\Edge\Application\msedge.exe"
if not exist "%EDGE_EXE%" set "EDGE_EXE=%LocalAppData%\Microsoft\Edge\Application\msedge.exe"

if not exist "%EDGE_EXE%" (
    echo Erro: Microsoft Edge nao encontrado.
    echo Abra manualmente: file:///%APP_URL%
    pause
    exit /b 1
)

start "G Web Clone" "%EDGE_EXE%" --app="file:///%APP_URL%" --new-window
exit /b 0
