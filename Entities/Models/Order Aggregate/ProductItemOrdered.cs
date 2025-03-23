using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Order_Aggregate
{
	public class ProductItemOrdered
	{
        public ProductItemOrdered()
        {
            
        }
        public ProductItemOrdered(int productItemId, string productName, string picUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PicUrl = picUrl;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PicUrl { get; set; }

    }
}
