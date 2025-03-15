using Entities.Models;
using Entities.RepositoriesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(string id)
		{
			var basket = await _basketRepository.GetBasketAsync(id);
			//Handling null.
			return Ok(basket ?? new CustomerBasket(id));
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync(CustomerBasket customerBasket)
		{
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
