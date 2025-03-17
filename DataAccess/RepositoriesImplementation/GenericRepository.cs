using DataAccess.Data;
using Core.RepositoriesInterfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoriesImplementation
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly ApplicationContext _context;
        public GenericRepository(ApplicationContext context)
        {
			_context = context;
        }

		

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<T> GetEntityWithSpec(ISpecification<T> specification)
		{
			return await ApplySpecification(specification).FirstOrDefaultAsync();
		}

		public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
		{
			return await ApplySpecification(specification).ToListAsync();
		}
		public async Task<int> CountAsync(ISpecification<T> specification)
		{
			return await ApplySpecification(specification).CountAsync();
		}
		private IQueryable<T> ApplySpecification(ISpecification<T> specification)
		{
			return SpecificationEvaluator<T>.GetQuary(_context.Set<T>().AsQueryable(), specification);
		}
	}
}
