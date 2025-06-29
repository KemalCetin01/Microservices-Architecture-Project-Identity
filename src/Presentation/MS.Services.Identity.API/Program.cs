using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Middlewares;
using MS.Services.Core.ExceptionHandling;
using MS.Services.Core.Logging;
using MS.Services.Core.Networking.Http.Infrastructure;
using MS.Services.Identity.API.CustomProviders;
using MS.Services.Identity.API.Swagger;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Application.Helpers.Options;
using MS.Services.Identity.Application.Registrations;
using MS.Services.Identity.Infrastructure;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Services;
using MS.Services.Identity.Persistence;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = builder.Configuration;

configuration
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{env}.json", true, true)
    .Build();


builder.Services.AddOptions<KeycloakOptions>().BindConfiguration("KeycloakOptions");
builder.Services.AddOptions<OtpOptions>().BindConfiguration("OtpOptions");
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});


builder.Services.AddControllers().AddMvcLocalization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();


#region Internal DI Registrations

builder.Services.AddInfrastructureLayer();
builder.Services.AddApplicationLayer(configuration);
builder.Services.AddPersistenceLayer(configuration);

#endregion

#region Base DI Registrations

builder.Services.AddApiLayer();
builder.Services.AddBaseHealthChecks();

#endregion

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//builder.UseSerilogLogging(configuration); //TODO: Serilog Logging altyapý hazýr eklenecek

builder.Services.AddHttpService<KeycloakBaseService>();
builder.Services.AddHttpService<KeycloakUserService>();

builder.Services.AddHeaderContext();

builder.Services.AddServices();
builder.Services.AddHttpClient();


builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
        options.SuppressModelStateInvalidFilter = true);


//builder.Services.AddLocalizationlayer<IdentityDbContext, IIdentityUnitOfWork>(configuration); //TODO: Localization altyapý hazýr eklenecek



builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ErrorResponses = new ApiVersioningErrorResponseProvider();
});



builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

// builder.Services.ConfigureOptions<ConfigureSwaggerOptions>(); //TODO: Swagger altyapý hazýr eklenecek Hata verdigi icin commentlendi

var app = builder.Build();



var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (!app.Environment.IsProduction())
{ 
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}
app.UseRouting();
app.UseRequestLocalization();

//app.UseBaseHealthChecks();  //TODO: HealthCheck altyapý hazýr eklenecek


app.AddHeaderContextMiddleware();
app.AddExceptionHandlingMiddleware(true);



app.MapControllers();

app.Run();
