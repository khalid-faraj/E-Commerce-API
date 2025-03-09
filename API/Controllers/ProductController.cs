using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using Entities.Models;
using Entities.RepositoriesInterfaces;
using Entities.Specifications;
using API.DTOs;
using AutoMapper;
namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IGenericRepository<ProductBrand> _productBrandRepo;
		private readonly IGenericRepository<ProductType> _productTypeRepo;
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IMapper _mapper;

		public ProductController(IGenericRepository<ProductBrand> productBrandRepo
			, IGenericRepository<ProductType> productTypeRepo
			, IGenericRepository<Product> productRepo
			, IMapper mapper)
		{
			_productBrandRepo = productBrandRepo;
			_productRepo = productRepo;
			_productTypeRepo = productTypeRepo;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<List<ProductToReturnDTO>>> GetProducts()
		{
			var spec = new ProductBrandAndTypeSpecification();
			var products = await _productRepo.ListAsync(spec);
			return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products));
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
		{
			var spec = new ProductBrandAndTypeSpecification(id);
			var product = await _productRepo.GetEntityWithSpec(spec);
			return _mapper.Map<Product, ProductToReturnDTO>(product);	
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
