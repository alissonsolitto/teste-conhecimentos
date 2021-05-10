using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Api.Two.Swagger;
using Api.Two.V1.Querys;
using Api.Two.V1.Validators;
using Domain.Two.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Useful.ActionResult;
using Useful.Interfaces;
using Useful.Interfaces.Middlewares;
using Useful.Services;
using Useful.Transformers;

namespace Api.Two
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            });

            services.AddHttpClient();
            services.AddTransient<LoggerMiddleware>();
            services.AddSingleton<CalcularJurosService>();            
            services.AddSingleton<IRestService, RestService>();            
            services.AddTransient<IValidator<CalcularJurosQuery>, CalcularJurosValidator>();

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var messages = context.ModelState.Values
                            .Where(x => x.ValidationState == ModelValidationState.Invalid)
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage)
                            .ToList();

                        return new BadRequestObjectResult(new WebApiResultModel<object>(messages));
                    };
                });
            
            services.AddMvc().AddFluentValidation();            

            services.AddApiVersioning(c =>
            {
                c.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                var basePath = AppContext.BaseDirectory;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                options.IncludeXmlComments(Path.Combine(basePath, fileName));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            
                app.UseSwagger(options =>
                {
                    options.RouteTemplate = "swagger/{documentName}/swagger.json";
                });

                app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            }

            app.UseExceptionHandler(ExceptionMiddleware.ExceptionMiddlewareHaldler(loggerFactory.CreateLogger("ExceptionMiddlewareHaldler")));
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
