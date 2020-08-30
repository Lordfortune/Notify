using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Notify.WebApi.Controllers
{
	[Route("api/[controller]/v1")]
	[ApiController]
	public class TestController : ControllerBase
	{
		[HttpGet("test-ok")]
		public Task<string> Test()
		{
			return Task.FromResult("OK");
		}
	}
}
