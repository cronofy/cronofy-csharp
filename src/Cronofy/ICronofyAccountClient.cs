using System;
using System.Collections.Generic;
using Cronofy.Requests;

namespace Cronofy
{
	public interface ICronofyAccountClient
	{
		IEnumerable<Calendar> GetCalendars();
		IEnumerable<Event> GetEvents();
		void UpsertEvent(string calendarId, UpsertEventRequestBuilder builder);
		void UpsertEvent(string calendarId, UpsertEventRequest eventRequest);
		void DeleteEvent(string calendarId, string eventId);
	}
}
