cd SampleNetCoreAngularKeycloak/
start dotnet run
start C:/Users/ben.rosenbaum/tools/ngrok/ngrok.exe http -host-header=rewrite -subdomain keycloak-demo     http://localhost:5000

cd ../KeycloakDemoAPI 
start dotnet run
start C:/Users/ben.rosenbaum/tools/ngrok/ngrok.exe http -host-header=rewrite -subdomain keycloak-demo-api http://localhost:26060


rem cd ../reverse
rem docker network rm dockernet
rem docker network create -d bridge --subnet 192.168.0.0/24 --gateway 192.168.0.1 dockernet

REM start C:/Users/ben.rosenbaum/tools/ngrok/ngrok.exe http -subdomain keycloak-demo-ui  http://localhost:5000
REM start C:/Users/ben.rosenbaum/tools/ngrok/ngrok.exe http -subdomain keycloak-demo-api http://localhost:26060
rem start docker-compose up --build
rem start C:/Users/ben.rosenbaum/tools/ngrok/ngrok.exe http -subdomain keycloak-demo-ui     http://localhost
REM to open a shell to the reverse proxy docker: docker exec -it reverse_reverse_1 bash

cd ..

 "C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --auth-server-whitelist="keycloak-demo.virtualcorp.ch" http://keycloak-demo.ngrok.io

