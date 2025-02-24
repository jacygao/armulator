namespace Emu.Services.VirtualMachine
{
    using Emu.Common.Utils;
    using Emu.Services.Common;
    using Emu.Services.VirtualMachine.Extensions;
    using System.Threading.Tasks;
    using VirtualMachineController;

    public class VirtualMachineService(IStorageService storageService) : ServiceBase<VirtualMachine>(storageService), IVirtualMachineService
    {

        public async Task<VirtualMachine> CreateOrUpdateAsync(string subscriptionId, string resourceGroup, string vmName, VirtualMachine parameters)
        {
			// Input Validation
			ArgumentException.ThrowIfNullOrEmpty(vmName, nameof(vmName));
			ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

            parameters.ValidateAsInput();

            // Storage Profile
            if (!await FileExists(ServiceConstants.GalleryImageContainerName, parameters.Properties.StorageProfile.ImageReference.Id))
            {
                // TODO: throw error
            }

            // Network Profile
            foreach (var ni in parameters.Properties.NetworkProfile.NetworkInterfaces)
            {
                if (!await FileExists(ServiceConstants.NetworkInterfaceContainerName, ni.Id))
                {
                    // TODO: throw error
                }
            }

            parameters.Id = ParameterHelper.GetComputeResourceId(subscriptionId, resourceGroup, $"{ParameterHelper.ResourceCategoryCompute}/{ParameterHelper.ResourceTypeVirtualMachine}", vmName);
            parameters.Type = "Microsoft.Compute/virtualMachines";
            parameters.Name = vmName;

            // TODO: implement Provision State Transitions
            parameters.Properties.ProvisioningState = VirtualMachineConstants.ProvisioningStateCreating;

            parameters.Enrich();

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