using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectX.App.Data;
using ProjectX.App.Models;
using ProjectX.App.ViewModels;

namespace ProjectX.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new ThoughtViewModel { TotalThoughts = _context.Thoughts.Count(t => !t.IsDeleted), CurrentPage = page, PageSize = 10 };
            model.Thoughts = _context.Thoughts.OrderByDescending(t => t.Id).Where(t => !t.IsDeleted).Skip(model.PageSize * (page - 1)).Take(model.PageSize).ToList();

            var ip = HttpContext.Request.Headers["HTTP_CF_CONNECTING_IP"].ToString()?.ToLower();
            if (!string.IsNullOrWhiteSpace(ip))
            {
                _logger.LogInformation($"Content requested by {ip}");
            }
            else
            {
                _logger.LogInformation($"Content requested");
            }

            model.Thoughts.ForEach(m =>
            {
                _context.Entry(m).State = EntityState.Modified;
                m.Views++;
            });
            await _context.SaveChangesAsync();
            return View(model);
        }

        [HttpGet("/thought/{id}")]
        public async Task<IActionResult> GetSpecificThought(int id)
        {
            var thought = await _context.Thoughts.FindAsync(id);
            if (thought == null || thought.IsDeleted)
            {
                _logger.LogInformation("Thought Not Found!", $"Thought Id: {thought.Id}");
                return NotFound();
            }
            _logger.LogInformation("Thought Fetched!", thought);
            _context.Entry(thought).State = EntityState.Modified;
            thought.Views++;
            await _context.SaveChangesAsync();
            return View(thought);
        }

        [HttpGet("/add")]
        public IActionResult AddThought()
        {
            var model = new ThoughtViewModel();
            return View(model);
        }

        [HttpPost("/add")]
        public async Task<IActionResult> AddThought(ThoughtViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _logger.LogInformation("Adding thought!");
            var thought = new Thought
            {
                Content = model.Content,
                CreatedAt = DateTime.Now,
                Tags = model.Tags,
                TagsDelimiter = ',',
                Views = 0
            };
            await _context.Thoughts.AddAsync(thought);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Thought added!", thought);
            return RedirectToAction("GetSpecificThought", "Home", new { id = thought.Id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var model = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            _logger.LogError("Error occurred!", model);
            return View(model);
        }
    }
}
