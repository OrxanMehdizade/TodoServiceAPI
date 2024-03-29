﻿using System.ComponentModel.DataAnnotations;

namespace TodoServiceAPI.Models.DTOs.Todo
{
    public class CreateTodoItemRequest
    {
        [Required]
        [MinLength(5)]
        public string Text { get; set; } = string.Empty;
    }
}
