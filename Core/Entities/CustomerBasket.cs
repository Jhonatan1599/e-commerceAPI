namespace Core.Entities
{
    public class CustomerBasket
    {   
        //This constructor is so that "Redis" does not throw any problems.
        public CustomerBasket()
        {
        }

        public CustomerBasket(string id)
        {
            Id = id;            
        }

        public string Id {get; set;}
        public List<BasketItem> Items {get; set;} = new List<BasketItem>();
    }
}