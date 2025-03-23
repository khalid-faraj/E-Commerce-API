using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Order_Aggregate
{
	public class DeliveryMethod
	{
		public int Id { get; set; }
		public string ShortName { get; set; }
		public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}
