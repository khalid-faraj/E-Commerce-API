using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess.Data
{
	public class ApplicationContext : DbContext
	{
        public ApplicationContext()
        {
            
        }
        public ApplicationContext(DbContextOptions options): base(options) 
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
