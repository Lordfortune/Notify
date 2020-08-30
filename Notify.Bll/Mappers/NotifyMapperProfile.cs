using AutoMapper;
using Notify.Bll.Notificators;
using Notify.Dal.Models;

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