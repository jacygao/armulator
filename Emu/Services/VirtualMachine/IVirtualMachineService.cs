namespace Emu.Services.VirtualMachine
{
    using VirtualMachineController;

    public interface IVirtualMachineService
    {
        Task<OperationType> CreateOrUpdateAsync(string subscriptionId, string resourceGroupName, string vmName, VirtualMachine parameters);
        Task PowerOffAsync(string subscriptionId, string resourceGroupName, string vmName);
        Task GeneralizeAsync(string subscriptionId, string resourceGroupName, string vmName);
        Task DeallocateAsync(string subscriptionId, string resourceGroupName, string vmName);
        Task StartAsync(string subscriptionId, string resourceGroupName, string vmName);
        Task<VirtualMachine> GetAsync(string subscriptionId, string resourceGroupName, string vmName);
        Task RunCommandAsync(string subscriptionId, string resourceGroupName, string vmName, string commandId, string script);
    }
}