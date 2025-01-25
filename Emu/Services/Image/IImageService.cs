namespace Emu.Services.Image
{
    public interface IImageService
    {
        Task CreateImageAsync(string filename, ImageController.Image image);

        Task<ImageController.Image> GetImageAsync(string id);
    }
}
