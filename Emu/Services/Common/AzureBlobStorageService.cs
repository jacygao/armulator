namespace Emu.Services.Common
{
    using Azure.Storage;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
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

        public async Task<List<T>> ListFilesContentRecursiveAsync<T>(string containerName, string prefix, Func<string, Stream, Task<T>> converter)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var result = new List<T>();
            await ListFilesContentRecursiveInternalAsync(containerClient, prefix, result, converter);
            return result;
        }

        private async Task ListFilesContentRecursiveInternalAsync<T>(BlobContainerClient containerClient, string prefix, List<T> result, Func<string, Stream, Task<T>> converter)
        {
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync(prefix: prefix))
            {
                if (blobItem.Properties.ContentLength.HasValue)
                {
                    var blobClient = containerClient.GetBlobClient(blobItem.Name);
                    var content = await blobClient.OpenReadAsync();
                    var convertedItem = await converter(blobItem.Name, content);
                    result.Add(convertedItem);
                }
                else
                {
                    await ListFilesContentRecursiveInternalAsync(containerClient, blobItem.Name + "/", result, converter);
                }
            }

        }
    }
}
