using Emu.Controllers.Compute.ImageController;
using Emu.Services.Common;
using Emu.Services.Image;
using Emu.UnitTest.Mocks;
using Emu.UnitTest.TestUtils;
using ImageController;

namespace Emu.UnitTest.Controllers
{
    public class ImageControllerTest
    {
        private readonly ImageController.IImagesController controller;
        private readonly string testSubscriptionId1;
        private readonly string testResourceGroup1;
        private static readonly string apiVersion = "1";

        public ImageControllerTest()
        {
            // Do "global" initialization here; Called before every test method.
            var service = new ImageService(new InMemoryStorageService());
            controller = new ImageHandler(service);
            testSubscriptionId1 = Guid.NewGuid().ToString();
            testResourceGroup1 = "my-rg";
        }

        [Fact]
        public async Task TestCreateOrUpdateImageAsync()
        {
            var imageName = "my-image";

            var image = new Image().GetPostMock();

            var act = await controller.CreateOrUpdateAsync(testResourceGroup1, imageName, image, apiVersion, testSubscriptionId1);

            Assert.Equal(image, act);
        }

        [Fact]
        public async Task TestGetImageAsync()
        {
            var imageName = "my-image";
            var image = new Image().GetPostMock();
            var exp = await controller.CreateOrUpdateAsync(testResourceGroup1, imageName, image, apiVersion, testSubscriptionId1);
            var act = await controller.GetAsync(testResourceGroup1, imageName, null, apiVersion, testSubscriptionId1);
            Assert.Equal(exp, act);
        }
    }
}
