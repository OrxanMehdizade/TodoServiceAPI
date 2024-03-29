﻿using Microsoft.EntityFrameworkCore;
using TodoServiceAPI.Data;
using TodoServiceAPI.Services.Todo;

namespace TodoServiceAPI
{
    public static class DI
    {
        public static IServiceCollection AddTodoContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoDbContext>(op => op.UseSqlServer(configuration.GetConnectionString("TodoConStr")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
            return services;
        }
    }
}
