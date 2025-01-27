using Emu.Common.Validators;
using Emu.Services.VirtualMachine;
using System.Reflection.Metadata;
using VirtualMachineController;

namespace Emu.Controllers.Compute.VirtualMachineController
{
    public class VirtualMachineControllerImpl : IVirtualMachinesController
    {
        private readonly IVirtualMachineService _virtualMachineService;
        public VirtualMachineControllerImpl(IVirtualMachineService virtualMachineService) { 
            _virtualMachineService = virtualMachineService;
        }

        public Task<VirtualMachineAssessPatchesResult> AssessPatchesAsync(string resourceGroupName, string vmName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<global::VirtualMachineController.StorageProfile> AttachDetachDataDisksAsync(string resourceGroupName, string vmName, AttachDetachDataDisksRequest parameters, string subscriptionId, string api_version)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineCaptureResult> CaptureAsync(string resourceGroupName, string vmName, VirtualMachineCaptureParameters parameters, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task ConvertToManagedDisksAsync(string resourceGroupName, string vmName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public async Task<VirtualMachine> CreateOrUpdateAsync(string resourceGroupName, string vmName, VirtualMachine parameters, string api_version, string subscriptionId, string if_Match, string if_None_Match)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            // enrich
            parameters.Id = $"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{vmName}";
            parameters.Type = "Microsoft.Compute/virtualMachines";
            parameters.Name = vmName;

            // TODO: implement Provision State Transitions
            parameters.Properties.ProvisioningState = VirtualMachineConstants.ProvisioningStateCreating;

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

            // Network Profile

            // Storage Profile
            if (parameters.Properties.StorageProfile.DataDisks == null)
            {
                parameters.Properties.StorageProfile.DataDisks = [];
            }


            return await _virtualMachineService.CreateOrUpdateAsync(resourceGroupName, vmName, parameters);
        }

        public Task DeallocateAsync(string resourceGroupName, string vmName, bool? hibernate, string api_version, string subscriptionId)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            throw new NotImplementedException();
        }

        public Task DeleteAsync(string resourceGroupName, string vmName, bool? forceDeletion, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task GeneralizeAsync(string resourceGroupName, string vmName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachine> GetAsync(string resourceGroupName, string vmName, _expand? expand, string api_version, string subscriptionId)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            throw new NotImplementedException();
        }

        public Task<VirtualMachineInstallPatchesResult> InstallPatchesAsync(string resourceGroupName, string vmName, VirtualMachineInstallPatchesParameters installPatchesInput, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineInstanceView> InstanceViewAsync(string resourceGroupName, string vmName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineListResult> ListAllAsync(string api_version, string subscriptionId, string statusOnly, string filter, _expand expand)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineListResult> ListAsync(string resourceGroupName, string filter, _expand expand, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineSizeListResult> ListAvailableSizesAsync(string resourceGroupName, string vmName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachineListResult> ListByLocationAsync(string location, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task PerformMaintenanceAsync(string resourceGroupName, string vmName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task PowerOffAsync(string resourceGroupName, string vmName, bool skipShutdown, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task ReapplyAsync(string resourceGroupName, string vmName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task RedeployAsync(string resourceGroupName, string vmName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task ReimageAsync(string resourceGroupName, string vmName, VirtualMachineReimageParameters parameters, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task RestartAsync(string resourceGroupName, string vmName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<RetrieveBootDiagnosticsDataResult> RetrieveBootDiagnosticsDataAsync(string resourceGroupName, string vmName, int? sasUriExpirationTimeInMinutes, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task SimulateEvictionAsync(string resourceGroupName, string vmName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(string resourceGroupName, string vmName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualMachine> UpdateAsync(string resourceGroupName, string vmName, VirtualMachineUpdate parameters, string api_version, string subscriptionId, string if_Match, string if_None_Match)
        {
            throw new NotImplementedException();
        }
    }
}