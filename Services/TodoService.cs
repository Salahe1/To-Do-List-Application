using ToDoList.ViewModel;

namespace ToDoList.Services
{
    public class TodoService : ITodoService
    {
        private readonly SessionTodoStorage _storage;

        public TodoService(SessionTodoStorage storage)
        {
            _storage = storage;
        }

        public List<todoVM> GetAllTodos()
        {
            return _storage.GetAll();
        }

        public todoVM GetTodoById(int id)
        {
            return _storage.GetById(id);
        }

        public void AddTodo(todoVM todo)
        {
            _storage.Add(todo);
        }

        public void UpdateTodo(todoVM todo)
        {
            _storage.Update(todo);
        }

        public void DeleteTodo(int id)
        {
            _storage.Delete(id);
        }
    }
}
