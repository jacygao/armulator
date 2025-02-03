namespace Emu.Services.Common
{
    public class AzureStorageOptions
    {
        public required bool IsHeadless { get; set; } = false;

        public required string Endpoint { get; set; }

        public required string AccountName { get; set; }

        public required string AccountKey { get; set; }
    }
}
