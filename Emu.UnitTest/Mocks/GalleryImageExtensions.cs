using GalleryController;

namespace Emu.UnitTest.Mocks
{
    internal static class GalleryImageExtensions
    {
        public static GalleryImage PostSimpleGalleryImageMock(this GalleryImage _)
        {
            return new GalleryImage
            {
                Location = "West US",
                Properties = new GalleryImageProperties
                {
                    OsType = GalleryImagePropertiesOsType.Windows,
                    OsState = GalleryImagePropertiesOsState.Generalized,
                    HyperVGeneration = GalleryImagePropertiesHyperVGeneration.V1,
                    Identifier = new GalleryImageIdentifier
                    {
                        Publisher = "myPublisherName",
                        Offer = "myOfferName",
                        Sku = "mySkuName"
                    }
                }
            };
        }
    }
}
