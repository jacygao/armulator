namespace Emu.Services.Gallery
{
    public interface IGalleryService
    {
        Task<GalleryOperationType> UpsertGallery(string subscriptionId, string resourceGroup, string name, GalleryController.Gallery gallery);

        Task UpsertGalleryImage(string subscriptionId, string resourceGroup, string name, GalleryController.GalleryImage image);

        Task<GalleryController.GalleryImage> GetGalleryImage(string subscriptionId, string resourceGroup, string name);

        Task<GalleryController.GalleryImageVersion> GetGalleryImageVersion(string subscriptionId, string resourceGroup, string name, string version);
    }
}
