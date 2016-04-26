using System;
using System.IO.Ports;

/// <summary>
/// Link.
/// </summary>
namespace Linklaget
{
	/// <summary>
	/// Link.
	/// </summary>
	public class Link
	{
		/// <summary>
		/// The DELIMITE for slip protocol.
		/// </summary>
		const byte DELIMITER = (byte)'A';
		/// <summary>
		/// The buffer for link.
		/// </summary>
		private byte[] buffer;
		/// <summary>
		/// The serial port.
		/// </summary>
		SerialPort serialPort;

		/// <summary>
		/// Initializes a new instance of the <see cref="link"/> class.
		/// </summary>
		public Link (int BUFSIZE)
		{
			// Create a new SerialPort object with default settings.
			serialPort = new SerialPort("/dev/ttyS1",115200,Parity.None,8,StopBits.One);

			if(!serialPort.IsOpen)
				serialPort.Open();

			buffer = new byte[(BUFSIZE*2)];

			serialPort.ReadTimeout = 200;
			serialPort.DiscardInBuffer ();
			serialPort.DiscardOutBuffer ();
		}

		/// <summary>
		/// Send the specified buf and size.
		/// </summary>
		/// <param name='buf'>
		/// Buffer.
		/// </param>
		/// <param name='size'>
		/// Size.
		/// </param>
		public void send (byte[] buf, int size)
		{
	    	// TO DO Your own code
		}

		/// <summary>
		/// Receive the specified buf and size.
		/// </summary>
		/// <param name='buf'>
		/// Buffer.
		/// </param>
		/// <param name='size'>
		/// Size.
		/// </param>
		public int receive (ref byte[] buf)
		{
	    	// TO DO Your own code
		}

	    byte[] _returnArray;


        public Tuple<byte[], int> Slip(byte[] buf, int size)
	    {
	        _returnArray = new byte[2008];
            _returnArray[0] = (byte)'A';
            int pointerToLastconversion=0, i, returnArrayPointer=1;

            for (i = 0; i < size; i++)
            {
                if (buf[i] == 'A')
                { 
                    Array.Copy(buf,pointerToLastconversion,_returnArray,returnArrayPointer,((i-pointerToLastconversion)-1));
                    returnArrayPointer += ((i - pointerToLastconversion));
                    pointerToLastconversion = i+1;
                    _returnArray[returnArrayPointer] = (byte)'B';
                    _returnArray[returnArrayPointer+1] = (byte)'C';
                    returnArrayPointer =+ 2;
                }

            }
	    }
	}
}
