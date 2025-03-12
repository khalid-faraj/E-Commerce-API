using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Specifications
{
	public class ProductBrandAndTypeSpecification : BaseSpecification<Product>
	{
        public ProductBrandAndTypeSpecification(string sort)
        {
            AddIncludes(p=>p.ProductBrand);
            AddIncludes(p=>p.ProductType);
            AddOrderBy(x=>x.Name);
            if (!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "priceAsc": 
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDes":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }
        public ProductBrandAndTypeSpecification(int id): base(p => p.Id == id)
        {
			AddIncludes(p => p.ProductBrand);
			AddIncludes(p => p.ProductType);
		}
    }
}
