using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace KeycloakDemoAPI
{
    public class CorsInspectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICorsService _service;
        private readonly ILogger<CorsInspectionMiddleware> _logger;
        
        public CorsInspectionMiddleware(RequestDelegate next, ICorsService service, ILogger<CorsInspectionMiddleware> logger)
        {
            _next = next;
            _service = service;
            _logger = logger;
        }

        // IMyScopedService is injected into Invoke
        public async Task Invoke(HttpContext httpContext)
        {
            var policy = CorsHelper.SetupCors().Build();
            var origin = httpContext.Request.Headers[CorsConstants.Origin];
            StringBuilder output = new StringBuilder();
            output.AppendLine($"\n-----------\nCORS request at {DateTime.Now} from {httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "unknown IP"}");
            output.AppendLine($"policy origins: {string.Join(",", policy.Origins)}, origin: {origin}, allowed: {policy.IsOriginAllowed(origin)}");
            try
            {

                CorsResult result = _service.EvaluatePolicy(httpContext, policy);
                output.AppendLine(result.ToString());
                output.AppendLine($"is origin allowed: {result.IsOriginAllowed}");

                output.AppendLine($"headers before applying cors: {PrintHeaders(httpContext.Response)}");
                _service.ApplyResult(result, httpContext.Response);
                output.AppendLine($"headers after applying cors: {PrintHeaders(httpContext.Response)}");

                output.AppendLine($"is origin allowed: {result.IsOriginAllowed}");

            }
            catch (Exception e)
            {
                output.AppendLine($"Caught {e.Message}: {e.StackTrace}");
            }
            _logger.LogInformation(output.ToString());
            await _next(httpContext);
        }

        private static string PrintHeaders(HttpResponse response) => 
            string.Join("\n", response.Headers.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
    }
}