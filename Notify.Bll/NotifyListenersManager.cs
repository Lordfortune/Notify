using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Notify.Bll
{
	public class NotifyListenersManager : IHostedService
	{
		public NotifyListenersManager(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		private readonly IServiceProvider _serviceProvider;

		public Task StartAsync(CancellationToken cancellationToken)
		{
			Console.WriteLine("NotifyListenersManager.StartAsync");

			while (!cancellationToken.IsCancellationRequested)
			{
				Console.WriteLine("NotifyListenersManager.StartAsync...");
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