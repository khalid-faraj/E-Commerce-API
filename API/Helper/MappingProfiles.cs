using API.DTOs;
using API.Helper;
using AutoMapper;
using Core.Identity;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Helper
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles() 
		{
			CreateMap<Product, ProductToReturnDTO>()
				.ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
				.ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
				.ForMember(d => d.PicUrl, o => o.MapFrom<ProductUrlResolver>());

			CreateMap<Address, AddressDTO>().ReverseMap();
			CreateMap<CustomerBasketDTO, CustomerBasket>().ReverseMap();
			CreateMap<BasketItemDTO, BasketItem>().ReverseMap();
			CreateMap<AddressDTO, Core.Models.Order_Aggregate.Address>();

		}
	}
}
