using Microsoft.AspNetCore.Mvc;


namespace ToDoList.Controllers
{
    public class ThemeController : Controller
    {
        public IActionResult Switch(string theme)
        {
            // Capture the previous URL from the request headers
            string previousUrl = Request.Headers["Referer"].ToString();

            // Store the previous URL in the session
            HttpContext.Session.SetString("PreviousUrl", previousUrl);

            // Set the theme in a cookie
            Response.Cookies.Append("Theme", theme, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(3)
            });

            // Redirect to the previous URL or default to Home/Index if previous URL is not found
            return Redirect(HttpContext.Session.GetString("PreviousUrl") ?? Url.Action("Index", "Home"));
        }
    }
}
