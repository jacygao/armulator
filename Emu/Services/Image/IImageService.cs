namespace Emu.Services.Image
{
    public interface IImageService
    {
        Task CreateImageAsync(string subscriptionId, string resourceGroup, string filename, ImageController.Image image);

        Task<ImageController.Image> GetImageAsync(string subscriptionId, string resourceGroup, string id);

        Task<List<ImageController.Image>> ListImagesAsync(string subscriptionId);
    }
}
