using Newtonsoft.Json;
using System;

namespace ChandlerSharpPlus.Objects
{
    /// <summary>
    /// Board Object
    /// </summary>
    public struct Board
    {
        /// <summary>
        /// Board's tag
        /// </summary>
        [JsonProperty("tag")]
        public string Tag { get; private set; }

        /// <summary>
        /// Board's name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// Board Description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; private set; }

        /// <summary>
        /// Board Image Uri
        /// </summary>
        [JsonProperty("image")]
        public Uri ImageUri { get; private set; }

        /// <summary>
        /// Board's message of the day
        /// </summary>
        [JsonProperty("motd")]
        public string MessageOfTheDay { get; private set; }
    }
}
