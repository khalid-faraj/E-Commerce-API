using API.DTOs;
using API.Helper;
using AutoMapper;
using Core.Identity;
using Core.Models;
using Core.Models.Order_Aggregate;
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

			CreateMap<Core.Identity.Address, AddressDTO>().ReverseMap();
			CreateMap<CustomerBasketDTO, CustomerBasket>().ReverseMap();
			CreateMap<BasketItemDTO, BasketItem>().ReverseMap();
			CreateMap<AddressDTO, Core.Models.Order_Aggregate.Address>();
            CreateMap<Order, OrderToReturnDTO>()
            .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));

			CreateMap<OrderItem, OrderItemDTO>()
				.ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductItemOrdered.ProductItemId))
				.ForMember(d => d.ProductName, o => o.MapFrom(s => s.ProductItemOrdered.ProductName))
				.ForMember(d => d.PicUrl, o => o.MapFrom(s => s.ProductItemOrdered.PicUrl))
                .ForMember(d => d.PicUrl, o => o.MapFrom<OrderItemUrlResolver>());

        }
    }
}
