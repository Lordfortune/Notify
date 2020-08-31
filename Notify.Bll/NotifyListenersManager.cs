using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Notify.Common.Dto;

namespace Notify.Bll
{
	public class NotifyListenersManager : IHostedService
	{
		public NotifyListenersManager()
		{
		}

		private Guid Id { get; } = Guid.NewGuid();
		private readonly IServiceProvider _serviceProvider;

		public void Send(SendMessageDto request)
		{
			Console.WriteLine($"NotifyListenersManager.Send {Id}, {request.NotificatorId}, {request.Message}");
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			Console.WriteLine($"NotifyListenersManager.StartAsync {Id}");

			while (!cancellationToken.IsCancellationRequested)
			{
				Console.WriteLine($"NotifyListenersManager.StartAsync... {Id}");
				Thread.Sleep(500);
			}

			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
