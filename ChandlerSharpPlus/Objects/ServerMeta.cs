using Newtonsoft.Json;
using System;

namespace ChandlerSharpPlus.Objects
{
    public struct ServerMeta
    {
        [JsonProperty("starttime")]
        public DateTime StartTime { get; private set; }

        [JsonProperty("uptime")]
        public TimeSpan Uptime { get; private set; }
    }
}
