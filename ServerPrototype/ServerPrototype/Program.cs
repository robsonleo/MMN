using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerPrototype
{
    class Program
    {
        private static int countConnection = 0;

        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                ThreadPool.SetMaxThreads(100, 100);
                ThreadPool.SetMinThreads(2, 2);

                int port = 9595;
                //IPAddress adress = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(IPAddress.Any, port);

                server.Start();
                Console.WriteLine("Server start...");
                while (true)
                {
                    ThreadPool.QueueUserWorkItem(FuncClient, server.AcceptTcpClient());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                server.Stop();
            }
        }



        static void FuncClient(object client_tcp)
        {
                Byte[] bytes = new byte[1100];
                string data = null;

                TcpClient client = client_tcp as TcpClient;
            try
            {
                IPAddress ipAddress = ((IPEndPoint) client.Client.RemoteEndPoint).Address;
                if (!client.Connected) client.Connect(ipAddress, 9595);
                
                NetworkStream stream = client.GetStream();

                Console.WriteLine("Start processing request... "+(++countConnection));

                int i;
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                    //data = data.ToUpper() + data;

                    Byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);




                    //for (int ii = 0; ii < 100000; ii++)
                    //{
                    //    double temp = 765.333333;
                    //    temp /= ii;
                    //    double ll = temp;
                    //}


                    stream.Write(msg, 0, msg.Length);
                    stream.Flush();
                }
                stream.Close();
                Console.WriteLine("End processing request...\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }

        }

    }
}
