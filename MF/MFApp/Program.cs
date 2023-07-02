using System.Net.Http.Headers;
using MediatR;
using MFApp.Clients;
using MFApp.Commands;
using MFApp.EF;
using MFApp.Exceptions.Middleware;
using MFApp.HostedService;
using MFApp.Kernel;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


builder.Services.AddScoped<ErrorHandlerMiddleware>();
builder.Services.AddSingleton<IClock, Clock>();
builder.Services.AddHttpClient<IMFClient, MFClient>(context =>
{
    context.BaseAddress = new Uri("https://wl-api.mf.gov.pl/");
    context.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddDbContext<MFDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<AppInitializer>();

builder.Services.Configure<JsonOptions>(options =>
{
    // Set this to true to ignore null or default values
    options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapGet("/api/nip/{nip}", async (string nip, IMediator mediator, IClock clock) =>
{
    var result = await mediator.Send(new GetNipAndSave(nip, DateOnly.FromDateTime(clock.GetUtcNow())));

    return Results.Ok(result);
});

app.Run();