﻿using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BuggyController : BaseApiController
	{
		private readonly ApplicationContext _context;
        public BuggyController(ApplicationContext context)
        {
            _context = context;
        }

		[HttpGet("notfound")]
		public IActionResult GetNotFoundRequest()
		{
			var thing = _context.Products.Find(50);
			if (thing == null) {return NotFound();}
			return Ok();
		}

		[HttpGet("servererror")]
		public IActionResult GetServerError()
		{
			var thing = _context.Products.Find(50);
			var thingToReturn = thing.ToString();
			return Ok();
		}
		[HttpGet("badrequest")]
		public IActionResult GetBadRequest()
		{
			return BadRequest();
		}
		[HttpGet("badrequest/{id}")]
		public IActionResult GetBadRequest(int id)
		{
			return Ok();
		}
    }
}
