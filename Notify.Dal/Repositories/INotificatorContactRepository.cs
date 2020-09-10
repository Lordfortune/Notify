using System.Threading.Tasks;
using Notify.Dal.Models;

namespace Notify.Dal.Repositories
{
	public interface INotificatorContactRepository
	{
		Task<NotificatorContactDal> GetAsync(int id);
		Task<NotificatorContactDal> GetForNotificatorAndContact(int notificatorId, int contactId);
	}
}
