using System.Threading.Tasks;
using Notify.Dal.Models;
using Notify.Dto;

namespace Notify.Bll.Interfaces
{
	public interface INotificationRequestManager
	{
		Task<NotificationRequestDal> CreateRequest(NotificationRequestDto request, string controllerName = null, string method = null);
		Task UpdateRequest(NotificationRequestDal request);
		Task<NotificationRequestDal[]> GetUnprocessRequest(int limit);
	}
}
