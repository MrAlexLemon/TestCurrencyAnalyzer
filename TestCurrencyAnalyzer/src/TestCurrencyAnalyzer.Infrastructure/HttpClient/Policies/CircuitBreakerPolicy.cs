using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Infrastructure.HttpClient.Policies
{
    public static class CircuitBreakerPolicy
    {
        public static IHttpClientBuilder AddCircuitBreaker(this IHttpClientBuilder builder, int numEventsBeforeBreak = 3)
        {
            var services = builder.Services.BuildServiceProvider();
            var logger = services.GetService<ILogger<IHttpClientBuilder>>();
            var policy = Policy.HandleResult<HttpResponseMessage>(r =>
            {
                if (!r.IsSuccessStatusCode)
                {
                    logger.LogError($"Circuit Breaker: Response status code not success =>  {r.StatusCode}");
                }
                return !r.IsSuccessStatusCode;
            }).Or<Exception>(r =>
            {
                logger.LogError("Polly Circuit Breaker catch exception");
                logger.LogError(r.Message);
                return true;
            })
            .CircuitBreakerAsync(numEventsBeforeBreak, TimeSpan.FromSeconds(10), (message, timeSpan) =>
            {
                logger.LogInformation("Circuit break start");
            }, () => {
                logger.LogInformation("Circuit break resume");
            });
            return builder.AddPolicyHandler(policy);
        }
    }
}
