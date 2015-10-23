# Cronofy

[Cronofy](https://www.cronofy.com) - one API for all the calendars (Google, iCloud, Exchange, Office 365, Outlook.com)

## Usage

In order to use the Cronofy API you will need to [create a developer account](https://app.cronofy.com/sign_up/new).

From there you can [create personal access tokens](https://app.cronofy.com/oauth/applications/5447ae289bd94726da00000f/tokens)
to access your own calendars, or you can [create an OAuth application](https://app.cronofy.com/oauth/applications/new)
to obtain an OAuth `client_id` and `client_secret` to be able to use the full
API.

## Authorization

[API documentation](https://www.cronofy.com/developers/api/#authorization)

Generate a link for a user to grant access to their calendars:

```csharp
const string CallbackUrl = "http://yoursite.dev/oauth2/callback";

var cronofy = new CronofyOAuthClient("clientId", "clientSecret");
var authorizationUrl = cronofy.GetAuthorizationUrlBuilder(CallbackUrl).Build();
```

The callback URL is a page on your website that will handle the OAuth 2.0
callback and receive a `code` parameter. You can then use that code to retrieve
an OAuth token granting access to the user's Cronofy account:

```csharp
var token = cronofy.GetTokenFromCode(code, CallbackUrl);
```

You should save the response's `AccessToken` and `RefreshToken` for later use.

Note that the **exact same** callback URL must be passed to both methods for
access to be granted.

## List calendars

[API documentation](https://www.cronofy.com/developers/api/#calendars)

Get a list of all the user's calendars:

```csharp
var cronofy = new CronofyAccountClient(accessToken);
var calendars = cronofy.GetCalendars();
```

## Read events

[API documentation](https://www.cronofy.com/developers/api/#read-events)

Get a list of events from the user's calendars:

```csharp
var cronofy = new CronofyAccountClient(accessToken);
var events = cronofy.GetEvents();
```

Note that the SDK handles iterating through the pages on your behalf.

## Create or update events

[API documentation](https://www.cronofy.com/developers/api/#upsert-event)

To create/update an event in the user's calendar:

```csharp
var cronofy = new CronofyAccountClient(accessToken);

var eventBuilder = new UpsertEventRequestBuilder()
    .EventId("uniq-id")
    .Summary("Event summary")
    .Description("Event description")
    .Start(2015, 10, 20, 17, 00)
    .End(2015, 10, 20, 17, 30)
    .Location("Meeting room");

cronofy.UpsertEvent(calendarId, eventBuilder);
```

## Delete events

[API documentation](https://www.cronofy.com/developers/api/#delete-event)

To delete an event from user's calendar:

```csharp
var cronofy = new CronofyAccountClient(accessToken);
cronofy.DeleteEvent(calendarId, "uniq-id");
```

## Links

 * [API documentation](https://www.cronofy.com/developers/api)
 * [API mailing list](https://groups.google.com/d/forum/cronofy-api)
