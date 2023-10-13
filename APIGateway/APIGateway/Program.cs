using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using JwtTokenAuthenticationManager;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddJwtAuthentication();
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json",optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
await app.UseOcelot();



app.Run();
