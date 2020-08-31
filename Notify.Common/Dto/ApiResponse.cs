using Notify.Common.Enums;

namespace Notify.Common.Dto
{
	public class ApiResponse
	{
		public ApiResponse()
		{
		}

		public ApiResponse(ResponseCodeEnum code, string message = null)
		{
			Code = (int)code;
			Message = message;
		}

		public ApiResponse(int code, string message = null)
		{
			Code = code;
			Message = message;
		}

		public string Message { get; set; }
		public int Code { get; set; }
	}

	public class ApiResponse<T> : ApiResponse
	{
		public ApiResponse(T data) : base(ResponseCodeEnum.Success)
		{
			Data = data;
		}

		public T Data { get; set; }
	}
}
