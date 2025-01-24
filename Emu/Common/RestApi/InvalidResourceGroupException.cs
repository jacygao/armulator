namespace Emu.Common.RestApi
{
    public class InvalidResourceGroupException : Exception
    {
        public InvalidResourceGroupException(string message = "unknown exception", string? resourceGroup = "unknown") : base(message)
        {
            Data.Add("code", "ResourceGroupNotFound");
            Data.Add("message", $"The provided subscription identifier '{resourceGroup}' is malformed or invalid.");
        }
    }
}
