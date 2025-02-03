using GalleryController;

namespace Emu.Services.Gallery
{
    public interface IGalleryImageVersionService
    {
        Task<GalleryOperationType> UpsertGalleryImageVersion(string subscriptionId, string resourceGroup, string galleryName, string imageName, string version, GalleryImageVersion imageVersion);

        Task<GalleryController.GalleryImageVersion> GetGalleryImageVersion(string subscriptionId, string resourceGroup, string galleryName, string name, string version);
    }
}
