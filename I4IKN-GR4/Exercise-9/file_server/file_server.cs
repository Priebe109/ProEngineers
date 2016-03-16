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
			IPEndPoint groupEP = new IPEndPoint(IPAddress.Any,listenPort);

			try 
			{
				while (true) 
				{
					Console.WriteLine("Waiting for broadcast");
					byte[] bytes = listener.Receive( ref groupEP);

					Console.WriteLine("Received broadcast from {0} :\n {1}\n",
						groupEP.ToString(),
						Encoding.ASCII.GetString(bytes,0,bytes.Length));

					byte[] data = new byte[1024];
					switch (Encoding.ASCII.GetString(bytes,0,bytes.Length))
					{
						case "U": 
						case "u":
							data = Encoding.ASCII.GetBytes("Message received(U), but I don't know the answer");
							listener.Send(data, data.Length, groupEP);
							break;
						default:
							data = Encoding.ASCII.GetBytes("Message received(idk), but I don't know the answer");
							listener.Send(data, data.Length, groupEP);
							break;
					}

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