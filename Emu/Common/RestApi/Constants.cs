namespace Emu.Common.RestApi
{
    public class Constants
    {
        public readonly static (string substatus, string message) LocationRequired = new("LocationRequired", "The location property is required for this definition");

        public readonly static (string substatus, string message) InvalidParameter = new("InvalidParameter", "Required parameter 'properties' is missing (null).");

    }
}
