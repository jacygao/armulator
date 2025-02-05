using Emu.Controllers.Compute.GalleryController;
using Emu.Controllers.Compute.VirtualMachineController;
using Emu.Controllers.Network.NetworkInterfaceController;
using Emu.Services.Common;
using Emu.Services.Gallery;
using Emu.Services.NetworkInterface;
using Emu.Services.VirtualMachine;
using Emu.UnitTest.Mocks;
using GalleryController;
using VirtualNetworkController;
namespace Emu.UnitTest.Controllers
{
    public class VirtualMachineControllerTest
    {
        private readonly VirtualMachineController.IVirtualMachinesController vmController;
        private readonly GalleryController.IGalleriesController galleryController;
        private readonly GalleryController.IGalleryImagesController galleryImagesController;
        private readonly NetworkInterfaceController.INetworkInterfacesController networkInterfaceController;
        private readonly string testSubscriptionId1;
        private readonly string testResourceGroup1;
        private static readonly string apiVersion = "1";

        public VirtualMachineControllerTest()
        {
            // Do "global" initialization here; Called before every test method.
            var storage = new InMemoryStorageService();
            var service = new VirtualMachineService(storage);
            vmController = new VirtualMachineHandler(new VirtualMachineService(storage));
            galleryController = new GalleryHandler(new GalleryService(storage));
            galleryImagesController = new GalleryImageHandler(new GalleryImageService(storage));
            networkInterfaceController = new NetworkInterfaceHandler(new NetworkInterfaceService(storage));

            testSubscriptionId1 = Guid.NewGuid().ToString();
            testResourceGroup1 = "my-rg";
        }

        [Fact]
        public async Task TestCreateOrUpdateVirtualMachineAsync()
        {
            var gallery = await galleryController.CreateOrUpdateAsync(
                testSubscriptionId1, 
                testResourceGroup1, 
                "my-gallery", 
                apiVersion, 
                new Gallery().PostSimpleGalleryMock());

            var galleryImage = await galleryImagesController.CreateOrUpdateAsync(
                testSubscriptionId1, 
                testResourceGroup1, 
                gallery.Name,
                "my-image",
                apiVersion, 
                new GalleryImage().PostSimpleGalleryImageMock());

            var networkInterface = await networkInterfaceController.CreateOrUpdateAsync(
                testResourceGroup1, 
                "my-ni", 
                new NetworkInterfaceController.NetworkInterface().GetNetworkInterfaceMock(),
                apiVersion,
                testSubscriptionId1);

            var virtualMachine = new VirtualMachineController.VirtualMachine().CreateFromGeneralizedSharedImageMock(galleryImage.Id, networkInterface.Id);

            var act = await vmController.CreateOrUpdateAsync(
                testResourceGroup1, 
                "my-vm", 
                virtualMachine, 
                apiVersion, 
                testSubscriptionId1,
                null, null);

            var exp = new VirtualMachineController.VirtualMachine().CreateFromGeneralizedSharedImageMock(galleryImage.Id, networkInterface.Id);
            exp.Properties.StorageProfile.DataDisks = [];
            exp.Properties.OsProfile.AdminPassword = null; // masked
            exp.Properties.OsProfile.WindowsConfiguration = new VirtualMachineController.WindowsConfiguration()
            {
                ProvisionVMAgent = true,
                EnableAutomaticUpdates = true
            };
            exp.Properties.OsProfile.Secrets = [];
            exp.Properties.ProvisioningState = "Creating";
            exp.Id = $"/subscriptions/{testSubscriptionId1}/resourceGroups/{testResourceGroup1}/providers/Microsoft.Compute/virtualMachines/my-vm";
            exp.Name = "my-vm";
            exp.Type = "Microsoft.Compute/virtualMachines";
            Assert.Equivalent(exp, act);
        }
    }
}
