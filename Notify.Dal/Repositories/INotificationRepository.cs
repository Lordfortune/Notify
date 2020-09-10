using System.Threading.Tasks;
using Notify.Dal.Models;

namespace Notify.Dal.Repositories
{
	public interface INotificationRepository
	{
		Task Create(NotificationDal notification);
	}
}
