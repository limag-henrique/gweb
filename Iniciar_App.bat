@echo off
echo Iniciando o Software G Web Clone - Portugues...

set "APP_PATH=%~dp0index.html"
set "APP_URL=%APP_PATH:\=/%"
start "" msedge.exe --app="file:///%APP_URL%"
exit
