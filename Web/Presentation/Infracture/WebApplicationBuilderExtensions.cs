namespace WebHook.WebHook.MinimalApi.Infracture;

using FluentValidation;
using global::WebHook.Data.Models.AppSettings;
using global::WebHook.EntityFrameWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Quartz;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;

[ExcludeFromCodeCoverage]
public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureApplicationBuilder(this WebApplicationBuilder builder)
    {
        #region AppSettings
        _ = builder.Services.AddSingleton(builder.Configuration);
        _ = builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
        #endregion

        #region Dbcontext
        _ = builder.Services.AddDbContext<ApplicationDbContext>(
         options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        #endregion

        #region DependencyInjection
        _ = builder.Services.AddDependencyInjection();
        #endregion

        #region Logging

        _ = builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
        {
            var assembly = Assembly.GetEntryAssembly();

            _ = loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration)
                .Enrich.WithProperty(
                    "Assembly Version",
                    assembly?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version)
                .Enrich.WithProperty(
                    "Assembly Informational Version",
                    assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion);
        });

        #endregion Logging

        #region Serialisation

        _ = builder.Services.Configure<JsonOptions>(opt =>
        {
            opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            opt.SerializerOptions.PropertyNameCaseInsensitive = true;
        });

        #endregion Serialisation

        #region Background job

        builder.BackgroundJobConfigure();

        #endregion

        #region Swagger

        var ti = CultureInfo.CurrentCulture.TextInfo;

        _ = builder.Services.AddEndpointsApiExplorer();
        _ = builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"WebHook API - {ti.ToTitleCase(builder.Environment.EnvironmentName)}",
                    Description = "WebHook API with Minimal API in .NET 6.",
                    Contact = new OpenApiContact
                    {
                        Name = "WebHook API",
                        Email = "webhook@pos.dev"
                    },
                });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            options.DocInclusionPredicate((name, api) => true);
        });

        #endregion Swagger

        #region Cache

        _ = builder.Services.AddMemoryCache();

        #endregion

        #region Validation
        _ = builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        #endregion Validation

        return builder;
    }
}

