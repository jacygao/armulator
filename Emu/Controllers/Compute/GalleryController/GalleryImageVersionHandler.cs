using Emu.Common.RestApi;
using Emu.Common.Utils;
using Emu.Common.Validators;
using Emu.Services.Gallery;
using GalleryController;

namespace Emu.Controllers.Compute.GalleryController
{
    public class GalleryImageVersionHandler : IGalleryImageVersionsController
    {
        private readonly IGalleryImageVersionService _galleryImageVersionService;

        public GalleryImageVersionHandler(IGalleryImageVersionService galleryImageVersionService)
        {
            _galleryImageVersionService = galleryImageVersionService;
        }

        public async Task<GalleryImageVersion> CreateOrUpdateAsync(string subscriptionId, string resourceGroupName, string galleryName, string galleryImageName, string galleryImageVersionName, string api_version, GalleryImageVersion galleryImageVersion)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            try
            {
                var op = await _galleryImageVersionService.UpsertGalleryImageVersion(subscriptionId, resourceGroupName, galleryName, galleryImageName, galleryImageVersionName, galleryImageVersion);

                // mark state as creating to emulate response even through status is Succeeded in storage;
                switch (op)
                {
                    case GalleryOperationType.Create:
                        galleryImageVersion.Properties.ProvisioningState = GalleryProvisioningState.Creating;
                        break;
                    case GalleryOperationType.Update:
                        galleryImageVersion.Properties.ProvisioningState = GalleryProvisioningState.Updating;
                        break;
                    default:
                        break;
                }

                return galleryImageVersion;
            }
            catch (FileNotFoundException ex)
            {
                // FIXME: udpate according to real response
                var galleryId = ParameterHelper.GetComputeResourceId(subscriptionId, resourceGroupName, ParameterHelper.ResourceTypeGallery, galleryName);
                throw new ParentResourceNotFoundException(ex.Message, "galleries/images", galleryId);
            }
        }

        public Task DeleteAsync(string subscriptionId, string resourceGroupName, string galleryName, string galleryImageName, string galleryImageVersionName, string api_version)
        {
            throw new NotImplementedException();
        }

        public Task<GalleryImageVersion> GetAsync(string subscriptionId, string resourceGroupName, string galleryName, string galleryImageName, string galleryImageVersionName, _expand? expand, string api_version)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            return _galleryImageVersionService.GetGalleryImageVersion(subscriptionId, resourceGroupName, galleryName, galleryImageName, galleryImageVersionName);
        }

        public Task<GalleryImageVersionList> ListByGalleryImageAsync(string subscriptionId, string resourceGroupName, string galleryName, string galleryImageName, string api_version)
        {
            throw new NotImplementedException();
        }

        public Task<GalleryImageVersion> UpdateAsync(string subscriptionId, string resourceGroupName, string galleryName, string galleryImageName, string galleryImageVersionName, string api_version, GalleryImageVersionUpdate galleryImageVersion)
        {
            throw new NotImplementedException();
        }
    }
}