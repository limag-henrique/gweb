@echo off
setlocal
echo Iniciando o Software G Web Clone - Portugues...

set "APP_PATH=%~dp0index.html"
if not exist "%APP_PATH%" (
    echo Erro: arquivo index.html nao encontrado em "%APP_PATH%".
    pause
    exit /b 1
)

set "EDGE_EXE=%ProgramFiles(x86)%\Microsoft\Edge\Application\msedge.exe"
if not exist "%EDGE_EXE%" set "EDGE_EXE=%ProgramFiles%\Microsoft\Edge\Application\msedge.exe"
if not exist "%EDGE_EXE%" set "EDGE_EXE=%LocalAppData%\Microsoft\Edge\Application\msedge.exe"

if not exist "%EDGE_EXE%" (
    echo Erro: Microsoft Edge nao encontrado.
    echo Abra manualmente: file:///%APP_URL%
    pause
    exit /b 1
)

for /f "usebackq delims=" %%U in (`powershell -NoProfile -ExecutionPolicy Bypass -Command "$path = (Resolve-Path -LiteralPath $env:APP_PATH).Path; ([System.Uri]$path).AbsoluteUri"`) do set "APP_URL=%%U"

if not defined APP_URL (
    echo Erro: nao foi possivel montar a URL do aplicativo.
    pause
    exit /b 1
)

start "G Web Clone" "%EDGE_EXE%" --app="%APP_URL%" --new-window
exit /b 0
