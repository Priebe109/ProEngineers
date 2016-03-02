using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace tcp
{
	class file_client
	{
		/// <summary>
		/// The Socket.
		/// </summary>
		TcpClient clientSocket = new TcpClient();

		/// <summary>
		/// The PORT.
		/// </summary>
		const int PORT = 9000;
		/// <summary>
		/// The BUFSIZE.
		/// </summary>
		const int BUFSIZE = 1000;

		/// <summary>
		/// Initializes a new instance of the <see cref="file_client"/> class.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments. First ip-adress of the server. Second the filename
		/// </param>
		private file_client (string[] args)
		{
			String fileServerIp;
			String filePath;

			// Client-applikationen skal kunne startes fra en terminal med kommandoen:
			// #./file_client.exe <file_server’s ip-adr.> <[path] + filename>
			if (args.Length != 2) {
				writeInputInterpretError ();
				return;
			}

			fileServerIp = args [0];
			filePath = args [1];

			// Connect to the server using a TCP connection socket, and send a file request.
			clientSocket.Connect (fileServerIp, PORT);
			sendFileRequest (filePath, clientSocket.GetStream ());
		}

		/// <summary>
		/// Sends a request for a specified file.
		/// </summary>
		/// <param name='fileName'>
		/// File name.
		/// </param>
		/// <param name='io'>
		/// Network stream for sending to the server
		/// </param>
		private void sendFileRequest (String filePath, NetworkStream stream)
		{
			// Client’en sender indledningsvis en tekststreng, som er indtastet af operatøren, til serveren.
			// Tekststrengen skal indeholde et filnavn + en eventuel stiangivelse til en fil i serveren.
			byte[] outStream = System.Text.Encoding.ASCII.GetBytes(filePath + "$");
			stream.Write(outStream, 0, outStream.Length);
			stream.Flush();

			// Read answer from server.
			receiveFile(filePath, stream);
		}

		private void writeInputInterpretError()
		{
			// Print an error message to the console.
			Console.WriteLine ("Not able to interpret input. Use this form:");
			Console.WriteLine ("#./file_client.exe <file_server’s ip-adr.> <[path] + filename>");
		}

		/// <summary>
		/// Receives the file.
		/// </summary>
		/// <param name='fileName'>
		/// File name.
		/// </param>
		/// <param name='io'>
		/// Network stream for reading from the server
		/// </param>
		private void receiveFile (String fileName, NetworkStream io)
		{
			// Client’en skal modtage den ønskede fil fejlfrit fra serveren – eller udskrive en fejlmelding hvis filen ikke findes i serveren.
			byte[] inStream = new byte[BUFSIZE];
			io.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
			string returndata = System.Text.Encoding.ASCII.GetString(inStream);
			Console.WriteLine (returndata);
		}

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		public static void Main (string[] args)
		{
			Console.WriteLine ("Client starts...");
			new file_client(args);
		}
	}
}
