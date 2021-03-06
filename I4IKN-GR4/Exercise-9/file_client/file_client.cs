using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace udp
{
	public class UDPClient
	{
		private const int serverPort = 9000;
		private static UdpClient client;

		static void StartClient(string argument)
		{
			// Setup client
			IPAddress address = IPAddress.Parse ("10.0.0.1");
			IPEndPoint endPoint = new IPEndPoint (address, serverPort);
			client = new UdpClient();

			// Send
			byte[] sendBuf = Encoding.ASCII.GetBytes(argument);
			client.Send(sendBuf, sendBuf.Length ,endPoint);
			Console.WriteLine (">> " + argument + " send to server");

			// Receive
			IPEndPoint groupEndPoint = new IPEndPoint(IPAddress.Any, serverPort);
			byte[] receiveBuf = client.Receive (ref groupEndPoint);
			Console.WriteLine (">> Response from server:");
			Console.WriteLine(Encoding.ASCII.GetString(receiveBuf, 0, receiveBuf.Length));
		}

		static void Main(string[] args)
		{
			string argument;
			if (args.Length > 0) {
				argument = args [0];
			} else {
				Console.WriteLine (">> Type L or U to receive statistics from the server...");
				argument = Console.ReadLine ();
			}

			// Start
			StartClient(argument);

			// Done
			Console.ReadKey ();
		}
	}
}