using Azure.Storage.Blobs.Models;
using Emu.Common.RestApi;
using Emu.Services.Common;
using ImageController;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Emu.Services.Image
{
    public class ImageService : ServiceBase<ImageController.Image>, IImageService
    {
        private readonly IStorageService _storage;
        private readonly string containerName = "images";

        public ImageService(IStorageService stroageService) : base(stroageService) {
            _storage = stroageService;
        }

        async Task IImageService.CreateImageAsync(string subscriptionId, string resourceGroup, string filename, ImageController.Image image)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(filename, nameof(filename));
            ArgumentNullException.ThrowIfNull(image, nameof(image));

            await CreateAsync(containerName, $"{subscriptionId}/{resourceGroup}/{filename}.json", image);
        }

        async Task<ImageController.Image> IImageService.GetImageAsync(string subscriptionId, string resourceGroup, string filename)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(filename, nameof(filename));

            return await GetAsync(containerName, $"{subscriptionId}/{resourceGroup}/{filename}.json");
        }

        async Task<List<ImageController.Image>> IImageService.ListImagesAsync(string subscriptionId)
        {
            return await _storage.ListFilesContentRecursiveAsync(containerName, subscriptionId, ConvertToCustomFileDataAsync);
        }

        private async Task<ImageController.Image> ConvertToCustomFileDataAsync(string blobName, Stream content)
        {
            using (content)
            {
                var image = await JsonSerializer.DeserializeAsync<ImageController.Image>(content);
                return image;
            }
        }
    }
}
