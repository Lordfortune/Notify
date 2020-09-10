using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FT.Extending;
using FT.Extending.Abstractions;
using Microsoft.Extensions.Logging;
using Notify.Bll.Notificators;
using Notify.Common.Dto;
using Notify.Dal.Models;
using Notify.Dal.Repositories;

namespace Notify.Bll
{
	public class NotificatorsManagerService : CustomizeAbstractContinuousHostedService
	{
		public NotificatorsManagerService(
			ILogger<NotificatorsManagerService> logger,
			INotificatorRepository notificatorRepository,
			IContactRepository contactRepository,
			IServiceProvider serviceProvider,
			IMapper mapper)
			: base(logger)
		{
			_notificatorRepository = notificatorRepository;
			_contactRepository = contactRepository;
			_serviceProvider = serviceProvider;
			_mapper = mapper;
		}

		private readonly IMapper _mapper;
		private readonly IServiceProvider _serviceProvider;
		private readonly IContactRepository _contactRepository;
		private readonly INotificatorRepository _notificatorRepository;
		private readonly List<NotificatorBase> _notificators = new List<NotificatorBase>();

		protected override bool LogStartIteration => true;
		protected override int IterationInterval => 10000;

		public async Task Send(SendMessageDto request)
		{
			var notificator = _notificators.FirstOrDefault(x => x.Id == request.NotificatorId);
			if (notificator is null)
			{
				return;
			}

			var contact = await _contactRepository.GetAsync(request.ContactId);

			notificator.Notify(request, contact);
		}

		protected override async Task ProcessingAsync()
		{
			var notificators = await _notificatorRepository.GetAll();

			foreach (var notificatorDal in notificators)
			{
				var existingNotificator = _notificators.FirstOrDefault(x => x.Id == notificatorDal.Id);
				if (existingNotificator != null)
				{
					continue;
				}

				TryCreateNotificator(notificatorDal);
			}
		}

		private void TryCreateNotificator(NotificatorDal notificatorDal)
		{
			var bllNotificator = _mapper.Map<NotificatorBase>(notificatorDal);
			bllNotificator.Init(notificatorDal);

			_notificators.Add(bllNotificator);
			Logger.LogTrace($"Added new notificator {notificatorDal.ToJson()}");
		}
	}
}
