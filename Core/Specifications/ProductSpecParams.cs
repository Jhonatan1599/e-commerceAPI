namespace Core.Specifications
{
    /// <summary>
    /// Represents the parameters used for filtering and pagination when querying products.
    /// </summary>
    public class ProductSpecParams
    {

        private const int MaxPageSize = 50;
        private int _pageSize = 6;
        private string _search;

        /// <summary>
        /// The index of the page to retrieve.
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// The number of products per page.
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }


        /// <summary>
        /// Filter Products by brand
        /// </summary>
        public int? BrandId { get; set; }

        /// <summary>
        /// Filter Products by type
        /// </summary>
        public int? TypeId { get; set; }

        /// <summary>
        /// Sort products by price e.j priceAsc or priceDesc
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Search Products by name.
        /// </summary>
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }


    }
}