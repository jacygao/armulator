namespace Emu.Utilities.RestApi
{
    public class CommonErrorResponse
    {
        public CommonErrorResponse(string substatusCode, string message) {
            this.error = new ErrorContext
            {
                code = substatusCode,
                message = message
            };
        }

        public ErrorContext error;
    }
    
    public struct ErrorContext
    {
        public string code;
        public string message;
    }
}
