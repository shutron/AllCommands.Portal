using Newtonsoft.Json;
using System.Collections.Generic;

namespace AllCommands
{
    public class Commands
    {
        [JsonProperty("command")]
        public string Command { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("usage")]
        public string Usage { get; set; }

        [JsonProperty("options")]
        public IEnumerable<Option> Options { get; set; }
    }

    public class Option
    {
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("usage")]
        public string Usage { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class CommandFile
    {
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("release")]
        public Release Release { get; set; }
    }
    public class Release
    {
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("commands")]
        public IEnumerable<Commands> Commands { get; set; }
    }
}
