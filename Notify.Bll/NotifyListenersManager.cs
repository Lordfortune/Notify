using System;
using System.Threading.Tasks;
using FT.Extending.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notify.Common.Dto;
using Notify.Dal.Repositories;

namespace Notify.Bll
{
	public class NotifyListenersManager : CustomizeAbstractContinuousHostedService
	{

		public NotifyListenersManager(
			ILogger<NotifyListenersManager> logger,
			INotificatorTypeRepository repository,
			IServiceProvider serviceProvider)
			: base(logger)
		{
			_repository = repository;
			_serviceProvider = serviceProvider;
		}

		private readonly IServiceProvider _serviceProvider;
		private readonly INotificatorTypeRepository _repository;

		public void Send(SendMessageDto request)
		{
			Console.WriteLine($"NotifyListenersManager.Send, {request.NotificatorId}, {request.Message}");

			using var scope = _serviceProvider.CreateScope();
			var repo = scope.ServiceProvider.GetService<INotificatorTypeRepository>();
			var types = repo.GetAll().Result;
			types = _repository.GetAll().Result;
		}

		protected override async Task ProcessingAsync()
		{
			//Console.WriteLine($"NotifyListenersManager.StartAsync... {Id}");
			await Task.Delay(500);
		}
	}
}
