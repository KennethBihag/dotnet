internal class Person : IDisposable
{
   internal static MemoryCache mc;
   internal static MemoryCacheEntryOptions mceo;
   internal static Person[] _people;
   internal static Person[]People{
      get {
         mc.TryGetValue("people",out Person[] cachedPeople);
         if (cachedPeople == null) {
            MakeRandomPeople(50);
            mc.Set("people",_people,mceo);
            cachedPeople = mc.Get("people") as Person[];
         }
         return cachedPeople;
      }
   }
   public byte ID;
   public string FirstName;

   public override string ToString()
   {
      return $"ID:{ID}; First Name:{FirstName}";
   }
   public static void MakeRandomPeople(int count)
   {
      Faker<Person> fakePerson = new();
      fakePerson.RuleFor("ID", faker => (byte)faker.Random.Number(1, byte.MaxValue));
      fakePerson.RuleFor("FirstName", faker =>
      {
         //Thread.Sleep(33);
         return faker.Name.FirstName();
      });
      _people = fakePerson.Generate(count).ToArray();
   }

   public void Dispose()
   {
      ID=0;
      FirstName=null;
      GC.SuppressFinalize(this);
   }
}