using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json;
using System.Text;
using Serilog;
using InsuranceSystem.Application.Dtos;
using InsuranceSystem.Domain.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using InsuranceSystem.Application.Abstraction;
using InsuranceSystem.Application.Implementation;
using InsuranceSystem.Infrastructure.Abstraction;
using InsuranceSystem.Infrastructure.Implementation;

namespace InsuranceSystem.API.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // service registrations
            services.AddScoped<ICalimsService, ClaimsService>();
            services.AddScoped<IClaimsRepository, ClaimsRepository>();
            services.AddScoped<IPolicyHolderRepository, PolicyHolderRepository>();
            services.AddScoped<IPolicyService, PolicyService>();
            services.AddScoped<EncryptionActionFilter>();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //services.AddTransient<IConfiguration>(sp =>
            //{
            //    IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            //    configurationBuilder.AddJsonFile("appsettings.json");
            //    return configurationBuilder.Build();
            //});

            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //var respSettings = configuration.GetSection("ServiceResponseSettings");
            //services.Configure<ServiceResponseSettings>(respSettings);


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("dbConnection"));
            });
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "ENaira.API", Version = "v1" });
                //c.OperationFilter<CustomHeaderFilters.AddRequiredHeaderParameter>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("")
                .AllowAnyHeader()
                .WithMethods("POST", "GET", "PUT", "DELETE")
                .AllowCredentials()
                .Build());
            });
            var appSettings = appSettingsSection.Get<AppSettings>();

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
              .Enrich.FromLogContext()
              .WriteTo.File(appSettings.LogPath, rollingInterval: RollingInterval.Hour)
              .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
              .CreateLogger();

            return services;
        }
    }
}
