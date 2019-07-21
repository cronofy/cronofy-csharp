# Cronofy

[Cronofy](https://www.cronofy.com) - Service to centrally manage application settings and feature toggles for applications and services.

## Installation

The Appconfi .NET SDK is available as a Nuget package, to install run the following command in the [Package Manager Console](https://docs.nuget.org/consume/package-manager-console)
```
Install-Package Appconfi
```
More info is available on [nuget](https://www.nuget.org/packages/Appconfi/)

## Usage

In order to use the Appconfi you will need to [create an account](https://appconfi.com/account/register).

From there you can create your first application and setup your configuration. To use the Appconfi API to access your configuration go to `/accesskeys` there you can find the `application_id` and your `application_secret`.

## How to use

```csharp

var manager = Configuration.NewInstance(applicationId, apiKey);

//Start monitoring changes in your application settings and features toggles.
manager.StartMonitor();

//Access your application settings
var color = manager.GetSetting("application.color");

//Check if your feature toggles are enable
var status = manager.IsFeatureEnabled("you.feature");

```

## Optional parameters

Change your environments:

```csharp
var env = "PRODUCTION";
var refreshInterval =  TimeSpan.FromSeconds(10);
var manager = Configuration.NewInstance(applicationId, apiKey, env, refreshInterval);
```

## Links

 * [Web](https://appconfi.com)
