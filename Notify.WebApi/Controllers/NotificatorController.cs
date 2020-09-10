using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notify.Bll;
using Notify.Common.Dto;
using Notify.Dal.Models;
using Notify.Dal.Repositories;

namespace Notify.WebApi.Controllers
{
	[Route("api/[controller]/v1")]
	[ApiController]
	public class NotificatorController : ControllerBase
	{
		public NotificatorController(
			INotificatorRepository repository,
			NotificatorsManagerService manager)
		{
			_manager = manager;
			_repository = repository;
		}

		private readonly INotificatorRepository _repository;
		private readonly NotificatorsManagerService _manager;

		[HttpGet("{id}")]
		public Task<NotificatorDal> Get([FromRoute] int id)
		{
			return _repository.GetAsync(id);
		}

		[HttpPost("send")]
		public async Task Send(SendMessageDto request)
		{
			await _manager.Send(request);
		}
	}
}
