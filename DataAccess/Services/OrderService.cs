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
		private readonly IUnitOfWork _unitOfWork;	
		private readonly IBasketRepository _basketRepo;
        public OrderService(
            IUnitOfWork unitOfWork,
            IBasketRepository basketRepo
            )
        {
			_unitOfWork = unitOfWork;
			_basketRepo = basketRepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
		{
			var basket = await _basketRepo.GetBasketAsync(basketId);
			var items = new List<OrderItem>();
			foreach (var item in basket.Items)
			{
				var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
				var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PicUrl);
				var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
				items.Add(orderItem);
			}
			var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
			var subtotal = items.Sum(item => item.Price * item.Quantity);
			var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subtotal);
			_unitOfWork.Repository<Order>().Add(order);
			var result = await _unitOfWork.Complete();
			await _basketRepo.DeleteBasketAsync(basketId);
			if (result <= 0) return null;
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
