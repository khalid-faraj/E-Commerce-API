using API.DTOs;
using AutoMapper;
using Entities.Models;

namespace API.Helper
{
	public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
	{
		private readonly IConfiguration _config;
		public ProductUrlResolver(IConfiguration config)
		{
			_config = config;
		}

		public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
		{
			if(!string.IsNullOrEmpty(source.PicUrl))
			{
				return _config["ApiUrl"] + source.PicUrl;
			}
			return null;
		}
	}
}
