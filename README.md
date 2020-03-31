# ChandlerSharpPlus
I dont even know at this point. [Naam made a thing](https://github.com/Naamloos/CHANdler) so I made a thing for [his thing](https://github.com/Naamloos/CHANdler) so now it's a clusterfuck of things. 

The main repo can be found at: https://github.com/Naamloos/CHANdler

# Example
```cs
static async Task Main(string[] args)
{
  //Create Client
  var client = new ChandlerClient("SERVER BASE URL");

  //Create a thread
  await client.CreatePostAsync("b", "Test Topic", "This is a test");

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
