namespace Emu.Common.RestApi
{
    public class InvalidInputException : Exception
    {
        public string Code { get; set; }

        public string ErrorMessage {  get; set; }

        public InvalidInputException(string message = "unknown exception", string substatus = "Unknown") : base(message)
        {
            Data.Add("code", substatus);
            Data.Add("message", message);
        }
    }
}

