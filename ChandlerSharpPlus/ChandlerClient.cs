using ChandlerSharpPlus.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChandlerSharpPlus
{
    /// <summary>
    /// Main Client
    /// </summary>
    public class ChandlerClient
    {
        private HttpClient Http { get; set; }

        /// <summary>
        /// Event Error Delegate
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <returns></returns>
        public delegate Task ClientErrorEvent(string message);

        /// <summary>
        /// Invoked when the client encounters an error
        /// </summary>
        public event ClientErrorEvent ClientErrored;
        
        /// <summary>
        /// The base url of the server you want to get data from
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Client Ctor
        /// </summary>
        /// <param name="base_url">The base url for the instance to get data from</param>
        public ChandlerClient(string base_url)
        {
            this.Http = new HttpClient();
            this.BaseUrl = base_url;
        }

        /// <summary>
        /// Get a list of all boards
        /// </summary>
        /// <returns>Collection of Board Object</returns>
        public async Task<IEnumerable<Board>> GetBoardsListAsync()
        {
            var res = await this.Http.GetAsync(new Uri($"{this.BaseUrl}/api/board")).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!res.IsSuccessStatusCode)
            {
                this.ClientErrored?.Invoke(cont);
                return null;
            }
            var data = JsonConvert.DeserializeObject<IEnumerable<Board>>(cont);
            return data;
        }

        /// <summary>
        /// Get data for the given board
        /// </summary>
        /// <param name="tag">The tag for the board</param>
        /// <returns>Board Object</returns>
        public async Task<Board> GetBoardDataAsync(string tag)
        {
            var res = await this.Http.GetAsync(new Uri($"{this.BaseUrl}/api/board/data?tag={tag}")).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!res.IsSuccessStatusCode)
            {
                this.ClientErrored?.Invoke(cont);
                return null;
            }
            var data = JsonConvert.DeserializeObject<Board>(cont);
            return data;
        }

        /// <summary>
        /// Get server meta data
        /// </summary>
        /// <returns>ServerMeta Object</returns>
        public async Task<ServerMeta> GetServerMetaAsync()
        {
            var res = await this.Http.GetAsync(new Uri($"{this.BaseUrl}/api/server")).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            var data = JsonConvert.DeserializeObject<ServerMeta>(cont);
            return data;
        }

        /// <summary>
        /// Get the server health
        /// </summary>
        /// <returns>Server Health Object</returns>
        public async Task<ServerHealth> GetServerHealthAsync()
        {
            var res = await this.Http.GetAsync($"{this.BaseUrl}/api/server/health").ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            var data = JsonConvert.DeserializeObject<ServerHealth>(cont);
            return data;
        }

        /// <summary>
        /// Gets all threads in a given board
        /// </summary>
        /// <param name="tag">Board Tag</param>
        /// <param name="include_children">Whether or not to include child threads</param>
        /// <returns>Collection of Thread Object</returns>
        public async Task<IEnumerable<Thread>> GetThreadsAsync(string tag, bool include_children = false)
        {
            var res = await this.Http.GetAsync(new Uri($"{this.BaseUrl}/api/thread?tag={tag}&includechildren={include_children}")).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!res.IsSuccessStatusCode)
            {
                this.ClientErrored?.Invoke(cont);
                return null;
            }
            var data = JsonConvert.DeserializeObject<IEnumerable<Thread>>(cont);
            return data;
        }

        /// <summary>
        /// Gets a single thread in a given board
        /// </summary>
        /// <param name="tag">Board Tag</param>
        /// <param name="include_children">Whether or not to include child threads</param>
        /// <returns>Collection of Thread Object</returns>
        public async Task<Thread> GetSingleThreadAsync(string tag, bool include_children = false)
        {
            var res = await this.Http.GetAsync(new Uri($"{this.BaseUrl}/api/thread/single?tag={tag}&includechildren={include_children}")).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!res.IsSuccessStatusCode)
            {
                this.ClientErrored?.Invoke(cont);
                return null;
            }
            var data = JsonConvert.DeserializeObject<Thread>(cont);
            return data;
        }

        /// <summary>
        /// Gets all of the child threads from a given thread
        /// </summary>
        /// <param name="thread_id">Id of the thread to get child threads from</param>
        /// <returns>Collection of Thread Object</returns>
        public async Task<IEnumerable<Thread>> GetChildPostsAsync(int thread_id = -1)
        {
            var res = await this.Http.GetAsync(new Uri($"{this.BaseUrl}/api/post?thread={thread_id}")).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!res.IsSuccessStatusCode)
            {
                this.ClientErrored?.Invoke(cont);
                return null;
            }
            var data = JsonConvert.DeserializeObject<IEnumerable<Thread>>(cont);
            return data;
        }

        /// <summary>
        /// Creates a post
        /// </summary>
        /// <param name="thread">Thread Object</param>
        /// <returns>True, if successful</returns>
        public async Task<bool> CreatePostAsync(Thread thread)
        {
            var res = await this.Http.PostAsync(new Uri($"{this.BaseUrl}/api/thread/create"), new StringContent(JsonConvert.SerializeObject(thread), Encoding.UTF8, "application/json")).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!res.IsSuccessStatusCode) this.ClientErrored?.Invoke(cont);
            return res.IsSuccessStatusCode;
        }

        /// <summary>
        /// Creates a post
        /// </summary>
        /// <param name="board_tag">Board's tag</param>
        /// <param name="topic">The thread topic</param>
        /// <param name="text">Thread Text</param>
        /// <param name="username">Username to show</param>
        /// <param name="image_url">Image url</param>
        /// <param name="password">Password required to delete the post</param>
        /// <param name="parent_id">Id of the parent thread if reply</param>
        /// <returns>True, if posted</returns>
        public async Task<bool> CreatePostAsync(string board_tag, string topic, string text, string username = "Anonymous", string image_url = null, string password = null, int parent_id = -1)
        {
            var thread = new Thread()
            {
                BoardTag = board_tag,
                Image = image_url,
                Topic = topic,
                Text = text,
                Username = username,
                ParentId = parent_id,
                Password = password
            };
            return await this.CreatePostAsync(thread).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a post
        /// </summary>
        /// <param name="password">User Password</param>
        /// <param name="post_id">Id of the post</param>
        /// <returns>True, if deleted</returns>
        public async Task<bool> DeletePostAsync(int post_id, string password)
        {
            var req = new HttpRequestMessage(HttpMethod.Delete, new Uri($"{this.BaseUrl}/api/delete?postid={post_id}&pass={password}"));
            var res = await this.Http.SendAsync(req).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!res.IsSuccessStatusCode) this.ClientErrored?.Invoke(cont);
            return res.IsSuccessStatusCode;
        }

        /// <summary>
        /// Subscribe a webhook to a board or thread
        /// </summary>
        /// <param name="wh_url">The url of the webhook</param>
        /// <param name="secret">Secret password to generate the hash on</param>
        /// <param name="board_tag">The tag of the board to subscribe to</param>
        /// <param name="thread_id">The id of the thread to subscribe to</param>
        /// <returns>Data on the webhook</returns>        
        public async Task<WebhookData> SubscribeWebhookAsync(string wh_url, string secret, string board_tag = null, int? thread_id = null)
        {
            if (board_tag == null && thread_id == null) throw new ArgumentNullException("Board tag or Thread Id are required to subscribe a webhook to");

            HttpResponseMessage res;
            if (board_tag != null) res = await Http.GetAsync(new Uri($"{BaseUrl}/api/webhooks/subscribe?url={wh_url}&secret={secret}&boardtag={board_tag}")).ConfigureAwait(false);
            else res = await Http.GetAsync(new Uri($"{BaseUrl}/api/webhooks/subscribe?url={wh_url}&secret={secret}&threadid={thread_id}")).ConfigureAwait(false);
            
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!res.IsSuccessStatusCode)
            {
                this.ClientErrored?.Invoke(cont);
                return null;
            }
            return JsonConvert.DeserializeObject<WebhookData>(cont);
        }

        /// <summary>
        /// Unsubscribe a subscribed webhook
        /// </summary>
        /// <param name="secret">The secret</param>
        /// <returns>True, if successfully unsubscribed the webhook</returns>
        public async Task<bool> UnsubscribeWebhookAsync(string secret)
        {
            var res = await Http.SendAsync(new HttpRequestMessage(HttpMethod.Delete, new Uri($"{BaseUrl}/api/webhooks/unsubscribe?secret={secret}"))).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false); 
            if (!res.IsSuccessStatusCode) this.ClientErrored?.Invoke(cont);
            return res.IsSuccessStatusCode;
        }
    }
}