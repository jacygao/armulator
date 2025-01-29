using Emu.Common.RestApi;

namespace Emu.Common.Validators
{
    public class CommonValidators
    {
        // TODO: consider implementing decorators
        public static void Validate(string subscriptionId, string resourceGroupName)
        {
            if (!Guid.TryParse(subscriptionId, out _)) { throw new InvalidSubscriptionIdException("invalid subscription id", subscriptionId); }

            if (resourceGroupName == "unknown") { throw new InvalidResourceGroupException("invalid resource group", resourceGroupName); }
        }

        public static void ValidateSubscription(string subscriptionId)
        {
            if (!Guid.TryParse(subscriptionId, out _)) { throw new InvalidSubscriptionIdException("invalid subscription id", subscriptionId); }
        }
    }
}
