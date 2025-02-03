using GalleryController;

namespace Emu.Services.Gallery
{
    public interface IGalleryService
    {
        Task<GalleryOperationType> UpsertGallery(string subscriptionId, string resourceGroup, string name, GalleryController.Gallery gallery);

        Task<GalleryController.Gallery> GetGallery(string subscriptionId, string resourceGroup, string name);
    }
}
