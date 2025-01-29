namespace Emu.Services.Common
{
    public interface IStorageService
    {
        Task UploadFileAsync(string containerName, string fileName, Stream fileStream);
        Task<Stream> DownloadFileAsync(string containerName, string fileName);
        Task DeleteFileAsync(string containerName, string fileName);
        Task<List<T>> ListFilesContentRecursiveAsync<T>(string containerName, string prefix, Func<string, Stream, Task<T>> converter);
    }
}
