using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Portal.TM.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, string title, string version)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(version, new OpenApiInfo { Title = title, Version = version });
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Coloque **_APENAS_** seu JWT Bearer token na caixa de texto abaixo. O texto Bearer, não é necessário!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
                c.SchemaFilter<EnumSchemaFilter>();
            });

            //services.AddSwaggerGen(c => c.DocumentFilter<JsonPatchDocumentFilter>());
            return services;
        }

        public static void UseSwaggerConfigUi(this IApplicationBuilder app, string name, string url = "/swagger/v1/swagger.json")
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(url, name));
        }

        public class EnumSchemaFilter : ISchemaFilter
        {
            public void Apply(OpenApiSchema model, SchemaFilterContext context)
            {
                if (context.Type.IsEnum)
                {
                    model.Enum.Clear();
                    Enum.GetNames(context.Type)
                        .ToList()
                        .ForEach(name => model.Enum.Add(new OpenApiString($"{name} = {Convert.ToInt64(Enum.Parse(context.Type, name))}")));
                }
            }
        }
    }
}
