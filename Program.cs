using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            // dns : localhost
            // ip : 127.0.0.1  v4
            Console.WriteLine("Init Server.");
            Socket serverSck = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            IPAddress localip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(localip,8070);
            Console.WriteLine("Bind End Point To server.");
            serverSck.Bind(endPoint);
            Console.WriteLine("Server Start Listening.");
            serverSck.Listen(1);

            Console.WriteLine("Waiting For Connection ...");
            Socket AcceptedClientSck = serverSck.Accept();
            Console.WriteLine("Connect To Client.");



            while(true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    Console.WriteLine("Waiting for Recvive Date ...");
                    int byteRecv = AcceptedClientSck.Receive(buffer);
                    Console.Write("--- Recvived date : ");

                    string str = Encoding.UTF8.GetString(buffer, 0, byteRecv);

                    Console.WriteLine(str); // Cleint Message


                    Console.Write("Enter Message (1024 Byte): ");
                    string message = Console.ReadLine();

                    AcceptedClientSck.Send(Encoding.UTF8.GetBytes(message));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    serverSck.Close();
                    AcceptedClientSck.Close();
                    return;
                }

            }

            serverSck.Close();
            AcceptedClientSck.Close();

            Console.ReadLine();

        }
    }
}
