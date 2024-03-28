using Microsoft.EntityFrameworkCore;
using MVC_CORE.Models;
using MVC_CORE.Models.Employee;

namespace MVC_CORE.Data
{
    public class ExampleDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}MVCExampleDb.db");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<AuthUser>? AuthUser { get; set; }

        public DbSet<Employees>? Employees { get; set; }
        public DbSet<EmployeePhones>? EmployeePhones { get; set; }
        public DbSet<EmployeeAddresses>? EmployeeAddresses { get; set; }
    }
}
