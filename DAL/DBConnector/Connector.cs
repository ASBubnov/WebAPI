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

       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            
            optionsBuilder.UseNpgsql("Host = 127.0.0.1; Port = 5432; Database = WebApiExample; User Id = postgres; Password = 0000;");
        }*/
    }
}
