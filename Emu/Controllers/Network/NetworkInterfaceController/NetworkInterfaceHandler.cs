using Emu.Common.Utils;
using Emu.Common.Validators;
using Emu.Services.NetworkInterface;
using NetworkInterfaceController;
using System.Xml.Linq;

namespace Emu.Controllers.Network.NetworkInterfaceController
{
    public class NetworkInterfaceHandler : INetworkInterfacesController
    {
        private readonly INetWorkInterfaceService _networkInterfaceService;

        public NetworkInterfaceHandler(INetWorkInterfaceService netWorkInterfaceService) { 
            _networkInterfaceService = netWorkInterfaceService;
        }

        public async Task<NetworkInterface> CreateOrUpdateAsync(string resourceGroupName, string networkInterfaceName, NetworkInterface parameters, string api_version, string subscriptionId)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);
            
            // Enrich
            parameters.Id = ParameterHelper.GetComputeResourceId(subscriptionId, resourceGroupName, $"{ParameterHelper.ResourceCategoryNetwork}/{ParameterHelper.ResourceTypeNetworkInterface}", networkInterfaceName);

            await _networkInterfaceService.UpsertNetworkInterfaceAsync(subscriptionId, resourceGroupName, networkInterfaceName, parameters);

            return parameters;
        }

        public Task DeleteAsync(string resourceGroupName, string networkInterfaceName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public async Task<NetworkInterface> GetAsync(string resourceGroupName, string networkInterfaceName, string api_version, string subscriptionId, string expand)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            return await _networkInterfaceService.GetNetworkInterfaceAsync(subscriptionId, resourceGroupName, networkInterfaceName);
        }

        public Task<EffectiveRouteListResult> GetEffectiveRouteTableAsync(string resourceGroupName, string networkInterfaceName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<NetworkInterfaceListResult> ListAllAsync(string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<NetworkInterfaceListResult> ListAsync(string resourceGroupName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<EffectiveNetworkSecurityGroupListResult> ListEffectiveNetworkSecurityGroupsAsync(string resourceGroupName, string networkInterfaceName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<NetworkInterface> UpdateTagsAsync(string resourceGroupName, string networkInterfaceName, TagsObject parameters, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }
    }
}
