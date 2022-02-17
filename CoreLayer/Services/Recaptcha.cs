using CoreLayer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class Recaptcha : IRecaptcha
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _accessor;
		public Recaptcha(IConfiguration configuration, IHttpContextAccessor accessor)
		{
			_configuration = configuration;
			_accessor = accessor;
		}
		public async Task<bool> IsSatisfy()
		{
			var secretkey = _configuration.GetSection("GoogleRecaptcha")["SecretKey"];
			var clientresponse = _accessor.HttpContext.Request.Form["g-recaptcha-response"];
			var httpClient = new HttpClient();
			var response = await httpClient.PostAsync(
				$" https://www.google.com/recaptcha/api/siteverify?secret={secretkey}&response={clientresponse}", null);

			if (response.IsSuccessStatusCode)
			{
				var GoogleResponse = JsonConvert.DeserializeObject<GoogleResponse>(await response.Content.ReadAsStringAsync());
				return GoogleResponse.Success;
			}

			return false;
		}

		public class GoogleResponse
		{
			[JsonProperty("success")]
			public bool Success { get; set; }
			[JsonProperty("challenge_ts")]
			public string Challenge_Ts { get; set; }
			[JsonProperty("hostname")]
			public string HostName { get; set; }
		}
	}
}
