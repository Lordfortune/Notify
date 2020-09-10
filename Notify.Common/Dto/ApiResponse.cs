using System.Runtime.Serialization;
using Notify.Common.Enums;

namespace Notify.Common.Dto
{
	[DataContract]
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

		[DataMember(Name = "message")]
		public string Message { get; set; }
		[DataMember(Name = "code")]
		public int Code { get; set; }
	}

	[DataContract]
	public class ApiResponse<T> : ApiResponse
	{
		public ApiResponse(T data) : base(ResponseCodeEnum.Success)
		{
			Data = data;
		}

		[DataMember(Name = "data")]
		public T Data { get; set; }
	}
}
