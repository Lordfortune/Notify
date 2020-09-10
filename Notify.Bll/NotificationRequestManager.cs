using System.Threading.Tasks;
using AutoMapper;
using FT.Extending;
using Microsoft.Extensions.Logging;
using Notify.Bll.Interfaces;
using Notify.Dal.Models;
using Notify.Dal.Repositories;
using Notify.Dto;

namespace Notify.Bll
{
	public class NotificationRequestManager : INotificationRequestManager
	{
		public NotificationRequestManager(
			IMapper mapper,
			ILogger<NotificationRequestManager> logger,
			INotificationRequestRepository repository)
		{
			_mapper = mapper;
			_logger = logger;
			_repository = repository;
		}

		private readonly IMapper _mapper;
		private readonly ILogger<NotificationRequestManager> _logger;
		private readonly INotificationRequestRepository _repository;

		public async Task<NotificationRequestDal> CreateRequest(NotificationRequestDto request, string controllerName = null, string method = null)
		{
			_logger.LogTrace($"Notify requested: {request.ToJson()}");

			var dal = _mapper.Map<NotificationRequestDal>(request);
			var methodName = $"{controllerName ?? "unknow"}.{method ?? "unknow"}";
			dal.Method = methodName;
			dal.Comment = "Создано";

			var created = await _repository.CreateRequestAsync(dal);

			_logger.LogTrace($"Notify-request created: {created.ToJson()}");
			return created;
		}

		public Task UpdateRequest(NotificationRequestDal request)
		{
			return _repository.UpdateRequest(request);
		}

		public Task<NotificationRequestDal[]> GetUnprocessRequest(int limit)
		{
			return _repository.GetUnprocessRequest(limit);
		}
	}
}
