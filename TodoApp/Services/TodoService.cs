using Microsoft.EntityFrameworkCore;
using TodoApp.Context;
using TodoApp.Models;
using TodoApp.Services.IService;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext context;
        public TodoService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<string> AddTodoAsync(Todo todo)
        {
            context.Todos.Add(todo);
            await context.SaveChangesAsync();
            return "Todo added successfully";
        }

        public async Task<string> DeleteTodoAsync(Guid id)
        {
            var todo = await context.Todos.FindAsync(id);
            if (todo == null)
            {
                return "Todo not found";
            }
            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
            return "Todo deleted successfully";
        }

        public async Task<Todo> GetTodoByIdAsync(Guid id)
        {
            var result = await context.Todos.Where(x => x.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync()
        {
            return await context.Todos.ToListAsync();

        }

        public async Task<string> UpdateTodoAsync(Todo todo)
        {
            context.Todos.Update(todo);
            await context.SaveChangesAsync();
            return "Todo updated successfully";
        }
    }
}