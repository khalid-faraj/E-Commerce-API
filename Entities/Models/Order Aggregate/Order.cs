using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Order_Aggregate
{
	public class Order
	{
        public Order() { }
        public Order(string buyerEmail, Address shippedToAddress, 
            DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems, 
            decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippedToAddress = shippedToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
        }
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public Address ShippedToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return SubTotal+DeliveryMethod.Price;
        }

    }
}
