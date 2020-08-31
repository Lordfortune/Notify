using System;

namespace Notify.WebApi.Exceptions
{
	public class HttpResponseException : Exception
	{
		public HttpResponseException() { }
		public HttpResponseException(int statusCode)
		{
			StatusCode = statusCode;
		}

		public int StatusCode { get; set; }
	}
}
