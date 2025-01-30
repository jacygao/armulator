namespace Emu.Services.VirtualMachine
{
    public class VirtualMachineConstants
    {
        // available provisioning states: https://learn.microsoft.com/en-us/azure/virtual-machines/states-billing#provisioning-states
        public const string ProvisioningStateCreating = "Creating";
        public const string ProvisioningStateUpdating = "Updating";
        public const string ProvisioningStateFailed = "Updating";
        public const string ProvisioningStateSucceeded = "Succeeded";
        public const string ProvisioningStateDeleting = "Deleting";
        public const string ProvisioningStateMigrating = "Migrating";

        // available power states: https://learn.microsoft.com/en-us/azure/virtual-machines/states-billing#power-states-and-billing
        public const string PowerStateCreating = "Creating";
        public const string PowerStateStarting = "Starting";
        public const string PowerStateRunning = "Running";
        public const string PowerStateStopping = "Stopping";
        public const string PowerStateStopped = "Stopped";
        public const string PowerStateDeallocating = "Deallocating";
        public const string PowerStateDeallocated = "Deallocated";
    }
}
