using PublicIpAddressController;

namespace Emu.Controllers.Network.PublicIpAddressController
{
    public class PublicIpAddressHandler : IPublicIPAddressesController
    {
        public PublicIpAddressHandler() { }

        public Task<PublicIPAddress> CreateOrUpdateAsync(string resourceGroupName, string publicIpAddressName, PublicIPAddress parameters, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<PublicIpDdosProtectionStatusResult> DdosProtectionStatusAsync(string resourceGroupName, string publicIpAddressName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string resourceGroupName, string publicIpAddressName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<PublicIPAddress> GetAsync(string resourceGroupName, string publicIpAddressName, string api_version, string subscriptionId, string expand)
        {
            throw new NotImplementedException();
        }

        public Task<PublicIPAddressListResult> ListAllAsync(string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<PublicIPAddressListResult> ListAsync(string resourceGroupName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<PublicIPAddress> UpdateTagsAsync(string resourceGroupName, string publicIpAddressName, TagsObject parameters, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }
    }
}
