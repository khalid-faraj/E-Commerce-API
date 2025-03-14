using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Specifications
{
	public class ProductWithFiltersForCountSpecifications : BaseSpecification<Product>
	{
        public ProductWithFiltersForCountSpecifications(ProductSpecParams productSpecParams)
            : base(x => 
            (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId)&&
            (!productSpecParams.TypeId.HasValue || x.ProductTypeId==productSpecParams.TypeId))
        {
            
        }
    }
}
