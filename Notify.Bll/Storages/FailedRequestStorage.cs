using System.Collections.Generic;

namespace Notify.Bll.Storages
{
	public class FailedRequestStorage
	{
		private readonly List<int> _failedRequestIds = new List<int>();

		public void Add(int requestId)
		{
			_failedRequestIds.Add(requestId);
		}

		public bool HasFailedRequest(int requestId)
		{
			return _failedRequestIds.Contains(requestId);
		}
	}
}
