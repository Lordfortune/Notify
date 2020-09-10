using System.Threading.Tasks;
using Notify.Dal.Models;

namespace Notify.Dal.Repositories
{
	public interface IContactRepository
	{
		Task<ContactDal> GetAsync(int id);
	}
}
