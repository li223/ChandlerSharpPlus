using Newtonsoft.Json;
using System;

namespace ChandlerSharpPlus.Objects
{
    public struct Board
    {
        [JsonProperty("tag")]
        public char Tag { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonProperty("image")]
        public Uri ImageUri { get; private set; }

        [JsonProperty("motd")]
        public string MessageOfTheDay { get; private set; }
    }
}
