using Microsoft.EntityFrameworkCore;
using TodoServiceAPI.Data;
using TodoServiceAPI.Models.DTOs.Pagintions;
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
            try
            {
                var todoItem = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == id);
                if (todoItem == null)
                {
                    return null;
                }

                todoItem.IsCompleted = isCompleted;
                await _context.SaveChangesAsync();


                return new TodoItemDto(
                        id: todoItem.Id,
                        text: todoItem.Text,
                        isCompleted: todoItem.IsCompleted,
                        createdTime: todoItem.CreatedTime);

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in ChangeTodoItemStatus:{ex.Message}");
                throw;
            }

        }

        public async Task<TodoItemDto> CreateTodo(CreateTodoItemRequest request)
        {
            try
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
                        id: todoItem.Id,
                        text: todoItem.Text,
                        isCompleted: todoItem.IsCompleted,
                        createdTime: todoItem.CreatedTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateTodo:{ex.Message}");
                throw;
            }

        }

        public async Task<bool> DeleteTodo(int id)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteTodo:{ex.Message}");
                throw;
            }


        }

        public async Task<PagintionListDto<TodoItemDto>> GetAll(int page, int pageSize, bool? isComleted)
        {
            try
            {
                IQueryable<TodoItem> todoQuery = _context.TodoItems.AsQueryable();
                if (isComleted.HasValue)
                {
                    todoQuery = todoQuery.Where(e => e.IsCompleted == isComleted);
                }
                var items = await todoQuery.Skip((page - 1) - pageSize).Take(pageSize).ToListAsync();
                var totalCount = await todoQuery.CountAsync();
                return new PagintionListDto<TodoItemDto>(

                    items.Select(e => new TodoItemDto(
                        id: e.Id,
                        text: e.Text,
                        isCompleted: e.IsCompleted,
                        createdTime: e.CreatedTime

                        )),
                    new PagintionMeta(page, pageSize, totalCount)

                    );

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAll:{ex.Message}");
                throw;
            }
        }


        public async Task<TodoItemDto?> GetTodoItem(int id)
        {
            try
            {
                var todoItem = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == id);

                return todoItem is not null
                    ? new TodoItemDto(
                        id: todoItem.Id,
                        text: todoItem.Text,
                        isCompleted: todoItem.IsCompleted,
                        createdTime: todoItem.CreatedTime)
                    : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetTodoItem:{ex.Message}");
                throw;
            }
        }
    }
}
