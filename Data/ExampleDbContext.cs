using MVC_CORE.Models;

namespace MVC_CORE.Data
{
    /// <summary>
    /// ExampleDbContext class to handle the database connection and the tables in the database.
    /// </summary>
    public class ExampleDbContext : DbContext
    {
        /// <summary>
        /// OnConfiguring method to configure the database connection.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}MVCExampleDb.db");
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// AuthUser table in the database.
        /// </summary>
        public DbSet<AuthUser>? AuthUser { get; set; }

        /// <summary>
        /// Employees tables in the database.
        /// </summary>
        public DbSet<Employees>? Employees { get; set; }
        public DbSet<EmployeePhones>? EmployeePhones { get; set; }
        public DbSet<EmployeeAddresses>? EmployeeAddresses { get; set; }
    }
}
