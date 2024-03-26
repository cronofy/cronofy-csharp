## [1.12.4]
* Adds support for the `conferencing_profiles` field to the response object from GetUserInfo. [#132]

## [1.12.3]
* Adds the `domain` property to the service account details when a `service_account` is returned in GetUserInfo.

## [1.12.2]

* Adding `GetAuthorizationUrlBuilder` and `GetEnterpriseConnectAuthorizationUrlBuilder` to the `ICronofyOAuthClient` interface. [#128]

## [1.12.1]

* Internal changes to support constructing multiple client classes concurrently. [#127]

## [1.12.0]

* Adds `Conferencing` property to the `CronofyAccountClient.GetEvents()` return value. [#125]

## [1.11.0]

* Adds authorization parameters for the Enterprise Connect and Individual Connect UserInfo responses, and `provider_name` parameter for Enterprise Connect UserInfo response. [#123]

## [1.10.0]

* Increases the coverage of `GetUserInfo()`, returning information about connected profiles and calendars. [#120]

## [1.9.0]

* Add `change_participation_status` to Read Events. [#118]

## [1.8.0]

* Adds support for more Real-Time Scheduling callback URL types: No Time Suitable, No Times Displayed. [More info can be found here](https://docs.cronofy.com/developers/api/scheduling/real-time-scheduling/#param-callback_urls.completed_url) [#116]

## [1.7.0]

* Add `series_identifier` to Read Events. [#114]

## [1.6.0]

* Add `max_results` to AvailabilityBuilder. [More info can be found here](https://docs.cronofy.com/developers/api/scheduling/availability/#param-max_results) [#110]

## [1.5.0]

* Add additional FreeBusy API parameters [#107]

## [1.4.0]

* Adds for providing multiple users when requesting [Enterprise Connect delegated access](https://docs.cronofy.com/developers/api/enterprise-connect/delegated-access/) [#105]

## [1.3.0]

* Adds `ProviderService` to profile information returned by `GetProfiles()` and `GetTokenFromCode()`. [#104]

## [1.2.0]

* Enables [strong name signing](https://docs.microsoft.com/en-us/dotnet/standard/assembly/strong-named), which allows applications that also use strong names to reference this package. [#103]

## [1.1.4]

* Adds support for additional parameters when revoking an authorization [#102]

## [1.1.3]

* Adds support for [requesting conferencing provisioning](https://docs.cronofy.com/developers/api/conferencing-services/create/) and related [push notifications](https://docs.cronofy.com/developers/api/conferencing-services/subscriptions/) when creating an event [#97]
* Adds support for [conferencing authorization URL generation](https://docs.cronofy.com/developers/api/conferencing-services/authorization/) [#98]

## [1.1.2]

* Adds support for [Managed Availability](https://docs.cronofy.com/developers/api/scheduling/availability/#param-participants.members.managed_availability) and [Availability Rules](https://docs.cronofy.com/developers/api/scheduling/availability/#param-participants.members.availability_rule_ids) as part of Availability Query requests [#95]

## [1.1.1]

* Updates Newtonsoft.Json dependency to [13.0.1](https://www.nuget.org/packages/Newtonsoft.Json/13.0.1) [#91]

## [1.1.0]

* Adds the optional `state` parameter for [Enterprise Connect delegated access](https://docs.cronofy.com/developers/api/enterprise-connect/delegated-access/) [#87]
* Adds the ability to fetch Real-Time Scheduling token status by ID [#88]

## [1.0.0]

* Updates the target of the SDK to .NET Standard 2.0, enabling support for .NET Core consumers. [#81]
* **Breaking changes:**
   * the `DataCentre` type has been removed.
      * Update usages of Client constructors to use an _SDK Identifier_ string from [our Data Centers documentation](https://docs.cronofy.com/developers/data-centers/).
      * Update usages of `Configuration.DefaultDataCentre` to set an SDK Identifier string on the renamed property `Configuration.DefaultDataCenter`

## [0.29.0]

 * Exposes full response when creating Real-Time Scheduling link [#85]

## [0.28.2]

 * Adds missing Real-Time Scheduling functionality [#84]
   * Get Status
   * Disabling
   * Adding callback URLs and return URLs to RTS requests
 * Adds Element Token authorization endpoint support [#83]

## [0.28.1]

 * Fix NRE in Smart Invite methods [#82]

## [0.28.0]

 * Add Admin API methods [#80]

## [0.27.1]

 * Support organizer email for single recipient form of Smart Invites [#77]

## [0.27.0]

 * Support Smart Invite organizer email [#76]

## [0.26.0]

 * Allow provider name to be set via OAuth URL builder [#73]

## [0.25.3]

 * Update how license is published [#69]

## [0.25.2]

 * Fixed Application Calendar URL [#68]

## [0.25.1]

 * Fixed mapping of event status [#66]

## [0.25.0]

 * Added support for multi-recipient Smart Invites [#62]

## [0.24.0]

 * Added support for sequences, buffers and intervals [#59]
 * Added read only meeting url [#60]

## [0.23.0]

 * Allow no reminders to be set explicitly [#58]

## [0.22.0]

 * Allow setting of Smart Invite organizer name [#55]

## [0.21.0]

 * Private event support [#54]

## [0.20.0]

 * Application Calendar support [#49]
 * Add subject to oauth token responses [#53]
 * Added support for Smart Invite proposal [#52]

## [0.19.1]

 * Make EventTimeConverter serialize without the need for an attribute [#51]

## [0.19.0]

 * Make EventTimeConverter public [#50]

## [0.18.0]

 * Support returning Google event IDs [#48]

## [0.17.0]

 * Support for create-only reminders [#47]

## [0.16.0]

 * Support for batch endpoint [#42]
 * Set default compression headers [#46]

## [0.15.1]

 * Support for Cancelling Smart Invites [#44]

## [0.15.0]

 * Support for Smart Invites [#43]

## [0.14.0]

 * Support for revoking profile authorization [#41]

## [0.13.0]

 * Support for Real-Time Scheduling and Add To Calendar hour format option [#40]

## [0.12.0]

 * Support for color with calendar creation and event upsert [#38]
 * Helper to verify push notification HMACs [#37]

## [0.11.0]

 * Support for real time booking [#36]

## [0.10.0]

 * Support for add to calendar with availability [#34]

## [0.9.0]

 * Support for add to calendar [#31]

## [0.8.0]

 * Support for adding and removing attendees [#29]

## [0.7.0]

 * Support for explicit linking of accounts [#28]

## [0.6.0]

 * Support for setting include_geo flag on read events [#27]

## [0.5.1]

 * Failures when authorizing a service account user now raise a more specific
   error [#25]

## [0.5.0]

 * Support setting event transparency [#22]
 * Support member-specific calendar selection for Availability API [#23]

## [0.4.0]

 * Support multiple data centres [#21]

## [0.3.0]

 * Support member-specific available periods for Availability API [#18]

## [0.2.0]

 * Improved errors [#3]

## [0.1.1]

 * Support for upcoming geo location feature [#16]

## [0.1.0]

 * Support for Availability API [#15]

## [0.0.20]

 * Further support for Enterprise Connect [#13] [#14]
 * Expose event URL when reading events [#10]
 * Expose linking profile when redeeming OAuth authorization code [#10]
 * Support bulk deletion of events [#10]
 * Expose scope when retrieving account details [#10]
 * Support userinfo endpoint [#10]

## [0.0.19]

 * Support for Enterprise Connect [#9] [#12]


[0.0.19]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.0.19
[0.0.20]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.0.20
[0.1.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.1.0
[0.1.1]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.1.1
[0.2.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.2.0
[0.3.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.3.0
[0.4.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.4.0
[0.5.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.5.0
[0.5.1]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.5.1
[0.6.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.6.0
[0.7.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.7.0
[0.8.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.8.0
[0.9.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.9.0
[0.10.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.10.0
[0.11.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.11.0
[0.12.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.12.0
[0.13.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.13.0
[0.14.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.14.0
[0.15.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.15.0
[0.15.1]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.15.1
[0.16.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.16.0
[0.17.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.17.0
[0.18.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.18.0
[0.19.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.19.0
[0.19.1]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.19.1
[0.20.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.20.0
[0.21.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.21.0
[0.22.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.22.0
[0.23.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.23.0
[0.24.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.24.0
[0.25.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.25.0
[0.25.1]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.25.1
[0.25.2]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.25.2
[0.25.3]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.25.3
[0.26.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.26.0
[0.27.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.27.0
[0.27.1]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.27.1
[0.28.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.28.0
[0.28.1]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.28.1
[0.28.2]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.28.2
[0.29.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-0.29.0
[1.0.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.0.0
[1.1.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.1.0
[1.1.1]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.1.1
[1.1.2]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.1.2
[1.1.3]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.1.3
[1.1.4]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.1.4
[1.2.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.2.0
[1.3.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.3.0
[1.4.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.4.0
[1.5.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.5.0
[1.6.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.6.0
[1.7.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.7.0
[1.8.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.8.0
[1.9.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.9.0
[1.10.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.10.0
[1.11.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.11.0
[1.12.0]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.12.0
[1.12.1]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.12.1
[1.12.2]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.12.2
[1.12.3]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.12.3
[1.12.4]: https://github.com/cronofy/cronofy-csharp/releases/tag/rel-1.12.4

[#3]: https://github.com/cronofy/cronofy-csharp/pull/3
[#10]: https://github.com/cronofy/cronofy-csharp/pull/10
[#12]: https://github.com/cronofy/cronofy-csharp/pull/12
[#13]: https://github.com/cronofy/cronofy-csharp/pull/13
[#14]: https://github.com/cronofy/cronofy-csharp/pull/14
[#15]: https://github.com/cronofy/cronofy-csharp/pull/15
[#16]: https://github.com/cronofy/cronofy-csharp/pull/16
[#18]: https://github.com/cronofy/cronofy-csharp/pull/18
[#21]: https://github.com/cronofy/cronofy-csharp/pull/21
[#22]: https://github.com/cronofy/cronofy-csharp/pull/22
[#23]: https://github.com/cronofy/cronofy-csharp/pull/23
[#25]: https://github.com/cronofy/cronofy-csharp/pull/25
[#27]: https://github.com/cronofy/cronofy-csharp/pull/27
[#28]: https://github.com/cronofy/cronofy-csharp/pull/28
[#29]: https://github.com/cronofy/cronofy-csharp/pull/29
[#31]: https://github.com/cronofy/cronofy-csharp/pull/31
[#34]: https://github.com/cronofy/cronofy-csharp/pull/34
[#36]: https://github.com/cronofy/cronofy-csharp/pull/36
[#37]: https://github.com/cronofy/cronofy-csharp/pull/37
[#38]: https://github.com/cronofy/cronofy-csharp/pull/38
[#40]: https://github.com/cronofy/cronofy-csharp/pull/40
[#41]: https://github.com/cronofy/cronofy-csharp/pull/41
[#42]: https://github.com/cronofy/cronofy-csharp/pull/42
[#43]: https://github.com/cronofy/cronofy-csharp/pull/43
[#44]: https://github.com/cronofy/cronofy-csharp/pull/44
[#46]: https://github.com/cronofy/cronofy-csharp/pull/46
[#47]: https://github.com/cronofy/cronofy-csharp/pull/47
[#48]: https://github.com/cronofy/cronofy-csharp/pull/48
[#49]: https://github.com/cronofy/cronofy-csharp/pull/49
[#50]: https://github.com/cronofy/cronofy-csharp/pull/50
[#51]: https://github.com/cronofy/cronofy-csharp/pull/51
[#52]: https://github.com/cronofy/cronofy-csharp/pull/52
[#53]: https://github.com/cronofy/cronofy-csharp/pull/53
[#54]: https://github.com/cronofy/cronofy-csharp/pull/54
[#55]: https://github.com/cronofy/cronofy-csharp/pull/55
[#58]: https://github.com/cronofy/cronofy-csharp/pull/58
[#59]: https://github.com/cronofy/cronofy-csharp/pull/59
[#60]: https://github.com/cronofy/cronofy-csharp/pull/60
[#62]: https://github.com/cronofy/cronofy-csharp/pull/62
[#66]: https://github.com/cronofy/cronofy-csharp/pull/66
[#68]: https://github.com/cronofy/cronofy-csharp/pull/68
[#69]: https://github.com/cronofy/cronofy-csharp/pull/69
[#73]: https://github.com/cronofy/cronofy-csharp/pull/73
[#76]: https://github.com/cronofy/cronofy-csharp/pull/76
[#77]: https://github.com/cronofy/cronofy-csharp/pull/77
[#80]: https://github.com/cronofy/cronofy-csharp/pull/80
[#81]: https://github.com/cronofy/cronofy-csharp/pull/81
[#82]: https://github.com/cronofy/cronofy-csharp/pull/82
[#83]: https://github.com/cronofy/cronofy-csharp/pull/83
[#84]: https://github.com/cronofy/cronofy-csharp/pull/84
[#85]: https://github.com/cronofy/cronofy-csharp/pull/85
[#87]: https://github.com/cronofy/cronofy-csharp/pull/87
[#88]: https://github.com/cronofy/cronofy-csharp/pull/88
[#91]: https://github.com/cronofy/cronofy-csharp/pull/91
[#95]: https://github.com/cronofy/cronofy-csharp/pull/95
[#97]: https://github.com/cronofy/cronofy-csharp/pull/97
[#98]: https://github.com/cronofy/cronofy-csharp/pull/98
[#102]: https://github.com/cronofy/cronofy-csharp/pull/102
[#103]: https://github.com/cronofy/cronofy-csharp/pull/103
[#104]: https://github.com/cronofy/cronofy-csharp/pull/104
[#105]: https://github.com/cronofy/cronofy-csharp/pull/105
[#107]: https://github.com/cronofy/cronofy-csharp/pull/107
[#110]: https://github.com/cronofy/cronofy-csharp/pull/110
[#114]: https://github.com/cronofy/cronofy-csharp/pull/114
[#116]: https://github.com/cronofy/cronofy-csharp/pull/116
[#118]: https://github.com/cronofy/cronofy-csharp/pull/118
[#120]: https://github.com/cronofy/cronofy-csharp/pull/120
[#123]: https://github.com/cronofy/cronofy-csharp/pull/123
[#125]: https://github.com/cronofy/cronofy-csharp/pull/125
[#127]: https://github.com/cronofy/cronofy-csharp/pull/127
[#132]: https://github.com/cronofy/cronofy-csharp/pull/132
