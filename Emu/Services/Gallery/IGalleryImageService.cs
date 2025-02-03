using GalleryController;

namespace Emu.Services.Gallery
{
    public interface IGalleryImageService
    {
        Task<GalleryOperationType> UpsertGalleryImage(string subscriptionId, string resourceGroup, string galleryName, string imageName, GalleryImage image);

        Task<GalleryController.GalleryImage> GetGalleryImage(string subscriptionId, string resourceGroup, string galleryName, string imageName);
    }
}
