using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NJsonSchema;
using NSwag.AspNetCore;
using System.Reflection;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;
using UTag.Helpers;
using UTag.Services;
using UTag.Services.Interfaces;

namespace UTag
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UTagContext>(options =>
                options.UseSqlServer(connection));
            services.AddMvc();
            services.AddAutoMapper();
            services.AddSwagger();
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                            var userId = int.Parse(context.Principal.Identity.Name);
                            var user = userService.GetById(userId);
                            if (user == null) context.Fail("Unauthorized");
                            return Task.CompletedTask;
                        }
                    };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonService, PersonService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();
            app.UseStaticFiles();

#pragma warning disable CS0618 // Type or member is obsolete
            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "UTag API";
                    document.Info.Description = "App for recomendations";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new SwaggerContact
                    {
                        Name = "Anastasiia Shcherbakova",
                        Email = "nshcherbakovaa@gmail.com"
                        
                    };

                };
                settings.GeneratorSettings.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));

                settings.GeneratorSettings.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT token",
                    new SwaggerSecurityScheme
                    {
                        Type = SwaggerSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = "Copy 'Bearer ' + valid JWT token into field",
                        In = SwaggerSecurityApiKeyLocation.Header,
                    }));
            });
#pragma warning restore CS0618 // Type or member is obsolete
            app.UseMvc();
        }
    }
}