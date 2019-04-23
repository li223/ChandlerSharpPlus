using Newtonsoft.Json;

namespace ChandlerSharpPlus.Objects
{
    public sealed class Thread
    {
        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("parentid")]
        public int ParentId { get; set; } = -1;

        [JsonProperty("boardtag")]
        public string BoardTag { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; } = "Anonymous";

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; private set; }
    }
}
