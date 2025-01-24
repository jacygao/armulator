namespace Emu.Services.Image
{
    public interface IImageService
    {
        Task CreateImageAsync(string id);

        Task<ImageController.Image> GetImageAsync(string id);
    }
}
