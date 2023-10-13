namespace ChatApplication.Db;

using ChatApplication.Model;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext, IDataContext
{
    //protected readonly IConfiguration Configuration;

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        // Configuration = configuration;
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //{
    //    // connect to sql server database
    //   // options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
    //}

    public DbSet<User> User { get; set; }
    public async Task<int> SaveChanges()
    {
        return await base.SaveChangesAsync();
    }

}