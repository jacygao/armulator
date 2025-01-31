using GalleryController;

namespace Emu.Services.Gallery
{
    public interface IGalleryService
    {
        Task<GalleryOperationType> UpsertGallery(string subscriptionId, string resourceGroup, string name, GalleryController.Gallery gallery);

        Task<GalleryController.Gallery> GetGallery(string subscriptionId, string resourceGroup, string name);

        Task<GalleryOperationType> UpsertGalleryImage(string subscriptionId, string resourceGroup, string galleryName, string imageName, GalleryImage image);

        Task<GalleryController.GalleryImage> GetGalleryImage(string subscriptionId, string resourceGroup, string galleryName, string imageName);

        Task<GalleryOperationType> UpsertGalleryImageVersion(string subscriptionId, string resourceGroup, string galleryName, string imageName, string version, GalleryImageVersion imageVersion);

        Task<GalleryController.GalleryImageVersion> GetGalleryImageVersion(string subscriptionId, string resourceGroup, string galleryName, string name, string version);
    }
}
