using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace chatTest1_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MyContinuousClient myContinuousClient = new MyContinuousClient();
            myContinuousClient.Run();
        }
    }

    class MyContinuousClient
    {
        TcpClient client = null;
        public void Run()
        {

            while(true)
            {
                Console.WriteLine("==============Client==============");
                Console.WriteLine("1. 서버 연결");
                Console.WriteLine("2. Message 보내기");
                Console.WriteLine("==================================");

                string key = Console.ReadLine();
                int order = 0;

                if (int.TryParse(key, out order))
                {
                    switch (order)
                    {
                        case 1:
                            if (client != null)
                            {
                                Console.WriteLine("Already Connected");
                                Console.ReadKey();
                            }
                            else
                            {
                                Connect();
                            }
                            break;
                        case 2:
                            if (client == null)
                            {
                                Console.WriteLine("Connect with server first");
                                Console.ReadKey();
                            }
                            else
                            {
                                SendMessage();
                            }
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input");
                    Console.ReadKey();
                }
                Console.Clear();
            }
            
        }
        private void SendMessage()
        {
            Console.WriteLine("Input the message you want to send");
            string message = Console.ReadLine();
            byte[] byteData = new byte[message.Length];
            byteData = Encoding.Default.GetBytes(message);

            client.GetStream().Write(byteData, 0, byteData.Length);
            Console.WriteLine("Successfully sent");
            Console.ReadKey();
        }

        private void Connect()
        {
            client = new TcpClient();
            client.Connect("127.0.0.2", 9999);
            Console.WriteLine("Server connected, now input message");
            Console.ReadKey();
        }
    }    
}
