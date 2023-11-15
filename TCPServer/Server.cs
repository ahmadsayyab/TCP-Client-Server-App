using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPServer
{
    internal class Server
    {
        

       public static void StartServer()
        {
            int port = 11000;
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

            try
            {
                Socket listnerSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listnerSocket.Bind(ipEndPoint);

                listnerSocket.Listen(10);

                Console.WriteLine("Waiting for connection...");
                
                Socket clientSocket = default(Socket);
                int counter = 0;

                
                Server server = new Server();
                while (true)
                {
                    counter++;
                    clientSocket = listnerSocket.Accept();
                    Console.WriteLine($"{counter} clients are connected");

                    Thread userThread = new Thread(new ThreadStart(() => server.User(clientSocket)));
                    userThread.Start();
                }



            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void User(Socket client)
        {
            while(true)
            {
                byte[] msg = new byte[1024];
                int size = client.Receive(msg);
                string data = Encoding.ASCII.GetString(msg, 0, size);
                Console.WriteLine($"Data received from client =>  {data}");
                client.Send(msg, 0, size, SocketFlags.None);
            }
            
        }
    }
}
