using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThoughtBox.App.Data;
using ThoughtBox.App.Models;
using ThoughtBox.App.Services;
using ThoughtBox.App.ViewModels;

namespace ThoughtBox.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IThoughtService _thoughtService;
        private readonly IViewService _viewService;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IThoughtService thoughtService, IViewService viewService)
        {
            _logger = logger;
            _context = context;
            _thoughtService = thoughtService;
            _viewService = viewService;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var model = _thoughtService.GetThoughts(page, 10);

            //var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
			var ip = Request.Headers["X-Real-IP"];
            if (!string.IsNullOrWhiteSpace(ip) && model != null && ip != "::1")
            {
                _logger.LogInformation($"Content requested by {ip}");
                await _viewService.CountViewsAsync(model.Thoughts, ip);
            }
            else
            {
                _logger.LogInformation($"Content requested");
            }

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

            //var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
			var ip = Request.Headers["X-Real-IP"];
            if (!string.IsNullOrWhiteSpace(ip) && ip != "::1")
            {
                _logger.LogInformation($"Content requested by {ip}");
                await _viewService.CountViewsAsync(thought, ip);
            }
            else
            {
                _logger.LogInformation($"Content requested");
            }

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
			var distinctTags = model.Tags.Split(',').Distinct();
			model.Tags = string.Join(",", distinctTags);
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
