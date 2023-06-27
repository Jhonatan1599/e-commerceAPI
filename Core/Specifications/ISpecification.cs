using System.Linq.Expressions;

namespace Core.Specifications
{   
    /// <summary>
    /// Represents a specification interface for querying entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity</typeparam>
    public interface ISpecification<T>
    {   
        /// <summary>
        /// Gets the criteria expression used to filter entities.
        /// </summary>
        Expression<Func<T,bool>> Criteria {get;}

        /// <summary>
        /// Gets a list of expressions used to include related entities.
        /// </summary>
        List<Expression<Func<T,object>>> Includes {get; }

        /// <summary>
        /// Gets the expression used to specify the ordering of entities in ascend order.
        /// </summary>
        Expression<Func<T,object>> OrderBy {get;}

        /// <summary>
        /// Gets the expression used to specify the ordering of entities in descent order.
        /// </summary>
        Expression<Func<T,object>> OrderByDescending {get;}

        /// <summary>
        /// Gets the maximum number of entities to take.
        /// </summary>
        int Take {get;}

        /// <summary>
        /// Gets the number of entities to skip.
        /// </summary>
        int Skip {get;}

        /// <summary>
        /// Gets a value indicating whether paging is enabled.
        /// </summary>
        bool IsPagingEnabled {get;}
    }
}