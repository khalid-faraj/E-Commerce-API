using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using Entities.Models;
using Entities.RepositoriesInterfaces;
using Entities.Specifications;
namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IGenericRepository<ProductBrand> _productBrandRepo;
		private readonly IGenericRepository<ProductType> _productTypeRepo;
		private readonly IGenericRepository<Product> _productRepo;


		public ProductController(IGenericRepository<ProductBrand> productBrandRepo
			, IGenericRepository<ProductType> productTypeRepo
			, IGenericRepository<Product> productRepo) 
		{
			_productBrandRepo = productBrandRepo;
			_productRepo = productRepo;
			_productTypeRepo = productTypeRepo;
		}
		[HttpGet]
		public async Task<ActionResult<List<Product>>> GetProducts()
		{
			var spec = new ProductBrandAndTypeSpecification();
			var products = await _productRepo.ListAsync(spec);
			return Ok(products);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var spec = new ProductBrandAndTypeSpecification(id);
			var product = await _productRepo.GetEntityWithSpec(spec);
			return product;
		}
		[HttpGet("brands")]
		public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
		{
			var productBrands = await _productBrandRepo.ListAllAsync();
			return Ok(productBrands);
		}
		[HttpGet ("types")]
		public async Task<ActionResult<List<ProductType>>> GetProductTypes()
		{
			var productTypes = await _productTypeRepo.ListAllAsync();
			return Ok(productTypes);
		}
	}
}
