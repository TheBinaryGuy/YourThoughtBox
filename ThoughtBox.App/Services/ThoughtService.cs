using System.Linq;
using System.Threading.Tasks;
using ThoughtBox.App.Data;
using ThoughtBox.App.ViewModels;

namespace ThoughtBox.App.Services
{
    public class ThoughtService : IThoughtService
    {
        private readonly AppDbContext _context;

        public ThoughtService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ThoughtViewModel> GetThoughts(int currentPage, int pageSize)
        {
            var model = new ThoughtViewModel { TotalThoughts = _context.Thoughts.Count(t => !t.IsDeleted), CurrentPage = currentPage, PageSize = pageSize };
            model.Thoughts = _context.Thoughts.OrderByDescending(t => t.Id).Where(t => !t.IsDeleted).Skip(model.PageSize * (currentPage - 1)).Take(model.PageSize).ToList();
            return model;
        }
    }
}
