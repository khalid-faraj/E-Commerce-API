using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Data
{
	public class ApplicationContextSeed
	{
		public static async Task SeedAsync(ApplicationContext context)
		{
			if(!context.ProductBrands.Any())
			{
				var BrandsData = File.ReadAllText("../DataAccess/Data/SeedData/brands.json");
			    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
				context.ProductBrands.AddRange(Brands);
			}
			if (!context.ProductTypes.Any())
			{
				var TypesData = File.ReadAllText("../DataAccess/Data/SeedData/types.json");
				var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
				context.ProductTypes.AddRange(Types);
			}
			if (!context.Products.Any())
			{
				var ProductsData = File.ReadAllText("../DataAccess/Data/SeedData/products.json");
				var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
				context.Products.AddRange(Products);
			}
			if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
		}
	}
}
