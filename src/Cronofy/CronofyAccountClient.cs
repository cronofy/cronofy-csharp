using System;
using System.Collections;
using System.Collections.Generic;
using Cronofy.Responses;
using Cronofy.Requests;
using Cronofy;
using Newtonsoft.Json;
using System.Linq;

namespace Cronofy
{
	public sealed class CronofyAccountClient : ICronofyAccountClient
	{
		private const string CalendarsUrl = "https://api.cronofy.com/v1/calendars";
		private const string ReadEventsUrl = "https://api.cronofy.com/v1/events";
		private const string ManagedEventUrlFormat = "https://api.cronofy.com/v1/calendars/{0}/events";
		
		private readonly string accessToken;

		public CronofyAccountClient(string accessToken)
		{
			Preconditions.NotEmpty("accessToken", accessToken);

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

		/// <inheritdoc />
		public IEnumerable<Calendar> GetCalendars()
		{
			var request = new HttpRequest();

			request.Method = "GET";
			request.Url = CalendarsUrl;

			request.Headers = new Dictionary<string, string> {
				{ "Authorization", "Bearer " + this.accessToken },
			};

			var calendarsResponse = HttpClient.GetJsonResponse<CalendarsResponse>(request);

			return calendarsResponse.Calendars.Select(c => c.ToCalendar());
		}

		/// <inheritdoc />
		public IEnumerable<Event> GetEvents()
		{
			var request = new HttpRequest();

			request.Method = "GET";
			request.Url = ReadEventsUrl;

			request.Headers = new Dictionary<string, string> {
				{ "Authorization", "Bearer " + this.accessToken },
			};

			// TODO Support parameters
			request.QueryString = new Dictionary<string, string> {
				{ "tzid", "Etc/UTC" },
				{ "localized_times", "true" },
			};

			// Eagerly fetch the first page to hit access token and validation issues.
			var response = HttpClient.GetJsonResponse<ReadEventsResponse>(request);

			return new GetEventsIterator(this.HttpClient, this.accessToken, response);
		}

		/// <inheritdoc />
		public void UpsertEvent(string calendarId, UpsertEventRequestBuilder eventBuilder)
		{
			Preconditions.NotEmpty("calendarId", calendarId);
			Preconditions.NotNull("eventBuilder", eventBuilder);

			var request = eventBuilder.Build();
			UpsertEvent(calendarId, request);
		}

		/// <inheritdoc />
		public void UpsertEvent(string calendarId, UpsertEventRequest eventRequest)
		{
			Preconditions.NotEmpty("calendarId", calendarId);
			Preconditions.NotNull("eventRequest", eventRequest);

			var request = new HttpRequest();

			request.Method = "POST";
			request.Url = string.Format(ManagedEventUrlFormat, calendarId);
			request.Headers = new Dictionary<string, string> {
				{ "Authorization", "Bearer " + this.accessToken },
				{ "Content-Type", "application/json; charset=utf-8" },
			};

			request.Body = JsonConvert.SerializeObject(eventRequest);

			var response = HttpClient.GetResponse(request);

			if (response.Code != 202) {
				// TODO More useful exceptions
				throw new ApplicationException("Request failed");
			}
		}

		/// <inheritdoc />
		public void DeleteEvent(string calendarId, string eventId)
		{
			Preconditions.NotEmpty("calendarId", calendarId);
			Preconditions.NotEmpty("eventId", eventId);

			var request = new HttpRequest();

			request.Method = "DELETE";
			request.Url = string.Format(ManagedEventUrlFormat, calendarId);
			request.Headers = new Dictionary<string, string> {
				{ "Authorization", "Bearer " + this.accessToken },
				{ "Content-Type", "application/json; charset=utf-8" },
			};

			var requestBody = new { event_id = eventId };

			request.Body = JsonConvert.SerializeObject(requestBody);

			var response = HttpClient.GetResponse(request);

			if (response.Code != 202) {
				// TODO More useful exceptions
				throw new ApplicationException("Request failed");
			}
		}

		internal sealed class GetEventsIterator : IEnumerable<Event>
		{
			private readonly IHttpClient httpClient;
			private readonly string accessToken;
			private readonly ReadEventsResponse firstPage;

			public GetEventsIterator(IHttpClient httpClient, string accessToken, ReadEventsResponse firstPage)
			{
				this.httpClient = httpClient;
				this.accessToken = accessToken;
				this.firstPage = firstPage;
			}

			public IEnumerator<Event> GetEnumerator()
			{
				return this.GetEvents().GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			private IEnumerable<Event> GetEvents()
			{
				return GetPages().SelectMany(EventsFromPage);
			}

			private IEnumerable<ReadEventsResponse> GetPages()
			{
				var currentPage = firstPage;

				while (true)
				{
					yield return currentPage;

					if (currentPage.Pages.NextPageUrl == null)
					{
						break;
					}

					currentPage = GetNextPageResponse(currentPage);
				}
			}

			private ReadEventsResponse GetNextPageResponse(ReadEventsResponse page)
			{
				var request = new HttpRequest();

				request.Method = "GET";
				request.Url = page.Pages.NextPageUrl;
				request.Headers = new Dictionary<string, string> {
					{ "Authorization", "Bearer " + this.accessToken },
				};

				return this.httpClient.GetJsonResponse<ReadEventsResponse>(request);
			}

			private static IEnumerable<Event> EventsFromPage(ReadEventsResponse response)
			{
				return response.Events.Select(e => e.ToEvent());
			}
		}
	}
}
