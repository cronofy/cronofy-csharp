﻿namespace Cronofy.Test.CronofyEnterpriseConnectAccountClientTests
{
    using NUnit.Framework;

    internal sealed class GetUserInfo : Base
    {
        [Test]
        public void CanGetUserInfo()
        {
            this.Http.Stub(
                HttpGet
                    .Url("https://api.cronofy.com/v1/userinfo")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(200)
                    .ResponseBody(
                    @"{
  ""sub"": ""acc_5700a00eb0ccd07000000000"",
  ""email"": ""janed@company.com"",
  ""name"": ""Jane Doe"",
  ""zoneinfo"": ""Europe/London"",
  ""cronofy.type"": ""account"",
  ""cronofy.data"": {
    ""authorization"": {
      ""scope"": ""read_write"",
      ""status"": ""active""
    },
    ""profiles"": [
      {
        ""provider_name"": ""google"",
        ""provider_service"": ""gsuite"",
        ""profile_id"": ""pro_n23kjnwrw2"",
        ""profile_name"": ""example1@cronofy.com"",
        ""profile_connected"": true,
        ""profile_initial_sync_required"": true,
        ""profile_calendars"": [
          {
            ""calendar_id"": ""cal_n23kjnwrw2_jsdfjksn234"",
            ""calendar_name"": ""Home"",
            ""calendar_readonly"": false,
            ""calendar_deleted"": false,
            ""calendar_primary"": true,
            ""calendar_integrated_conferencing_available"": true,
            ""calendar_attachments_available"": false,
            ""permission_level"": ""sandbox""
          },
          {
            ""calendar_id"": ""cal_n23kjnwrw2_n1k323nkj23"",
            ""calendar_name"": ""Work"",
            ""calendar_readonly"": true,
            ""calendar_deleted"": true,
            ""calendar_primary"": false,
            ""calendar_integrated_conferencing_available"": true,
            ""calendar_attachments_available"": false,
            ""permission_level"": ""sandbox""
          }
        ]
      },
      {
        ""provider_name"": ""apple"",
        ""provider_service"": ""icloud"",
        ""profile_id"": ""pro_fe145c37de"",
        ""profile_name"": ""example2@cronofy.com"",
        ""profile_connected"": false,
        ""profile_initial_sync_required"": false,
        ""profile_relink_url"": ""https://app.cronofy.com/relink/apple?email=example@cronofy.com"",
        ""profile_calendars"": [
          {
            ""calendar_id"": ""cal_fe145c37de_3nkj23wejk1"",
            ""calendar_name"": ""Bank Holidays"",
            ""calendar_readonly"": true,
            ""calendar_deleted"": false,
            ""calendar_primary"": false,
            ""calendar_integrated_conferencing_available"": false,
            ""calendar_attachments_available"": true,
            ""permission_level"": ""sandbox""
          }
        ]
      }
    ]
  }
}"));

            var actualUserInfo = this.Client.GetUserInfo();
            var expectedUserInfo = new UserInfo
            {
                Sub = "acc_5700a00eb0ccd07000000000",
                CronofyType = "account",
                Profiles = new UserInfo.Profile[] {
                    new UserInfo.Profile {
                        ProviderName = "google",
                        ProviderService = "gsuite",
                        Id = "pro_n23kjnwrw2",
                        Name = "example1@cronofy.com",
                        Connected = true,
                        Calendars = new Calendar[] {
                            new Calendar {
                                Profile = new Calendar.ProfileSummary {
                                    ProviderName = "google",
                                    ProfileId = "pro_n23kjnwrw2",
                                    Name = "example1@cronofy.com",
                                },
                                CalendarId = "cal_n23kjnwrw2_jsdfjksn234",
                                Name = "Home",
                                ReadOnly = false,
                                Deleted = false,
                                Primary = true,
                                IntegratedConferencingAvailable = true,
                            },
                            new Calendar {
                                Profile = new Calendar.ProfileSummary {
                                    ProviderName = "google",
                                    ProfileId = "pro_n23kjnwrw2",
                                    Name = "example1@cronofy.com",
                                },
                                CalendarId = "cal_n23kjnwrw2_n1k323nkj23",
                                Name = "Work",
                                ReadOnly = true,
                                Deleted = true,
                                Primary = false,
                                IntegratedConferencingAvailable = true,
                            },
                        }
                    },
                    new UserInfo.Profile {
                        ProviderName = "apple",
                        ProviderService = "icloud",
                        Id = "pro_fe145c37de",
                        Name = "example2@cronofy.com",
                        Connected = false,
                        RelinkUrl = "https://app.cronofy.com/relink/apple?email=example@cronofy.com",
                        Calendars = new Calendar[] {
                            new Calendar {
                                Profile = new Calendar.ProfileSummary {
                                    ProviderName = "apple",
                                    ProfileId = "pro_fe145c37de",
                                    Name = "example2@cronofy.com",
                                },
                                CalendarId = "cal_fe145c37de_3nkj23wejk1",
                                Name = "Bank Holidays",
                                ReadOnly = true,
                                Deleted = false,
                                Primary = false,
                                IntegratedConferencingAvailable = false,
                            },
                        },
                    },
                }
            };

            Assert.AreEqual(expectedUserInfo, actualUserInfo);
        }
    }
}
