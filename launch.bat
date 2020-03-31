cd SampleNetCoreAngularKeycloak/
start dotnet run
start C:/Users/ben.rosenbaum/tools/ngrok/ngrok.exe http -subdomain keycloak-demo     http://localhost:5000

cd ../KeycloakDemoAPI 
start dotnet run

cd ../reverse
REM start C:/Users/ben.rosenbaum/tools/ngrok/ngrok.exe http -subdomain keycloak-demo-ui  http://localhost:5000
REM start C:/Users/ben.rosenbaum/tools/ngrok/ngrok.exe http -subdomain keycloak-demo-api http://localhost:26060
start docker-compose up --build
REM start C:/Users/ben.rosenbaum/tools/ngrok/ngrok.exe http -subdomain keycloak-demo     http://localhost

cd ..
