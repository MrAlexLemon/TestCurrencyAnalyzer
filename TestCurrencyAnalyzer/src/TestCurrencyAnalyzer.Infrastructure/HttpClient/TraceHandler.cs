using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Infrastructure.HttpClient
{
    public class TraceHandler : DelegatingHandler
    {
        private readonly ILogger<TraceHandler> logger;
        public TraceHandler(ILogger<TraceHandler> _logger)
        {
            logger = _logger;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation($"Begin Request => " + request.RequestUri);
                var response = base.SendAsync(request, cancellationToken);
                if (response.Result.IsSuccessStatusCode)
                {
                    logger.LogInformation("Request Success");
                }
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: Request cancelled {ex.Message}");
                throw;
            }
        }
    }
}
