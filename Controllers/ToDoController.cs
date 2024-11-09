//using Microsoft.AspNetCore.Mvc;
//using System.Text.Json;
//using ToDoList.ViewModel;
//using ToDoList.Filters;

//namespace ToDoList.Controllers
//{
//    [AuthFilter]
//    public class ToDoController : Controller
//    {

//        public IActionResult Index()
//        {
//            //if (HttpContext.Session.GetString("isAuth") == null)
//            //{
//            //    return RedirectToAction("Index", "User");
//            //}
//            List<todoVM> toDoList = new List<todoVM>();

//            if (HttpContext.Session.GetString("Todolist") != null)
//            {
//                toDoList = JsonSerializer.Deserialize<List<todoVM>>(HttpContext.Session.GetString("Todolist"));
//            }

//            var lastConnexion = HttpContext.Request.Cookies["LastConnexion"] ?? "";
//            TempData["LastConnexion"] = lastConnexion;

//            return View(toDoList);
//        }




//        public IActionResult Add()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Add(todoVM t)
//        {
//            //if (HttpContext.Session.GetString("isAuth") == null)
//            //{
//            //    return RedirectToAction("Index", "User");
//            //}

//            if (ModelState.IsValid)
//            {
//                List<todoVM> toDoList = new List<todoVM>();

//                if (HttpContext.Session.GetString("Todolist") != null)
//                {
//                    //toDoList = JsonSerializer.Deserialize<List<todoVM>>(HttpContext.Session.GetString("Todolist"));

//                    var todoListJSON = HttpContext.Session.GetString("Todolist") ?? "";
//                    toDoList = JsonSerializer.Deserialize<List<todoVM>>(todoListJSON);
//                }

//                int newId = toDoList.Count == 0 ? 1 : toDoList.Max(o => o.Id) + 1;

//                t.Id = newId;

//                toDoList.Add(t);

//                var toDoListJSON = JsonSerializer.Serialize(toDoList);
//                HttpContext.Session.SetString("Todolist", toDoListJSON);

//                return RedirectToAction("Index", "ToDo");
//            }
//            return View();
//        }


//        [HttpGet]
//        public IActionResult Modify(int id)
//        {
//            //if (HttpContext.Session.GetString("isAuth") == null)
//            //{
//            //    return RedirectToAction("Index", "User");
//            //}
//            var todoListJSON = HttpContext.Session.GetString("Todolist") ?? "";
//            var todoList = JsonSerializer.Deserialize<List<todoVM>>(todoListJSON);
//            var todoItem = todoList.FirstOrDefault(item => item.Id == id);

//            return View(todoItem);
//        }


//        [HttpPost]
//        public IActionResult Modify(todoVM updatedItem)
//        {
//            //if (HttpContext.Session.GetString("isAuth") == null)
//            //{
//            //    return RedirectToAction("Index", "User");
//            //}
//            if (ModelState.IsValid)
//            {
//                TempData.Keep("LastConnexion");
//                var toDoListJSON = HttpContext.Session.GetString("Todolist");
//                var toDoList = JsonSerializer.Deserialize<List<todoVM>>(toDoListJSON);


//                var existingItem = toDoList.FirstOrDefault(item => item.Id == updatedItem.Id);

//                existingItem.Libelle = updatedItem.Libelle;
//                existingItem.Description = updatedItem.Description;
//                existingItem.State = updatedItem.State;
//                existingItem.DateLimite = updatedItem.DateLimite;

//                toDoListJSON = JsonSerializer.Serialize(toDoList);
//                HttpContext.Session.SetString("Todolist", toDoListJSON);

//                return RedirectToAction("Index", "ToDo");
//            }

//            return View();

//        }


//        [HttpPost]
//        public IActionResult Delete(int id)
//        {
//            //if (HttpContext.Session.GetString("isAuth") == null)
//            //{
//            //    return RedirectToAction("Index", "User");
//            //}

//            var toDoListJSON = HttpContext.Session.GetString("Todolist");
//            var toDoList = JsonSerializer.Deserialize<List<todoVM>>(toDoListJSON);

//            var itemToRemove = toDoList.FirstOrDefault(t => t.Id == id);

//            toDoList.Remove(itemToRemove);

//            toDoListJSON = JsonSerializer.Serialize(toDoList);
//            HttpContext.Session.SetString("Todolist", toDoListJSON);

//            return RedirectToAction("Index", "ToDo");
//        }
//    }
//}

// Controllers/ToDoController.cs 
using Microsoft.AspNetCore.Mvc;
using ToDoList.ViewModel;
using ToDoList.Filters;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [AuthFilter]
    public class ToDoController : Controller
    {
        private readonly ITodoService _todoService;

        public ToDoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public IActionResult Index()
        {
            var lastConnexion = HttpContext.Request.Cookies["LastConnexion"] ?? "";
            TempData["LastConnexion"] = lastConnexion;

            var todos = _todoService.GetAllTodos();
            return View(todos);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(todoVM t)
        {
            if (ModelState.IsValid)
            {
                _todoService.AddTodo(t);
                return RedirectToAction("Index", "ToDo");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Modify(int id)
        {
            var todoItem = _todoService.GetTodoById(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        [HttpPost]
        public IActionResult Modify(todoVM updatedItem)
        {
            if (ModelState.IsValid)
            {
                _todoService.UpdateTodo(updatedItem);
                return RedirectToAction("Index", "ToDo");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _todoService.DeleteTodo(id);
            return RedirectToAction("Index", "ToDo");
        }
    }
}