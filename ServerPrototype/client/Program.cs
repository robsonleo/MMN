using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch time=new Stopwatch();
            time.Start();
            ConsoleKeyInfo key;
            int count = 0;
            while (count < 1000)
            {
                try
                {
                    string test_string = new string('t', 10);

                    Connect("127.0.0.1", "message" + (++count) + test_string+'\n');
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            time.Stop();
            Console.WriteLine(time.ElapsedMilliseconds);
            Console.ReadKey();
        }


        static void Connect(string server, string msg)
        {
                int port = 9595;
                TcpClient client=new TcpClient(server, port);
                
            try
            {
                NetworkStream stream = client.GetStream();
                
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
                //stream.BeginWrite(data, 0, data.Length, new AsyncCallback(ar => { }), stream);
                    //stream.WriteByte(data[0]);
                stream.Write(data, 0, data.Length);
                stream.Flush();

                data=new byte[1100];

                int countBytes = stream.Read(data, 0, data.Length);
                
                string dataServ = System.Text.Encoding.ASCII.GetString(data, 0, countBytes);
                Console.WriteLine("Response from server: " + dataServ);
                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {client.Close();}

        }
    }
}
