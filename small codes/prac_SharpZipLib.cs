using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
namespace ConsoleApp1 {
   internal class SharpZipLib_prac {
      static void Main(string[] args) {

         MemoryStream dstStream;
         byte[] compBytes;
         byte[] decompressed;
#if StringData
         string origString = "Hello";
         char[] origChars = origString.ToCharArray();
         byte[] origBytes = Array.ConvertAll(origChars, x => (byte)(x));
         origChars = null; origString = null;

         foreach (byte b in origBytes)
         {
            Console.Write("{0:x} ", b);
         }

         Console.WriteLine("\n");
#else
         compBytes=File.ReadAllBytes("C:/Users/Kenneth/Desktop/C#Tests/ConsoleApp2/carpark_exportMenu.w3d");
         Console.WriteLine("Orig data");
         foreach(byte b in compBytes)
            Console.Write((char)b);
         Console.WriteLine();
#endif
         byte[] temp = new byte[compBytes.Length*2];
         Inflater inflater = new Inflater();
         inflater.SetInput(compBytes);
         inflater.Inflate(temp);
         decompressed=new byte[inflater.TotalOut];
         Array.Copy(temp,decompressed,inflater.TotalOut);
         temp=null;

         #region WRITE_TO_FILE_COMPRESSED
         File.WriteAllBytes("Compressed",compBytes);
         File.WriteAllBytes("Decompressed",decompressed);
#endregion

      }
		#region FunctionsUsingZLib_Net
		//public static void CompressData(byte[] inData,out byte[] outData) {
		//   using(MemoryStream outMemoryStream = new MemoryStream())
		//   using(ZOutputStream outZStream = new ZOutputStream(outMemoryStream,zlibConst.Z_DEFAULT_COMPRESSION))
		//   using(Stream inMemoryStream = new MemoryStream(inData)) {
		//      CopyStream(inMemoryStream,outZStream);
		//      outZStream.finish();
		//      outData=outMemoryStream.ToArray();
		//   }
		//}

		//public static void DecompressData(byte[] inData,out byte[] outData) {
		//   using(MemoryStream outMemoryStream = new MemoryStream())
		//   using(ZOutputStream outZStream = new ZOutputStream(outMemoryStream,zlibConst.Z_DEFAULT_COMPRESSION))
		//   using(Stream inMemoryStream = new MemoryStream(inData)) {
		//      CopyStream(inMemoryStream,outZStream);
		//      outZStream.finish();
		//      outData=outMemoryStream.ToArray();
		//   }
		//}

		//public static void CopyStream(System.IO.Stream input,System.IO.Stream output) {
		//   byte[] buffer = new byte[2000];
		//   int len;
		//   while((len=input.Read(buffer,0,2000))>0) {
		//      output.Write(buffer,0,len);
		//   }
		//   output.Flush();
		//}
		#endregion
	}
}
