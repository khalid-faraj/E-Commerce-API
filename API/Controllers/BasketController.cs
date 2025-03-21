using API.DTOs;
using AutoMapper;
using Core.Models;
using Core.RepositoriesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static StackExchange.Redis.Role;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
			_mapper = mapper;

		}

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(string id)
		{
			var basket = await _basketRepository.GetBasketAsync(id);
			//Handling null.
			return Ok(basket ?? new CustomerBasket(id));
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync(CustomerBasketDTO basket)
		{
			var customerBasket = _mapper.Map<CustomerBasket>(basket);
			var updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket);
			return Ok(updatedBasket);
		}

		[HttpDelete]
		public async Task DeleteBasketAsync(string id)
		{
			await _basketRepository.DeleteBasketAsync(id);
		}
    }
}
