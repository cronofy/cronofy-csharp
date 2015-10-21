using System;

namespace Cronofy.Example
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			Console.Write("Enter access token: ");
			var accessToken = Console.ReadLine();

			Console.WriteLine();
			var client = new CronofyAccountClient(accessToken);

			Console.WriteLine("Fetching calendars...");
			var calendars = client.GetCalendars();

			Console.WriteLine();

			foreach (var calendar in calendars)
			{
				Console.WriteLine("{0} - {1} - {2} ({3})", calendar.CalendarId, calendar.Name, calendar.Profile.Name, calendar.Profile.ProviderName);
			}

			Console.WriteLine();

			Console.WriteLine("Fetching events...");
			var events = client.GetEvents();

			Console.WriteLine();

			foreach (var evt in events)
			{
				Console.WriteLine("{0} - {1}", evt.Start, evt.Summary);
			}

			Console.WriteLine();

			const string eventId = "CronofyExample";

			Console.WriteLine("Creating event with ID {0}", eventId);
			Console.WriteLine();

			Console.Write("Enter calendar ID: ");
			var calendarId = Console.ReadLine();
			Console.WriteLine();

			var eventBuilder = new UpsertEventRequestBuilder()
				.EventId(eventId)
				.Summary("Cronofy Example")
				.Description("Example from the Cronofy .NET SDK")
				.Start(new DateTimeOffset(2015, 10, 20, 17, 0, 0, new TimeSpan(0)))
				.End(new DateTimeOffset(2015, 10, 20, 17, 30, 0, new TimeSpan(0)));

			client.UpsertEvent(calendarId, eventBuilder);
			Console.WriteLine("Event upserted");
			Console.WriteLine();

			Console.WriteLine("Press enter to delete...");
			Console.ReadLine();

			client.DeleteEvent(calendarId, eventId);
			Console.WriteLine("Event deleted");
			Console.WriteLine();

			Console.WriteLine("Press enter to continue...");
			Console.ReadLine();
		}
	}
}
