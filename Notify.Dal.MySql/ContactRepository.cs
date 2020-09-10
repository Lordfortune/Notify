using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notify.Dal.Models;
using Notify.Dal.Repositories;

namespace Notify.Dal.Mysql
{
	public class ContactRepository : BaseRepository, IContactRepository
	{
		public ContactRepository(NotifyDbContext context) : base(context)
		{
		}

		public ContactRepository(string connectionString) : base(connectionString)
		{
		}

		public Task<ContactDal> GetAsync(int id)
		{
			return InContext(c => c.Contacts.FirstOrDefaultAsync(x => x.Id == id));
		}
	}
}
