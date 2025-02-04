using Emu.Common.RestApi;
using Emu.Services.Common;
using Emu.Services.Gallery;
using System.Text.Json;

namespace Emu.Services
{
    public abstract class ServiceBase<T>
    {
        protected readonly IStorageService _storageService;

        public ServiceBase(IStorageService storageService) { 
            _storageService= storageService;
        }

        protected async Task<OperationType> CreateAsync(string containerName, string filename, T item)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(filename, nameof(filename));
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            var op = OperationType.Create;
            if (await FileExists(containerName, filename))
            {
                op = OperationType.Update;
            }

            // Serialize the item to JSON
            var json = JsonSerializer.Serialize(item);
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
            await _storageService.UploadFileAsync(containerName, filename, stream);

            return op;
        }

        protected async Task<T> GetAsync(string containerName, string filename)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(filename, nameof(filename));

            // Download item metadata
            try
            {
                var stream = await _storageService.DownloadFileAsync(containerName, filename);
                using var reader = new StreamReader(stream);
            
                var json = await reader.ReadToEndAsync();
                var item = JsonSerializer.Deserialize<T>(json);

                if (item != null)
                {
                    return item;
                }

                throw new ResourceNotFoundException($"Item {filename} does not exist");
            } catch
            {
                throw;
            }
        }

        protected async Task<bool> FileExists(string containerName, string filename)
        {
            return await _storageService.ExistFile(containerName, filename);
        }

        protected string getComputeResourceId(string subscriptionId, string resourceGroupName, params string[] additionalParams)
        {
            var allParams = new List<string> { "subscriptions", subscriptionId, "resourceGroup", resourceGroupName };
            allParams.AddRange(additionalParams);

            // Join the parameters with "/"
            return string.Join("/", allParams);
        }
    }
    public enum OperationType
    {
        Create = 0,
        Update = 1,
        Unknown = 2,
    }
}