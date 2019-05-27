using System.Threading.Tasks;
using ThoughtBox.App.ViewModels;

namespace ThoughtBox.App.Services
{
    public interface IThoughtService
    {
        Task<ThoughtViewModel> GetThoughts(int currentPage, int pageSize);
    }
}
