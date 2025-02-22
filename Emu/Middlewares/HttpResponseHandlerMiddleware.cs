using System.Net;

namespace Emu.Middlewares
{
    public class HttpResponseHandlerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Items.ContainsKey("statusoverride"))
            {
                var statusOverride = context.Items["statusoverride"];
                if (statusOverride is int)
                {
                    var code = (HttpStatusCode)statusOverride;
                    if (code == HttpStatusCode.Created || code == HttpStatusCode.OK)
                    {
                        context.Response.StatusCode = (int)code;
                    }
                }
            }
        }
    }
}