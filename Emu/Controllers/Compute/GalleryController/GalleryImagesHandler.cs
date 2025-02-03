using Emu.Common.RestApi;
using Emu.Common.Utils;
using Emu.Common.Validators;
using Emu.Services.Gallery;
using GalleryController;

namespace Emu.Controllers.Compute.GalleryController
{
    public class GalleryImageHandler : IGalleryImagesController
    {

        private readonly IGalleryImageService _galleryimageService;

        public GalleryImageHandler(IGalleryImageService galleryImageService) {
            _galleryimageService = galleryImageService;
        }

        public async Task<GalleryImage> CreateOrUpdateAsync(string subscriptionId, string resourceGroupName, string galleryName, string galleryImageName, string api_version, GalleryImage galleryImage)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            galleryImage.Id = ParameterHelper.GetComputeResourceId(subscriptionId, resourceGroupName, ParameterHelper.ResourceTypeGallery, galleryName, ParameterHelper.ResourceTypeGalleryImage, galleryImageName);
            galleryImage.Type = $"{ParameterHelper.ResourceCategoryCompute}/{ParameterHelper.ResourceTypeGalleryImage}";
            galleryImage.Name = galleryImageName;

            try
            {
                var op = await _galleryimageService.UpsertGalleryImage(subscriptionId, resourceGroupName, galleryName, galleryImageName, galleryImage);

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
                var galleryId = ParameterHelper.GetComputeResourceId(subscriptionId, resourceGroupName, ParameterHelper.ResourceTypeGallery, galleryName);
                throw new ParentResourceNotFoundException(ex.Message, "galleries/images", galleryId);
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

            return _galleryimageService.GetGalleryImage(subscriptionId, resourceGroupName, galleryName, galleryImageName);
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