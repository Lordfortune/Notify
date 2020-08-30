using System.Threading.Tasks;
using Notify.Dal.Models;
using Notify.Enums;

namespace Notify.Dal.Repositories
{
	public interface INotificatorRepository
	{
		Task<NotificatorDal[]> GetAll();
		Task<NotificatorDal[]> GetList(string nameFilter);
		Task<NotificatorDal[]> GetForType(NotificationTypeEnum typeId);
		Task<NotificatorDal> Get(int id);
		Task<NotificatorDal> GetBySlug(string slug);
		Task<NotificatorDal> GetByName(string name);
	}
}