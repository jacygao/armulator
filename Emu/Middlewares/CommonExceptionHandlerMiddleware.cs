using Emu.Common.RestApi;
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
                InvalidSubscriptionIdException or
                InvalidResourceGroupException => HttpStatusCode.BadRequest,
                NotImplementedException => HttpStatusCode.NotImplemented,
                _ => HttpStatusCode.InternalServerError,
            };
            return (code, JsonConvert.SerializeObject(
                new CommonErrorResponse(
                    exception.Data["code"]?.ToString() ?? "Unknown", 
                    exception.Data["message"]?.ToString() ?? exception.Message
                    )
                ));
        }
    }
}
