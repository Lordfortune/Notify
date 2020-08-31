using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notify.Dal.Models;
using Notify.Dal.Repositories;

namespace Notify.WebApi.Controllers
{
	[Route("api/[controller]/v1")]
	[ApiController]
	public class NotifyTypeController : ControllerBase
	{
		public NotifyTypeController(INotificatorTypeRepository repository)
		{
			_repository = repository;
		}

		private readonly INotificatorTypeRepository _repository;

		[HttpGet("list")]
		public Task<NotificatorTypeDal[]> GetAll()
		{
			return _repository.GetAll();
		}
	}
}
