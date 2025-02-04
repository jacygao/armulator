using Emu.Controllers.Compute.GalleryController;
using Emu.Services.Common;
using Emu.Services.Gallery;
using Emu.UnitTest.Mocks;
using GalleryController;

namespace Emu.UnitTest.Controllers
{
    public class GalleryControllerTest
    {
        private readonly GalleryController.IGalleriesController controller;
        private readonly string testSubscriptionId1;
        private readonly string testResourceGroup1;
        private static readonly string apiVersion = "1";

        public GalleryControllerTest()
        {
            // Do "global" initialization here; Called before every test method.
            var service = new GalleryService(new InMemoryStorageService());
            controller = new GalleryHandler(service);
            testSubscriptionId1 = Guid.NewGuid().ToString();
            testResourceGroup1 = "my-rg";
        }

        [Fact]
        public async Task TestCreateOrUpdateGalleryAsync()
        {
            var galleryName = "my-gallery";
            var galleryItem = new Gallery().PostSimpleGalleryMock();
            var act = await controller.CreateOrUpdateAsync(testSubscriptionId1, testResourceGroup1, galleryName, apiVersion, galleryItem);
            var exp = new Gallery
            {
                Id = $"/subscriptions/{testSubscriptionId1}/resourceGroups/{testResourceGroup1}/providers/Microsoft.Compute/galleries/{galleryName}",
                Properties = new GalleryProperties
                {
                    Description = galleryItem.Properties.Description,
                    Identifier = new GalleryIdentifier
                    {
                        UniqueName = $"{testSubscriptionId1}-{galleryName}"
                    },
                    ProvisioningState = GalleryProvisioningState.Creating,
                    SoftDeletePolicy = new SoftDeletePolicy
                    {
                        IsSoftDeleteEnabled = false
                    }
                },
                Location = galleryItem.Location,
                Name = galleryName,
                Type = "Microsoft.Compute/galleries"
            };

            Assert.Equivalent(exp, act);
        }

        [Fact]
        public async Task TestGetGalleryAsync()
        {
            var galleryName = "my-gallery";
            var galleryItem = new Gallery().PostSimpleGalleryMock();
            await controller.CreateOrUpdateAsync(testSubscriptionId1, testResourceGroup1, galleryName, apiVersion, galleryItem);

            var act = await controller.GetAsync(testSubscriptionId1, testResourceGroup1, galleryName, apiVersion, null, null);

            var exp = new Gallery
            {
                Id = $"/subscriptions/{testSubscriptionId1}/resourceGroups/{testResourceGroup1}/providers/Microsoft.Compute/galleries/{galleryName}",
                Properties = new GalleryProperties
                {
                    Description = galleryItem.Properties.Description,
                    Identifier = new GalleryIdentifier
                    {
                        UniqueName = $"{testSubscriptionId1}-{galleryName}"
                    },
                    ProvisioningState = GalleryProvisioningState.Succeeded,
                    SoftDeletePolicy = new SoftDeletePolicy
                    {
                        IsSoftDeleteEnabled = false
                    }
                },
                Location = galleryItem.Location,
                Name = galleryName,
                Type = "Microsoft.Compute/galleries"
            };

            Assert.Equivalent(exp, act);
        }
    }
}
