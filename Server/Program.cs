using Microsoft.AspNetCore.ResponseCompression;
using WheelOfFortune.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;
using WheelOfFortune.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http.Features;
using WheelOfFortune.Server.Hubs;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .Build();

builder.Services.AddSingleton(configuration);
// Add services to the container.

builder.Services.AddControllersWithViews().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    var connectionString = configuration.GetConnectionString("MSSQL");
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("WheelOfFortune.Server"));
});
// Add signalr
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

// Add auth services
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSecret = Encoding.ASCII.GetBytes(configuration["JwtAuth:Secret"]);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            RequireSignedTokens = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(jwtSecret),

            ValidateAudience = false,
            ValidateIssuer = false,

            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        options.RequireHttpsMetadata = false;

        var tokenHandler = options.SecurityTokenValidators.OfType<JwtSecurityTokenHandler>().Single();
        tokenHandler.InboundClaimTypeMap.Clear();
        tokenHandler.OutboundClaimTypeMap.Clear();
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Headers["Authorization"];
                
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &
                    path.StartsWithSegments("/hubs"))
                {
                    context.Token = accessToken[0].Substring(8).Remove(accessToken[0].Length - 9);
                }
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat");
app.UseAuthentication();
app.UseAuthorization();
app.MapFallbackToFile("index.html");
app.Run();