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
			// Opretter en socket.
			TcpListener serverSocket = new TcpListener(PORT);
			int requestCount = 0;
			TcpClient clientSocket = default(TcpClient);
			serverSocket.Start();
			Console.WriteLine(" >> Server Started");

			/// Venter på en connect fra en klient.
			clientSocket = serverSocket.AcceptTcpClient();
			Console.WriteLine(" >> Accept connection from client");

			while (requestCount == 0)
			{
				try
				{
					// Receive bytes from client and convert to string.
					requestCount += 1;
					NetworkStream networkStream = clientSocket.GetStream();
					/*
					byte[] bytesFrom = new byte[BUFSIZE];	
					networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
					string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
					dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

					// Send response to client.
					Console.WriteLine(" >> Data from client: " + dataFromClient);
					string serverResponse = "Last Message from client: " + dataFromClient;
					Byte[] sendBytes = System.Text.Encoding.ASCII.GetBytes(serverResponse);
					networkStream.Write(sendBytes, 0, sendBytes.Length);
					networkStream.Flush();
					Console.WriteLine(" >> " + serverResponse);
					*/

					//Reads text from networkStream with TCP. Checks if file exists and sends file.
					string readText = LIB.readTextTCP (networkStream);
					Console.WriteLine (" >> Data from client: " + readText);
					long fileCheck = LIB.check_File_Exists (readText);

					sendFile (readText, fileCheck, networkStream);
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
			Console.WriteLine(" >> exit");
			Console.ReadLine();
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
				Console.WriteLine ("\rSent {1} out of {2} packets", i, packetCount);
			}
			
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