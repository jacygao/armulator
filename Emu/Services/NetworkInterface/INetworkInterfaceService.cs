namespace Emu.Services.NetworkInterface
{
    public interface INetWorkInterfaceService
    {
        Task<OperationType> UpsertNetworkInterfaceAsync(string subscriptionId, string resourceGroup, string networkInterfaceName, NetworkInterfaceController.NetworkInterface ni);

        Task<NetworkInterfaceController.NetworkInterface> GetNetworkInterfaceAsync(string subscriptionId, string resourceGroup, string networkInterfaceName);
    }
}