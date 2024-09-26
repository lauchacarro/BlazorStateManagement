using BlazorStateManagement.Api.Entities;

using Microsoft.EntityFrameworkCore;

namespace BlazorStateManagement.Api.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }


    }
}
