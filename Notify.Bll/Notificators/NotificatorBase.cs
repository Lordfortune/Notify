using Notify.Common.Dto;
using Notify.Dal.Models;

namespace Notify.Bll.Notificators
{
	public abstract class NotificatorBase
	{
		public int Id => Data.Id;
		public NotificatorDal Data { get; private set; }

		public virtual void Init(NotificatorDal data)
		{
			Data = data;
		}

		public abstract NotificationDal Notify(SendMessageDto request, ContactDal contact);
	}
}
