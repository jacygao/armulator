using Emu.Controllers.Network.NetworkInterfaceController;
using Emu.Services.Common;
using Emu.Services.NetworkInterface;
using Emu.UnitTest.Mocks;
using NetworkInterfaceController;
using VirtualNetworkController;

namespace Emu.UnitTest.Controllers
{
    public class NetworkInterfaceControllerTest
    {
        private readonly NetworkInterfaceController.INetworkInterfacesController controller;
        private readonly string testSubscriptionId1;
        private readonly string testResourceGroup1;
        private static readonly string apiVersion = "1";

        public NetworkInterfaceControllerTest() {

            // Do "global" initialization here; Called before every test method.
            var service = new NetworkInterfaceService(new InMemoryStorageService());
            controller = new NetworkInterfaceHandler(service);
            testSubscriptionId1 = Guid.NewGuid().ToString();
            testResourceGroup1 = "my-rg";
        }


        [Fact]
        public async Task TestCreateOrUpdateNetworkInterfaceAsync()
        {
            var networkInterfaceName = "my-ni";

            var networkInterface = new NetworkInterfaceController.NetworkInterface().GetNetworkInterfaceMock();

            var act = await controller.CreateOrUpdateAsync(testResourceGroup1, networkInterfaceName, networkInterface, apiVersion, testSubscriptionId1);

            var exp = new NetworkInterfaceController.NetworkInterface().GetNetworkInterfaceMock();
            exp.Properties.ProvisioningState = NetworkInterfaceController.ProvisioningState.Succeeded;
            exp.Id = $"/subscriptions/{testSubscriptionId1}/resourceGroups/{testResourceGroup1}/providers/Microsoft.Network/networkInterfaces/{networkInterfaceName}";
            
            Assert.Equivalent(exp, act);
        }

        [Fact]
        public async Task TestGetNetworkInterfaceAsync()
        {
            var networkInterfaceName = "my-ni";

            var networkInterface = new NetworkInterfaceController.NetworkInterface().GetNetworkInterfaceMock();

            var exp = await controller.CreateOrUpdateAsync(testResourceGroup1, networkInterfaceName, networkInterface, apiVersion, testSubscriptionId1);
            var act = await controller.GetAsync(testResourceGroup1, networkInterfaceName, apiVersion, testSubscriptionId1, null);

            Assert.Equivalent(exp, act);
        }
    }
}
