using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using PandoraAnalyticsAPI.Application.Interfaces;
using PandoraAnalyticsAPI.Application.Services;
using PandoraAnalyticsAPI.Infrastructure.Data;
using PandoraAnalyticsAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var enforceHttps = builder.Configuration.GetValue("EnforceHttps", true);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<ITrialRepository, TrialRepository>();

builder.Services.AddScoped<AnalyticsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsync("{\"title\":\"An unexpected error occurred.\",\"status\":500}");
        });
    });
    app.UseHsts();
}

app.UseForwardedHeaders();

if (enforceHttps)
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();