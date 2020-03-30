cd KeycloakDemoAPI 
start dotnet run
cd ../../SampleNetCoreAngularKeycloak/
start dotnet run
cd ../net31-keycloak-demo/reverse
start C:/Users/ben.rosenbaum/tools/ngrok/ngrok.exe http -subdomain keycloak-demo-ui  http://localhost:5000
start C:/Users/ben.rosenbaum/tools/ngrok/ngrok.exe http -subdomain keycloak-demo-api http://localhost:26060
start docker-compose up --build
start C:/Users/ben.rosenbaum/tools/ngrok/ngrok.exe http -subdomain keycloak-demo     http://localhost
cd ..
