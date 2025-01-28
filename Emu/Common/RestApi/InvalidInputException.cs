namespace Emu.Common.RestApi
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message = "unknown exception", string substatus = "Unknown") : base(message)
        {
            Data.Add("code", substatus);
            Data.Add("message", message);
        }
    }
}
