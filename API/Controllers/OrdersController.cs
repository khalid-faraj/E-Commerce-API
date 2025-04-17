using API.DTOs;
using API.Errors;
using API.Extentions;
using AutoMapper;
using Core.Identity;
using Core.RepositoriesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrdersController (IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder (OrderDTO orderDTO)
        {
            var email = HttpContext.User?.RetrieveEmailFromPrincipal();
            var address = _mapper.Map<AddressDTO, Core.Models.Order_Aggregate.Address>(orderDTO.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDTO.DeliveryMethodId, orderDTO.BasketId, address);
            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));
            return Ok(order);
        }
    }
}
