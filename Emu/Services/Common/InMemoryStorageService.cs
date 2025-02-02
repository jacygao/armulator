namespace Emu.Services.Common
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class InMemoryStorageService : IStorageService
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, byte[]>> storage = new();

        public Task UploadFileAsync(string containerName, string fileName, Stream fileStream)
        {
            if (!storage.ContainsKey(containerName))
            {
                storage[containerName] = new ConcurrentDictionary<string, byte[]>();
            }

            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                storage[containerName][fileName] = memoryStream.ToArray();
            }

            return Task.CompletedTask;
        }

        public Task<Stream> DownloadFileAsync(string containerName, string fileName)
        {
            if (storage.ContainsKey(containerName) && storage[containerName].ContainsKey(fileName))
            {
                var fileBytes = storage[containerName][fileName];
                var memoryStream = new MemoryStream(fileBytes);
                return Task.FromResult<Stream>(memoryStream);
            }
            else
            {
                throw new FileNotFoundException("File not found in the specified container.");
            }
        }

        public Task DeleteFileAsync(string containerName, string fileName)
        {
            if (storage.ContainsKey(containerName))
            {
                storage[containerName].TryRemove(fileName, out _);
            }

            return Task.CompletedTask;
        }

        public async Task<List<T>> ListFilesContentRecursiveAsync<T>(string containerName, string prefix, Func<string, Stream, Task<T>> converter)
        {
            var result = new List<T>();

            if (storage.ContainsKey(containerName))
            {
                var files = storage[containerName]
                    .Where(kvp => kvp.Key.StartsWith(prefix))
                    .ToList();

                foreach (var file in files)
                {
                    using (var memoryStream = new MemoryStream(file.Value))
                    {
                        var converted = await converter(file.Key, memoryStream);
                        result.Add(converted);
                    }
                }
            }

            return result;
        }

        public Task<bool> ExistFile(string containerName, string fileName)
        {
            var exists = storage.ContainsKey(containerName) && storage[containerName].ContainsKey(fileName);
            return Task.FromResult(exists);
        }
    }
}
