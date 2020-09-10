using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notify.Dal.Models;
using Notify.Dal.Repositories;

namespace Notify.Dal.Mysql
{
	public class NotificatorContactRepository : BaseRepository, INotificatorContactRepository
	{
		public NotificatorContactRepository(NotifyDbContext context) : base(context)
		{
		}

		public NotificatorContactRepository(string connectionString) : base(connectionString)
		{
		}

		public Task<NotificatorContactDal> GetAsync(int id)
		{
			return InContext(c =>
			{
				return c.NotificatorContacts
					.Include(nc => nc.Notificator)
					.Include(nc => nc.Contact)
					.Where(nc => nc.Id == id)
					.FirstOrDefaultAsync();
			});
		}

		public Task<NotificatorContactDal> GetForNotificatorAndContact(int notificatorId, int contactId)
		{
			return InContext(c =>
			{
				return c.NotificatorContacts
					.Where(nc => nc.NotificatorId == notificatorId
					             & nc.ContactId == contactId)
					.FirstOrDefaultAsync();
			});
		}
	}
}
