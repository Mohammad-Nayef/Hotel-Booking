using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HotelBooking.Api.Extensions
{
    internal static class SwaggerDependencyInjection
    {
        /// <summary>
        /// Register SwaggerUI for documentation.
        /// </summary>
        public static IServiceCollection AddSwaggerUi(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                AddComments(setup);
                AddSecurity(setup);
            });

            return services;
        }

        private static void AddComments(SwaggerGenOptions setup)
        {
            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

            setup.IncludeXmlComments(xmlCommentsFullPath);
        }

        private static void AddSecurity(SwaggerGenOptions setup)
        {
            setup.AddSecurityDefinition("UserAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Description = "Input an authorized token"
            });

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "UserAuth"
                            }
                        },
                        new List<string>()
                    }
                });
        }
    }
}