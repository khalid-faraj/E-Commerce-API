using DataAccess.Data;
using Entities.RepositoriesInterfaces;
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

		public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

	}
}
