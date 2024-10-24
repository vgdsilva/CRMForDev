using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Microsoft.Extensions.Hosting;

namespace CRMDevFreelancer.WebApp;

public class Startup
{

    public IConfiguration Configuration { get; set; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    IContractResolver resolver = opt.SerializerSettings.ContractResolver;
                    if (resolver != null)
                        (resolver as DefaultContractResolver).NamingStrategy = null;
                });

        services
            .AddRazorPages(options =>
            {
                options.Conventions.AllowAnonymousToPage("/");
                options.Conventions.AllowAnonymousToPage("/Login");
            });
            //.AddDataAnnotationsLocalization()
            //.AddViewLocalization();

        services
            .AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                if (CRMDevFreelancerUtils.IsDebugMode())
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(5);
                }
                else
                {
                    options.ExpireTimeSpan= TimeSpan.FromDays(1);
                    options.SlidingExpiration = true;
                }

                options.LoginPath = "/Login";
            });

        services
            .AddSwaggerGen(swagger =>
            {
                swagger.EnableAnnotations();

                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CRM Dev Freelancer API",
                    Description = "",
                });

                //swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

                swagger.TagActionsBy(api =>
                {
                    if (api.GroupName is null)
                        return new[] { api.GroupName };

                    if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                        return new[] { controllerActionDescriptor.ControllerName };

                    throw new InvalidOperationException("Não foi possivel determinar a tag para o endpoint");
                });

                swagger.DocInclusionPredicate((name, api) => true);
            });

        services
            .AddCors(opt =>
            {
                opt.AddPolicy("AnyOrigin", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("AnyOrigin");

        app.UseStaticFiles();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(s =>
        {
            s.SwaggerEndpoint("/swagger/v1/swagger.json", "CRM Dev Freelancer API v1");

            if (!CRMDevFreelancerUtils.IsDebugMode())
            {
                s.DefaultModelExpandDepth(-1);
            }

            s.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
            s.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStatusCodePagesWithRedirects("~/{0}.html");

        app.UseEndpoints(config =>
        {
            config.MapControllers();
            config.MapRazorPages();
        });

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}
