using Emu.Utilities.RestApi;
using Newtonsoft.Json;
using System.Net;

namespace Emu.Middlewares
{
    public class CommonExceptionHandlerMiddleware(RequestDelegate next) : AbstractExceptionHandlerMiddleware(next)
    {
        public override (HttpStatusCode code, string message) GetResponse(Exception exception)
        {
            var code = exception switch
            {
                InvalidSubscriptionIdException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            };
            return (code, JsonConvert.SerializeObject(
                new CommonErrorResponse(
                    exception.Data["code"]?.ToString() ?? "Unknown", 
                    exception.Data["message"]?.ToString() ?? "Unknown Error"
                    )
                ));
        }
    }
}
