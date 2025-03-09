using API.DTOs;
using API.Helper;
using AutoMapper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Helper
{
	public class ProductMapper : Profile
	{
		public ProductMapper() 
		{
			CreateMap<Product, ProductToReturnDTO>()
				.ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
				.ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
				.ForMember(d => d.PicUrl, o => o.MapFrom<ProductUrlResolver>());


		}
	}
}
