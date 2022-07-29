using ProjetoAutenticacaoAWS.Application.DTOs;
using ProjetoAutenticacaoAWS.Web.Controllers;

namespace ProjetoAutenticacaoAWS.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ILogger<UsuarioController> _log { get; set; }
        public ExceptionMiddleware(RequestDelegate next, ILogger<UsuarioController> log)
        {
            _next = next;
            _log = log;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                _log.LogError(ex.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new ErroResponseModel{Msg = ex.Message});
            }
        }
    }
}