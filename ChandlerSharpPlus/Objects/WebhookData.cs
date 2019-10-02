using Newtonsoft.Json;

namespace ChandlerSharpPlus
{
    /// <summary>
    /// Data returned when you subscribe a webhook
    /// </summary>
    public class WebhookData
    {
        /// <summary>
        /// Webhook Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; internal set; }
        
        /// <summary>
        /// Webhook Url
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; internal set; }

        /// <summary>
        /// Board tag you subscribed to
        /// </summary>
        [JsonProperty("boardtag")]
        public string BoardTag { get; internal set; }

        /// <summary>
        /// Id of the thread you subscribed to
        /// </summary>
        [JsonProperty("threadid")]
        public int? ThreadId { get; internal set; }

        /// <summary>
        /// The hashed secret. You will need this to unsubscribe the webhook
        /// </summary>
        [JsonProperty("hashSecret")]
        public string HashSecret { get; internal set; }

        /// <summary>
        /// Number of cycles of the hash
        /// </summary>
        [JsonProperty("hashCycles")]
        public int HashCycles { get; internal set; }
    }
}