using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace ToDoList.Filters
{
    public class LogFilter : ActionFilterAttribute 
    {


        //private readonly ILogger<LogFilter> _logger;
        //public LogFilter(ILogger<LogFilter> logger)
        //{
        //    _logger = logger;
        //}

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    var userName = context.HttpContext.Session.GetString("Username") ?? "Anonymous";
        //    var controllerName = context.RouteData.Values["controller"];
        //    var actionName = context.RouteData.Values["action"];

        //    _logger.LogInformation(
        //        "Action Executed: {User = userName},{Controller = controllerName},{Action = actionName}",
        //        DateTime.Now, userName, controllerName, actionName);
        //}



        public override void OnActionExecuted(ActionExecutedContext context)
        {

            var username = context.HttpContext.Session.GetString("Username") ?? "Anonymous";
            var ControllerName = context.RouteData.Values["controller"];
            var ActionName = context.RouteData.Values["action"];

            var logMessage = $"{DateTime.Now}: User={username}, Controller={ControllerName}, Action={ActionName}";

            // Define the log directory and file path
            string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            string logFilePath = Path.Combine(logDirectory, "action_log.txt");

            logFilePath = @"C:\Users\pc\source\repos\ToDoList\ToDoList\Logs\action_log.txt";


            try
            {
                // Ensure the Logs directory exists
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // Append log message to the file
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to log file: " + ex.Message);
            }
            //OnActionExecuted(context);
            //Fibase.le.AppendAllText("Logs/action_log.txt", logMessage + "\n");

        }
    }
}
