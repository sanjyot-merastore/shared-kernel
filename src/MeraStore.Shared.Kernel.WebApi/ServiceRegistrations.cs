using MeraStore.Shared.Kernel.Logging;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using MeraStore.Shared.Kernel.WebApi.Extensions;

namespace MeraStore.Shared.Kernel.WebApi
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistrations
    {
        public static void AddApiServices(this WebApplicationBuilder builder, string serviceName,  bool defaultLogging = true)
        {
            builder.Services.AddMeraStoreCompression();
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Formatting = Formatting.None;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    });
                });
            builder.Services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.SerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
                options.SerializerOptions.DefaultBufferSize = 16 * 1024; // 16 KB
                options.SerializerOptions.PropertyNameCaseInsensitive = true;
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.SerializerOptions.WriteIndented = true; // Set to true if you want pretty print
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            });

            // JSON serialization config
            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.SerializerOptions.WriteIndented = true;
            });

            if (defaultLogging)
            {
                builder.AddLoggingServices(Constants.ServiceName, options =>
                {
                    options.UseElasticsearch = true;
                    options.UseInfrastructureSink = true;
                    options.ElasticsearchUrl = builder.Configuration["ElasticSearchUrl"];
                });
            }

            builder.Services.AddProblemDetails();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddResponseCompression();

        }
    }
}