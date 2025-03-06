using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using Entities.Models;
using Entities.RepositoriesInterfaces;
namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository _productRepository;
		public ProductController(IProductRepository productRepository) 
		{
			_productRepository = productRepository;
		}
		[HttpGet]
		public async Task<ActionResult<List<Product>>> GetProducts()
		{
			var products = await _productRepository.GetProductsAsync();
			return Ok(products);
		}
		[HttpGet("brands")]
		public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
		{
			var productBrands = await _productRepository.GetProductBrandsAsync();
			return Ok(productBrands);
		}
		[HttpGet ("types")]
		public async Task<ActionResult<List<ProductType>>> GetProductTypes()
		{
			var productTypes = await _productRepository.GetProductTypesAsync();
			return Ok(productTypes);
		}
	}
}
