using Microsoft.AspNetCore.Mvc;
using TodoServiceAPI.Models.DTOs.Todo;
using TodoServiceAPI.Services.Todo;

namespace TodoServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<TodoItemDto>> Get(int id)
        {
            var item = await _todoService.GetTodoItem(id);


            return item is not null
                ? item
                : NotFound();
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<TodoItemDto>> Delete(int id)
        {
            var item = await (_todoService.DeleteTodo(id));
            return item
                ? NoContent()
                : NotFound();
        }


        [HttpPost("create")]
        public async Task<ActionResult<TodoItemDto>> Create([FromBody] CreateTodoItemRequest request)
        {
            var createdItem = await _todoService.CreateTodo(request);

            return CreatedAtAction(nameof(Get), new { id = createdItem.Id }, createdItem);
        }

        [HttpPost("change-status")]
        public async Task<ActionResult<TodoItemDto>> ChangeStatus(int id, bool isCompleted)
        {
            var updatedItem = await _todoService.ChangeTodoItemStatus(id, isCompleted);

            return updatedItem is not null
                ? updatedItem
                : NotFound();
        }
    }
}
