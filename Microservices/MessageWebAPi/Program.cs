
using JwtTokenAuthenticationManager;
using ChatApplication.Db;
using Microsoft.EntityFrameworkCore;
using MessageWebAPi.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddJwtAuthentication();

/*Database context dependency Injection */
var dbHost = "(localdb)\\mssqllocaldb";
var dbName = "WebRTCChat";
var dbPassword = "Test@12345";

//var dbHost =Environment.GetEnvironmentVariable("DB_HOST");
//var dbName = Environment.GetEnvironmentVariable("DB_NAME"); ;
//var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD"); ;
string connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User Id=TestKathija; Password ={dbPassword}";

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));
_ = builder.Services.AddScoped<IDataContext>(provider => provider.GetService<DataContext>());

// configure automapper with all automapper profiles from this assembly
builder.Services.AddAutoMapper(typeof(Program));



var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseRouting();
app.MapHub<MessageHub>("/hubs/message");


app.Run();
