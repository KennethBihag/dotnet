namespace Pipes;
using System.IO.Pipes;
internal class Server
{
   static byte[] buffer;
   private static void Main(string[] args)
   {
      ushort bufferLength = 12;
      buffer = new byte[bufferLength];
      Action bufferString = () => {
         for(int i = 0; i < bufferLength; i++) { 
            Console.Write((char)buffer[i]);
         }
         Console.WriteLine();
      };
 
      //create random characters
      int seed = (int)DateTime.Now.Ticks;
      Random rn = new Random(seed);      
      for(int i = 0; i < bufferLength; i++)
         buffer[i] = (byte)rn.Next(33,126);

      Console.WriteLine("Bytes to send...");
      bufferString();

      NamedPipeServerStream npss = new ("ServerWritingPipe",PipeDirection.InOut);
      npss.WaitForConnection();
      npss.Write(buffer);
      npss.Read(buffer);
      npss.Close();
      Console.WriteLine("Bytes received from client...");
      bufferString();

      Console.WriteLine("Done");
   }
}