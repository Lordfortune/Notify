using Microsoft.AspNetCore.Http;
using Notify.WebApi.Exceptions;

namespace Notify.WebApi
{
	public static class HttpResponseExtention
	{
		public static void CheckHttpStatus(this HttpResponse response)
		{
			if (response.StatusCode >= 300 || response.StatusCode < 200)
			{
				throw new HttpResponseException(response.StatusCode);
			}
		}
	}
}
