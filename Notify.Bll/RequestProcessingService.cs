using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FT.Extending.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notify.Bll.Interfaces;

namespace Notify.Bll
{
	public class RequestProcessingService : CustomizeAbstractContinuousHostedService
	{
		public RequestProcessingService(
			IServiceProvider serviceProvider,
			ILogger<RequestProcessingService> logger)
			: base(logger)
		{
			_serviceProvider = serviceProvider;
		}

		private int _iterationInterval = 0;
		private readonly IServiceProvider _serviceProvider;
		protected override int IterationInterval => _iterationInterval;

		protected override async Task ProcessingAsync()
		{
			using var scope = _serviceProvider.CreateScope();
			var requestProcessor = scope.ServiceProvider.GetService<INotificationRequestProcessor>();

			//Если обработано хоть что-то, следующую обработку запускать сразу
			_iterationInterval = await requestProcessor.Process() ? 0 : 300;
		}
	}
}
