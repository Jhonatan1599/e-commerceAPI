using Core.Entities;

namespace Core.Specifications
{
    /// <summary>
    /// Represents a specification for counting the number of products with filters applied.
    /// </summary>
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductWithFiltersForCountSpecification"/> class with the specified product parameters.
        /// </summary>
        /// <param name="productParams">The product parameters used for filtering, sorting, and pagination.</param>
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
            :  base(x => 
                //Searching by product name
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                //Filtering by brandId
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                //Filtering by TypeId
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {

        }
    }
}