using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;



namespace ConnetcionTest
{
    class Program
    {
        public delegate void ConnectTo(IPAddress Addr, ref Connection K);
        public delegate void MakeConnect(ref Connection K);
        public delegate void Reciever(ref Connection K);
        static void Main(string[] args)
        {
            Connection K = new Connection();
            MakeConnect CF1 = Connection.CreateConnetion;
            ConnectTo CF2 = Connection.ConnectTo;
            Reciever Rer = GetEvents;
            
            Console.WriteLine("Выбирте:\n 1.Создать поключение\n 2. Подключиться");
            int i = int.Parse(Console.ReadLine());
            if (i == 1)
            {
                Console.WriteLine("Ваш IP: {0}", K.MyAddr.ToString());
                Console.WriteLine("Ожидаем подключения");
                Connection.CreateConnetion(ref K);
            }
            else if (i == 2)
            {
                Console.WriteLine("Тогда скажите по какому IP:");
                IPAddress TmpAddr = IPAddress.Parse(Console.ReadLine());
                Connection.ConnectTo(TmpAddr, ref K);
            }
            else Console.WriteLine("Я не знаю чего вы хотите");

            Rer.BeginInvoke(ref K, null, null);
            string msg;
            while (true)
            {
                msg = Console.ReadLine();
                Connection.SendEvent(Connection.GameEvent.ActionBegin, msg, ref K);
                //Console.WriteLine("*");
                // Thread.Sleep(2000);
            }
            //Console.ReadLine();
        }
        public static void GetEvents(ref Connection K)
        {
            while (true)
            {
                Thread.Sleep(100);
                Connection.GetEvent(ref K);
            }
        }
    }
}
