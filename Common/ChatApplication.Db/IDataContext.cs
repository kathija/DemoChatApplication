using ChatApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Db
{
    public interface IDataContext
    {
        DbSet<User> User { get; set; }

        Task<int> SaveChanges();
    }
}
