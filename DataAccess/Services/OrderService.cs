using Core.Models;
using Core.Models.Order_Aggregate;
using Core.RepositoriesInterfaces;
using DataAccess.RepositoriesImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
	public class OrderService : IOrderService
	{
		private readonly IGenericRepository<Order> _orderRepo;
		private readonly IGenericRepository<DeliveryMethod> _deliveryRepo;
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IBasketRepository _basketRepo;
        public OrderService(IGenericRepository<Order> orderRepo, 
			IGenericRepository<DeliveryMethod> deliveryRepo,
            IGenericRepository<Product> productRepo,
            IBasketRepository basketRepo
            )
        {
            _orderRepo = orderRepo;
			_deliveryRepo = deliveryRepo;
			_productRepo = productRepo;
			_basketRepo = basketRepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
		{
			var basket = await _basketRepo.GetBasketAsync(basketId);
			var items = new List<OrderItem>();
			foreach (var item in basket.Items)
			{
				var productItem = await _productRepo.GetByIdAsync(item.Id);
				var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PicUrl);
				var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
				items.Add(orderItem);
			}
			var deliveryMethod = await _deliveryRepo.GetByIdAsync(deliveryMethodId);
			var subtotal = items.Sum(item => item.Price * item.Quantity);
			var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subtotal);
			return order;
		}

		public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string byuyerEmail)
		{
			throw new NotImplementedException();
		}
	}
}
