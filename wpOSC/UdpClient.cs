using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace wpOSC
{
    class UdpClient : IDisposable
    {
        private EventHandler<SocketAsyncEventArgs> socketCompletedHandler; 

        public UdpClient()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            socketCompletedHandler = new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
            {
                if (e.SocketError.ToString() != "Success")
                {
                    System.Diagnostics.Debug.WriteLine("socket error: ");
                    System.Diagnostics.Debug.WriteLine(e.SocketError);
                }
            });
        }
        
        
        public void Send(byte[] data, System.Net.IPEndPoint destination)
        {

            if (socket != null)
            {
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.RemoteEndPoint = destination;
                socketEventArg.Completed += socketCompletedHandler;

                // Add the data to be sent into the buffer
                socketEventArg.SetBuffer(data, 0, data.Length);

                // Make an asynchronous Send request over the socket
                socket.SendToAsync(socketEventArg);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("no socket");
            }

        }

        public void Dispose()
        {
            socket.Close();
        }

        private Socket socket { get; set; }

        
    }
}
