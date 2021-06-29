using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Interfaces.Repositories.Currency;
using TestCurrencyAnalyzer.Application.Interfaces.Repositories.Identity;
using TestCurrencyAnalyzer.Application.Interfaces.Services;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Currency;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Identity;
using TestCurrencyAnalyzer.Infrastructure.HttpClient;
using TestCurrencyAnalyzer.Infrastructure.HttpClient.Policies;
using TestCurrencyAnalyzer.Infrastructure.Options;
using TestCurrencyAnalyzer.Infrastructure.Persistence;
using TestCurrencyAnalyzer.Infrastructure.Repositories.Currency;
using TestCurrencyAnalyzer.Infrastructure.Repositories.Identity;
using TestCurrencyAnalyzer.Infrastructure.Services;
using TestCurrencyAnalyzer.Infrastructure.Services.Currency;
using TestCurrencyAnalyzer.Infrastructure.Services.Identity;

namespace TestCurrencyAnalyzer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var jwtTokenConfig = configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });


            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("CleanArchitectureDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddSingleton<IRng, Rng>();
            services.AddSingleton<IPasswordHasher<IPasswordService>, PasswordHasher<IPasswordService>>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IJwtAuthManager, JwtAuthManager>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            

            services.AddScoped<TraceHandler>();

            services.AddHttpClient("testCurrencyClient", opt=>
            {
                opt.BaseAddress = new Uri("https://api.privatbank.ua/p24api/");
                opt.DefaultRequestHeaders.Clear();
            })
                .AddHttpMessageHandler<TraceHandler>()
                .AddWaitAndRetryPolicy()
                .AddCircuitBreaker()
                .AddTimeoutPolicy();


            services.AddScoped<ICurrencyService, CurrencyService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Currency Analyzer", Version = "v1" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });

            return services;
        }
    }
}
