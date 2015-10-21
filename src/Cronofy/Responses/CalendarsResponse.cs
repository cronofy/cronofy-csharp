using System;
using Newtonsoft.Json;

namespace Cronofy.Responses
{
	internal sealed class CalendarsResponse
	{
		[JsonProperty("calendars")]
		public CalendarResponse[] Calendars { get; set; }

		internal sealed class CalendarResponse
		{
			[JsonProperty("provider_name")]
			public string ProviderName { get; set; }

			[JsonProperty("profile_id")]
			public string ProfileId { get; set; }

			[JsonProperty("profile_name")]
			public string ProfileName { get; set; }

			[JsonProperty("calendar_id")]
			public string CalendarId { get; set; }

			[JsonProperty("calendar_name")]
			public string CalendarName { get; set; }

			[JsonProperty("calendar_readonly")]
			public bool CalendarReadonly { get; set; }

			[JsonProperty("calendar_deleted")]
			public bool CalendarDeleted { get; set; }

			public Calendar ToCalendar()
			{
				var profile = new Profile {
					ProviderName = this.ProviderName,
					ProfileId = this.ProfileId,
					Name = this.ProfileName,
				};

				return new Calendar {
					Profile = profile,
					CalendarId = this.CalendarId,
					Name = this.CalendarName,
					ReadOnly = this.CalendarReadonly,
					Deleted = this.CalendarDeleted,
				};
			}
		}
	}
}
