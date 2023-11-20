﻿namespace Cronofy.Test.CronofyEnterpriseConnectAccountClientTests
{
    using NUnit.Framework;

    internal sealed class GetUserInfo : Base
    {
        [Test]
        public void CanGetUserInfoForServiceAccount()
        {
            this.Http.Stub(
                HttpGet
                    .Url("https://api.cronofy.com/v1/userinfo")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(200)
                    .ResponseBody(
                    @"
{
    ""sub"": ""ser_61a8b807a341fc00bee53042"",
    ""cronofy.type"": ""service_account"",
    ""cronofy.data"": {
        ""service_account"": {
            ""provider_name"": ""exchange"",
            ""domain"": ""example.org""
        },
        ""authorization"": {
            ""scope"": ""service_account/accounts/manage service_account/resources/manage"",
            ""status"": ""active"",
            ""delegated_scope"": ""read_write""
        }
    },
    ""email"": ""exchange-service-account@example.org""
}
"));

            var actualUserInfo = this.Client.GetUserInfo();
            var expectedUserInfo = new UserInfo
            {
                Sub = "ser_61a8b807a341fc00bee53042",
                CronofyType = "service_account",

                ServiceAccountInfo = new UserInfo.ServiceAccount
                {
                    ProviderName = "exchange",
                    Domain = "example.org",
                },
                AuthorizationInfo = new UserInfo.Authorization
                {
                    Scope = "service_account/accounts/manage service_account/resources/manage",
                    Status = "active",
                    DelegatedScope = "read_write",
                },
                Email = "exchange-service-account@example.org",
            };

            Assert.AreEqual(expectedUserInfo, actualUserInfo);
        }
    }
}
