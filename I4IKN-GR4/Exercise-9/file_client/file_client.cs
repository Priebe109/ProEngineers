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

		static void Main(string[] args) {

			string argument;
			if (args.Length > 0) {
				argument = args [0];
			} else {
				Console.WriteLine (">> Type in a string to send to server...");
				argument = Console.ReadLine ();
			}

			// More UDP
			Socket socket = new Socket (AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			IPAddress broadcast = IPAddress.Parse ("10.0.0.1");

			// UDP
			byte[] sendBuf = Encoding.ASCII.GetBytes(argument);
			IPEndPoint endPoint = new IPEndPoint (broadcast, serverPort);
			socket.SendTo (sendBuf, endPoint);
			Console.WriteLine (">> " + argument + " send to server");
			Console.ReadKey ();
		}
	}
}