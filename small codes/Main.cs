namespace FileScopedNS;
using System;
using static Console;
using stdCout = Console;
using Sys = System;
internal class MainClass
{
   private int myNumber;
   public static void Main(string[] args)
   {
      Console.WriteLine("Start");

      /* //different string formatting, escape sequences
      System.Console.WriteLine("Hello");
      Console.WriteLine("1 Hello\bHi");
      Console.WriteLine("2 Hello\fHi");
      Console.WriteLine("3 Hello\rHi");
      Console.WriteLine("4 Hello\vHi");
      string tae = @"Hello
hi";
      Console.WriteLine("5 "+tae);
      string tobol = "Hello\nhi";
      Console.WriteLine("6 {0}",tobol);
      const int x = 17;
      const string y = "17";
      Console.WriteLine($@"7 x =
{x:f3}");
      Console.WriteLine($@"8 x =
{x:f1}");
      const string ms = $"Hello {y}";
      */
      /* //indexing arrays
      byte[] data = {2,4,6,8,10 };
      Index first = 0;
      Index last = ^1;
      Index mid = Index.FromEnd(3);
      Index mid2 = Index.FromStart(3);
      Console.WriteLine("Indices :{0}:{1}:{2}:{3}",first.Value,last.Value,mid.Value,mid2.Value);
      byte[] firstThree = data[0..3];
      foreach(byte b in firstThree)
      Console.WriteLine(b);
      */
      /* //declaration,  defininition of multi-dim arrays
      int[][] ints = new int[][] { new int[] { 0 }, new[] { 1, 2 }, new[] { 3, 4, 5 } };
      int[][] ints3 = { new[] { -1 }, new int[] { -2, -3 } };
      int[,] ints2 = { { 1, 2, 3 }, { 4, 5, 6 } };
      object[] ints4 = { new int[] { 12, 23, 34 }, new int[] { 45, 56 } };
      */
      /* //switch-cases
      //switch-case on type
      TellMeTheType(27);
      TellMeTheType("Hello world");
      TellMeTheType(new short[] { 2, 4, 6 });
      TellMeTheType(3.14f);
      TellMeTheType(-99f);
      //switch expression
      while (true)
      {
         int k = Console.ReadKey(true).Key.GetHashCode();
         string utf = k switch
         {
            65 => ((char)65).ToString(),
            _ => k.ToString()
         };
         Console.WriteLine(k);
         Console.WriteLine("UTF : " + utf);

         if (k == 32)
            break;
      }
      // goto in switch-case
      int y = 100;
      switch (y)
      {
         case 10:
            Console.WriteLine("Is a ten");
            break;
         case 100:
            Console.WriteLine("One hundred");
            goto case 10;
         default:
            Console.WriteLine("None");
            break;
      }
      */
      /* // other tests
      string nullStr = null;
      Console.WriteLine(nullStr?.ToUpper());
      nullStr = "Not a null string";
      Console.WriteLine(nullStr?.ToUpper());
      //aliasing
      stdCout.WriteLine("Hi program!");
      Sys.Console.WriteLine("Hey program!");
      */
      Console.WriteLine("End");
   }


   /*
   void withParams(params char[] chars)
   {
      string s = new (chars);
      Console.WriteLine(s);
   }
   */
   //switch-case on type
   /*static void TellMeTheType(object x)
   {
      switch (x)
      {
         case int i:
            Console.WriteLine("It's an int!");
            Console.WriteLine($"The square of {i} is {i * i}"); break;
         case string s:
            Console.WriteLine("It's a string");
            Console.WriteLine($"The length of {s} is {s.Length}"); break;
         case float f when f < 0:
            Console.WriteLine("Floating point that is negative."); break;
         case float _:
            Console.WriteLine("It's a floating point."); break;
         default:
            Console.WriteLine("I don't know what x is"); break;
      }
   }*/

   /* test initialization of static fields and classes
   static class StaticClass //fields are initialized at start of program
   {
      internal static string s_field1 = "Static Field_1";
      internal static int s_int1;
   }
   class NonStaticClass //static fields also init at start
   {
      internal static string s_field2 = "Static Field_2";
      internal static int s_int2;
      internal string _field2 = "Field 2";
      internal int _int2;
   }
   class Singleton
   {
      internal static NonStaticClass Instance;// = new NonStaticClass(); //called at start of prog
      internal NonStaticClass GetInstance
      {
         get
         {
            if (Instance is not null) return Instance;
            else return new();
         }
      }
      internal NonStaticClass OtherInstance
      {
         get
         {
            Lazy<NonStaticClass> l = new();
            return l.Value;
         }
}
   }
   */
}
