using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoServiceAPI.Models.Entities;

namespace TodoServiceAPI.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    }
}
