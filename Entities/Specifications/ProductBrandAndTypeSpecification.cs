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
        public ProductBrandAndTypeSpecification(ProductSpecParams productSpecParams)
            : base
            (c =>
            (string.IsNullOrEmpty(productSpecParams.Search)|| c.Name.ToLower().Contains(productSpecParams.Search)) &&
            (!productSpecParams.BrandId.HasValue || c.ProductBrandId == productSpecParams.BrandId) &&
            (!productSpecParams.TypeId.HasValue || c.ProductTypeId == productSpecParams.TypeId)
            )
        {
            AddIncludes(p=>p.ProductBrand);
            AddIncludes(p=>p.ProductType);
            AddOrderBy(x=>x.Name);
            ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);
            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch(productSpecParams.Sort)
                {
                    case "priceAsc": 
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
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
