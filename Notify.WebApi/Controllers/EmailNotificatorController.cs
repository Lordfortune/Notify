using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Notify.WebApi.Controllers
{
	[Route("api/[controller]/v1")]
	[ApiController]
	public class EmailNotificatorController : ControllerBase
	{
		[HttpPost("send-to-contact")]
		public Task SendToContact()
		{
			return Task.CompletedTask;
		}

		[HttpPost("send-to-person")]
		public Task SendToPerson()
		{
			return Task.CompletedTask;
		}
		
		[HttpPost("send-to-group")]
		public Task SendToGroup()
		{
			return Task.CompletedTask;
		}
	}
}
