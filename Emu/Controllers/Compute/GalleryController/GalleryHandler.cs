using Emu.Common.Validators;
using Emu.Services.Gallery;
using GalleryController;

namespace Emu.Controllers.Compute.GalleryController
{
    public class GalleryHandler : IGalleriesController
    {
        private readonly IGalleryService _galleryService;

        public GalleryHandler(IGalleryService galleryService) {
            _galleryService = galleryService;
        }

        public async Task<Gallery> CreateOrUpdateAsync(string subscriptionId, string resourceGroupName, string galleryName, string api_version, Gallery gallery)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            if (gallery.Properties.Identifier == null)
            {
                gallery.Properties.Identifier = new GalleryIdentifier
                {
                    UniqueName = $"{subscriptionId}-{galleryName}"
                };
            } else
            {
                gallery.Properties.Identifier.UniqueName = $"{subscriptionId}-{galleryName}";
            }

            gallery.Name = galleryName;

            var op = await _galleryService.UpsertGallery(subscriptionId, resourceGroupName, galleryName, gallery);

            // mark state as creating to emulate response even through status is Succeeded in storage;
            switch (op) {
                case GalleryOperationType.Create:
                    gallery.Properties.ProvisioningState = GalleryProvisioningState.Creating;
                    break;
                case GalleryOperationType.Update:
                    gallery.Properties.ProvisioningState = GalleryProvisioningState.Updating;
                    break;
                default:
                    break;
            }

            return gallery;
        }

        public Task DeleteAsync(string subscriptionId, string resourceGroupName, string galleryName, string api_version)
        {
            throw new NotImplementedException();
        }

        public Task<Gallery> GetAsync(string subscriptionId, string resourceGroupName, string galleryName, string api_version, _select? select, _expand? expand)
        {
            throw new NotImplementedException();
        }

        public Task<GalleryList> ListAsync(string subscriptionId, string api_version)
        {
            throw new NotImplementedException();
        }

        public Task<GalleryList> ListByResourceGroupAsync(string subscriptionId, string resourceGroupName, string api_version)
        {
            throw new NotImplementedException();
        }

        public Task<Gallery> UpdateAsync(string subscriptionId, string resourceGroupName, string galleryName, string api_version, GalleryUpdate gallery)
        {
            throw new NotImplementedException();
        }
    }
}