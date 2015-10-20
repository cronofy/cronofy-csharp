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

			Console.WriteLine("Fetching events...");
			var events = client.GetEvents();

			Console.WriteLine();

			foreach (var evt in events)
			{
				Console.WriteLine("{0} - {1}", evt.Start, evt.Summary);
			}

			Console.WriteLine();
			Console.WriteLine("Press enter to continue...");
			Console.ReadLine();
		}
	}
}
