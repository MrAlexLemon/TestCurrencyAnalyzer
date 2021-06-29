﻿using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Infrastructure.HttpClient.Policies
{
    public static class TimeoutPolicy
    {
        public static IHttpClientBuilder AddTimeoutPolicy(this IHttpClientBuilder builder, int seconds = 5)
        {

            var policy = Policy.TimeoutAsync<HttpResponseMessage>(seconds);
            return builder.AddPolicyHandler(policy);
        }
    }
}
