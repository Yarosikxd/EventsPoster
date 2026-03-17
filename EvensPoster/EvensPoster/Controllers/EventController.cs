using EvensPoster.DataBase;
using EvensPoster.Models;
using EvensPoster.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvensPoster.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Список усіх подій (Візуал)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.ToListAsync();
            return View(events);
        }

        // 2. Сторінка з формою створення (Візуал)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 3. Обробка форми створення
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newEvent = new Event
                {
                    Title = model.Title,
                    Description = model.Description,
                    Date = model.Date,
                    City = model.City,
                    Price = model.Price,
                    ImageURL = model.ImageUrl, // Записуємо URL як рядок
                    IsFinished = false,
                    OrganizerId = 1 // Тимчасове значення, поки немає авторизації
                };

                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Якщо валідація не пройшла, повертаємо користувача на форму з помилками
            return View(model);
        }

        // 4. Видалення (спрощене)
        [HttpPost] // У MVC видалення з кнопок зазвичай роблять через POST
        public async Task<IActionResult> Delete(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
