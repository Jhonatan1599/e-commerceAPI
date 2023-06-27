using System.Linq.Expressions;

namespace Core.Specifications
{
    /// <summary>
    ///  Represents a specification class for querying entities
    /// </summary>
    /// <typeparam name="T">The type of the entity</typeparam>
    public class BaseSpecification<T> : ISpecification<T>
    {   
        /// <summary>
        /// Initializes a new instance of <see cref="BaseSpecification{T}"/>
        /// </summary>
        public BaseSpecification()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of  <see cref="BaseSpecification{T}"/>
        /// </summary>
        /// <param name="criteria">The criteria expression used to filter entities.</param>
        public BaseSpecification(Expression<Func<T,bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria {get;}

        public List<Expression<Func<T, object>>> Includes {get;} = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy {get; private set;}

        public Expression<Func<T, object>> OrderByDescending  {get; private set;}

        public int Take  {get; private set;}

        public int Skip  {get; private set;}

        public bool IsPagingEnabled  {get; private set;}


        /// <summary>
        /// Adds an include expression to include related entities.
        /// </summary>
        /// <param name="includeExpression">The expression used to include related entities</param>
        protected void AddInclude(Expression<Func<T,object>> includeExpression)
        {
            //add include statements to the Includes list
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// Adds an expression to specify the ordering of entities in ascending order.
        /// </summary>
        /// <param name="orderByExpression">The expression used to specify the ordering of entities.</param>
        protected void AddOrderBy( Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        /// <summary>
        /// Adds an expression to specify the ordering of entities in descending order.
        /// </summary>
        /// <param name="orderByDescExpression">The expression used to specify the ordering of entities in descending order</param>
        protected void AddOrderByDescending( Expression<Func<T, object>> orderByDescExpression)
        {
            OrderBy = orderByDescExpression;
        }

        /// <summary>
        /// Applies paging to the query.
        /// </summary>
        /// <param name="skip">The number of entities to skip.</param>
        /// <param name="take">The maximum number of entities to take.</param>
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}