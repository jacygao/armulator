using Emu.Common.RestApi;
using Emu.Services.Common;
using GalleryController;
using System.Threading.Tasks;
using System.Text.Json;

namespace Emu.Services.Gallery
{
    public class GalleryService : IGalleryService
    {
        private readonly IStorageService _storage;
        private readonly string containerName = "galleries";

        public GalleryService(IStorageService stroageService) { 
            _storage = stroageService;
        }

        public Task<GalleryImage> GetGalleryImage(string subscriptionId, string resourceGroup, string name)
        {
            throw new NotImplementedException();
        }

        public Task<GalleryImageVersion> GetGalleryImageVersion(string subscriptionId, string resourceGroup, string name, string version)
        {
            throw new NotImplementedException();
        }

        public async Task<GalleryOperationType> UpsertGallery(string subscriptionId, string resourceGroup, string name, GalleryController.Gallery gallery)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentNullException.ThrowIfNull(gallery, nameof(gallery));

            var op = GalleryOperationType.Create;
            if (await _storage.ExistFile(containerName, $"{subscriptionId}/{resourceGroup}/metadata/{name}.json"))
            {
                op = GalleryOperationType.Update;
            }

            gallery.Properties.ProvisioningState = GalleryProvisioningState.Succeeded;

            // Serialize the Image object to JSON
            var json = JsonSerializer.Serialize(gallery);
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
            await _storage.UploadFileAsync(containerName, $"{subscriptionId}/{resourceGroup}/metadata/{name}.json", stream);

            return op;
        }

        public Task UpsertGalleryImage(string subscriptionId, string resourceGroup, string name, GalleryImage image)
        {
            throw new NotImplementedException();
        }

        private async Task<T> get<T>(string subscriptionId, string resourceGroup, string filename, Func<Stream, Task<T>> converter)
        {
            try
            {
                var stream = await _storage.DownloadFileAsync(containerName, $"{subscriptionId}/{resourceGroup}/metadata/{filename}.json");
                var res = await converter(stream);
                return res;

            }
            catch
            {
                throw;
            }
        }
    }
}
