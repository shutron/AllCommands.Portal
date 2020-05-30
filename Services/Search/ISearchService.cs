using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllCommands.Portal.Services
{
    public interface ISearchService
    {
        IEnumerable<string> GetCategories();
        Task<List<Commands>> GetCommandsAsync(string category);
    }
}
