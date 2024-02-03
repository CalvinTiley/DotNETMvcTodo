using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcTodo.Services;

namespace MvcTodo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITodoService _service;

        public HomeController(ITodoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetTodos());
        }

        public IActionResult Create()
        {
            return View();
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _service.FindTodo(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Description,CreatedDate")] Models.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.EditTodo(task);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.TodoExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CreatedDate")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateTodo(task);
                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _service.FindTodoAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteTodo(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
