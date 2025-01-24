namespace Emu.Services.Common
{
    public interface IStorage<TItem> : IDisposable
    {
        public void Save(string key, TItem? value);

        TItem Load(string key);

        void Delete(string key);
    }
}
