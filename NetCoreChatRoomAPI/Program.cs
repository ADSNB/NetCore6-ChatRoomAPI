using Domain;
using Domain.Models;
using Domain.Services;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.UnitOfWork;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Log/log-.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();

Log.Information("Serviço de logs iniciado");

var _appSettingsModel = new AppSettingsModel();
builder.Configuration.Bind(_appSettingsModel);

builder.Services.AddSingleton<AppSettingsModel>(_appSettingsModel);
builder.Services.AddScoped<GroupChatDomain>();
builder.Services.AddScoped<GroupChatMessageDomain>();
builder.Services.AddSingleton<RobotDomain>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ISignalRService, SignalRService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<IHostedService, RobotService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization();

var AllowSpecificOrigins = "AllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSpecificOrigins, builder => builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithOrigins("https://localhost:7115"));
});

builder.Services.AddDbContext<NetCoreChatRoomAPIDbContext>(options =>
{
    if (_appSettingsModel.Database.InMemoryEnabled)
        options.UseInMemoryDatabase("InMemoryDatabase");
    else
        options.UseSqlServer(_appSettingsModel.Database.ConnectionString);
}, ServiceLifetime.Transient);

// builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<NetCoreChatRoomAPIDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllers();
/*builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});*/

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://login.microsoftonline.com/441ab8a6-cc02-439d-8c20-e9bd91a4f6a2/oauth2/v2.0/authorize"),
                Scopes = new Dictionary<string, string>
                            {
                                { "https://ChatRoomB2C.onmicrosoft.com/9407c611-8d10-40ac-a90d-416a8f5c95dd/access_as_user", "Access as a app user" }
                            }
            }
        }
    });
});

/*builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                Scopes = new Dictionary<string, string>
                {
                    { "openid", "Open Id" }
                },
                AuthorizationUrl = new Uri("https://" + _appSettingsModel.Auth0.Domain + "/authorize?audience=" + _appSettingsModel.Auth0.Audience)
            }
        }
    });
});*/

builder.Services.AddHealthChecks();

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHealthChecks("/health");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    /*app.UseSwaggerUI();*/
    app.UseSwaggerUI(c =>
    {
        //c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestService");
        c.OAuthClientId("40e3c6f1-8499-48b0-aed9-af691b7255e7");
        c.OAuthClientSecret("JcO7Q~eJSbDMd_dYdCs2lue61EroHYaEp9Wqy");
        //c.OAuth2RedirectUrl("https://localhost:7269/swagger/");
        //c.OAuthRealm("client-realm");
        //c.OAuthAppName("OAuth-app");
        c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    });

    IdentityModelEventSource.ShowPII = true; // for deep troubleshooting
}

app.UseHttpsRedirection();
app.UseCors(AllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<SignalRService>($"/{_appSettingsModel.SignalR.HubName}");

app.Run();