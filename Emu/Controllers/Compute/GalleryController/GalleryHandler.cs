using Emu.Common.Utils;
using Emu.Common.Validators;
using Emu.Services.Gallery;
using GalleryController;
using System.Net;

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

            if (gallery.Properties.SoftDeletePolicy == null)
            {
                gallery.Properties.SoftDeletePolicy = new SoftDeletePolicy
                {
                    IsSoftDeleteEnabled = false
                };
            }

            gallery.Name = galleryName;
            gallery.Type = $"{ParameterHelper.ResourceCategoryCompute}/{ParameterHelper.ResourceTypeGallery}";
            gallery.Id = ParameterHelper.GetComputeResourceId(
                subscriptionId, 
                resourceGroupName, 
                ParameterHelper.ResourceCategoryCompute, 
                ParameterHelper.ResourceTypeGallery, 
                galleryName);

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

        public async Task<Gallery> GetAsync(string subscriptionId, string resourceGroupName, string galleryName, string api_version, _select? select, _expand? expand)
        {
            // TODO: implement select and expand filters
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            return await _galleryService.GetGallery(subscriptionId, resourceGroupName, galleryName);
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