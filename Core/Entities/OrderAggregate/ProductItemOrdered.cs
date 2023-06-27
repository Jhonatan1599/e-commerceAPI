namespace Core.Entities.OrderAggregate
{
    /// <summary>
    /// A class that represents an item ordered by a user
    /// </summary>
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemId, string productName, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        /// <summary>
        /// Id of the product
        /// </summary>
        public int ProductItemId { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Url of the product
        /// </summary>
        public string PictureUrl { get; set; }
    }
}