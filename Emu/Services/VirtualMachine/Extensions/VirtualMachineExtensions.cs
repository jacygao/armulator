using Emu.Common.RestApi;

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
                throw new InvalidInputException(Constants.InvalidParameter.message, Constants.InvalidParameter.substatus);
            }

            if (vm.Properties.NetworkProfile == null)
            {
                throw new InvalidInputException(Constants.InvalidParameter.message, Constants.InvalidParameter.substatus);
            }
        }
    }
}
