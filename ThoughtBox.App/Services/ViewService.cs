using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThoughtBox.App.Data;
using ThoughtBox.App.Models;

namespace ThoughtBox.App.Services
{
    public class ViewService : IViewService
    {
        private readonly AppDbContext _context;

        public ViewService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CountViewsAsync(List<Thought> thoughts, string ip)
        {
            var viewerViews = _context.Viewers.Where(v => v.IP == ip).AsNoTracking();
            foreach (var thought in thoughts)
            {
                var viewed = viewerViews.Where(vv => vv.ThoughtId == thought.Id).Count();
                if (viewed == 0)
                {
                    await _context.Viewers.AddAsync(new Viewer { IP = ip, ThoughtId = thought.Id });
                    _context.Entry(thought).State = EntityState.Modified;
                    thought.Views++;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task CountViewsAsync(Thought thought, string ip)
        {
            var viewed = _context.Viewers.Where(v => v.IP == ip && v.ThoughtId == thought.Id).AsNoTracking().Count();
            if (viewed == 0)
            {
                await _context.Viewers.AddAsync(new Viewer { IP = ip, ThoughtId = thought.Id });
                _context.Entry(thought).State = EntityState.Modified;
                thought.Views++;
            }
            await _context.SaveChangesAsync();
        }
    }
}
