using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {   
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products =  await _productsRepo.ListAsync(spec);
            

            return  Ok( _mapper.Map<IReadOnlyList<Product> , IReadOnlyList< ProductToReturnDto>>(products));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {   
            //Create an spec object that holds a criteria and a list of expressions
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            //Returns the entity to which the criteria and a list of specifications were applied
            var product =  await _productsRepo.GetEntityWithSpec(spec);
            
            if(product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(product);

        }

        [HttpGet("brands")]        
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands(int id)
        {
            return  Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]        
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes(int id)
        {
            return  Ok(await _productType.ListAllAsync());
        }

    }
}