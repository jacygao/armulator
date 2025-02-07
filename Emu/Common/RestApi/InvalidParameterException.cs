namespace Emu.Common.RestApi
{
    public class InvalidParameterException(string message = "unknown exception", string substatus = "Unknown", string target = "") : Exception(message)
    {
        public string Code { get; set; } = substatus;

        public string ErrorMessage { get; set; } = message;

        public string Target { get; set; } = target;
    }
}

