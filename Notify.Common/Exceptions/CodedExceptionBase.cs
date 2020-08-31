using System;
using Notify.Common.Enums;

namespace Notify.Common.Exceptions
{
	public abstract class CodedExceptionBase : Exception
	{
		public abstract ResponseCodeEnum ErrorCode { get; }
	}
}
