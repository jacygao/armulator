
using Emu.Services.Common;

namespace Emu.Services.NetworkInterface
{
    public class NetworkInterfaceService(IStorageService storageService) : ServiceBase<NetworkInterfaceController.NetworkInterface>(storageService), INetWorkInterfaceService
    {
        public async Task<NetworkInterfaceController.NetworkInterface> GetNetworkInterfaceAsync(string subscriptionId, string resourceGroup, string networkInterfaceName)
        {
            ArgumentException.ThrowIfNullOrEmpty(networkInterfaceName, nameof(networkInterfaceName));

            return await GetAsync(ServiceConstants.NetworkInterfaceContainerName, $"{subscriptionId}/{resourceGroup}/{networkInterfaceName}");
        }

        public async Task<OperationType> UpsertNetworkInterfaceAsync(string subscriptionId, string resourceGroup, string networkInterfaceName, NetworkInterfaceController.NetworkInterface networkInterface)
        {
            ArgumentException.ThrowIfNullOrEmpty(networkInterfaceName, nameof(networkInterfaceName));
            ArgumentNullException.ThrowIfNull(networkInterface, nameof(networkInterface));

            networkInterface.Properties.ProvisioningState = NetworkInterfaceController.ProvisioningState.Succeeded;

            return await CreateAsync(ServiceConstants.NetworkInterfaceContainerName, $"{subscriptionId}/{resourceGroup}/{networkInterfaceName}", networkInterface);
        }
    }
}