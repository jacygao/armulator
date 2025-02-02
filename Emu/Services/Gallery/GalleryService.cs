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
        private readonly string GalleryContainerName = "galleries";
        private readonly string GalleryImageContainerName = "galleryimages";
        private readonly string GalleryImageVersionContainerName = "galleryimageversions";

        public GalleryService(IStorageService stroageService) { 
            _storage = stroageService;
        }

        public async Task<GalleryController.Gallery> GetGallery(string subscriptionId, string resourceGroup, string name)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            // Download Image Metadata
            try
            {
                var stream = await _storage.DownloadFileAsync(GalleryContainerName, $"{subscriptionId}/{resourceGroup}/{name}.json");
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                var gallery = JsonSerializer.Deserialize<GalleryController.Gallery>(json);

                if (gallery != null)
                {
                    return gallery;
                }

                throw new ResourceNotFoundException($"image {name} does not exist");

            }
            catch
            {
                throw;
            }
        }

        public async Task<GalleryImage> GetGalleryImage(string subscriptionId, string resourceGroup, string galleryName, string imageName)
        {
            ArgumentException.ThrowIfNullOrEmpty(galleryName, nameof(galleryName));
            ArgumentException.ThrowIfNullOrEmpty(imageName, nameof(imageName));

            // Download Image Metadata
            try
            {
                var stream = await _storage.DownloadFileAsync(GalleryImageContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}.json");
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                var image = JsonSerializer.Deserialize<GalleryController.GalleryImage>(json);

                if (image != null)
                {
                    return image;
                }

                throw new ResourceNotFoundException($"image {imageName} does not exist");

            }
            catch
            {
                throw;
            }
        }

        public async Task<GalleryImageVersion> GetGalleryImageVersion(string subscriptionId, string resourceGroup, string galleryName, string imageName, string version)
        {
            ArgumentException.ThrowIfNullOrEmpty(galleryName, nameof(galleryName));
            ArgumentException.ThrowIfNullOrEmpty(imageName, nameof(imageName));
            ArgumentException.ThrowIfNullOrEmpty(version, nameof(version));

            // Download Image Metadata
            try
            {
                var stream = await _storage.DownloadFileAsync(GalleryImageVersionContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}/{version}.json");
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                var imageVersion = JsonSerializer.Deserialize<GalleryController.GalleryImageVersion>(json);

                if (imageVersion != null)
                {
                    return imageVersion;
                }

                throw new ResourceNotFoundException($"image version {version} does not exist");

            }
            catch
            {
                throw;
            }
        }

        public async Task<GalleryOperationType> UpsertGallery(string subscriptionId, string resourceGroup, string name, GalleryController.Gallery gallery)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentNullException.ThrowIfNull(gallery, nameof(gallery));

            var op = GalleryOperationType.Create;
            if (await _storage.ExistFile(GalleryContainerName, $"{subscriptionId}/{resourceGroup}/{name}.json"))
            {
                op = GalleryOperationType.Update;
            }

            gallery.Properties.ProvisioningState = GalleryProvisioningState.Succeeded;

            // Serialize the Image object to JSON
            var json = JsonSerializer.Serialize(gallery);
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
            await _storage.UploadFileAsync(GalleryContainerName, $"{subscriptionId}/{resourceGroup}/{name}.json", stream);

            return op;
        }

        public async Task<GalleryOperationType> UpsertGalleryImage(string subscriptionId, string resourceGroup, string galleryName, string imageName, GalleryImage image)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(galleryName, nameof(galleryName));
            ArgumentException.ThrowIfNullOrEmpty(imageName, nameof(imageName));
            ArgumentNullException.ThrowIfNull(image, nameof(image));

            // Validate if Gallery exists
            if (!await _storage.ExistFile(GalleryContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}.json")) {
                throw new FileNotFoundException($"Gallery {galleryName} can not be found.");
            }

            var op = GalleryOperationType.Create;
            // Check if image already exists, if yes it is an update request.
            if (await _storage.ExistFile(GalleryImageContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}.json"))
            {
                op = GalleryOperationType.Update;
            }

            image.Properties.ProvisioningState = GalleryProvisioningState.Succeeded;

            // Serialize the Image object to JSON
            var json = JsonSerializer.Serialize(image);
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
            await _storage.UploadFileAsync(GalleryImageContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}.json", stream);

            return op;
        }

        public async Task<GalleryOperationType> UpsertGalleryImageVersion(string subscriptionId, string resourceGroup, string galleryName, string imageName, string version, GalleryImageVersion imageVersion)
        {
            ArgumentException.ThrowIfNullOrEmpty(galleryName, nameof(galleryName));
            ArgumentException.ThrowIfNullOrEmpty(imageName, nameof(imageName));
            ArgumentNullException.ThrowIfNull(version, nameof(version));

            // Validate if Gallery Image exists
            if (!await _storage.ExistFile(GalleryImageContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}.json"))
            {
                throw new FileNotFoundException($"GalleryImage {imageName} can not be found.");
            }

            var op = GalleryOperationType.Create;
            // Check if image already exists, if yes it is an update request.
            if (await _storage.ExistFile(GalleryImageVersionContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}/{version}.json"))
            {
                op = GalleryOperationType.Update;
            }

            imageVersion.Properties.ProvisioningState = GalleryProvisioningState.Succeeded;

            // Serialize the Image object to JSON
            var json = JsonSerializer.Serialize(imageVersion);
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
            await _storage.UploadFileAsync(GalleryImageContainerName, $"{subscriptionId}/{resourceGroup}/{galleryName}/{imageName}/{version}.json", stream);

            return op;
        }

        private async Task<T> get<T>(string containerName, string subscriptionId, string resourceGroup, string filename, Func<Stream, Task<T>> converter)
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
