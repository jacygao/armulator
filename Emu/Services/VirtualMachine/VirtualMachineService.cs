namespace Emu.Services.VirtualMachine
{
    using Emu.Common.RestApi;
    using Emu.Services.Common;
    using System.Text.Json;
    using System.Threading.Tasks;
    using VirtualMachineController;

    public class VirtualMachineService : IVirtualMachineService
    {
		private readonly IStorageService _storage;
		private readonly string containerName = "vms";

		public VirtualMachineService(IStorageService storageService)
        {
            _storage = storageService;
        } 

        public async Task<VirtualMachine> CreateOrUpdateAsync(string resourceGroup, string vmName, VirtualMachine parameters)
        {
			// Input Validation
			ArgumentException.ThrowIfNullOrEmpty(vmName, nameof(vmName));
			ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));

			// Serialize the Image object to JSON
			var json = JsonSerializer.Serialize(parameters);
			using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
			{
				await _storage.UploadFileAsync($"{containerName}", $"{vmName}.json", stream);

			}

            return parameters;

		}

        public Task DeallocateAsync(string resourceGroupName, string vmName)
        {
            throw new NotImplementedException();
        }

        public Task GeneralizeAsync(string resourceGroupName, string vmName)
        {
            throw new NotImplementedException();
        }

        public async Task<VirtualMachine> GetAsync(string resourceGroupName, string vmName)
        {
            // Input Validation
            ArgumentException.ThrowIfNullOrEmpty(vmName, nameof(vmName));

            // Download vm Metadata
            try
            {
                var stream = await _storage.DownloadFileAsync(containerName, $"{vmName}.json");
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();
                var vm = JsonSerializer.Deserialize<VirtualMachine>(json);

                if (vm != null)
                {
                    return vm;
                }

                throw new ResourceNotFoundException($"image {vmName} does not exist");

            }
            catch
            {
                throw;
            }
        }

        public Task PowerOffAsync(string resourceGroupName, string vmName)
        {
            throw new NotImplementedException();
        }

        public Task RunCommandAsync(string resourceGroupName, string vmName, string commandId, string script)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(string resourceGroupName, string vmName)
        {
            throw new NotImplementedException();
        }

    }
}