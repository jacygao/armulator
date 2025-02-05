using NetworkInterfaceController;
using VirtualMachineController;

namespace Emu.UnitTest.Mocks
{
    internal static class VirtualMachineExtensions
    {
       
        public static VirtualMachine CreateFromGeneralizedSharedImageMock(this VirtualMachine _, string imageId, string networkInterfaceId)
        {
            return new VirtualMachine()
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
        }
    }
}
