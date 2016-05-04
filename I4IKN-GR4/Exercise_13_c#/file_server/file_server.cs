using System;
using System.IO;
using System.Text;
using Transportlaget;
using Library;

namespace Application
{
	class file_server
	{
		/// <summary>
		/// The BUFSIZE
		/// </summary>
		private const int BUFSIZE = 1000;

		/// <summary>
		/// Initializes a new instance of the <see cref="file_server"/> class.
		/// </summary>
		private file_server ()
        {
            byte[] buf = new byte[BUFSIZE];
            Transport transport = new Transport(BUFSIZE);
            Console.WriteLine(" >> Server Started");

            while (true)
            {
                
                try
                {
                    // Receive bytes from client and convert to string.
                    transport.receive(ref buf);
					Console.WriteLine(" >> Server waiting for a client");

                    //Reads text from networkStream with TCP. Checks if file exists and sends file.
                    var fileReq = System.Text.Encoding.Default.GetString(buf);

                    Console.WriteLine(" >> Data from client: " + fileReq);
                    long fileCheck = LIB.check_File_Exists(fileReq);
					var answerBuf = Encoding.ASCII.GetBytes("NOT FOUND");
                    if (fileCheck == 0)
                    {
                        Console.WriteLine(" >> File not found");
                        //Write error message to client
						transport.send(answerBuf, answerBuf.Length);
                    }
                    else
                    {
						answerBuf = Encoding.ASCII.GetBytes("OK");
						transport.send(answerBuf, answerBuf.Length);
						sendFile(fileReq, fileCheck, transport);

						Console.WriteLine(" >> File send to client");
                    }
                    
                    Console.WriteLine(" >> Closed client connection");
                    Console.WriteLine(" >> Server waiting for a client");
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Sends the file.
        /// </summary>
        /// <param name='fileName'>
        /// File name.
        /// </param>
        /// <param name='fileSize'>
        /// File size.
        /// </param>
        /// <param name='tl'>
        /// Tl.
        /// </param>
        private void sendFile(String fileName, long fileSize, Transport transport)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            int fileLength = (int)fileStream.Length;
            int packetLength;
            //Calculate total packet amount 
            int packetCount = (int)Math.Ceiling((double)fileLength / (double)BUFSIZE);

            for (int i = 0; i < packetCount; i++)
            {
                //Set current packetLength
                if (fileLength > BUFSIZE)
                {
                    packetLength = BUFSIZE;
                    fileLength -= packetLength;
                }
                else {
                    packetLength = fileLength;
                }
                //Send packet
                var outBuffer = new byte[packetLength];
                fileStream.Read(outBuffer, 0, packetLength);
                transport.send(outBuffer, packetLength);
                Console.Write("\rSent {0} out of {1} packets", i + 1, packetCount);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name='args'>
        /// The command-line arguments.
        /// </param>
        public static void Main (string[] args)
		{
			new file_server();
		}
	}
}