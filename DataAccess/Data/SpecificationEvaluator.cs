using Entities.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
	public class SpecificationEvaluator <T> where T : class
	{
		public static IQueryable<T> GetQuary(IQueryable<T> inputQuery, ISpecification<T> spec)
		{
			var query = inputQuery;
			if (spec.Criteria != null)
			{
				query = query.Where(spec.Criteria);
			}
			if(spec.OrderBy!=null)
			{
				query = query.OrderBy(spec.OrderBy);	
			}
			if(spec.OrderByDescending != null)
			{
				query = query.OrderByDescending(spec.OrderByDescending);
			}
			query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
			return query;
		}
	}
}
