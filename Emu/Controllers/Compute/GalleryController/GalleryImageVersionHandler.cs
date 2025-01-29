using GalleryController;

namespace Emu.Controllers.Compute.GalleryController
{
    public class GalleryImageVersionHandler : IGalleryImageVersionsController
    {
        public Task<GalleryImageVersion> CreateOrUpdateAsync(string subscriptionId, string resourceGroupName, string galleryName, string galleryImageName, string galleryImageVersionName, string api_version, GalleryImageVersion galleryImageVersion)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string subscriptionId, string resourceGroupName, string galleryName, string galleryImageName, string galleryImageVersionName, string api_version)
        {
            throw new NotImplementedException();
        }

        public Task<GalleryImageVersion> GetAsync(string subscriptionId, string resourceGroupName, string galleryName, string galleryImageName, string galleryImageVersionName, _expand? expand, string api_version)
        {
            throw new NotImplementedException();
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