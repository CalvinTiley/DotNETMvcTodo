using Microsoft.EntityFrameworkCore;

namespace MvcTodo.Data
{
    public class MvcTodoContext : DbContext
    {
        public MvcTodoContext(DbContextOptions<MvcTodoContext> options) : base(options)
        {
        }

        public DbSet<MvcTodo.Models.Task> Task { get; set; } = default!;
    }
}