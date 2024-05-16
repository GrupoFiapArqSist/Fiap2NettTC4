using ComandaPro.Domain.Extensions;
using Command.Domain.Dtos;
using Command.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Command.API.Controllers
{
	public class CommandController(ICommandService commandService) : Controller
	{
		[HttpGet]
		[Route("open")]
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
		[Route("close")]
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
