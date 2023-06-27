using Core.Entities;

namespace Core.Interfaces
{
    //IDisposable: when we've finished, our transaction is going to dispose of our context 
    /// <summary>
    /// Represents a unit of work for managing database operations.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the repository instance for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity.</typeparam>
        /// <returns>The repository instance for the specified entity type.</returns>
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Saves all changes made within the unit of work to the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result represents the number of changes made to the database.</returns>
        Task<int> Complete();
    }
}
/*Ef is going to track all of the changes to the entities, whether we add, remove,whatever we do inside
this unit of work, we're then  going to run the complete method and that's the part that's going to save
our changes to our database and return a number of changes  */