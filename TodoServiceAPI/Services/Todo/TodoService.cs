using Microsoft.EntityFrameworkCore;
using TodoServiceAPI.Data;
using TodoServiceAPI.Models.DTOs.Todo;
using TodoServiceAPI.Models.Entities;

namespace TodoServiceAPI.Services.Todo
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItemDto> ChangeTodoItemStatus(int id, bool isCompleted)
        {
            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == id);
            if (todoItem == null)
            {
                return null;
            }

            todoItem.IsCompleted = isCompleted;
            await _context.SaveChangesAsync();


            return new TodoItemDto(
                    Id: todoItem.Id,
                    Text: todoItem.Text,
                    IsCompleted: todoItem.IsCompleted,
                    CreatedTime: todoItem.CreatedTime);

        }

        public async Task<TodoItemDto> CreateTodo(CreateTodoItemRequest request)
        {
            var todoItem = new TodoItem
            {
                Text = request.Text,
                IsCompleted = false,
                CreatedTime = DateTime.Now
            };

            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();

            return new TodoItemDto(
                    Id: todoItem.Id,
                    Text: todoItem.Text,
                    IsCompleted: todoItem.IsCompleted,
                    CreatedTime: todoItem.CreatedTime);
        }

        public async Task<bool> DeleteTodo(int id)
        {
            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == id);
            if (todoItem == null)
            {
                return false;
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return true;

        }

        public Task<List<TodoItemDto>> GetAll(int page, int pageSize)
        {
            throw new NotImplementedException();
        }


        public async Task<TodoItemDto?> GetTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == id);

            return todoItem is not null
                ? new TodoItemDto(
                    Id: todoItem.Id,
                    Text: todoItem.Text,
                    IsCompleted: todoItem.IsCompleted,
                    CreatedTime: todoItem.CreatedTime)
                : null;
        }
    }
}
