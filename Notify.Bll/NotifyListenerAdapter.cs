using System;

namespace Notify.Bll
{
	public class NotifyListenerAdapter
	{
		private static NotifyListenersManager _managerSync;
		private static NotifyListenersManager _manager;

		private static NotifyListenersManager Manager => _manager ?? CreateManager();

		private static NotifyListenersManager CreateManager()
		{
			lock (_managerSync)
			{
				return _manager ?? (_manager = new NotifyListenersManager());
			}
		}
	}
}
