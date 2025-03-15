using Entities.Models;
using Entities.RepositoriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoriesImplementation
{
	public class BasketRepository : IBasketRepository
	{
		public Task<bool> DeleteBasketAsync(string basketId)
		{
			throw new NotImplementedException();
		}

		public Task<CustomerBasket> GetBasketAsync(string basketId)
		{
			throw new NotImplementedException();
		}

		public Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
		{
			throw new NotImplementedException();
		}
	}
}
