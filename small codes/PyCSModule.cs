namespace PyCS
{
   public class Class1
   {
      public bool bool1 = true;
      public byte byte1 = 127;
      public int int1 = 69;
      public enum Enums { val1, val2, val3 };
      public Enums Enums1 = (Enums)2;
      public Class2 p_Class2 = new();
   }
   public class Class2
   {
      public List<Class3> p_Class3;
      public Class2()
      {
         p_Class3 = new();
         p_Class3.Add(new());
         p_Class3.Add(new(){x=1,y=2,z=3});
         p_Class3.Add(new(){x=-100,y=-10,z=-1});
      }
   }
   public class Class3
   {
      public int x=23;
      public int y=34;
      public int z=45;
   }
}