global using Bogus;
global using Microsoft.Extensions.Caching;
global using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

Console.WriteLine("Started...");

MemoryCacheEntryOptions mceo = new() { Size = 1, AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1) };
MemoryCache mc = new(new MemoryCacheOptions() { SizeLimit = 10 });
//Person.mc = mc;
//Person.mceo = mceo;
Console.WriteLine("Generating data...");
Person.MakeRandomPeople(25);
Console.WriteLine("Generated data...");
int i=0;
for(;i<Person._people.Length;)
{
   var person = Person._people[i++];
   if(i%2==0)
      mceo.SetPriority(CacheItemPriority.High);
   else
      mceo.SetPriority(CacheItemPriority.Low);
   mc.Set(person.GetHashCode(), person,mceo);
}
Loop:
   Console.WriteLine("Cache count : {0}", mc.Count);
   Console.WriteLine("Generating data...");
   Stopwatch stopwatch = Stopwatch.StartNew();
   //int i = 0;
   //foreach (var person in Person.People)
   //{
   //   Console.WriteLine(++i+":\n\t"+person.ToString());
   //}

   for(i=0;i<Person._people.Length;)
   {
      var person = Person._people[i++];
      Console.Write($"{i}:");
      if (mc.Get(person.GetHashCode()) != null)
      {
         Console.WriteLine($"\t{person}");
      }
      else
      {
         Console.WriteLine("Not in cache");
      }
   }

   stopwatch.Stop();
   Console.WriteLine("{0:F8}s elapsed",
      stopwatch.Elapsed.TotalMilliseconds / 1000);

if (Console.ReadKey(true).Key != ConsoleKey.Escape)
{
   Console.Clear();
   goto Loop;
}

Console.WriteLine("\nEnded...");