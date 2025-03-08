using Entities.RepositoriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoriesImplementation
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		public Task<T> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<T>> ListAllAsync()
		{
			throw new NotImplementedException();
		}
	}
}
