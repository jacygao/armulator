using GalleryController;

namespace Emu.UnitTest.Mocks
{
    internal static class GalleryExtensions
    {
        public static Gallery PostSimpleGalleryMock(this Gallery _)
        {
            return new Gallery
            {
                Location = "West US",
                Properties = new GalleryProperties
                {
                    Description = "This is the gallery description."
                }
            };
        }

        public static Gallery PostSimpleGalleryWithSoftDeletionEnabledMock(this Gallery _)
        {
            return new Gallery
            {
                Location = "West US",
                Properties = new GalleryProperties
                {
                    Description = "This is the gallery description.",
                    SoftDeletePolicy = new SoftDeletePolicy
                    {
                        IsSoftDeleteEnabled = true
                    }
                }
            };
        }
    }
}
