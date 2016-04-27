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
		/// Sender en forspÃ¸rgsel for en bestemt fil om denne findes pÃ¥ serveren
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

            // Act on message from server.
            switch (Encoding.ASCII.GetString(truncatedBuf))
            {
                case "OK":
                    // File found on server
                    receiveFile(LIB.extractFileName(filePath), transport);
                    break;
                case "NOT FOUND":
                    Console.WriteLine(" >>  File not found on server");
                    return;
                default:
                    break;
            }

            Console.WriteLine(">> Response from server:");
            Console.WriteLine(Encoding.ASCII.GetString(receiveBuf, 0, receiveBuf.Length));
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
		private void receiveFile (String fileName, Transport transport)
		{
			// TO DO Your own code
            Console.WriteLine("File received!");
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