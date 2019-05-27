using System.Threading.Tasks;
using ThoughtBox.App.ViewModels;

namespace ThoughtBox.App.Services
{
    public interface IThoughtService
    {
        Task<ThoughtViewModel> GetThoughtsAsync(int currentPage, int pageSize);
    }
}
