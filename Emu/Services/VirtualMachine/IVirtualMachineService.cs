namespace Emu.Services.VirtualMachine
{
    using VirtualMachineController;

    public interface IVirtualMachineService
    {
        Task<VirtualMachine> CreateOrUpdateAsync(string resourceGroupName, string vmName, VirtualMachine parameters);
        Task PowerOffAsync(string resourceGroupName, string vmName);
        Task GeneralizeAsync(string resourceGroupName, string vmName);
        Task DeallocateAsync(string resourceGroupName, string vmName);
        Task StartAsync(string resourceGroupName, string vmName);
        Task<VirtualMachine> GetAsync(string resourceGroupName, string vmName);
        Task RunCommandAsync(string resourceGroupName, string vmName, string commandId, string script);
    }
}