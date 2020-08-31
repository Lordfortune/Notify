using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notify.Bll;

namespace Notify.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.ConfigureIoC(Configuration)
				.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
					{
						builder
							.AllowAnyOrigin()
							.AllowAnyHeader()
							.AllowAnyMethod();
					}))
				.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


			services.AddSwaggerDocument(config =>
			{
				config.PostProcess = document =>
				{
					document.Info.Version = "v1";
					document.Info.Title = "Notify service";
					document.Info.Description = "Notify service API";
				};
			});

			services.AddSingleton<NotifyListenersManager>();
			services.AddHostedService<NotifyListenersManager>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), appBuilder =>
			{
				appBuilder.UseResponseWrapper();
			});

			app.UseCors("CorsPolicy");

			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});

			app.UseMvc();

			app.UseOpenApi(config => config.PostProcess = (document, request) =>
			{
				// Change document server settings to public
				document.Host = ExtractHostWithPort(request);
				document.BasePath = ExtractPath(request);
			});

			app.UseSwaggerUi3(config =>
				config.TransformToExternalPath =
					(route, request) => ExtractPath(request) + route);
		}

		private string ExtractPort(HttpRequest request)
		{
			return request.Headers["X-Forwarded-Port"].FirstOrDefault() ?? new Uri(request.Headers["Referer"]).Port.ToString();
		}

		private string ExtractHost(HttpRequest request) =>
			request.Headers.ContainsKey("X-Forwarded-Host") ?
				new Uri($"{ExtractProto(request)}://{request.Headers["X-Forwarded-Host"].First()}").Host :
				request.Host.Host;

		private string ExtractHostWithPort(HttpRequest request)
		{
			var port = ExtractPort(request);
			return ExtractHost(request) + (string.IsNullOrEmpty(port) ? "" : $":{port}");
		}

		private string ExtractProto(HttpRequest request) =>
			request.Headers["X-Forwarded-Proto"].FirstOrDefault() ?? request.Protocol;

		private string ExtractPath(HttpRequest request) =>
			request.Headers.ContainsKey("X-Forwarded-Host") ?
				new Uri($"{ExtractProto(request)}://{request.Headers["X-Forwarded-Host"].First()}").AbsolutePath :
				string.Empty;
	}
}
