using Emu.Services.Common;
using System.Text.Json;

namespace Emu.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly IStorageService _storage;
        private readonly string containerName = "images";
        public ImageService(IStorageService stroage) { 
            _storage = stroage;
        }

        async Task IImageService.CreateImageAsync(string filename, ImageController.Image image)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(filename, nameof(filename));
            ArgumentNullException.ThrowIfNull(image, nameof(image));

            // Serialize the Image object to JSON
            var json = JsonSerializer.Serialize(image);
            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
            {
                await _storage.UploadFileAsync(containerName, $"{filename}.json", stream);

            }
        }

        async Task<ImageController.Image> IImageService.GetImageAsync(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
