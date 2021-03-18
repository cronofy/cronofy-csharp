namespace Cronofy.Example
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Example program for interacting with the Cronofy API.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The entry point of the program, where the program control starts and
        /// ends.
        /// </summary>
        /// <param name="args">
        /// The command-line arguments.
        /// </param>
        public static void Main(string[] args)
        {
            if (args.Any(t => t == "real-time-scheduling"))
            {
                RealTimeSchedulingExample();
                return;
            }
            else if (args.Any(t => t == "add-to-calendar"))
            {
                AddToCalendarExample();
                return;
            }

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

            const string EventId = "CronofyExample";

            Console.WriteLine("Creating event with ID {0}", EventId);
            Console.WriteLine();

            Console.Write("Enter calendar ID: ");
            var calendarId = Console.ReadLine();
            Console.WriteLine();

            var tomorrow = DateTime.Today.AddDays(1);
            var start = tomorrow.AddHours(17);
            var end = start.AddMinutes(30);

            var eventBuilder = new UpsertEventRequestBuilder()
                .EventId(EventId)
                .Summary("Cronofy Example")
                .Description("Example from the Cronofy .NET SDK")
                .Start(start)
                .End(end);

            client.UpsertEvent(calendarId, eventBuilder);
            Console.WriteLine("Event upserted");
            Console.WriteLine();

            Console.WriteLine("Press enter to delete...");
            Console.ReadLine();

            client.DeleteEvent(calendarId, EventId);
            Console.WriteLine("Event deleted");
            Console.WriteLine();

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        /// <summary>
        /// Add To Calendar example.
        /// </summary>
        private static void AddToCalendarExample()
        {
            Console.Write("Enter Client id: ");
            var clientId = Console.ReadLine();
            Console.Write("Enter Secret: ");
            var clientSecret = Console.ReadLine();

            string redirectUrl = "http://example.com/redirectUri";
            string scope = "read_events create_event";

            string eventId = "testEventId";
            string summary = "Test Summary";
            DateTimeOffset start = DateTime.Now;
            DateTimeOffset end = DateTime.Now + new TimeSpan(2, 0, 0);

            var client = new CronofyOAuthClient(clientId, clientSecret);

            var upsertEventRequest = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Start(start)
                .End(end)
                .Build();

            var request = new AddToCalendarRequestBuilder()
                .OAuthDetails(redirectUrl, scope)
                .UpsertEventRequest(upsertEventRequest)
                .Build();

            var actualUrl = client.AddToCalendar(request);
            Console.WriteLine(actualUrl);

            Process.Start(actualUrl);
        }

        /// <summary>
        /// Real-Time Scheduling example.
        /// </summary>
        private static void RealTimeSchedulingExample()
        {
            Console.Write("Enter Client id: ");
            var clientId = Console.ReadLine();
            Console.Write("Enter Secret: ");
            var clientSecret = Console.ReadLine();
            Console.Write("Enter Account id for availability: ");
            var sub = Console.ReadLine();
            Console.Write("Enter calendar id for availability: ");
            var calendarId = Console.ReadLine();

            string redirectUrl = "http://example.com/redirectUri";
            string scope = "read_events create_event";

            string eventId = "testEventId";
            string summary = "Test Summary";
            DateTimeOffset start = DateTime.Now;
            DateTimeOffset end = DateTime.Now + new TimeSpan(2, 0, 0);

            var client = new CronofyOAuthClient(clientId, clientSecret);

            var upsertEventRequest = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Build();

            var availabilityRequest = new AvailabilityRequestBuilder()
                .AddParticipantGroup(new ParticipantGroupBuilder().AddMember(sub).AllRequired())
                .AddAvailablePeriod(start, end)
                .RequiredDuration(60)
                .Build();

            var request = new RealTimeSchedulingRequestBuilder()
                .OAuthDetails(redirectUrl, scope)
                .Timezone("Etc/UTC")
                .UpsertEventRequest(upsertEventRequest)
                .AvailabilityRequest(availabilityRequest)
                .AddTargetCalendar(sub, calendarId)
                .Build();

            var actualUrl = client.RealTimeScheduling(request);
            Console.WriteLine(actualUrl);

            Process.Start(actualUrl);
        }
    }
}
