using Emu.Common.RestApi;
using Emu.Common.Utils;
using Emu.Common.Validators;
using Emu.Services.Gallery;
using GalleryController;

namespace Emu.Controllers.Compute.GalleryController
{
    public class GalleryImageHandler : IGalleryImagesController
    {

        private readonly IGalleryService _galleryService;

        public GalleryImageHandler(IGalleryService galleryService) {
            _galleryService = galleryService;
        }

        public async Task<GalleryImage> CreateOrUpdateAsync(string subscriptionId, string resourceGroupName, string galleryName, string galleryImageName, string api_version, GalleryImage galleryImage)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            galleryImage.Id = ParameterHelper.GetComputeResourceId(subscriptionId, resourceGroupName, ParameterHelper.ResourceTypeGalleryImage, galleryName);
            galleryImage.Type = $"{ParameterHelper.ResourceCategoryCompute}/{ParameterHelper.ResourceTypeGalleryImage}";
            galleryImage.Name = galleryImageName;

            try
            {
                var op = await _galleryService.UpsertGalleryImage(subscriptionId, resourceGroupName, galleryName, galleryImageName, galleryImage);

                // mark state as creating to emulate response even through status is Succeeded in storage;
                switch (op)
                {
                    case GalleryOperationType.Create:
                        galleryImage.Properties.ProvisioningState = GalleryProvisioningState.Creating;
                        break;
                    case GalleryOperationType.Update:
                        galleryImage.Properties.ProvisioningState = GalleryProvisioningState.Updating;
                        break;
                    default:
                        break;
                }

                return galleryImage;
            }
            catch (FileNotFoundException ex)
            {
                throw new ParentResourceNotFoundException(ex.Message, "galleries/images", galleryImage.Id);
            }
        }

        public Task DeleteAsync(string subscriptionId, string resourceGroupName, string galleryName, string galleryImageName, string api_version)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            throw new NotImplementedException();
        }

        public Task<GalleryImage> GetAsync(string subscriptionId, string resourceGroupName, string galleryName, string galleryImageName, string api_version)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            throw new NotImplementedException();
        }

        public Task<GalleryImageList> ListByGalleryAsync(string subscriptionId, string resourceGroupName, string galleryName, string api_version)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            throw new NotImplementedException();
        }

        public Task<GalleryImage> UpdateAsync(string subscriptionId, string resourceGroupName, string galleryName, string galleryImageName, string api_version, GalleryImageUpdate galleryImage)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            throw new NotImplementedException();
        }
    }
}