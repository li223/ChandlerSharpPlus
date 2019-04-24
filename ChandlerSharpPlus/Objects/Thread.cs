using Newtonsoft.Json;

namespace ChandlerSharpPlus.Objects
{
    /// <summary>
    /// Thread Object
    /// </summary>
    public sealed class Thread
    {
        /// <summary>
        /// Thread Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Parent Thread Id, -1 if Original Post
        /// </summary>
        [JsonProperty("parentid")]
        public int ParentId { get; set; } = -1;

        /// <summary>
        /// Tag for the board this thread belongs to
        /// </summary>
        [JsonProperty("boardtag")]
        public string BoardTag { get; set; }

        /// <summary>
        /// Image for the post, if any
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }

        /// <summary>
        /// Given username of the OP, defaults to "Anonymous"
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; } = "Anonymous";

        /// <summary>
        /// Thread topic
        /// </summary>
        [JsonProperty("topic")]
        public string Topic { get; set; }

        /// <summary>
        /// Post text
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// IP of the poster?
        /// </summary>
        [JsonProperty("ip")]
        public string Ip { get; private set; }
        
        /// <summary>
        /// Password to be used to later delete the thread
        /// </summary>
        [JsonProperty("generatepass")]
        public string Password { get; private set; } = "";
    }
}
