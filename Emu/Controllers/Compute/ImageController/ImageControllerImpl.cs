using Emu.Common.RestApi;
using Emu.Services.Image;
using ImageController;
using Azure.Core;

namespace Emu.Controllers.Compute.ImageController
{
    public class ImageControllerImpl : IImagesController
    {
        private readonly IImageService _imageService;
        public ImageControllerImpl(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task<Image> CreateOrUpdateAsync(string resourceGroupName, string imageName, Image parameters, string api_version, string subscriptionId)
        {
            Validate(subscriptionId, resourceGroupName);

            await _imageService.CreateImageAsync(imageName, parameters);

            // Enrich response
            parameters.Properties = parameters.Properties;
            parameters.Location = new AzureLocation(parameters.Location).Name;
            parameters.Name = imageName;

            return parameters;
        }

        public Task DeleteAsync(string resourceGroupName, string imageName, string api_version, string subscriptionId)
        {
            Validate(subscriptionId, resourceGroupName);
            throw new NotImplementedException();
        }

        public async Task<Image> GetAsync(string resourceGroupName, string imageName, string expand, string api_version, string subscriptionId)
        {
            Validate(subscriptionId, resourceGroupName);
            return null;
        }

        public Task<ImageListResult> ListAsync(string api_version, string subscriptionId)
        {
            ValidateSubscription(subscriptionId);

            throw new NotImplementedException();
        }

        public Task<ImageListResult> ListByResourceGroupAsync(string resourceGroupName, string api_version, string subscriptionId)
        {
            Validate(subscriptionId, resourceGroupName);

            throw new NotImplementedException();
        }

        public Task<Image> UpdateAsync(string resourceGroupName, string imageName, ImageUpdate parameters, string api_version, string subscriptionId)
        {
            Validate(resourceGroupName, subscriptionId);

            throw new NotImplementedException();
        }

        private void Validate(string subscriptionId, string resourceGroupName)
        {
            if (subscriptionId == "123") { throw new InvalidSubscriptionIdException("invalid subscription id", subscriptionId); }

            if (resourceGroupName == "unknown") { throw new InvalidResourceGroupException("invalid resource group", resourceGroupName); }
        }

        private void ValidateSubscription(string subscriptionId)
        {
            if (subscriptionId == "123") { throw new InvalidSubscriptionIdException("invalid subscription id", subscriptionId); }
        }
    }
}