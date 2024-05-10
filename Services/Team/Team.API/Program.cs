using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Team.Application;
using Team.Persistence;
using Team.Persistence.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);

//Custon services
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

// API
builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("all", builder => builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});

builder.Services.AddHttpContextAccessor();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Team.API", Version = "v1" });
    c.MapType<TimeSpan>(() => new OpenApiSchema { Type = "string", Example = new OpenApiString("00:00:00") });
});

builder.WebHost.UseUrls("http://*:80");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Swagger
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Team.API"));

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<TeamDatabaseContext>();
        dbContext.Database.EnsureCreated();
    }
}

app.UseCors("all");

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});

app.Run();
