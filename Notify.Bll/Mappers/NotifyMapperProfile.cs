using AutoMapper;
using Notify.Bll.Notificators;
using Notify.Dal.Models;
using Notify.Dal.Models.Email;
using Notify.Dal.Models.Telegram;

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
		}
	}
}
