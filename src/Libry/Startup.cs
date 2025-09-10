using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;
using Libry.Infrastructure;
using Libry.Infrastructure.Repositories;
using Libry.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Libry;

public static class Startup
{
    public static void AddServices(this WebApplicationBuilder builder, ConfigurationManager configurationManager)
    {
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configurationManager["JwtSettings:Issuer"],
                    ValidAudience = configurationManager["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationManager["JwtSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

        builder.Services.AddAuthorization();

        builder.Services.AddResponseCaching();

        builder.Services.AddControllers(opt =>
        {
            opt.CacheProfiles.Add("Default",
                new CacheProfile()
                {
                    Duration = 30,
                    VaryByQueryKeys = ["*"]
                });
        });

        builder.Services.AddProblemDetails();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Libry API",
                Description = "An ASP.NET Core Web API for managing Libry",
                TermsOfService = new Uri("https://example.com/terms"),
                License = new OpenApiLicense
                {
                    Name = "Copyright Libry (c) All rights reserved.",
                    Url = new Uri("https://example.com/license")
                }
            });

            //options.IncludeXmlComments(Assembly.GetExecutingAssembly());
            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();
        });

        builder.Services.AddRateLimiter(opt =>
        {
            opt.AddFixedWindowLimiter("fixed", opt =>
            {
                opt.PermitLimit = 4;
                opt.Window = TimeSpan.FromSeconds(12);
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                opt.QueueLimit = 2;
            });
        });

        builder.Services.AddHealthChecks();

        builder.Services.AddDbContext<DbContext>();
        builder.Services.AddScoped<ILibryRepository, LibryRepository>();
    }

    public static void ConfigureServices(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseResponseCaching();
        app.UseRateLimiter();

        app.MapHealthChecks("/health");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
            app.MapSwagger().CacheOutput();

            app.UseDeveloperExceptionPage();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.MapControllers();
    }
}

