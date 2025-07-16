using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace Web_Api.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            string logPath = "C:\\Logs\\ExceptionLog.txt";

            if (!Directory.Exists("C:\\Logs"))
            {
                Directory.CreateDirectory("C:\\Logs");
            }

            File.AppendAllText(logPath, $"{DateTime.Now}: {exception.Message}\n{exception.StackTrace}\n\n");

            context.Result = new ObjectResult("An error occurred, check the logs.")
            {
                StatusCode = 500
            };
        }
    }
}

