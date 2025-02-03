using Emu.Common.RestApi;
using Emu.Services.Common;
using GalleryController;
using System.Text.Json;

namespace Emu.Services.Gallery
{
    public class GalleryService(IStorageService storageService) : ServiceBase<GalleryController.Gallery>(storageService), IGalleryService
    {
        public async Task<GalleryController.Gallery> GetGallery(string subscriptionId, string resourceGroup, string name)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            return await GetAsync(ServiceConstants.GalleryContainerName, $"{subscriptionId}/{resourceGroup}/{name}.json");
        }

        public async Task<GalleryOperationType> UpsertGallery(string subscriptionId, string resourceGroup, string name, GalleryController.Gallery gallery)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentNullException.ThrowIfNull(gallery, nameof(gallery));

            var op = GalleryOperationType.Create;
            if (await FileExists(ServiceConstants.GalleryContainerName, $"{subscriptionId}/{resourceGroup}/{name}.json"))
            {
                op = GalleryOperationType.Update;
            }

            gallery.Properties.ProvisioningState = GalleryProvisioningState.Succeeded;

            await CreateAsync(ServiceConstants.GalleryContainerName, $"{subscriptionId}/{resourceGroup}/{name}.json", gallery);

            return op;
        }
    }
}
