namespace Emu.Common.RestApi
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string? message) : base(message)
        {
        }
    }
}