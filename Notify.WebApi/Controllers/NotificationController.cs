using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notify.Bll.Interfaces;
using Notify.Common.Dto;
using Notify.Dal.Models;
using Notify.Dto;
using NSwag.Annotations;

namespace Notify.WebApi.Controllers
{
	[Route("api/[controller]/v1")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		public NotificationController(INotificationRequestManager manager)
		{
			_manager = manager;
		}

		private readonly INotificationRequestManager _manager;

		[HttpPost("send")]
		[SwaggerResponse(System.Net.HttpStatusCode.OK, typeof(ApiResponse<NotificationRequestDal>))]
		public Task<NotificationRequestDal> Send(NotificationRequestDto request)
		{
			return _manager.CreateRequest(request, nameof(NotificationController), nameof(Send));
		}
	}
}
