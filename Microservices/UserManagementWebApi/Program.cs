using Microsoft.EntityFrameworkCore;
using UserManagementWebApi.Helpers;
using UserManagementWebApi.Repository;
using JwtTokenAuthenticationManager;
using ChatApplication.Db;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
    services.AddControllers();
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
    services.AddAutoMapper(typeof(Program));

    // configure DI for application services    
    services.AddScoped<IUserRepository, UserRepository>();    
}

var app = builder.Build();

// configure HTTP request pipeline
{
   

    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();    
    app.UseMiddleware<ErrorHandlerMiddleware>();
}

app.Run();