using System.Threading.Tasks;
using Notify.Dal.Models;
using Notify.Common.Enums;

namespace Notify.Dal.Repositories
{
	public interface INotificatorRepository
	{
		Task<NotificatorDal[]> GetAll();
		Task<NotificatorDal[]> GetList(string nameFilter);
		Task<NotificatorDal[]> GetForType(NotificationTypeEnum typeId);
		Task<NotificatorDal> GetAsync(int id);
		Task<NotificatorDal> GetBySlugAsync(string slug);
	}
}
