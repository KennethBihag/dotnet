using StackExchange;
using StackExchange.Redis;
using System.Diagnostics;
using DDB = DummyDB<System.Int32>;
Console.WriteLine("Start");

ConfigurationOptions op = new()
{
   EndPoints = { "127.0.0.1:6379" },
   //ConnectTimeout = 60000,
   //AbortOnConnectFail = false,
   //SyncTimeout = 60000,
   //AllowAdmin = false,
   //ReconnectRetryPolicy = new LinearRetry(5000)
};
ConnectionMultiplexer cm =
   //ConnectionMultiplexer.Connect("127.0.0.1:6379");
   //ConnectionMultiplexer.Connect(op.ToString());
   ConnectionMultiplexer.Connect(op);
IDatabase db = cm.GetDatabase();
List<DDB.Vertex> verts = null;
var expTime = TimeSpan.FromSeconds(5);
do
{
   Console.Clear();
   Stopwatch stopwatch = new Stopwatch();
   stopwatch.Start();
   if (db.StringGet("Vertices") == RedisValue.Null)
   {
      verts = DDB.GenerateVertices(50);
      db.StringSet("Vertices",true,expTime);
      foreach(DDB.Vertex v in verts)
      {
         db.StringSet(verts.IndexOf(v).ToString(),v.ToString(),expTime);
      }
      Console.WriteLine("New list...");
   }
   else
   {
      Console.WriteLine("Old list...");
   }
   foreach (var v in verts)
   {
      Console.WriteLine(v);
   }
   stopwatch.Stop();
   Console.WriteLine("Done in {0:F8} s",stopwatch.ElapsedMilliseconds/1000);
}
while (Console.ReadKey(true).Key != ConsoleKey.Escape);

Console.WriteLine("End");