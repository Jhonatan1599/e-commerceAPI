using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>
    /// An interface that defines a Basket
    /// </summary>
    public interface IBasketRepository
    {
        /// <summary>
        /// Get basket by Id
        /// </summary>
        /// <param name="basketId">The basket Id</param>
        /// <returns>A basket of products</returns>
        Task<CustomerBasket> GetBasketAsync(string basketId);

        /// <summary>
        /// Creates/Updates a basket
        /// </summary>
        /// <param name="basket">A basket of products</param>
        /// <returns>A basket of products</returns>
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);

        /// <summary>
        /// Deletes a basket
        /// </summary>
        /// <param name="basketId">The id of the basket</param>
        /// <returns>A boolean</returns>
        Task<bool> DeleteBasketAsync(string basketId);
        
    }
}