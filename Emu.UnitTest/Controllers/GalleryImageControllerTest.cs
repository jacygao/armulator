using Emu.Controllers.Compute.GalleryController;
using Emu.Services.Common;
using Emu.Services.Gallery;
using GalleryController;

namespace Emu.UnitTest.Controllers
{
    internal class GalleryImageControllerTest
    {
        private readonly IGalleryImagesController controller;
        private readonly string testSubscriptionId1;
        private readonly string testResourceGroup1;
        private static readonly string apiVersion = "1";

        public GalleryImageControllerTest()
        {
            // Do "global" initialization here; Called before every test method.
            var service = new GalleryImageService(new InMemoryStorageService());
            controller = new GalleryImageHandler(service);
            testSubscriptionId1 = Guid.NewGuid().ToString();
            testResourceGroup1 = "my-rg";
        }
    }
}
