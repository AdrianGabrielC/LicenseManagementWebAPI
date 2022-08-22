using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Database
{
    public class DatabaseContext : DbContext 
    {
        public DbSet<Licenses> LicensesTable { get; set; }
        public DbSet<Products> ProductsTable { get; set; }
        public DbSet<Customers> CustomersTable { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
    }
}
