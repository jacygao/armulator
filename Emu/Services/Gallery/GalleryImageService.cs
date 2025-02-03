using Emu.Common.RestApi;
using Emu.Services.Common;
using GalleryController;

namespace Emu.Services.Gallery
{
    public class GalleryImageService(IStorageService storageService) : ServiceBase<GalleryImage>(storageService), IGalleryImageService
    {
        public async Task<GalleryImage> GetGalleryImage(string subscriptionId, string resourceGroup, string galleryName, string imageName)
        {
            ArgumentException.ThrowIfNullOrEmpty(galleryName, nameof(galleryName));
            ArgumentException.ThrowIfNullOrEmpty(imageName, nameof(imageName));

            return await GetAsync(ServiceConstants.GalleryImageContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}.json");
        }

        public async Task<GalleryOperationType> UpsertGalleryImage(string subscriptionId, string resourceGroup, string galleryName, string imageName, GalleryImage image)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(galleryName, nameof(galleryName));
            ArgumentException.ThrowIfNullOrEmpty(imageName, nameof(imageName));
            ArgumentNullException.ThrowIfNull(image, nameof(image));

            // Validate if Gallery exists
            if (!await FileExists(ServiceConstants.GalleryContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}.json"))
            {
                throw new FileNotFoundException($"Gallery {galleryName} can not be found.");
            }

            var op = GalleryOperationType.Create;
            // Check if image already exists, if yes it is an update request.
            if (await FileExists(ServiceConstants.GalleryImageContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}.json"))
            {
                op = GalleryOperationType.Update;
            }

            image.Properties.ProvisioningState = GalleryProvisioningState.Succeeded;

            await CreateAsync(ServiceConstants.GalleryImageContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}.json", image);
         
            return op;
        }
    }
}
