using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MS.Services.Identity.API.Swagger;

public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        this.provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        // add swagger document for every API version discovered
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                CreateVersionInfo(description));

            options.AddSecurityDefinition(
                description.GroupName,
                CreateSecurityScheme());

            // pass the group name here: 
            options.AddSecurityRequirement(
                CreateSecurityRequirement(description.GroupName));

        }

        //Determine base path for the application.

        var basePath = AppContext.BaseDirectory;
        var assemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
        var fileName = System.IO.Path.GetFileName(assemblyName + ".xml");

        //Set the comments path for the swagger json and ui.
        options.IncludeXmlComments(System.IO.Path.Combine(basePath, fileName));
    }

    private OpenApiSecurityScheme CreateSecurityScheme()
    {
        var securityScheme = new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer token\"",
        };
        return securityScheme;
    }

    private OpenApiSecurityRequirement CreateSecurityRequirement(string groupName)
    {
        var securityRequirement = new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    // not "Bearer" here, as it was copied form VanillaProject
                        { Type = ReferenceType.SecurityScheme, Id = groupName }
                },
                new string[] { }
            }
        };

        return securityRequirement;
    }

    public void Configure(string name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "Identity API",
            Version = description.ApiVersion.ToString()
        };

        if (description.IsDeprecated)
        {
            info.Description += " deprecated API.";
        }

        return info;
    }
}