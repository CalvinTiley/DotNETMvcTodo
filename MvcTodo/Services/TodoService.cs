using Microsoft.EntityFrameworkCore;
using MvcTodo.Data;

namespace MvcTodo.Services;

public interface ITodoService
{
    Task<Models.Task> FindTodo(int id);
    Task<Models.Task> FindTodoAsync(int id);
    Task<int> CreateTodo(Models.Task task);
    Task<int> DeleteTodo(int id);
    Task<int> EditTodo(Models.Task task);
    Task<List<Models.Task>> GetTodos();
    bool TodoExists(int id);
}

public class TodoService : ITodoService
{
    private MvcTodoContext _context;

    public TodoService(MvcTodoContext context)
    {
        _context = context;
    }

    public async Task<List<Models.Task>> GetTodos()
    {
        return await _context.Task.ToListAsync();
    }

    public async Task<int> CreateTodo(Models.Task task)
    {
        _context.Add(task);
        return await _context.SaveChangesAsync();
    }
    
    public async Task<int> EditTodo(Models.Task task)
    {
        _context.Update(task);
        return await _context.SaveChangesAsync();
    }

    public async Task<Models.Task> FindTodo(int id)
    {
        Models.Task? task = await _context.Task.FindAsync(id);

        return task;
    }

    public async Task<Models.Task> FindTodoAsync(int id)
    {
        var task = await _context.Task.FirstOrDefaultAsync(m => m.Id == id);

        return task;
    }

    public async Task<int> DeleteTodo(int id)
    {
        var task = await _context.Task.FindAsync(id);

        if (task != null)
        {
            _context.Remove(task);
        }

        return await _context.SaveChangesAsync();
    }

    public bool TodoExists(int id)
    {
        return _context.Task.Any(e => e.Id == id);
    }
}
