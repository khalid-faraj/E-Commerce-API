using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoriesInterfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T> GetByIdAsync(int id);
		Task<IReadOnlyList<T>> ListAllAsync();
		Task<T> GetEntityWithSpec(ISpecification<T> specification);
		Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);
		Task<int> CountAsync(ISpecification<T> specification);
	}
}
