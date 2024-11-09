using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoList.Filters
{
    public class ThemeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // Retrieve the theme from cookies
            var theme = context.HttpContext.Request.Cookies["Theme"];

            // Access ViewData and set the theme
            if (context.Controller is Controller controller)
            {
                controller.ViewData["Theme"] = theme;
            }

        }
    }
}
