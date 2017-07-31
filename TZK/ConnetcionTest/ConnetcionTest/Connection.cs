using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConnetcionTest
{
    class Connection
    {
        ///<summary>IP текущего компьютера</summary>
        public IPAddress MyAddr { set; get; }
        //IP компьютера к которому соединяемся
        IPAddress ConnectAddr { set; get; }
        public Socket ConnSocket; //Сокет, который используется соединением

        public Connection()
        {
            foreach (IPAddress ad in Dns.GetHostEntry("").AddressList)
            {
                if ((ad.AddressFamily == IPAddress.Any.AddressFamily) && (ad.ToString() != "192.168.56.1"))
                {
                    MyAddr = ad;
                    //Console.WriteLine("=>Вы доступны по адресу: {0}", ad.ToString());
                }
            }
        }


        //Соединиться с пользователем с заданным IP
        static public void ConnectTo(IPAddress Addr, ref Connection Conn)
        {
            Conn.ConnectAddr = Addr;
           IPEndPoint ipEndPoint = new IPEndPoint(Addr, 11000);
            Conn.ConnSocket = new Socket(Addr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // Соединяем сокет с удаленной точкой
            Console.WriteLine("=<>=");
            Conn.ConnSocket.Connect(ipEndPoint);
            Console.WriteLine("Подключение завершено");
        }

        /// <summary>
        /// Открывает новый сокет, и ждет подключения (как бы "создает игру" и ожидает игроков)
        ///Желательно эту штуку выполнять в отедльном потоке, она просто будет ждать коннекта
        /// </summary>
        /// <param name="Conn"></param>
        public static void CreateConnetion(ref Connection Conn)
        {
            //Создаем "Ожидающий" сокет по 11000 порту
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 11000);
            Socket TempSocket = new Socket(IPAddress.Any.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            TempSocket.Bind(ipEndPoint);
            TempSocket.Listen(3);
            //Ожидаем подключения для передачи его сокету InPutSocket
            Console.WriteLine("=<>=");
            Conn.ConnSocket = TempSocket.Accept();
            Console.WriteLine("К нам подключились");
        }


        /// <summary>
        ///Enum для основных событий. Используются для распознования информации,
        ///Передаваемой сообщением между игроками
        /// </summary>
        public enum GameEvent
        {
            //Я не знаю, в каком виде ты представляещь себе события. 
            //Нажатие клавиши противником, или совершенние конкретное дейсвтие? 
            //Поясни, чего бы ты хотел.
            /// <Kim>
            /// если можно будет послать действие как параметр, то хватит и нажатия клавиши
            /// </Kim>
            DisConnect = 100,
            ChakePosition = 101,
            NewBullet = 102,
            ActionBegin = 103,
            ActionEnd = 104
        }
        /// <summary>
        ///Функции для обробатки поступающих и исходщих сообщений.
        ///Они не доработаны, так как я не знаю в какой вид у нас будут иметь события, и следовательно не знаю как их парсить.
        /// </summary>
        /// <param name="Event"></param>
        /// <param name="EventParam"></param>
        /// <param name="Conn"></param>
        public static void SendEvent(GameEvent Event, string EventParam, ref Connection Conn)
        {
            byte[] msg = Encoding.UTF8.GetBytes(((int)Event).ToString() + EventParam);
            int bytesSent = Conn.ConnSocket.Send(msg);
        }
        /// <Kim>
        /// На самом деле тут карты в твоих руках. Мне важно, чтоб класс корректно работал и справлялся со своими задачами:
        /// 1) Передавать сообщения о событиях разных клиентов;
        /// 2) Иметь возможность сверять координаты;
        /// 3) Дергать программу(через event) в случае сообщения.
        /// +  Зачем статические методы?
        /// </Kim>
        
          public static void GetEvent(ref Connection Conn)///<Kim></Kim>
        {
            string data = null;
            byte[] bytes = new byte[1024];
            int bytesRec = Conn.ConnSocket.Receive(bytes);
            data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

            // Показываем данные на консоли
            Console.WriteLine("Получено событие: {0}", data);
        }
        //Разьединяет соединение.
         public void Disconnect()
        {
            ConnSocket.Shutdown(SocketShutdown.Both);
            ConnSocket.Close();
        }

    }
}
