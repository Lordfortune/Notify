using Notify.Common.Dto;
using Notify.Dal.Models;
using Notify.Dal.Models.Telegram;
using Telegram.Bot;

namespace Notify.Bll.Notificators
{
	public class TelegramNotificator : NotificatorBase
	{
		private bool _isInit = false;
		private TelegramBotClient _client;
		private TelegramNotificatorDal _notificator;
		private TelegramNotificatorSettingsDal _settings;

		public override void Init(NotificatorDal data)
		{
			base.Init(data);
			_notificator = (TelegramNotificatorDal)Data;

			//_client = new TelegramBotClient(_notificator.Settings.Token);

			_isInit = true;
		}

		public override NotificationDal Notify(SendMessageDto request, ContactDal contact)
		{
			if (!_isInit)
			{

			}

			return null;
		}
	}
}
