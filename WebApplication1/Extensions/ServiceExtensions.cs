using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentations.ActionFilters;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;
using System.Runtime.CompilerServices;
using Presentations.Controllers;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace WebApplication1.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration)  =>
            services.AddDbContext<RepositoryContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager,RepositoryManager>(); //her kullanıcı için ayrı üretilmesini sağlar - addscoped

        public static void ConfigureServiceManager(this IServiceCollection services) =>
           services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggingService, LoggerManager>();
        public static void ConfigureActionFilters(this IServiceCollection services) 
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddSingleton<LogFilterAttribute>();
         }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
            });
        }

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var systemTextJsonOutputFormatter = config
                .OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

                if (systemTextJsonOutputFormatter != null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.yusufkggoglu.hateoas+json");

                    systemTextJsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.yusufkggoglu.apiroot+json");
                }

                var xmlOutputFormatter = config
                .OutputFormatters
                .OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

                if (xmlOutputFormatter is not null)
                {
                    xmlOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.yusufkggoglu.hateoas+xml");

                    xmlOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.yusufkggoglu.apiroot+xml");
                }
            });
        }
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning (opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");

                opt.Conventions.Controller<BooksController>()
                    .HasApiVersion(new ApiVersion(1, 0));

                opt.Conventions.Controller<BookV2Controller>()
                    .HasDeprecatedApiVersion(new ApiVersion(2, 0)); // servis dışı api - deprececated
            });
        }
    }
}
