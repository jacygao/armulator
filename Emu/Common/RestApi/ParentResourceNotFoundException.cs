namespace Emu.Common.RestApi
{
    public class ParentResourceNotFoundException : Exception
    {
        public ParentResourceNotFoundException(string message = "unknown exception", string? resourceType = "unknown", string? resourceId = "unknown") : base(message)
        {
            Data.Add("code", "ParentResourceNotFound");
            Data.Add("message", $"Failed to perform 'write' on resource(s) of type '{resourceType}', because the parent resource '{resourceId}' could not be found.");
        }
    
    }
}
