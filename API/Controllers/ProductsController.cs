using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<ProductType> _productType;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo, 
            IGenericRepository<ProductType> productType, IMapper mapper )
        {
            _mapper = mapper;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;
            _productType = productType;
        }

        /// <summary>
        /// Retrieves a paginated list of products based on the specified parameters.
        /// </summary>
        /// <param name="productParams">The parameters for filtering, sorting, and paging the products.</param>
        /// <returns>An ActionResult containing the paginated list of products.</returns>
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {   
            // Specifications 
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            // Applying specifications to obtain the desire data
            var totalItems = await _productsRepo.CountAsync(countSpec);
            var products = await _productsRepo.ListAsync(spec);

            // Map the retrieved products to DTOs
            var data = _mapper
                .Map<IReadOnlyList<Product> , IReadOnlyList<ProductToReturnDto>>(products);

            // Return the paginated list of products
            return Ok( new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        /// <summary>
        /// Retrieves a product based on  an Id
        /// </summary>
        /// <param name="id">The Id Of the product to retrieve</param>
        /// <returns>An ActionResult containing the product</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            // Specification
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            //Applying specification
            var product =  await _productsRepo.GetEntityWithSpec(spec);
            
            if(product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(product);

        }

        /// <summary>
        ///  Retrieves a list of products by brand
        /// </summary>
        /// <param name="id">Brand Id</param>
        /// <returns>An ActionResult containing a list of ProductBrand</returns>
        [HttpGet("brands")]        
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands(int id)
        {
            return  Ok(await _productBrandRepo.ListAllAsync());
        }

        /// <summary>
        /// Retrieves a list products by type
        /// </summary>
        /// <param name="id">Type Id</param>
        /// <returns>An ActionResult containing a list of ProductBrand</returns>
        [HttpGet("types")]        
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes(int id)
        {
            return  Ok(await _productType.ListAllAsync());
        }

    }
}