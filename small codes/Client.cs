namespace Pipes;
using System.IO.Pipes;

internal class Client
{
   private static void Main(string[] args)
   {
      NamedPipeClientStream npcs = new(".","ServerWritingPipe",PipeDirection.InOut);
      npcs.Connect();
      int bufferLength = 100;
      byte[] buffer  = new byte[bufferLength];
      Action bufferString = () => {
         for(int i = 0; i < bufferLength; i++) 
            Console.Write((char)buffer[i]);
         Console.WriteLine();
      };
      Console.WriteLine("Received bytes...");
      bufferLength = npcs.Read(buffer);
      bufferString();

      Console.WriteLine("Sending {0} bytes back to server...",bufferLength);
      for(int i = 0; i < bufferLength; i++)
         buffer[i]=(byte)'X';
      bufferString();
      npcs.Write(buffer,0,bufferLength);
      npcs.Close();

      Console.WriteLine("Done");
   }
}