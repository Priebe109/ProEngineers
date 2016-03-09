using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace tcp
{
	class file_server
	{
		/// <summary>
		/// The PORT
		/// </summary>
		const int PORT = 9000;
		/// <summary>
		/// The BUFSIZE
		/// </summary>
		const int BUFSIZE = 1000;

		/// <summary>
		/// Initializes a new instance of the <see cref="file_server"/> class.
		/// Opretter en socket.
		/// Venter på en connect fra en klient.
		/// Modtager filnavn
		/// Finder filstørrelsen
		/// Kalder metoden sendFile
		/// Lukker socketen og programmet
		/// </summary>
		private file_server ()
		{
			// Opretter en socket...
			TcpListener serverSocket = new TcpListener(PORT);
			int requestCount = 0;
			TcpClient clientSocket = default(TcpClient);
			serverSocket.Start();
			Console.WriteLine(" >> Server Started");

			while (true)
			{
				Console.WriteLine(" >> Server waiting for a client");
				/// Venter på en connect fra en klient.
				clientSocket = serverSocket.AcceptTcpClient();	//Crashes if filepath is wrong
				Console.WriteLine(" >> Accept connection from client");
				try
				{
					// Receive bytes from client and convert to string.
					requestCount += 1;
					NetworkStream networkStream = clientSocket.GetStream();

					//Reads text from networkStream with TCP. Checks if file exists and sends file.
					string readText = LIB.readTextTCP (networkStream);
					Console.WriteLine (" >> Data from client: " + readText);
					long fileCheck = LIB.check_File_Exists (readText);

					if (fileCheck == 0)
					{
						Console.WriteLine ("File not found");
						LIB.writeTextTCP (networkStream, "NOT FOUND");
					}
					else
					{
						LIB.writeTextTCP (networkStream, "OK");
						sendFile (readText, fileCheck, networkStream);
					}
					clientSocket.Close();
					Console.WriteLine(" >> Closed client connection");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					closeConnection (clientSocket, serverSocket);
				}
			}
		}

		private void closeConnection(TcpClient clientSocket, TcpListener serverSocket)
		{
			clientSocket.Close();
			serverSocket.Stop();
			Console.WriteLine(" >> Closed connection");
		}

		/// <summary>
		/// Sends the file.
		/// </summary>
		/// <param name='fileName'>
		/// The filename.
		/// </param>
		/// <param name='fileSize'>
		/// The filesize.
		/// </param>
		/// <param name='io'>
		/// Network stream for writing to the client.
		/// </param>
		private void sendFile (String fileName, long fileSize, NetworkStream io)
		{
			FileStream fileStream = new FileStream (fileName, FileMode.Open, FileAccess.Read);
			int fileLength = (int)fileStream.Length;
			int packetLength;
			//Calculate total packet amount 
			int packetCount = (int)Math.Ceiling ((double)fileLength / (double)BUFSIZE);

			for (int i = 0; i < packetCount; i++) 
			{
				//Set current packetLength
				if (fileLength > BUFSIZE) {
					packetLength = BUFSIZE;		
					fileLength -= packetLength;
				} else {
					packetLength = fileLength;
				}
				//Send packet
				var outBuffer = new byte[packetLength];
				fileStream.Read (outBuffer, 0, packetLength);
				io.Write (outBuffer, 0, packetLength);
				Console.Write ("\rSent {0} out of {1} packets", i + 1, packetCount);
			}
			Console.WriteLine ();
		}

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		public static void Main (string[] args)
		{
			Console.WriteLine ("Server starts...");
			new file_server();
		}
	}
}