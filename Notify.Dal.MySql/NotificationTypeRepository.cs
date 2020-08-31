using System.Linq;
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

		public Task<NotificatorTypeDal[]> GetAll()
		{
			return InContext(c => c.NotificatorTypes.ToArrayAsync());
		}

		public Task<NotificatorTypeDal[]> GetList(string nameFilter)
		{
			return InContext(c => c.NotificatorTypes.Where(x => x.Name.Contains(nameFilter)).ToArrayAsync());
		}

		public Task<NotificatorTypeDal> Get(NotificationTypeEnum id)
		{
			return InContext(c => c.NotificatorTypes.FirstOrDefaultAsync(x => x.Id == id));
		}

		public Task<NotificatorTypeDal> GetBySlug(string slug)
		{
			return InContext(c => c.NotificatorTypes.FirstOrDefaultAsync(x => x.Slug == slug));
		}

		public Task<NotificatorTypeDal> GetByName(string name)
		{
			return InContext(c => c.NotificatorTypes.FirstOrDefaultAsync(x => x.Name == name));
		}
	}
}
