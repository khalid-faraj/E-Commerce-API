using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
	public class Product
	{
	
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string PicUrl { get; set; }

		public ProductType ProductType { get; set; }
		public int ProductTypeId { get; set; }

		public ProductBrand ProductBrand { get; set; }
		public int ProductBrandId { get; set; }

	}
}
