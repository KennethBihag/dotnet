//#define MSDN_EXAMPLE
using System;
using System.Runtime.InteropServices;
public class testMarshal_2
{

   public static void Main(string[] args)
   {
#if MSDN_EXAMPLE
      // string array ByVal
      string[] strArray = { "one", "two", "three", "four", "five" };
      Console.WriteLine("\n\nstring array before call:");
      foreach (string s in strArray)
      {
         Console.Write(" " + s);
      }

      int lenSum = NativeMethods.TestArrayOfStrings(strArray, strArray.Length);
      Console.WriteLine("\nSum of string lengths:" + lenSum);
      Console.WriteLine("\nstring array after call:");
      foreach (string s in strArray)
      {
         Console.Write(" " + s);
      }
      /*
      // array ByRef
      int[] array2 = new int[10];
      int size = array2.Length;
      Console.WriteLine("\n\nInteger array passed ByRef before call:");
      for (int i = 0; i < array2.Length; i++)
      {
         array2[i] = i;
         Console.Write(" " + array2[i]);
      }

      IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(size)
         * array2.Length);
      Marshal.Copy(array2, 0, buffer, array2.Length);

      int sum2 = NativeMethods.TestRefArrayOfInts(ref buffer, ref size);
      Console.WriteLine("\nSum of elements:" + sum2);
      if (size > 0)
      {
         int[] arrayRes = new int[size];
         Marshal.Copy(buffer, arrayRes, 0, size);
         Marshal.FreeCoTaskMem(buffer);
         Console.WriteLine("\nInteger array passed ByRef after call:");
         foreach (int i in arrayRes)
         {
            Console.Write(" " + i);
         }
      }
      else
      {
         Console.WriteLine("\nArray after call is empty");
      }
      */
#endif
      SegmentsInit();
      IntPtr _segment = GetASegment(0);
      Segment segment = (Segment)Marshal.PtrToStructure(_segment, typeof(Segment));
      _segment = GetASegment(1);
      segment = (Segment)Marshal.PtrToStructure(_segment, typeof(Segment));
      _segment = GetASegment(2);
      segment = (Segment)Marshal.PtrToStructure(_segment, typeof(Segment));
      FreeResources();
      Console.WriteLine("bye");
   }

   #region imported_functions
   const string dllName = "TestDLL.dll";
   [DllImport(dllName)]
   public static extern int Add(int a, int b);
   [DllImport(dllName)]
   public static extern char myToUpper(char ch);
   [DllImport(dllName)]
   public static extern double Divide(double a, double b);
   [DllImport(dllName)]
   public static extern void PrintArray(int[] a, int a_size);
   [DllImport(dllName)]
   public static extern IntPtr GetArrayPtr(int a_size);
   [DllImport(dllName)]
   public static extern void SetArray(int[] a, int a_size);

   [DllImport(dllName)]
   public static extern void ClearArray(IntPtr arr);
   [DllImport(dllName)]
   [return: MarshalAs(UnmanagedType.BStr)]
   public static extern string RetString();
   [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
   public static extern string GetNewString(string source,int length);
   [DllImport(dllName)]
   public static extern void SetNewArray(ref IntPtr source, int size);
   //experimental
   //[DllImport(dllName,CallingConvention =CallingConvention.Cdecl)]
   //[return: MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)]
   //public static extern int[] GetNewArray(double[] source, int length);

   #endregion
#if MSDN_EXAMPLE
   // Declares a managed structure for each unmanaged structure.
   [StructLayout(LayoutKind.Sequential)]
   public struct MyPoint
   {
      public int X;
      public int Y;

      public MyPoint(int x, int y)
      {
         this.X = x;
         this.Y = y;
      }
   }

   [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
   public struct MyPerson
   {
      public string First;
      public string Last;

      public MyPerson(string first, string last)
      {
         this.First = first;
         this.Last = last;
      }
   }

   internal static class NativeMethods
   {
      // Declares a managed prototype for an array of integers by value.
      // The array size cannot be changed, but the array is copied back.
      [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
      internal static extern int TestArrayOfInts(
          [In,Out] int[] array, int size);

      // Declares a managed prototype for an array of integers by reference.
      // The array size can change, but the array is not copied back
      // automatically because the marshaler does not know the resulting size.
      // The copy must be performed manually.
      [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
      internal static extern int TestRefArrayOfInts(
          ref IntPtr array, ref int size);

      // Declares a managed prototype for a matrix of integers by value.
      [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
      internal static extern int TestMatrixOfInts(
          int[,] pMatrix, int row);

      // Declares a managed prototype for an array of strings by value.
      [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
      internal static extern int TestArrayOfStrings(
          [In,Out]string[] stringArray, int size);

      // Declares a managed prototype for an array of structures with integers.
      [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
      internal static extern int TestArrayOfStructs(
          MyPoint[] pointArray, int size);

      // Declares a managed prototype for an array of structures with strings.
      [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
      internal static extern int TestArrayOfStructs2(
          MyPerson[] personArray, int size);
   }
#endif
   [StructLayout(LayoutKind.Sequential)]
   struct Geometry
   {
      IntPtr vertices;
      IntPtr indices;
      public double [] Vertices
      {
         get
         {
            double [] _vertices = new double[1];
            Marshal.Copy(vertices, _vertices, 0, 1);
            return _vertices;
         }
      }
      public int[] Indices
      {
         get
         {
            int[] _indices = new int[1];
            Marshal.Copy(indices,_indices,0,1);
            return _indices;
         }
      }
   };
   [StructLayout(LayoutKind.Sequential)]
   struct Segment
   {
      public int nodeId;
      private IntPtr subsegments;
      private IntPtr geometries;
      public Segment[] SubSegments
		{
			get {
            Segment[] _SubSegments = new Segment[1];
            _SubSegments[0]=(Segment)Marshal.PtrToStructure(subsegments,typeof(Segment));
            return _SubSegments;
         }
		}
      public Geometry[] Geometries
      {
         get
         {
            Geometry[] _Geometries = new Geometry[1];
            _Geometries[0] = (Geometry)Marshal.PtrToStructure(geometries, typeof(Geometry));
            return _Geometries;
         }
      }
   };
   [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
   public static extern void SegmentsInit();
   [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
   public static extern void FreeResources();
   [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
   public static extern IntPtr GetASegment(int post);
}