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
		    (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search)) &&
			(!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId)&&
            (!productSpecParams.TypeId.HasValue || x.ProductTypeId==productSpecParams.TypeId))
        {
            
        }
    }
}
