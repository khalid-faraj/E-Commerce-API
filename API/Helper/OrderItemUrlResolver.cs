using API.DTOs;
using AutoMapper;
using Core.Models.Order_Aggregate;

namespace API.Helper
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration _config;
        public OrderItemUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrdered.PicUrl))
            {
                return _config["ApiUrl"] + source.ProductItemOrdered.PicUrl;
            }
            return null;
        }
    }
}
