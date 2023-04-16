global using Bogus;

public class DummyDB<T> where T : struct, IComparable, IConvertible, ISpanFormattable
{
   public class Vertex
   {
      public T X { get; set; }
      public T Y { get; set; }
      public T Z { get; set; }
      public override string ToString()
      {
         return $"{{X:{X},Y:{Y},Z:{Z}}}";
      }
   }
   public static List<Vertex> GenerateVertices(int maxLength)
   {
      Faker<Vertex> facker = new();
      int sleepTime = 20;
      switch (typeof(T).Name)
      {
         case "Single":
            facker.RuleForType(typeof(T), t =>
            {
               Thread.Sleep(sleepTime);
               return t.Random.Float(-1000, 1000);
            });
            break;
         case "Int32":
            facker.RuleForType(typeof(T), t =>
            {
               Thread.Sleep(sleepTime);
               return t.Random.Int(-1000, 1000);
            });
            break;
      }
      int vertsLength = new Random(DateTime.Now.Millisecond).Next(1, maxLength);
      return facker.Generate(vertsLength);
   }
}

public class Point2
{
   public static Point2 CreatePoint()
   {
      return new Point2();
   }
   public void WroteLine() => Console.WriteLine("This is a point - " + new DummyDB<float>.Vertex());
}