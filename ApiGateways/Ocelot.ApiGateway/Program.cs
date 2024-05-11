using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Ocelot
builder.Services.AddOcelot()
    .AddCacheManager(options => options.WithDictionaryHandle());
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("all", builder => builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});

builder.WebHost.UseUrls("http://*:80");

var app = builder.Build();

// Development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// CORS
app.UseCors("CorsPolicy");

// Endpoints
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello Ocelot");
    });
});

// Ocelot
await app.UseOcelot();

app.Run();
