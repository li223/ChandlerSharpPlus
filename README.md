# ChandlerSharpPlus
I dont even know at this point. [Naam made a thing](https://github.com/NaamloosDT/CHANdler) so I made a thing for [his thing](https://github.com/NaamloosDT/CHANdler) so now it's a clusterfuck of things. 

My personal fork if you ~~broken~~ fresh updates -> [Li223 Chandler](https://github.com/li223/Chandler)

# Example
```cs
static async Task Main(string[] args)
{
  //Create Client
  var client = new ChandlerClient("SERVER BASE URL");

  //Create a thread
  var res = await client.CreatePostAsync("b", "Test Topic", "This is a test");

  //Get all posts from the board
  var data = await client.GetThreadsAsync("b");

  //Find our post
  var thread = data.First(x => x.Topic == "Test Topic");

  //Print out generic stuff
  Console.WriteLine($"Username: {thread.Username}, Post Text: {thread.Text}");

  //Don't die pl0x
  Console.ReadKey();
}
```
