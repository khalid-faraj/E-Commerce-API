using API.DTOs;
using AutoMapper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Helper
{
	public class ProductMapper : Profile
	{
		public ProductMapper() 
		{
			CreateMap<Product, ProductToReturnDTO>();
		}
	}
}
