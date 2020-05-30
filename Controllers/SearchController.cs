using AllCommands.Portal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllCommands.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : BaseController
    {
        private readonly ILogger<SearchController> _logger;

        private readonly ISearchService _searchService;

        public SearchController(ILogger<SearchController> logger, ISearchService searchService)
        {
            _logger = logger;
            _searchService = searchService;
        }

        [HttpGet]
        [OutputCache(Duration = int.MaxValue)]
        [Route("GetCategories")]
        public IEnumerable<string> GetCategories()
        {
            return _searchService.GetCategories();
        }

        [HttpGet]
        [OutputCache(Duration = 86400, VaryByParam = "category")]
        public async Task<IEnumerable<Commands>> GetAsync(string category)
        {
            return await _searchService.GetCommandsAsync(category);
        }
    }
}
