namespace Emu.Services.VirtualMachine
{
    using Emu.Services.Common;
    using System.Threading.Tasks;
    using VirtualMachineController;

    public class VirtualMachineService(IStorageService storageService) : ServiceBase<VirtualMachine>(storageService), IVirtualMachineService
    {

        public async Task<VirtualMachine> CreateOrUpdateAsync(string subscriptionId, string resourceGroup, string vmName, VirtualMachine parameters)
        {
			// Input Validation
			ArgumentException.ThrowIfNullOrEmpty(vmName, nameof(vmName));
			ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

            // Storage Profile
            if (!await FileExists(ServiceConstants.GalleryImageContainerName, parameters.Properties.StorageProfile.ImageReference.Id))
            {
                // TODO: throw error
            }

            if (parameters.Properties.StorageProfile.DataDisks == null)
            {
                parameters.Properties.StorageProfile.DataDisks = [];
            }

            // Network Profile
            foreach (var ni in parameters.Properties.NetworkProfile.NetworkInterfaces)
            {
                if (!await FileExists(ServiceConstants.NetworkInterfaceContainerName, ni.Id))
                {
                    // TODO: throw error
                }
            }

            // OS Profile
            if (parameters.Properties.OsProfile.WindowsConfiguration == null)
            {
                parameters.Properties.OsProfile.WindowsConfiguration = new WindowsConfiguration
                {
                    ProvisionVMAgent = true,
                    EnableAutomaticUpdates = true,
                };
            }

            if (parameters.Properties.OsProfile.Secrets == null)
            {
                parameters.Properties.OsProfile.Secrets = [];
            }

            await CreateAsync(ServiceConstants.VirtualMachineContainerName, $"{subscriptionId}/{resourceGroup}/{vmName}.json", parameters);

            return parameters;
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