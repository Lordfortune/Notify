using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notify.Dal.Models;
using Notify.Dal.Repositories;

namespace Notify.Dal.Mysql
{
	public class ClientRepository : BaseRepository, IClientRepository
	{
		public ClientRepository(NotifyDbContext context) : base(context)
		{
		}

		public ClientRepository(string connectionString) : base(connectionString)
		{
		}

		public Task<ClientDal> GetByTokenAsync(string token)
		{
			return InContext(c =>
			{
				return c.Clients
					.Include(cl => cl.ClientNotificators)
					.ThenInclude(x => x.Notificator)
					.Where(sc => sc.Token == token)
					.FirstOrDefaultAsync();
			});
		}
	}
}
