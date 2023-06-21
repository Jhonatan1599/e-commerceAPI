using Core.Entities;

namespace Core.Interfaces
{   
    //IDisposable: when we've finished, our transaction is going to dispose of our context 
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        //returns the number of changes to our database
        Task<int> Complete();
    }
}
/*Ef is going to track all of the changes to the entities, whether we add, remove,whatever we do inside
this unit of work, we're then  going to run the complete method and that's the part that's going to save
our changes to our database and return a number of changes  */