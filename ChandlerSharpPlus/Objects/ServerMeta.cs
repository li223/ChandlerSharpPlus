using Newtonsoft.Json;
using System;

namespace ChandlerSharpPlus.Objects
{
    /// <summary>
    /// Server Meta Object
    /// </summary>
    public struct ServerMeta
    {
        /// <summary>
        /// Server Start Time
        /// </summary>
        [JsonProperty("starttime")]
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// Server uptime
        /// </summary>
        [JsonProperty("uptime")]
        public TimeSpan Uptime { get; private set; }
    }
}
