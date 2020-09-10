using System.Threading.Tasks;
using Notify.Dal.Models;

namespace Notify.Dal.Repositories
{
	public interface INotificationRequestRepository
	{
		Task<NotificationRequestDal> CreateRequestAsync(NotificationRequestDal request);
		Task UpdateRequest(NotificationRequestDal request);
		Task<NotificationRequestDal[]> GetUnprocessRequest(int limit);
	}
}
