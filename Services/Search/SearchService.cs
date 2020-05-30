using AllCommands.Portal.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static AllCommands.Portal.Constants.ApplicationConstants;

namespace AllCommands.Portal.Services
{
    public class SearchService : ISearchService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<SearchService> _logger;
        public SearchService(IHttpClientFactory httpClientFactory,ILogger<SearchService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public IEnumerable<string> GetCategories()
        {
            return ConfigUtility.GetSection(AppSettingsSection.Categories).Get<string[]>();
        }

        public async Task<List<Commands>> GetCommandsAsync(string category)
        {
            List<Commands> commands = null;
            string requestUri = $"https://api.github.com/repos/shutron/AllCommands/contents/{category}.json";

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("User-Agent", "AllCommands");

            var response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                JToken contentJToken = JToken.Parse(content);

                if (contentJToken.SelectToken("content") != null)
                {
                    byte[] data = Convert.FromBase64String(contentJToken.SelectToken("content").Value<string>());
                    string decodedString = Encoding.UTF8.GetString(data);

                    CommandFile commandFile = JsonConvert.DeserializeObject<CommandFile>(decodedString);
                    commands = commandFile?.Release?.Commands.ToList();

                }
                return commands ?? new List<Commands>();
            }
            else
            {
                _logger.LogError(response.ReasonPhrase);
                return null;
            }

        }
    }
}
