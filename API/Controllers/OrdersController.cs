using API.Dtos;
using API.Errors;
using API.Extentions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{   
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates an Order of a current logged in user
        /// </summary>
        /// <param name="orderDto">An object that contains info about the order</param>
        /// <returns>The order</returns>
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {   
            //get the email from the user
            var email = HttpContext.User.RetrieveEmailFromPricipal();

            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId,orderDto.basketId,
                address);
            
            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);
        }

        /// <summary>
        /// Retrieves the orders of a current logged in user
        /// </summary>
        /// <returns>An ActionResult of a list of Orders</returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            // get the email of the current logged in user
            var email = HttpContext.User.RetrieveEmailFromPricipal();

            var orders = await _orderService.GetOrdersForUserAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }


        /// <summary>
        /// Retrieves an order of a current logged in user by Id
        /// </summary>
        /// <param name="id">The Id Of the order</param>
        /// <returns>An order</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPricipal();

            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<OrderToReturnDto>(order);
        }

        /// <summary>
        /// Retrieves a list of delivery methods, the user must log-in
        /// </summary>
        /// <returns></returns>
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAync());
        }
        
    }
}