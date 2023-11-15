using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    internal class Client
    {
        public static void StartClient()
        {
            int port = 11000;
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

            try
            {
                Socket senderSocket = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
                senderSocket.Connect(ipEndPoint);
                Console.WriteLine("Client is connected");

                while(true)
                {
                    string msgFromClinet = null;
                    Console.WriteLine("Enter a message for the server: ");
                    msgFromClinet =  Console.ReadLine();

                    
                    
                    senderSocket.Send(Encoding.ASCII.GetBytes(msgFromClinet), 0 , msgFromClinet.Length, SocketFlags.None);


                    
                    byte[] msgFromServer = new byte[1024];
                    int size = senderSocket.Receive(msgFromServer);
                    string data = Encoding.ASCII.GetString(msgFromServer, 0 , size);
                    Console.WriteLine(data);


                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

