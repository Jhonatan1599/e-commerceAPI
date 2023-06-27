using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{   
    /// <summary>
    /// Represent a generic repository interface 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : BaseEntity
    {   
        /// <summary>
        /// Retrieves an entity by its Id
        /// </summary>
        /// <param name="id">The Id Of the entity</param>
        /// <returns>A entity. </returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all the entities
        /// </summary>
        /// <returns>A list of entities</returns>
        Task<IReadOnlyList<T>> ListAllAsync();   
        
        /// <summary>
        /// Retrieves an entity with a specific specification.
        /// </summary>
        /// <param name="spec">The specification to filter the entity.</param>
        /// <returns>An entity.</returns>
        Task<T> GetEntityWithSpec(ISpecification<T> spec);

        /// <summary>
        /// Retrieves a list of entities, each one with their specification.
        /// </summary>
        /// <param name="spec">The specification to filter the entities</param>
        /// <returns>A list of entities</returns>
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        /// <summary>
        /// Counts the entities based on a specification
        /// </summary>
        /// <param name="spec">The specification to filter the entities</param>
        /// <returns>Total of entities counted</returns>
        Task<int> CountAsync(ISpecification<T> spec);

        /// <summary>
        /// Adds an entity
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(T entity);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);
    }
}