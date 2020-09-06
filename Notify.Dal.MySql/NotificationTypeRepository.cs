using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notify.Dal.Models;
using Notify.Dal.Repositories;
using Notify.Common.Enums;

namespace Notify.Dal.Mysql
{
	public class NotificationTypeRepository : BaseRepository, INotificatorTypeRepository
	{
		public NotificationTypeRepository(NotifyDbContext context) : base(context)
		{
		}

		protected NotificationTypeRepository(string connectionString) : base(connectionString)
		{
		}

		public Task<NotificationTypeDal[]> GetAll()
		{
			return InContext(c => c.NotificationTypes.ToArrayAsync());
		}

		public Task<NotificationTypeDal> Get(NotificationTypeEnum id)
		{
			return InContext(c => c.NotificationTypes.FirstOrDefaultAsync(x => x.Id == id));
		}
	}
}
