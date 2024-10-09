namespace Cronofy.Example
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

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
            else if (args.Any(t => t == "attachments"))
            {
                AttachmentsExample();
                return;
            }
            else if (args.Any(t => t == "availability-rules"))
            {
                AvailabilityRulesExample();
                return;
            }

            Console.Write("Enter access token: ");
            var accessToken = Console.ReadLine();

            Console.WriteLine();
            var client = new CronofyAccountClient(accessToken);

            FetchAndPrintCalendars(client);
            FetchAndPrintEvents(client);

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

        /// <summary>
        /// Attachments usage example.
        /// </summary>
        private static void AttachmentsExample()
        {
            Console.Write("Enter Client id: ");
            var clientId = Console.ReadLine();
            Console.Write("Enter Secret: ");
            var clientSecret = Console.ReadLine();

            Console.WriteLine();
            var oaClient = new CronofyOAuthClient(clientId, clientSecret);

            Console.Write("Enter access token: ");
            var accessToken = Console.ReadLine();

            Console.WriteLine();
            var client = new CronofyAccountClient(accessToken);

            Console.WriteLine("Creating an example attachment");
            Console.WriteLine();

            var attachmentContent = Encoding.ASCII.GetBytes("Example file content");

            var attachment = oaClient.CreateAttachment(new Requests.CreateAttachmentRequest
            {
                Attachment = new Requests.CreateAttachmentRequest.AttachmentSummary
                {
                    FileName = "example.txt",
                    ContentType = "text/plain",
                    Base64Content = Convert.ToBase64String(attachmentContent),
                },
            });

            Console.WriteLine("Attachment created");
            Console.WriteLine();

            FetchAndPrintCalendars(client);

            const string EventId = "CronofyAttachmentExample";

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
                .Summary("Cronofy Attachment Example")
                .Description("Attachment example from the Cronofy .NET SDK")
                .AddAttachment(attachment.AttachmentId)
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
        /// Availability rules usage example.
        /// </summary>
        private static void AvailabilityRulesExample()
        {
            Console.Write("Enter access token: ");
            var accessToken = Console.ReadLine();

            Console.WriteLine();
            var client = new CronofyAccountClient(accessToken);

            FetchAndPrintCalendars(client);

            Console.Write("Enter calendar ID: ");
            var calendarId = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Creating an example availability rule");
            Console.WriteLine();

            const string AvailabilityRuleId = "CSharpExampleAvailabilityRule";

            client.UpsertAvailabilityRule(new AvailabilityRule
            {
                AvailabilityRuleId = AvailabilityRuleId,
                TimeZoneId = "America/Chicago",
                CalendarIds = new[] { calendarId },
                WeeklyPeriods = new[]
                {
                    new AvailabilityRule.WeeklyPeriod
                    {
                        Day = DayOfWeek.Monday,
                        StartTime = "09:30",
                        EndTime = "12:30",
                    },
                    new AvailabilityRule.WeeklyPeriod
                    {
                        Day = DayOfWeek.Monday,
                        StartTime = "14:00",
                        EndTime = "17:00",
                    },
                    new AvailabilityRule.WeeklyPeriod
                    {
                        Day = DayOfWeek.Wednesday,
                        StartTime = "09:30",
                        EndTime = "12:30",
                    },
                },
            });

            Console.WriteLine("Availability rule created");
            Console.WriteLine();

            Console.WriteLine("Fetching created availability rule...");
            var availabilityRule = client.GetAvailabilityRule(AvailabilityRuleId);

            Console.WriteLine();
            Console.WriteLine(availabilityRule.ToString());
            Console.WriteLine();

            Console.WriteLine("Fetching all availability rules...");
            var availabilityRules = client.GetAvailabilityRules();

            Console.WriteLine();

            foreach (var rule in availabilityRules)
            {
                Console.WriteLine(rule.ToString());
            }

            Console.WriteLine();

            Console.WriteLine("Press enter to delete the example rule...");
            Console.ReadLine();

            client.DeleteAvailabilityRule(AvailabilityRuleId);
            Console.WriteLine("Availability rule deleted");
            Console.WriteLine();

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        /// <summary>
        /// Fetches a list of all of the users calendars and prints a summary to the console.
        /// </summary>
        /// <param name="client">Account client to use to read the list of calendars.</param>
        private static void FetchAndPrintCalendars(CronofyAccountClient client)
        {
            Console.WriteLine("Fetching calendars...");
            var calendars = client.GetCalendars();

            Console.WriteLine();

            foreach (var calendar in calendars)
            {
                Console.WriteLine("{0} - {1} - {2} ({3})", calendar.CalendarId, calendar.Name, calendar.Profile.Name, calendar.Profile.ProviderName);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Fetches a list of all of the users events and prints a summary to the console.
        /// </summary>
        /// <param name="client">Account client to use to read the list of events.</param>
        private static void FetchAndPrintEvents(CronofyAccountClient client)
        {
            Console.WriteLine("Fetching events...");
            var events = client.GetEvents();

            Console.WriteLine();

            foreach (var evt in events)
            {
                Console.WriteLine("{0} - {1}", evt.Start, evt.Summary);
            }

            Console.WriteLine();
        }
    }
}
