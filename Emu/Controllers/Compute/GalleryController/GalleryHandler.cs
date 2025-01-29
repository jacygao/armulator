using GalleryController;

namespace Emu.Controllers.Compute.GalleryController
{
    public class GalleryHandler : IGalleriesController
    {
        public Task<Gallery> CreateOrUpdateAsync(string subscriptionId, string resourceGroupName, string galleryName, string api_version, Gallery gallery)
        {
            throw new NotImplementedException();
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