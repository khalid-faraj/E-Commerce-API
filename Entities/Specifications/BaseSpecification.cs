using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Specifications
{
	internal class BaseSpecification<T> : ISpecification<T>
	{
        public BaseSpecification(Expression<Func<T, bool>> filter)
		{
            Filter = filter;
        }
        public Expression<Func<T, bool>> Filter { get; }

		public List<Expression<Func<T, object>>> Includes { get; } =
			new List<Expression<Func<T, object>>>();

		protected void AddIncludes(Expression<Func<T, object>> IncludeExpression)
		{
			Includes.Add(IncludeExpression);
		}
	}
}
