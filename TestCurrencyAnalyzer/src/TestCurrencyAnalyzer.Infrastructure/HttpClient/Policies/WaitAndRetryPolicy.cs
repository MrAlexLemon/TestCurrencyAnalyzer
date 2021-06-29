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
    public static class WaitAndRetryPolicy
    {
        public static IHttpClientBuilder AddWaitAndRetryPolicy(this IHttpClientBuilder builder, int retryCount = 5)
        {
            var services = builder.Services.BuildServiceProvider();
            var logger = services.GetService<ILogger<IHttpClientBuilder>>();
            var policy = Policy.HandleResult<HttpResponseMessage>(r =>
            {
                if (!r.IsSuccessStatusCode)
                {
                    logger.LogError($"Wait and Retry: Response status code not success =>  {r.StatusCode}");
                }
                return !r.IsSuccessStatusCode;
            }).Or<Exception>(r =>
            {
                logger.LogError("Polly Waiting and Retry catch exception");
                logger.LogError(r.Message);
                return true;
            })
            .WaitAndRetryAsync(retryCount, (input) => TimeSpan.FromSeconds(2 + input), (result, timeSpan, retryCount, context) =>
            {
                logger.LogInformation($"Begin {retryCount} °th retry for correlation {context.CorrelationId} with {timeSpan.TotalSeconds} seconds of delay");
            });
            return builder.AddPolicyHandler(policy);
        }
    }
}
