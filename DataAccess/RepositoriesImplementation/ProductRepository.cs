using DataAccess.Data;
using Entities.Models;
using Entities.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoriesImplementation
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

		public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
		{
			return await _context.ProductBrands.ToListAsync();
		}

		public async Task<Product> GetProductByIdAsync(int id)
		{
			return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<IReadOnlyList<Product>> GetProductsAsync()
		{
			return await _context.Products
				.Include(p=>p.ProductBrand)
				.Include(p => p.ProductType)
				.ToListAsync();
		}

		public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
		{
			return await _context.ProductTypes.ToListAsync();
		}
	}
}
