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
        public ProductBrandAndTypeSpecification()
        {
            AddIncludes(p=>p.ProductBrand);
            AddIncludes(p=>p.ProductType);
            
        }
        public ProductBrandAndTypeSpecification(int id): base(p => p.Id == id)
        {
			AddIncludes(p => p.ProductBrand);
			AddIncludes(p => p.ProductType);
		}
    }
}
