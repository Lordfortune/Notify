using System.Threading.Tasks;
using Notify.Common.Dto;
using Notify.Dal.Models;

namespace Notify.Bll.Notificators
{
	public class EmailNotificator : NotificatorBase
	{
		public override NotificationDal Notify(SendMessageDto request, ContactDal contact)
		{
			return null;
		}
	}
}
