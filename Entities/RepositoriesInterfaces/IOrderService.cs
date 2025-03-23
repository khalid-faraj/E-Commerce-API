using Core.Models.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoriesInterfaces
{
	public interface IOrderService
	{
		Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress);
		Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string byuyerEmail);
		Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
		Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
	}
}
