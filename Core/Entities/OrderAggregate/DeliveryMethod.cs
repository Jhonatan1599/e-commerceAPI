
namespace Core.Entities.OrderAggregate
{   
    /// <summary>
    /// A class that represents a Delivery method
    /// </summary>
    public class DeliveryMethod : BaseEntity
    {
        // Delivery method name
        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }  
    }
}