﻿using System.Linq;
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
            _logger.LogInformation($"policy origins: {string.Join(",", policy.Origins)}, origin: {origin}, allowed: {policy.IsOriginAllowed(origin)}");

            
            CorsResult result = _service.EvaluatePolicy(httpContext, policy);
            _logger.LogInformation(result.ToString());
            _logger.LogInformation($"is origin allowed: {result.IsOriginAllowed}");

            _logger.LogInformation($"headers before applying cors: {PrintHeaders(httpContext.Response)}");
            _service.ApplyResult(result, httpContext.Response);
            _logger.LogInformation($"headers after applying cors: {PrintHeaders(httpContext.Response)}");

            _logger.LogInformation($"is origin allowed: {result.IsOriginAllowed}");
            await _next(httpContext);
        }

        private static string PrintHeaders(HttpResponse response) => 
            string.Join("\n", response.Headers.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
    }
}