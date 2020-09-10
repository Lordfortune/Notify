using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notify.Dal.Models;
using Notify.Dal.Repositories;

namespace Notify.Dal.Mysql
{
	public class NotificationRequestRepository : BaseRepository, INotificationRequestRepository
	{
		public NotificationRequestRepository(NotifyDbContext context) : base(context)
		{
		}

		protected NotificationRequestRepository(string connectionString) : base(connectionString)
		{
		}
		public Task<NotificationRequestDal> CreateRequestAsync(NotificationRequestDal request)
		{
			return InContext(async c =>
			{
				var entity = await c.Requests.AddAsync(request);
				await c.SaveChangesAsync();

				return entity.Entity;
			});
		}

		public Task UpdateRequest(NotificationRequestDal request)
		{
			return InContext(c =>
			{
				c.Requests.Update(request);
				return c.SaveChangesAsync();
			});
		}

		public Task<NotificationRequestDal[]> GetUnprocessRequest(int limit)
		{
			return InContext(c =>
			{
				return c.Requests.Where(x => x.IsSuccess == null).Take(limit).ToArrayAsync();
			});
		}
	}
}
