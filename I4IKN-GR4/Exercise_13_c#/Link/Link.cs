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
		    Tuple<byte[], int> slipData = Slip(buf,size);
		    serialPort.Write(slipData.Item1, 0, slipData.Item2);
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
			serialPort.Read (buffer, 0, buffer.Length);
			var deslippedInfo = Deslip (buffer);
			buf = deslippedInfo.Item1;
			return deslippedInfo.Item2;
		}

		public Tuple<byte[], int> Deslip(byte[] buffer)
		{
			var deslippedBuffer = new byte[buffer.Length / 2];
			var deslippedBufferIndex = 0;
			var delimitersEncountered = 0;

			for (int i = 0; i < buffer.Length && delimitersEncountered < 2; i++) {

				// if delimiter is encountered, register and continue
				if (buffer [i] == DELIMITER)
					delimitersEncountered++;
				
				// if the substitute character is encountered, check the case and continue
				else if (i < (buffer.Length - 1) && buffer [i] == (byte)'B') {
					switch (buffer [i]) {
					default:
					case (byte)'C':
						deslippedBuffer [deslippedBufferIndex++] = (byte)'A';
						i++;
						break;
					case (byte)'D':
						deslippedBuffer [deslippedBufferIndex++] = (byte)'B';
						i++;
						break;
					}

				// else the index represents a regular byte, add it to the deslipped buffer
				} else
					deslippedBuffer [deslippedBufferIndex++] = buffer [i];
			}

			return new Tuple<byte[], int>(deslippedBuffer, deslippedBufferIndex);
		}

	    


        public static Tuple<byte[], int> Slip(byte[] buf, int size)
	    {
	        byte[] returnArray = new byte[2010];
            returnArray[0] = (byte)'A';
            int pointerToLastconversion=0, i, returnArrayPointer=1;

            for (i = 0; i < size; i++)
            {
                if (buf[i] == 'A')
                { 
                    Array.Copy(buf,pointerToLastconversion,returnArray,returnArrayPointer,(i-pointerToLastconversion));
                    returnArrayPointer += ((i - pointerToLastconversion));
                    pointerToLastconversion = i+1;
                    returnArray[returnArrayPointer] = (byte)'B';
                    returnArray[returnArrayPointer+1] = (byte)'C';
                    returnArrayPointer += 2;
                }
                else if (buf[i] == 'B')
                {
                    Array.Copy(buf, pointerToLastconversion, returnArray, returnArrayPointer, (i - pointerToLastconversion));
                    returnArrayPointer += ((i - pointerToLastconversion));
                    pointerToLastconversion = i + 1;
                    returnArray[returnArrayPointer] = (byte)'B';
                    returnArray[returnArrayPointer + 1] = (byte)'D';
                    returnArrayPointer += 2;
                }

            }
            returnArray[returnArrayPointer] = (byte) 'A';
            ++returnArrayPointer;
            return new Tuple<byte[], int>(returnArray, returnArrayPointer);
	    }
	}
}