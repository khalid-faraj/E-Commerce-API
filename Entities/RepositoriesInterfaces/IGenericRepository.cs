using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RepositoriesInterfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T> GetByIdAsync(int id);
		Task<IReadOnlyList<T>> ListAllAsync();
	}
}
