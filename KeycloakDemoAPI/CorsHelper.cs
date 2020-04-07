using Microsoft.AspNetCore.Cors.Infrastructure;

namespace KeycloakDemoAPI
{
    public static class CorsHelper
    {
        public static CorsPolicyBuilder SetupCors(CorsPolicyBuilder options)
        {
            return options.WithOrigins("http://keycloak-demo.ngrok.io",
                    "https://keycloak-demo.ngrok.io", "http://localhost:5000",
                    "https://keycloak-demo-ui.ngrok.io", "http://keycloak-demo-ui.ngrok.io",
                    "https://keycloak-demo-api.ngrok.io", "http://keycloak-demo-api.ngrok.io",
                    "https://localhost:4321", "https://intranet-dev-usz.virtualcorp.ch/").AllowAnyMethod()
                // options.AllowAnyOrigin().AllowAnyMethod()
                .WithHeaders("Origin", "X-Requested-With", "Content-Type", "Accept", "Authorization")
                .WithExposedHeaders("Origin", "X-Requested-With", "Content-Type", "Accept", "Authorization").AllowCredentials();
        }

        public static CorsPolicyBuilder SetupCors()
        {
            return SetupCors(new CorsPolicyBuilder());
        }
    }
}