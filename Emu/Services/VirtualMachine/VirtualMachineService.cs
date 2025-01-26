namespace Emu.Services.VirtualMachine
{
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
				await _storage.UploadFileAsync($"{resourceGroup}/{containerName}", $"{vmName}.json", stream);

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

        public Task<VirtualMachine> GetAsync(string resourceGroupName, string vmName)
        {
            throw new NotImplementedException();
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