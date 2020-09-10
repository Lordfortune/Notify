using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FT.Extending;
using Microsoft.Extensions.Logging;
using Notify.Bll.Interfaces;
using Notify.Bll.Storages;
using Notify.Dal.Models;
using Notify.Dal.Repositories;

namespace Notify.Bll
{
	public class NotificationRequestProcessor : INotificationRequestProcessor
	{
		public NotificationRequestProcessor(
			IMapper mapper,
			IContactRepository contactRepository, 
			FailedRequestStorage failedRequestStorage,
			IClientRepository clientRepository,
			INotificationRequestManager requestManager,
			INotificatorRepository notificatorRepository,
			ILogger<NotificationRequestProcessor> logger,
			INotificatorContactRepository notificatorContactRepository, INotificationRepository notificationRepository)
		{
			_mapper = mapper;
			_logger = logger;
			_requestManager = requestManager;
			_clientRepository = clientRepository;
			_contactRepository = contactRepository;
			_failedRequestStorage = failedRequestStorage;
			_notificatorRepository = notificatorRepository;
			_notificationRepository = notificationRepository;
			_notificatorContactRepository = notificatorContactRepository;
		}

		private readonly int _limitForProcessRequest = 10;

		private readonly IMapper _mapper;
		private readonly IClientRepository _clientRepository;
		private readonly IContactRepository _contactRepository;
		private readonly FailedRequestStorage _failedRequestStorage;
		private readonly INotificationRequestManager _requestManager;
		private readonly INotificatorRepository _notificatorRepository;
		private readonly ILogger<NotificationRequestProcessor> _logger;
		private readonly INotificationRepository _notificationRepository;
		private readonly INotificatorContactRepository _notificatorContactRepository;

		public async Task<bool> Process()
		{
			var newRequests = await _requestManager.GetUnprocessRequest(_limitForProcessRequest);
			if (!newRequests.Any())
			{
				return false;
			}

			foreach (var request in newRequests)
			{
				await TryProcessRequest(request);
			}

			return true;
		}

		private async Task TryProcessRequest(NotificationRequestDal request)
		{
			try
			{
				if (_failedRequestStorage.HasFailedRequest(request.Id))
				{
					return;
				}

				await ProcessRequest(request);
			}
			catch (Exception e)
			{
				try
				{
					request.IsSuccess = false;
					request.Comment = e.GetShortText();

					_logger.LogError(e, $"Error on process messaging request #{request.Id} ({request.ToJson()}). {e.GetFullTextWithInner()}");
					await _requestManager.UpdateRequest(request);
				}
				catch
				{
					_failedRequestStorage.Add(request.Id);
				}
			}
		}

		private async Task ProcessRequest(NotificationRequestDal request)
		{
			_logger.LogTrace($"Request #{request.Id}");

			var client = await _clientRepository.GetByTokenAsync(request.ClientToken);
			if (client is null)
			{
				throw new LiteException($"Client {request.ClientToken} is not found");
			}

			ContactDal contact = null;
			NotificatorDal notificator = null;
			NotificatorContactDal notificatorContact = null;

			if (!request.Notificator.IsNullOrWhiteSpace())
			{
				notificator = await _notificatorRepository.GetBySlugAsync(request.Notificator);
				if (notificator is null)
				{
					throw new LiteException($"Notificator {request.Notificator} is not found");
				}

				contact = await _contactRepository.GetAsync(request.ContactId);
				if (contact is null)
				{
					throw new LiteException($"Contact #{request.ContactId} is not found");
				}

				notificatorContact = await _notificatorContactRepository.GetForNotificatorAndContact(notificator.Id, contact.Id);
				if (notificatorContact is null)
				{
					throw new LiteException($"NotificatorContact for notificator #{notificator.Id} and contact #{contact.Id} is not found");
				}
			}
			else
			{
				notificatorContact = await _notificatorContactRepository.GetAsync(request.ContactId);
				if (notificatorContact is null)
				{
					throw new LiteException($"NotificatorContact #{request.ContactId} is not found");
				}

				notificator = notificatorContact.Notificator;
				contact = notificatorContact.Contact;
			}

			if (notificator.TypeId != contact.TypeId)
			{
				throw new LiteException("Type of contact and type of notificator is not equals");
			}

			if (!notificator.IsActive
			    || !contact.IsActive
			    || !notificatorContact.IsActive)
			{
				throw new LiteException($"Part of notify data is not active (notificator: {notificator.IsActive}, contact: {contact.IsActive}, notificatorContact: {notificatorContact.IsActive})");
			}

			if (!client.ClientNotificators.Exists(x => x.NotificatorId == notificator.Id))
			{
				throw new LiteException($"Client {client.Name} not linked for notificator {notificator.Name}");
			}

			var notification = _mapper.Map<NotificationDal>(notificator.TypeId);
			await _notificationRepository.Create(notification);
		}
	}
}
