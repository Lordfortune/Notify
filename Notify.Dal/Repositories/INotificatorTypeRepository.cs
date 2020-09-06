using System.Threading.Tasks;
using Notify.Dal.Models;
using Notify.Common.Enums;

namespace Notify.Dal.Repositories
{
	public interface INotificatorTypeRepository
	{
		Task<NotificationTypeDal[]> GetAll();
		Task<NotificationTypeDal> Get(NotificationTypeEnum id);
	}
}
