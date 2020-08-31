using System;
using System.IO;
using System.Threading.Tasks;
using FT.Extending;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Notify.Common.Dto;
using Notify.Common.Enums;
using Notify.Common.Exceptions;
using Notify.WebApi.Exceptions;
using LiteException = FT.Extending.LiteException;

namespace Notify.WebApi
{
	public static class ResponseWrapperMiddleware
	{
		public static void UseResponseWrapper(this IApplicationBuilder app)
		{
			app.Use(Middleware);
		}

		private static async Task Middleware(HttpContext context, Func<Task> next)
		{
			var existingBody = context.Response.Body;
			ApiResponse resp;

			try
			{
				using var newBody = new MemoryStream();
				context.Response.Body = newBody;

				await next();
				context.Response.CheckHttpStatus();

				newBody.Seek(0, SeekOrigin.Begin);

				using var reader = new StreamReader(newBody);
				var content = reader.ReadToEnd();

				if (content.IsNullOrEmpty())
				{
					resp = new ApiResponse();
				}
				else if (context.Response.ContentType.StartsWith("text/plain"))
				{
					resp = new ApiResponse<object>(content);
				}
				else
				{
					resp = new ApiResponse<object>(JsonConvert.DeserializeObject(content));
				}
			}
			catch (CodedExceptionBase e)
			{
				resp = new ApiResponse(e.ErrorCode, e.Message);
			}
			catch (LiteException e)
			{
				resp = new ApiResponse(ResponseCodeEnum.UnknowError, e.Message);
			}
			catch (HttpResponseException e)
			{
				resp = new ApiResponse(ResponseCodeEnum.HttpError, e.Message);
			}
			catch (NotImplementedException e)
			{
				resp = new ApiResponse(ResponseCodeEnum.NotImplementation, $"Not implemented feature: {e.Message}");
			}
			catch (Exception e)
			{
				resp = new ApiResponse(ResponseCodeEnum.UnknowError, e.GetShortTextWithInner());
			}

			context.Response.Body = existingBody;
			var newContent = resp.ToJson();
			await context.Response.WriteAsync(newContent);
		}
	}
}
