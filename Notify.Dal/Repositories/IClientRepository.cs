using System.Threading.Tasks;
using Notify.Dal.Models;

namespace Notify.Dal.Repositories
{
	public interface IClientRepository
	{
		Task<ClientDal> GetByTokenAsync(string token);
	}
}
