using Emu.Services.Common;

namespace Emu.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly IStorage<ImageController.Image> _storage;
        public ImageService(IStorage<ImageController.Image> stroage) { 
            _storage = stroage;
        }

        Task IImageService.CreateImageAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<ImageController.Image> IImageService.GetImageAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
