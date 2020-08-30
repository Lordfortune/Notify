using System.Threading.Tasks;
using Notify.Dal.Models;
using Notify.Enums;

namespace Notify.Dal.Repositories
{
	public interface INotificatorTypeRepository
	{
		Task<NotificatorTypeDal[]> GetAll();
		Task<NotificatorTypeDal[]> GetList(string nameFilter);

		Task<NotificatorTypeDal> Get(NotificationTypeEnum id);
		Task<NotificatorTypeDal> GetBySlug(string slug);
		Task<NotificatorTypeDal> GetByName(string name);
	}
}