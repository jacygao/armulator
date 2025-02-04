namespace Emu.Services.VirtualMachine
{
    using Emu.Services.Common;
    using System.Threading.Tasks;
    using VirtualMachineController;

    public class VirtualMachineService(IStorageService storageService) : ServiceBase<VirtualMachine>(storageService), IVirtualMachineService
    {

        public async Task<OperationType> CreateOrUpdateAsync(string subscriptionId, string resourceGroup, string vmName, VirtualMachine parameters)
        {
			// Input Validation
			ArgumentException.ThrowIfNullOrEmpty(vmName, nameof(vmName));
			ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

            return await CreateAsync(ServiceConstants.VirtualMachineContainerName, $"{subscriptionId}/{resourceGroup}/{vmName}.json", parameters);
		}

        public Task DeallocateAsync(string subscriptionId, string resourceGroupName, string vmName)
        {
            throw new NotImplementedException();
        }

        public Task GeneralizeAsync(string subscriptionId, string resourceGroupName, string vmName)
        {
            throw new NotImplementedException();
        }

        public async Task<VirtualMachine> GetAsync(string subscriptionId, string resourceGroupName, string vmName)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(vmName, nameof(vmName));

            return await GetAsync(ServiceConstants.VirtualMachineContainerName, $"{subscriptionId}/{resourceGroupName}/{vmName}.json");
        }

        public Task PowerOffAsync(string subscriptionId, string resourceGroupName, string vmName)
        {
            throw new NotImplementedException();
        }

        public Task RunCommandAsync(string subscriptionId, string resourceGroupName, string vmName, string commandId, string script)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(string subscriptionId, string resourceGroupName, string vmName)
        {
            throw new NotImplementedException();
        }

    }
}