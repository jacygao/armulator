using Emu.Common.RestApi;
using Emu.Services.Common;
using GalleryController;

namespace Emu.Services.Gallery
{
    public class GalleryImageVersionService(IStorageService storageService) : ServiceBase<GalleryImageVersion>(storageService), IGalleryImageVersionService
    {
        public async Task<GalleryImageVersion> GetGalleryImageVersion(string subscriptionId, string resourceGroup, string galleryName, string imageName, string version)
        {
            ArgumentException.ThrowIfNullOrEmpty(galleryName, nameof(galleryName));
            ArgumentException.ThrowIfNullOrEmpty(imageName, nameof(imageName));
            ArgumentException.ThrowIfNullOrEmpty(version, nameof(version));

            return await GetAsync(ServiceConstants.GalleryImageVersionContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}/{version}.json");
        }

        public async Task<GalleryOperationType> UpsertGalleryImageVersion(string subscriptionId, string resourceGroup, string galleryName, string imageName, string version, GalleryImageVersion imageVersion)
        {
            ArgumentException.ThrowIfNullOrEmpty(galleryName, nameof(galleryName));
            ArgumentException.ThrowIfNullOrEmpty(imageName, nameof(imageName));
            ArgumentNullException.ThrowIfNull(version, nameof(version));

            // Validate if Gallery Image exists
            if (!await FileExists(ServiceConstants.GalleryImageContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}.json"))
            {
                throw new FileNotFoundException($"GalleryImage {imageName} can not be found.");
            }

            var op = GalleryOperationType.Create;
            // Check if image already exists, if yes it is an update request.
            if (await FileExists(ServiceConstants.GalleryImageVersionContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}/{version}.json"))
            {
                op = GalleryOperationType.Update;
            }

            imageVersion.Properties.ProvisioningState = GalleryProvisioningState.Succeeded;

            await CreateAsync(ServiceConstants.GalleryImageContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}/{version}.json", imageVersion);
            
            return op;
        }
    }
}
