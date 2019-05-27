using System.Threading.Tasks;
using ThoughtBox.App.ViewModels;

namespace ThoughtBox.App.Services
{
    public interface IThoughtService
    {
        ThoughtViewModel GetThoughts(int currentPage, int pageSize);
    }
}
