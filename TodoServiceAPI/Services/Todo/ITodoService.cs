using TodoServiceAPI.Models.DTOs.Pagintions;
using TodoServiceAPI.Models.DTOs.Todo;

namespace TodoServiceAPI.Services.Todo
{
    public interface ITodoService
    {
        Task<TodoItemDto?> GetTodoItem(int id);
        Task<TodoItemDto> CreateTodo(CreateTodoItemRequest request);
        Task<TodoItemDto> ChangeTodoItemStatus(int id, bool isCompleted);
        Task<bool> DeleteTodo(int id);
        Task<PagintionListDto<TodoItemDto>> GetAll(int page, int pageSize,bool? isComleted);
    }
}
