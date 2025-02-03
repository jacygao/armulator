using ImageController;

namespace Emu.UnitTest.Mocks
{
    internal static class ImageExtensions
    {
        public static Image GetPostMock(this Image image)
        {
            return new Image
            {
                Location = "West US",
                Properties = new ImageProperties
                {
                    StorageProfile = new ImageStorageProfile
                    {
                        OsDisk = new ImageOSDisk
                        {
                            OsType = ImageOSDiskOsType.Windows,
                            BlobUri = "https://mystorageaccount.blob.core.windows.net/osimages/osimage.vhd",
                            OsState = ImageOSDiskOsState.Generalized,
                        },
                        ZoneResilient = true,
                    }
                }
            };
        }
    }
}
