using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SSE.Client
{
    /// <summary>
    /// Simple implementation of a TCP client.
    /// </summary>
    public class TcpClient
    {
        /// <summary>
        /// Sends a string to a TCP endpoint, waits for an answer and returns it.
        /// </summary>
        /// <param name="ip">The IP address to send the request to.</param>
        /// <param name="port">The target port to send the request to.</param>
        /// <param name="message">The data to be sent as a string.</param>
        /// <returns>The data received as an answer.</returns>
        public string Request(string ip, int port, string message)
        {
            // create socket
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("TCP: Created socket");

            // connect
            var ipe = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                socket.Connect(ipe);
            }
            catch (SocketException e)
            {   // connection failed
                Console.WriteLine("TCP: The connection failed ({0}).", e.Message);
                return "";
            }
            Console.WriteLine("TCP: connection established between {0} and {1}.",
                socket.LocalEndPoint, socket.RemoteEndPoint);

            // send request
            var sendData = Encoding.ASCII.GetBytes(message);
            socket.Send(sendData, sendData.Length, SocketFlags.None);
            Console.WriteLine("TCP: Sent {0} bytes.", sendData.Length);

            // receive everything
            var receiveBuffer = new byte[10000];
            var result = "";
            while (true)
            {
                var receivedBytes = socket.Receive(receiveBuffer, receiveBuffer.Length, SocketFlags.None);
                if (receivedBytes == 0) // connection closed by remote host and
                    break;              // all available data has been received
                result += Encoding.ASCII.GetString(receiveBuffer, 0, receivedBytes);
                Console.WriteLine("TCP: Received {0} bytes.", receivedBytes);
            }

            // shutdown connection and close socket
            Console.WriteLine("TCP: Shutting down connection with {0} in both directions.",
                socket.RemoteEndPoint);
            socket.Shutdown(SocketShutdown.Both);

            return result;
        }

        /// <summary>
        /// Program entry point for testing purposes.
        /// </summary>  
        public static void Main(string[] args)
        {
           // create client
            var client = new TcpClient();

            // send a request to a locally running server program             
            var answer = client.Request("127.0.0.1", 13000, "ask NUMBER_OF_PROCESSORS\n");

            // display answer and wait for user
            Console.WriteLine("Answer: " + answer);
            Console.ReadLine();
        }
    }
}
