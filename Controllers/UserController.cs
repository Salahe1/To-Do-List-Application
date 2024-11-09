using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ToDoList.ViewModel;
using ToDoList.Filters;

namespace ToDoList.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult login(userVm u)
        {
            if (ModelState.IsValid)
            {
                string pass = new string(u.password.Reverse().ToArray());
                if (u.login == pass)
                {
                    HttpContext.Session.SetString("isAuth", "");
                    HttpContext.Session.SetString("Username", u.login); // Store the username in the session

                    return RedirectToAction("Index", "ToDo");
                }

            }
            return View();
        }

        [AuthFilter]
        public IActionResult Logout()
        {
            // HttpContext.Session.Clear();
            HttpContext.Session.Remove("isAuth");

            var lastConnexion = DateTime.Now;

            HttpContext.Response.Cookies.Append("LastConnexion", lastConnexion.ToString(), new CookieOptions
            {
                Expires = DateTime.Now.AddDays(3)
            });

            return RedirectToAction("Login");
        }



    }

  
}
