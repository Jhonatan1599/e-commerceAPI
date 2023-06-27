using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// Provides a mechanism to evaluate specifications and retrieve the corresponding query.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity the specification applies to.</typeparam>
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Gets the query resulting from applying the given specification to the input query.
        /// </summary>
        /// <param name="inputQuery">The input query to apply the specification to.</param>
        /// <param name="spec">The specification to apply.</param>
        /// <returns>The resulting query after applying the specification.</returns>
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> spec)
        {
                var query = inputQuery;

                if(spec.Criteria != null)
                {
                    query = query.Where(spec.Criteria);
                }

                if(spec.OrderBy != null)
                {
                    query = query.OrderBy(spec.OrderBy);
                }

                if(spec.OrderByDescending != null)
                {
                    query = query.OrderByDescending(spec.OrderByDescending);
                }

                if(spec.IsPagingEnabled)
                {
                    query = query.Skip(spec.Skip).Take(spec.Take);
                }

                // Include navigation properties
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include) );

                return query;
        }
    }
}