using AutoMapper;
using FT.Extending;
using Notify.Bll.Notificators;
using Notify.Common.Enums;
using Notify.Dal.Models;
using Notify.Dal.Models.Email;
using Notify.Dal.Models.Telegram;
using Notify.Dto;

namespace Notify.Bll.Mappers
{
	public class NotifyMapperProfile : Profile
	{
		public NotifyMapperProfile()
		{
			CreateMap<TelegramNotificatorDal, NotificatorBase>()
				.ConvertUsing(x => new TelegramNotificator());

			CreateMap<EmailNotificatorDal, NotificatorBase>()
				.ConvertUsing(x => new EmailNotificator());

			CreateMap<NotificationRequestDto, NotificationRequestDal>();

			CreateMap<NotificationTypeEnum, NotificationDal>()
				.ConvertUsing(x => x == NotificationTypeEnum.Telegram
					? new TelegramNotificationDal().As<NotificationDal>()
					: new EmailNotificationDal().As<NotificationDal>());
		}
	}
}
