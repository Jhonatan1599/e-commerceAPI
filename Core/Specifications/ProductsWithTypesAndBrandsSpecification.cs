using Core.Entities;
namespace Core.Specifications
{
    /// <summary>
    /// Represents a specification for querying products with their associated types and brands.
    /// </summary>
    public class ProductsWithTypesAndBrandsSpecification :BaseSpecification<Product>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsWithTypesAndBrandsSpecification"/> class with the specified product parameters.
        /// </summary>
        /// <param name="productParams">The product parameters used for filtering, sorting, and pagination.</param>
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) 
            : base(x =>
                //Searching by product name
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                //Filtering by brandId
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                //Filtering by TypeId
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {
            // Including navigation properties
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);

            // Applying OrderBy Name
            AddOrderBy(x => x.Name);
            // Applying pagination
            ApplyPaging(productParams.PageSize*(productParams.PageIndex -1), productParams.PageSize );

            // Applying OrderBy Price
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsWithTypesAndBrandsSpecification"/> class with the specified product ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}