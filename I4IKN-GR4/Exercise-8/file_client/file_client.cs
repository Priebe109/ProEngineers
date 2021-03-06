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
			int argsCount = args.Length;

			if (argsCount == 2) {
				// Get main args.
				fileServerIp = args [0];
				filePath = args [1];

			} else if (argsCount == 0) {
				// Ask for args.
				Console.WriteLine(" >> Enter [server ip] followed by enter");
				fileServerIp = Console.ReadLine();
				Console.WriteLine(" >> Enter [file path] followed by enter");
				filePath = Console.ReadLine();

			} else {
				// Error exit.
				writeInputInterpretError ();
				return;
			}

			connectToServer (fileServerIp, filePath);
		}

		/// <summary>
		/// Connects to server and writes a request message. Handles the response from server.
		/// </summary>
		private void connectToServer(string ip, string filePath)
		{
			// Connect to the server using a TCP connection socket.
			try {
				clientSocket.Connect (ip, PORT);
			} catch {
				Console.WriteLine(" >> Could not connect to server");
				return;
			}

			// Send a file request.
			var stream = clientSocket.GetStream();
			LIB.writeTextTCP (stream, filePath);

			// Receive message from server.
			var message = LIB.readTextTCP(stream);

			// Act on message from server.
			switch (message) {
			case "OK":
				// File found on server
				receiveFile (LIB.extractFileName(filePath), stream);
				break;
			case "NOT FOUND":
				Console.WriteLine (" >>  File not found on server");
				return;
			default:
				break;
			}
		}

		/// <summary>
		/// Print an error message to the console.
		/// </summary>
		private void writeInputInterpretError()
		{
			Console.WriteLine (" >> Not able to interpret input. Use this form:");
			Console.WriteLine (" >> #./file_client.exe <file_server’s ip-adr.> <[path] + filename>");
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
			byte[] outBuffer = new byte[BUFSIZE];
			int received;
			FileStream fileStream = new FileStream (fileName, FileMode.Create, FileAccess.ReadWrite);

			// While received > 0.
			while ((received = io.Read(outBuffer, 0, outBuffer.Length)) > 0)
				fileStream.Write (outBuffer, 0, received);

			Console.WriteLine (" >> Recieved file");
		}

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		public static void Main (string[] args)
		{
			Console.WriteLine (" >> Client starts...");
			new file_client(args);
		}
	}
}