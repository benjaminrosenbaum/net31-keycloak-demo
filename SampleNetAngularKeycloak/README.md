# Sample .Net Core Angular Keycloak
A sample single page app using Angular that allows users authentication through the use of Keycloak as an identity provider.
It is linked to an article Xavier wrote on the subject: https://medium.com/@xavier.hahn/asp-net-core-angular-openid-connect-using-keycloak-6437948c008

The base of the project is the default .Net Core 2.0 Angular template.

  docker rm keycloak
  docker run --name keycloak -p 8080:8080 -e KEYCLOAK_USER=key -e KEYCLOAK_PASSWORD=cloak jboss/keycloak
  
