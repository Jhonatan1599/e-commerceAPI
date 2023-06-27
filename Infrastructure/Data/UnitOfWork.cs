using System.Collections;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data 
{
    /// <summary>
    /// Represents a unit of work implementation for managing database operations.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;

        
        /// <summary>
        /// A Hashtable to store the entity type name and the repository instance as a key/value
        /// </summary>
        private Hashtable _repositories;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class with the specified context.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        
        /// <summary>
        /// Saves all changes made within the unit of work to the database.
        /// </summary>
        /// <returns>The task result represents the number of changes made to the database.</returns>
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        
        /// <summary>
        /// Releases all resources used by the unit of work. This method inherits from <see cref="IDisposable"/>.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }


        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            //Checks if _repositories already contains a repository with this particular type
            if(!_repositories.ContainsKey(type))
            {   
                // Creates an open generic type of GenericRepository<>
                var repositoryType = typeof(GenericRepository<>);

                //This creates a new instance of GenericRepository<TEntity> with _context as its constructor argument.
                var repositoryInstance = Activator.CreateInstance(
                    /*This part generates a closed generic type based on the open generic type GenericRepository<> and the  TEntity type */
                    repositoryType.MakeGenericType(typeof(TEntity)), 
                    _context );
                
                // Add repository to the HashTable
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>) _repositories[type];
        }
    }
}