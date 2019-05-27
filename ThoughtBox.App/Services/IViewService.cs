using System.Collections.Generic;
using System.Threading.Tasks;
using ThoughtBox.App.Models;

namespace ThoughtBox.App.Services
{
    public interface IViewService
    {
        Task CountViewsAsync(List<Thought> thoughts, string ip);
        Task CountViewsAsync(Thought thought, string ip);
    }
}
