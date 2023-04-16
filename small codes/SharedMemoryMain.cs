using System.IO.MemoryMappedFiles;
using SM = System.IO.MemoryMappedFiles;

#if false
string sharedFileName = "SharedFile";
string dataToWrite = "Hello this information is in the...\nMEMORY MAP!!!";
FileStreamOptions options = new()
{
   Mode = FileMode.OpenOrCreate,
   Access = FileAccess.ReadWrite,
   Share = FileShare.ReadWrite,
   BufferSize = 1024
};
FileStream fs = new(sharedFileName,options);
fs.Close();
MemoryMappedFile memoryMappedFile = MemoryMappedFile.CreateFromFile(sharedFileName,FileMode.OpenOrCreate,null,capacity:1024);
MemoryMappedViewAccessor memoryMappedViewAccessor = memoryMappedFile.CreateViewAccessor();
memoryMappedViewAccessor.WriteArray(500,dataToWrite.ToCharArray(),0,dataToWrite.Length);
char[] readChars = new char[dataToWrite.Length];
memoryMappedViewAccessor.ReadArray<char>(500,readChars,0,12);
Console.WriteLine(new string(readChars));
/*for(int i=500;i<510;++i)
   Console.Write((char)memoryMappedViewAccessor.ReadByte(i));*/
memoryMappedViewAccessor.Dispose();
memoryMappedFile.Dispose();
#else
#if WIN_SHARE
string sharedMemoryName = "Boost";
   using (var mmf = MemoryMappedFile.OpenExisting(sharedMemoryName, MemoryMappedFileRights.ReadWrite))
   {
      using (var accessor = mmf.CreateViewAccessor(0, 1024, MemoryMappedFileAccess.ReadWrite))
      {
         byte[] bytes = new byte[16];
         accessor.ReadArray(0, bytes, 0, 16);
         string str = Encoding.ASCII.GetString(bytes);
         Console.WriteLine(str);
      }
   }
Console.ReadKey(true);
#else
string sharedMemoryName = "Boost";
using(FileStream fs = File.Open(sharedMemoryName,FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite))
   using (var mmf = MemoryMappedFile.CreateFromFile(fs,sharedMemoryName,1024,MemoryMappedFileAccess.ReadWrite,HandleInheritability.None,true))
   {
      using (var accessor = mmf.CreateViewAccessor(0, 1024, MemoryMappedFileAccess.ReadWrite))
      {
         byte[] bytes = new byte[16];
         accessor.ReadArray(0, bytes, 0, 16);
         string str = Encoding.ASCII.GetString(bytes);
         Console.WriteLine(str);
         Console.ReadKey(true);
         accessor.WriteArray(0,"Now, it's C# LOL".ToCharArray(),0,16);
      }
   }
#endif
#if WIN_SHARE
#else

#endif
#endif