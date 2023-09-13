using Microsoft.EntityFrameworkCore;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DAL.DBConnector
{
    public class DBMyContext : DbContext
    {
        public DbSet<TaskApp> TasksApp { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Pipeline> Pipelines { get; set; }
        public DBMyContext(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();            
        }
    }
}
