using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {   
        /// <summary>
        /// Creates an order
        /// </summary>
        /// <param name="buyerEmail">The email of the Client</param>
        /// <param name="deliveryMethod">The delivery method</param>
        /// <param name="basketId">The id of the basket</param>
        /// <param name="shippingAddress">An object that contains info related to the Address</param>
        /// <returns></returns>
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, 
            Address shippingAddress);

        /// <summary>
        /// Get the Orders of a user
        /// </summary>
        /// <param name="buyerEmail">the buyer email</param>
        /// <returns>A list of Orders</returns>
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

        /// <summary>
        /// Get the Order of a user 
        /// </summary>
        /// <param name="id">The id of the order</param>
        /// <param name="buyerEmail">The email of the user</param>
        /// <returns></returns>
        Task<Order> GetOrderByIdAsync(int id, string buyerEmail);

        /// <summary>
        /// Get a list of available delivery methods
        /// </summary>
        /// <returns>A list of delivery methods</returns>
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAync();
    }
}