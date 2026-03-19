using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Load the Ocelot configuration file
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Register Ocelot services into Dependency Injection container
builder.Services.AddOcelot();

var app = builder.Build();

// Add Ocelot middleware to the HTTP pipeline
await app.UseOcelot();

app.Run();