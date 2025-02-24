using NetworkInterfaceController;
using VirtualMachineController;

namespace Emu.UnitTest.Mocks
{
    internal static class VirtualMachineExtensions
    {
       
        public static (VirtualMachine req, VirtualMachine resp) CreateFromGeneralizedSharedImageMock(
            this VirtualMachine _, 
            string subscriptionId,
            string resourceGroupName,
            string imageId, 
            string networkInterfaceId)
        {
            (VirtualMachine req, VirtualMachine resp) res = new();

            res.req = new VirtualMachine()
            {
                Location = "westus",
                Properties = new VirtualMachineProperties()
                {
                    HardwareProfile = new HardwareProfile()
                    {
                        VmSize = HardwareProfileVmSize.Standard_D1_v2
                    },
                    StorageProfile = new StorageProfile()
                    {
                        ImageReference = new ImageReference()
                        {
                            Id = imageId
                        },
                        OsDisk = new OsDisk()
                        {
                            Caching = Caching.ReadWrite,
                            ManagedDisk = new ManagedDisk()
                            {
                                StorageAccountType = StorageAccountType.Standard_LRS
                            },
                            Name = "mydisk",
                            CreateOption = CreateOption.FromImage
                        }
                    },
                    OsProfile = new OSProfile()
                    {
                        AdminUsername = "admin",
                        AdminPassword = "password",
                        ComputerName = "my-vm",
                    },
                    NetworkProfile = new NetworkProfile()
                    {
                        NetworkInterfaces = new List<NetworkInterfaces>()
                        {
                            new NetworkInterfaces() {
                                Id = networkInterfaceId,
                                Properties = new VirtualMachineController.Properties2()
                                {
                                    Primary = true
                                },
                            }
                        }
                    }
                }
            };

            res.resp = res.req;
            res.resp.Properties.StorageProfile.DataDisks = [];
            res.resp.Properties.OsProfile.AdminPassword = null; // masked
            res.resp.Properties.OsProfile.WindowsConfiguration = new WindowsConfiguration()
            {
                ProvisionVMAgent = true,
                EnableAutomaticUpdates = true
            };
            res.resp.Properties.OsProfile.Secrets = [];
            res.resp.Properties.ProvisioningState = "Creating";
            res.resp.Id = $"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/my-vm";
            res.resp.Name = "my-vm";
            res.resp.Type = "Microsoft.Compute/virtualMachines";

            return res;
        }
    }
}
