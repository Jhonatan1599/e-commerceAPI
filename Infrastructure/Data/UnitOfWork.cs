using System.Collections;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data 
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;

        /*Now, as we initialize a unit of work, we cannot create a new instance of our StoreContext and any 
        repositories that we use inside this unit of work are going to be stored inside this Hashtable.*/

        //the key represents the entity type name, and the value represents the repository instance.
        private Hashtable _repositories;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        /*save the changes made in the _context by calling _context.SaveChangesAsync(). 
        It returns a Task<int> representing the number of affected entities.*/
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        //The Dispose() method is used to clean up and dispose of resources associated with the _context.
        //This method inherits from IDisposable
        public void Dispose()
        {
            _context.Dispose();
        }

        //This method is responsible for providing the repositories associated with different entity types.
        //Returns a new instance of IGenericRepository<TEntity>
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            //Checks if _repositories already contains a repository with this particular type
            if(!_repositories.ContainsKey(type))
            {   
                // Create a open generic type of GenericRepository<>
                var repositoryType = typeof(GenericRepository<>);

                //This creates a new instance of GenericRepository<TEntity> with _context as its constructor argument.
                var repositoryInstance = Activator.CreateInstance(
                    /*This part generates a closed generic type based on the open generic type GenericRepository<> and the type TEntity-->*/
                    repositoryType.MakeGenericType(typeof(TEntity)), 
                    _context );
                
                // Add repository to the HashTable
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>) _repositories[type];
        }
    }
}