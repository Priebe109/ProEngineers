using System;
using System.IO;
using System.Linq;
using System.Text;
using Transportlaget;
using Library;

namespace Application
{
	class file_client
	{
		/// <summary>
		/// The BUFSIZE.
		/// </summary>
		const int BUFSIZE = 1000;
	    Transport transport;

		/// <summary>
		/// Initializes a new instance of the <see cref="file_client"/> class.
		/// 
		/// file_client metoden opretter en peer-to-peer forbindelse
		/// Sender en forspørgsel for en bestemt fil om denne findes på serveren
		/// Modtager filen hvis denne findes eller en besked om at den ikke findes (jvf. protokol beskrivelse)
		/// Lukker alle streams og den modtagede fil
		/// Udskriver en fejl-meddelelse hvis ikke antal argumenter er rigtige
		/// </summary>
		/// <param name='args'>
		/// Filnavn med evtuelle sti.
		/// </param>
	    private file_client(String[] args)
	    {
            transport = new Transport(BUFSIZE);
		    var filePath = args[0];

            // Send
            var sendBuf = Encoding.ASCII.GetBytes(filePath);
            transport.send(sendBuf, sendBuf.Length);
            Console.WriteLine(">> " + filePath + " send to server");

            // Receive
            var receiveBuf = new byte[BUFSIZE];
		    var receivedBytes = transport.receive(ref receiveBuf);
		    var truncatedBuf = receiveBuf.Take(receivedBytes).ToArray();

		    var fileSize = int.Parse(Encoding.ASCII.GetString(truncatedBuf));

            // Act on message from server.
            switch (fileSize)
            {
                case 0:
                    Console.WriteLine(" >>  File not found on server");
                    return;
                default:
                    // File found on server
                    receiveFile(LIB.extractFileName(filePath), fileSize, transport);
                    break;
            }
            
        }

		/// <summary>
		/// Receives the file.
		/// </summary>
		/// <param name='fileName'>
		/// File name.
		/// </param>
		/// <param name='transport'>
		/// Transportlaget
		/// </param>
		private void receiveFile (String fileName, int fileSize, Transport transport)
        {
            // Client’en skal modtage den ønskede fil fejlfrit fra serveren – eller udskrive en fejlmelding hvis filen ikke findes i serveren.
            byte[] inBuffer = new byte[BUFSIZE];
            int received;
            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);

            int packetCount = (int)Math.Ceiling((double)fileSize / (double)BUFSIZE);

            for (int i = 0; i < packetCount; i++)
		    {
		        received = transport.receive(ref inBuffer);
                fileStream.Write(inBuffer, 0, received);

                Console.Write("\rRecieved {0} out of {1} packets", i + 1, packetCount);
            }

            Console.WriteLine("\n >> Recieved file");
        }

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// First argument: Filname
		/// </param>
		public static void Main (string[] args)
		{
            string filePath;

            // Client-applikationen skal kunne startes fra en terminal med kommandoen:
            // #./file_client.exe <file_server’s ip-adr.> <[path] + filename>
            int argsCount = args.Length;

            if (argsCount == 1)
            {
                // Get main args.
                filePath = args[0];
            }
            else if (argsCount == 0)
            {
                // Ask for args.
                Console.WriteLine(" >> Enter [file path] followed by enter");
                filePath = Console.ReadLine();
            }
            else
            {
                return;
            }

            new file_client(new string[] {filePath});
		}
	}
}