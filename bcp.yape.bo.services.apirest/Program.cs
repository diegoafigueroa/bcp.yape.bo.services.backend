using bcp.yape.bo.core.Ports.Driven;
using bcp.yape.bo.core.Ports.Driving;
using bcp.yape.bo.infrastructure.Adapters;
using bcp.yape.bo.infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// REGISTERS THE IN-MEMORY DBCONTEXT
builder.Services.AddDbContext<InMemoryDbContext>(options =>
    options.UseInMemoryDatabase("YapeInMemoryDb"));

// ADD SERVICES TO THE CONTAINER
builder.Services.AddControllers();

// REGISTER INTERNAL/EXTERNAL PORTS IN THE DEPENDENCY CONTAINER (WITH THEIR ADAPTERS)
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepositoryPort, ClientRepositoryInMemoryAdapter>();
builder.Services.AddScoped<IPeopleServicePort, PeopleServiceWcfAdapter>();

// ADD SWAGGER CONFIGURATION
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // VISUALIZE XML COMMENTS FROM CODE
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "bcp.yape.bo.services.apirest.xml"));

    // SWAGGER BRANDING
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API. Yape Bolivia",
        Version = "v1",
        Description = "API que maneja múltiples funcionalidades relacionadas con Yape Bolivia, incluyendo gestión de clientes, transacciones y más."
    });
});

// BUILD THE WEB APPLICATION.
var app = builder.Build();

// CONFIGURE THE HTTP REQUEST PIPELINE
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Activates Swagger generation
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yape API v1");  // Swagger JSON endpoint
        c.RoutePrefix = string.Empty;  // Serve Swagger UI at the root of the server
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
