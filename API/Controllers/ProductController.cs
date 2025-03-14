using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using Entities.Models;
using Entities.RepositoriesInterfaces;
using Entities.Specifications;
using API.DTOs;
using AutoMapper;
using API.Helper;
namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : BaseApiController
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
		public async Task<ActionResult<Pagination<ProductToReturnDTO>>> 
			GetProducts([FromQuery]ProductSpecParams productSpecParams)
		{
			var spec = new ProductBrandAndTypeSpecification(productSpecParams);
			var countSpec = new ProductWithFiltersForCountSpecifications(productSpecParams);
			var totalItems = await _productRepo.CountAsync(countSpec);
			var products = await _productRepo.ListAsync(spec);
			var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);
			return Ok(new Pagination<ProductToReturnDTO>(productSpecParams.PageIndex, productSpecParams.PageSize,totalItems,data));
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
