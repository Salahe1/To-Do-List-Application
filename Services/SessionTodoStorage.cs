using System.Text.Json;
using ToDoList.ViewModel;

namespace ToDoList.Services
{
    public class SessionTodoStorage
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionKey = "Todolist";

        public SessionTodoStorage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private List<todoVM> GetTodos()
        {
            var todosJson = _httpContextAccessor.HttpContext.Session.GetString(SessionKey);

            return todosJson != null ? JsonSerializer.Deserialize<List<todoVM>>(todosJson): new List<todoVM>();
        }

        private void SaveTodos(List<todoVM> todos)
        {
            var todosJson = JsonSerializer.Serialize(todos);
            _httpContextAccessor.HttpContext.Session.SetString(SessionKey, todosJson);
        }

        // Methods for data access (notice these don't have business logic)
        public List<todoVM> GetAll() => GetTodos();

        public todoVM GetById(int id) => GetTodos().FirstOrDefault(t => t.Id == id);

        public void Add(todoVM todo)
        {
            var todos = GetTodos();
            todo.Id = todos.Any() ? todos.Max(t => t.Id) + 1 : 1;
            todos.Add(todo);
            SaveTodos(todos);
        }

        public void Update(todoVM todo)
        {
            var todos = GetTodos();
            var index = todos.FindIndex(t => t.Id == todo.Id);
            if (index != -1)
            {
                todos[index] = todo;
                SaveTodos(todos);
            }
        }

        public void Delete(int id)
        {
            var todos = GetTodos();
            var todoToRemove = todos.FirstOrDefault(t => t.Id == id);
            if (todoToRemove != null)
            {
                todos.Remove(todoToRemove);
                SaveTodos(todos);
            }
        }
    }
}
