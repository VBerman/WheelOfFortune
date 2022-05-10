using Microsoft.AspNetCore.ResponseCompression;
using WheelOfFortune.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;
using WheelOfFortune.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .Build();

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

builder.Services.AddScoped<JwtTokenService>();

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
app.MapFallbackToFile("index.html");

app.Run();