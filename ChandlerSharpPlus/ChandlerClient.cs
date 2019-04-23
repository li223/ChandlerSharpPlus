using ChandlerSharpPlus.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChandlerSharpPlus
{
    /// <summary>
    /// Main Client
    /// </summary>
    public class ChandlerClient
    {
        private HttpClient _http { get; set; }

        /// <summary>
        /// Client Ctor
        /// </summary>
        /// <param name="base_uri">The base uri for the instance to get data from</param>
        public ChandlerClient(Uri base_uri)
        {
            this._http = new HttpClient()
            {
                BaseAddress = base_uri
            };
        }

        /// <summary>
        /// Get a list of all boards
        /// </summary>
        /// <returns>Collection of Board Object</returns>
        public async Task<IEnumerable<Board>> GetBoardsListAsync()
        {
            var res = await this._http.GetAsync(new Uri($"{this._http.BaseAddress}/api/board")).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
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
            var res = await this._http.GetAsync(new Uri($"{this._http.BaseAddress}/api/board/data?tag={tag}")).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            var data = JsonConvert.DeserializeObject<Board>(cont);
            return data;
        }
        
        /// <summary>
        /// Get server meta data
        /// </summary>
        /// <returns>ServerMeta Object</returns>
        public async Task<ServerMeta> GetServerMetaAsync()
        {
            var res = await this._http.GetAsync(new Uri($"{this._http.BaseAddress}/api/server")).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            var data = JsonConvert.DeserializeObject<ServerMeta>(cont);
            return data;
        }

        /// <summary>
        /// Gets all threads in a given board
        /// </summary>
        /// <param name="tag">Board Tag</param>
        /// <returns>Collection of Thread Object</returns>
        public async Task<IEnumerable<Thread>> GetThreadsAsync(string tag)
        {
            var res = await this._http.GetAsync(new Uri($"{this._http.BaseAddress}/api/thread?tag={tag}")).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            var data = JsonConvert.DeserializeObject<IEnumerable<Thread>>(cont);
            return data;
        }

        /// <summary>
        /// Gets all of the child threads from a given thread
        /// </summary>
        /// <param name="thread_id">Id of the thread to get child threads from</param>
        /// <returns>Collection of Thread Object</returns>
        public async Task<IEnumerable<Thread>> GetChildPostsAsync(int thread_id = -1)
        {
            var res = await this._http.GetAsync(new Uri($"{this._http.BaseAddress}/api/post?thread={thread_id}")).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
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
            var res = await this._http.PostAsync(new Uri($"{this._http.BaseAddress}/api/create"), new StringContent(JsonConvert.SerializeObject(thread))).ConfigureAwait(false);
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
        /// <param name="parent_id">Id of the parent thread if reply</param>
        /// <returns>True, if posted</returns>
        public async Task<bool> CreatePostAsync(string board_tag, string topic, string text, string username = "Anonymous", string image_url = null, int parent_id = -1)
        {
            var thread = new Thread()
            {
                BoardTag = board_tag,
                Image = image_url,
                Topic = topic,
                Text = text,
                Username = username,
                ParentId = parent_id
            };
            return await this.CreatePostAsync(thread).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Delete a post
        /// </summary>
        /// <param name="password">User Password</param>
        /// <param name="post_id">Id of the post</param>
        /// <returns>Response String</returns>
        public async Task<string> DeletePostAsync(string password, int post_id = -1)
        {
            var req = new HttpRequestMessage(HttpMethod.Delete, new Uri($"{this._http.BaseAddress}/api/delete?postid={post_id}"))
            {
                Content = new StringContent(string.Concat(@"{""pass"":", password, @"""}"))
            };
            var res = await this._http.SendAsync(req).ConfigureAwait(false);
            var cont = await res.Content.ReadAsStringAsync();
            return cont;
        }
    }
}