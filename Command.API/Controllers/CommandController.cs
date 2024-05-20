using ComandaPro.Domain.Extensions;
using Command.Domain.Dtos;
using Command.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Command.API.Controllers
{
	[ApiController]
	[Authorize]
	[Route("api/[controller]")]
	public class CommandController(ICommandService commandService) : Controller
	{
		[HttpPost]
		[Route("open/{number}")]
		[SwaggerOperation(Summary = "open command")]
		[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(CommandDto))]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> Open(int number)
		{
			var response = await commandService.OpenCommand(number, this.GetUserIdLogged());
			return Ok(response);
		}

		[HttpGet]
		[SwaggerOperation(Summary = "get open commands")]
		[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<CommandDto>))]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> GetOpenCommands()
		{
			var response = await commandService.GetOpenCommands();
			return Ok(response);
		}

		[HttpPost]
		[Route("close/{number}")]
		[SwaggerOperation(Summary = "close command")]
		[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(CommandDto))]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> Close(int number)
		{
			var response = await commandService.CloseCommand(number, this.GetAccessToken());
			return Ok(response);
		}
	}
}
