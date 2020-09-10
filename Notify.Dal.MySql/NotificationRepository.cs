using System.Threading.Tasks;
using Notify.Dal.Models;
using Notify.Dal.Repositories;

namespace Notify.Dal.Mysql
{
	public class NotificationRepository : BaseRepository, INotificationRepository
	{
		public NotificationRepository(NotifyDbContext context) : base(context)
		{
		}

		public NotificationRepository(string connectionString) : base(connectionString)
		{
		}

		public Task Create(NotificationDal notification)
		{
			return InContext(c =>
			{
				c.Notifications.Add(notification);
				return c.SaveChangesAsync();
			});
		}
	}
}
