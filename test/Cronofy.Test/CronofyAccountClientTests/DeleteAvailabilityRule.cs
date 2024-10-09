namespace Cronofy.Test.CronofyAccountClientTests
{
    using NUnit.Framework;

    internal sealed class DeleteAvailabilityRule : Base
    {
        private const string AvailabilityRuleId = "my_really_cool_rule_id";

        [Test]
        public void CanDeleteAvailabilityRule()
        {
            this.Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/availability_rules/" + AvailabilityRuleId)
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(202));

            this.Client.DeleteAvailabilityRule(AvailabilityRuleId);
        }
    }
}
