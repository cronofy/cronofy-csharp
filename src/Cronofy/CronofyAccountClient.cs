using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Cronofy.Responses;
using Cronofy.Requests;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;

namespace Cronofy
{
	public sealed class CronofyAccountClient
	{
		private const string ReadEventsUrl = "https://api.cronofy.com/v1/events";
		
		private readonly string accessToken;

		public CronofyAccountClient(string accessToken)
		{
			this.accessToken = accessToken;
			this.HttpClient = new ConcreteHttpClient();
		}

		/// <summary>
		/// Gets or sets the HTTP client.
		/// </summary>
		/// <value>
		/// The HTTP client.
		/// </value>
		/// <remarks>
		/// Intend for test purposes only.
		/// </remarks>
		internal IHttpClient HttpClient { get; set; }

		public IEnumerable<Event> GetEvents()
		{
			var request = new HttpRequest();

			request.Method = "GET";
			request.Url = ReadEventsUrl;
			request.Headers = new Dictionary<string, string> {
				{ "Authorization", "Bearer " + this.accessToken },
			};
			request.QueryString = new Dictionary<string, string> {
				{ "tzid", "Etc/UTC" },
			};

			var response = HttpClient.GetResponse(request);
			var readEventsResponse = JsonConvert.DeserializeObject<ReadEventsResponse>(response.Body);

			return readEventsResponse.Events.Select(e => e.ToEvent());
		}
	}
}
