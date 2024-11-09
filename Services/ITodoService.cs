using ToDoList.ViewModel;

namespace ToDoList.Services

{
    public interface ITodoService
    {
        List<todoVM> GetAllTodos();
        todoVM GetTodoById(int id);
        void AddTodo(todoVM todo);
        void UpdateTodo(todoVM todo);
        void DeleteTodo(int id);
    }
}
