using Azure.Storage.Blobs.Models;
using Emu.Common.RestApi;
using Emu.Services.Common;
using ImageController;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Emu.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly IStorageService _storage;
        private readonly string containerName = "images";
        public ImageService(IStorageService stroageService) {
            _storage = stroageService;
        }

        async Task IImageService.CreateImageAsync(string subscriptionId, string resourceGroup, string filename, ImageController.Image image)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(filename, nameof(filename));
            ArgumentNullException.ThrowIfNull(image, nameof(image));

            // Serialize the Image object to JSON
            var json = JsonSerializer.Serialize(image);
            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
            {
                await _storage.UploadFileAsync(containerName, $"{subscriptionId}/{resourceGroup}/{filename}.json", stream);

            }
        }

        async Task<ImageController.Image> IImageService.GetImageAsync(string subscriptionId, string resourceGroup, string filename)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(filename, nameof(filename));

            // Download Image Metadata
            try
            {
                var stream = await _storage.DownloadFileAsync(containerName, $"{subscriptionId}/{resourceGroup}/{filename}.json");
                using (var reader = new StreamReader(stream))
                {
                    var json = await reader.ReadToEndAsync();
                    var image = JsonSerializer.Deserialize<ImageController.Image>(json);

                    if (image != null) {
                        return image;
                    }

                    throw new ResourceNotFoundException($"image {filename} does not exist");
                }

            } catch
            {
                throw;
            }
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
