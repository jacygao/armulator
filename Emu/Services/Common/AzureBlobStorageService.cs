namespace Emu.Services.Common
{
    using Azure.Storage;
    using Azure.Storage.Blobs;
    using Microsoft.Extensions.Options;
    using System.IO;
    using System.Threading.Tasks;

    public class AzureBlobStorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStorageService(IOptions<AzureStorageOptions> azureStorageOptions)
        {
            var options = azureStorageOptions.Value;
            _blobServiceClient = new BlobServiceClient(
                new Uri($"{options.Endpoint}/{options.AccountName}"),
                new StorageSharedKeyCredential(options.AccountName, options.AccountKey)
            );
        }

        public async Task UploadFileAsync(string containerName, string fileName, Stream fileStream)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);
        }

        public async Task<Stream> DownloadFileAsync(string containerName, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var response = await blobClient.DownloadAsync();
            return response.Value.Content;
        }

        public async Task DeleteFileAsync(string containerName, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }

    }

}
