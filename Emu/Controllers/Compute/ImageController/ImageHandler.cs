using Emu.Common.RestApi;
using Emu.Services.Image;
using ImageController;
using Azure.Core;
using Emu.Common.Validators;

namespace Emu.Controllers.Compute.ImageController
{
    public class ImageHandler : IImagesController
    {
        private readonly IImageService _imageService;

        public ImageHandler(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task<Image> CreateOrUpdateAsync(string resourceGroupName, string imageName, Image parameters, string api_version, string subscriptionId)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            // Enrich
            parameters.Location = new AzureLocation(parameters.Location).Name;
            parameters.Name = imageName;

            await _imageService.CreateImageAsync(subscriptionId, resourceGroupName, imageName, parameters);

            return parameters;
        }

        public Task DeleteAsync(string resourceGroupName, string imageName, string api_version, string subscriptionId)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);
            throw new NotImplementedException();
        }

        public async Task<Image> GetAsync(string resourceGroupName, string imageName, string expand, string api_version, string subscriptionId)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            return await _imageService.GetImageAsync(subscriptionId, resourceGroupName, imageName);
        
        }

        public async Task<ImageListResult> ListAsync(string api_version, string subscriptionId)
        {
            CommonValidators.ValidateSubscription(subscriptionId);
            var resp = new ImageListResult
            {
                Value = await _imageService.ListImagesAsync(subscriptionId)
            };
            // TODO: Support skip token / nextlink

            return resp;
        }

        public Task<ImageListResult> ListByResourceGroupAsync(string resourceGroupName, string api_version, string subscriptionId)
        {
            CommonValidators.Validate(subscriptionId, resourceGroupName);

            throw new NotImplementedException();
        }

        public Task<Image> UpdateAsync(string resourceGroupName, string imageName, ImageUpdate parameters, string api_version, string subscriptionId)
        {
            CommonValidators.Validate(resourceGroupName, subscriptionId);

            throw new NotImplementedException();
        }
    }
}