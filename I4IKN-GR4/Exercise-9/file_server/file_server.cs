using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace udp
{
	using System;

	public class UDPListener 
	{
		private const int listenPort = 9000;

		private static void StartListener() 
		{
			UdpClient listener = new UdpClient(listenPort);
			IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

			try 
			{
				while (true) 
				{
					Console.WriteLine(">> Waiting for broadcast");
					byte[] bytes = listener.Receive( ref groupEP);
					string request = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

					Console.WriteLine(">> Received: {1} from: {0}",
						groupEP.ToString(),
						request);

					// Interpret request from client and compose reply
					byte[] data = new byte[1024];
					switch (request)
					{
						case "U":
						case "u":
						{
							// Read the file and store it in 'line'.
							System.IO.StreamReader file = new System.IO.StreamReader("/proc/uptime");
							string line = file.ReadLine ();
							file.Close();

							// Compose reply
							data = Encoding.ASCII.GetBytes((string)("Server uptime: " + line));
							break;
						}
						case "L":
						case "l":
						{
							// Read the file and store it in 'line'.
							System.IO.StreamReader file = new System.IO.StreamReader("/proc/loadavg");
							string line = file.ReadLine ();
							file.Close();

							// Compose reply
							data = Encoding.ASCII.GetBytes((string)("Server load average: " + line));
							break;
						}
						default:
							data = Encoding.ASCII.GetBytes("Invalid request received\nValid requests are either U or L");
							break;
					}
					// Reply to request
					listener.Send(data, data.Length, groupEP);
					Console.WriteLine(">> Sent response to client: ");
					Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length) + "\n");
				}

			} 
			catch (Exception e) 
			{
				Console.WriteLine(e.ToString());
			}
			finally
			{
				listener.Close();
			}
		}

		public static int Main(string[] args) 
		{
			StartListener();

			Console.ReadKey ();

			return 0;
		}
	}
}