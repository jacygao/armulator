using NetworkInterfaceController;

namespace Emu.UnitTest.Mocks
{
    internal static class NetworkInterfaceExtensions
    {
        public static NetworkInterface GetNetworkInterfaceMock(this NetworkInterface _)
        {
            return new NetworkInterface
            {
                Location = "West US",
                Properties = new NetworkInterfacePropertiesFormat
                {
                    EnableAcceleratedNetworking = true,
                    DisableTcpStateTracking = false,
                    IpConfigurations = new List<NetworkInterfaceIPConfiguration>()
                    {
                        new NetworkInterfaceIPConfiguration {
                            Name = "my-ipconfig1",
                            Properties = new NetworkInterfaceIPConfigurationPropertiesFormat
                            {
                                PublicIPAddress = new PublicIPAddress
                                {
                                    Id = "/subscriptions/subid/resourceGroups/rg1/providers/Microsoft.Network/publicIPAddresses/test-ip"
                                },
                                Subnet = new Subnet
                                {
                                    Id = "/subscriptions/subid/resourceGroups/rg1/providers/Microsoft.Network/virtualNetworks/rg1-vnet/subnets/default"
                                }
                            }
                        },
                        new NetworkInterfaceIPConfiguration
                        {
                            Properties = new NetworkInterfaceIPConfigurationPropertiesFormat
                            {
                                PrivateIPAddressPrefixLength = 28
                            }
                        }
                    }
                }
            };
        }
    }
}
