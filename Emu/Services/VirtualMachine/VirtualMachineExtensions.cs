using Emu.Common.RestApi;
using System.Drawing;
using VirtualMachineController;

namespace Emu.Services.VirtualMachine.Extensions
{
    public static class VirtualMachineExtensions
    {
        public static VirtualMachineController.VirtualMachine Mask(this VirtualMachineController.VirtualMachine vm)
        {
            vm.Properties.OsProfile.AdminPassword = null;

            return vm;
        }

        public static void ValidateAsInput(this VirtualMachineController.VirtualMachine vm)
        {
            if (string.IsNullOrEmpty(vm.Location)) {
                throw new InvalidInputException(Constants.LocationRequired.message, Constants.LocationRequired.substatus);
            }

            if (vm.Properties == null)
            {
                throw new InvalidInputException(Constants.InvalidParameterMissingProperties.message, Constants.InvalidParameterMissingProperties.substatus);
            }

            ValidateHardwareProfile(vm.Properties.HardwareProfile);

            if (vm.Properties.NetworkProfile == null)
            {
                throw new InvalidInputException(Constants.InvalidParameterMissingProperties.message, Constants.InvalidParameterMissingProperties.substatus);
            }

        }

        // Enriches the input value before persisting data in the storage
        public static void Enrich(this VirtualMachineController.VirtualMachine vm)
        {
            if (vm.Properties.StorageProfile.DataDisks == null)
            {
                vm.Properties.StorageProfile.DataDisks = [];
            }

            // OS Profile
            if (vm.Properties.OsProfile.WindowsConfiguration == null)
            {
                vm.Properties.OsProfile.WindowsConfiguration = new WindowsConfiguration
                {
                    ProvisionVMAgent = true,
                    EnableAutomaticUpdates = true,
                };
            }

            if (vm.Properties.OsProfile.Secrets == null)
            {
                vm.Properties.OsProfile.Secrets = [];
            }
        }

        internal static void ValidateHardwareProfile(HardwareProfile hp)
        {
            if (hp.VmSize == null)
            {
                throw new InvalidParameterException(Constants.InvalidParameterVmSize.message, Constants.InvalidParameterVmSize.substatus);
            }
        }
    }
}
