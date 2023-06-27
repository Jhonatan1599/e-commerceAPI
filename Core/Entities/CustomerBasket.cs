namespace Core.Entities
{
    /// <summary>
    /// A class that represents a basket of products for a user 
    /// </summary>
    public class CustomerBasket
    {   
        //This constructor is so that "Redis" does not throw any problems.
        public CustomerBasket()
        {
        }

        /// <summary>
        /// Initialize a CustomerBasket with an string Id 
        /// </summary>
        /// <param name="id">The Id Of the basket</param>
        public CustomerBasket(string id)
        {
            Id = id;            
        }

        /// <summary>
        /// The Id Of the basket
        /// </summary>
        public string Id {get; set;}

        /// <summary>
        /// A set of Items, each item is a product
        /// </summary>
        public List<BasketItem> Items {get; set;} = new List<BasketItem>();
    }
}