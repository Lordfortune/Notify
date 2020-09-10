using System.Threading.Tasks;

namespace Notify.Bll.Interfaces
{
	public interface INotificationRequestProcessor
	{
		Task<bool> Process();
	}
}
