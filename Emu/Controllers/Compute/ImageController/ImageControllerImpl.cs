using Emu.Utilities.RestApi;
using ImageController;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace Emu.Controllers.Compute.ImageController
{
    public class ImageControllerImpl : IImagesController
    {
        public ImageControllerImpl()
        {
        }

        public Task<Image> CreateOrUpdateAsync(string resourceGroupName, string imageName, Image parameters, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string resourceGroupName, string imageName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public async Task<Image> GetAsync(string resourceGroupName, string imageName, string expand, string api_version, string subscriptionId)
        {
            if (subscriptionId == "123") { throw new InvalidSubscriptionIdException("invalid subscription id", subscriptionId); }

            return null;
        }

        public Task<ImageListResult> ListAsync(string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<ImageListResult> ListByResourceGroupAsync(string resourceGroupName, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<Image> UpdateAsync(string resourceGroupName, string imageName, ImageUpdate parameters, string api_version, string subscriptionId)
        {
            throw new NotImplementedException();
        }
    }
}