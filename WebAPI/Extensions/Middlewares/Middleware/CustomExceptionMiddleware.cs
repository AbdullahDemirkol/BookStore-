using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

namespace WebAPI.Extensions.Middlewares.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly string _logFilePath;
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            _logFilePath = Environment.CurrentDirectory + "\\log.txt";
            if (!File.Exists(_logFilePath))
            {
                using (StreamWriter writer = File.CreateText(_logFilePath))
                {
                    writer.WriteLine("Log Dosyası Oluşturuldu: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")+"\n");

                }
            }
        }
        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request] HTTP " + context.Request.Method + @" """ + context.Request.Path + @"""";
                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss --- ")} {message}");
                }

                await _next(context);
                watch.Stop();
                message = "[Response] HTTP " + context.Request.Method + @" """ + context.Request.Path + @""" responded. StatusCode: " + context.Response.StatusCode + " " + (HttpStatusCode)context.Response.StatusCode + "." ;
                message +=" Time: "+ watch.Elapsed.TotalSeconds + "ms.";
                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss --- ")} {message}");
                }
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }

        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            var exceptionType = ex.GetType().Name;
            FixResponse(context, exceptionType);

            var message = "[Error] HTTP " + context.Request.Method + @" """ + context.Request.Path + @""" -";
            message += " StatusCode: " + context.Response.StatusCode +" "+ (HttpStatusCode)context.Response.StatusCode + ". ";
            message += "Error Message: " + ex.Message + ". ";
            message += " Time: " + watch.Elapsed.TotalSeconds + "ms.";
            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss --- ")} {message}");
            }



            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);

        }

        private void FixResponse(HttpContext context, string exceptionType)
        {
            context.Response.ContentType = "application/json";
            if (exceptionType == "ValidationException")
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
