using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper )
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get basket by Id 
        /// </summary>
        /// <param name="id">The basket Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }

        /// <summary>
        /// Creates/Updates a basket
        /// </summary>
        /// <param name="basket">The basket to create/update</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {   
            var CustomerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var updatedBasket = await _basketRepository.UpdateBasketAsync(CustomerBasket);

            return Ok(updatedBasket);
        }
        
        /// <summary>
        /// Deletes a basket
        /// </summary>
        /// <param name="id">The Id of the basket to delete</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}