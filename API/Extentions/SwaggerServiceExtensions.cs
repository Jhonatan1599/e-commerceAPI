using Microsoft.OpenApi.Models;
using System.Reflection;

namespace API.Extentions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("LibraryOpenAPISpecification", new()
                {
                    Title = "E-Commerce API",
                    Version = "1"
                });

                // Adding XML documentation
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml ";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                c.IncludeXmlComments(xmlCommentsFullPath);

                var xmlCommentsFile2 = $"{typeof(Infrastructure.Data.StoreContext).Assembly.GetName().Name}.xml";
                var xmlCommentsFullPath2 = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile2);
                c.IncludeXmlComments(xmlCommentsFullPath2);

                var xmlCommentsFile3 = "Core.xml";
                var xmlCommentsFullPath3 = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile3);
                c.IncludeXmlComments(xmlCommentsFullPath3);

                // JWT
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema );

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        securitySchema, new[] {"Bearer"}
                    }
                };

                c.AddSecurityRequirement(securityRequirement);
            });

            

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/LibraryOpenAPISpecification/swagger.json", "Library API");
                setupAction.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}