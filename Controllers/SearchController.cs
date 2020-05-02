﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProgrammingStuffs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private IConfiguration _config;

        public SearchController(ILogger<SearchController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        [HttpGet]
        [Route("GetCategories")]
        public IEnumerable<string> GetCategories()
        {
            return _config.GetSection("Categories").Get<string[]>();
        }
        [HttpGet]
        public async Task<IEnumerable<Commands>> GetAsync(string category, string searchText)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(category))
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "ProgrammingStuffs");
                    var response = await client.GetAsync($"https://api.github.com/repos/shutron/CommandLine/contents/{category}.json");
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var bodyJson = JToken.Parse(content);
                        if (bodyJson.SelectToken("content") != null)
                        {
                            byte[] data = Convert.FromBase64String(bodyJson.SelectToken("content").Value<string>());
                            string decodedString = Encoding.UTF8.GetString(data);
                            var obj = JsonConvert.DeserializeObject<CommandFile>(decodedString);
                            var searchResults = obj?.Release?.Commands?.Where(x => string.IsNullOrEmpty(searchText) || x.Command.ToLower().Contains(searchText.ToLower()) || x.Description.ToLower().Contains(searchText.ToLower()));
                            return searchResults ?? new List<Commands>();
                        }
                    }
                    else
                    {
                        _logger.LogError($"StatusCode: {response.StatusCode} \nContent: {content}  \nReasonPhrase: {response.ReasonPhrase}");
                    }
                }
            }
            return new List<Commands>();
        }
    }
}