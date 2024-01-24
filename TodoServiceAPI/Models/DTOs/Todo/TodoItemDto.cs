namespace TodoServiceAPI.Models.DTOs.Todo
{
    public record TodoItemDto(int Id, string Text, bool IsCompleted, DateTime CreatedTime);

}
