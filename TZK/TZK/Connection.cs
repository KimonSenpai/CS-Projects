using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using TZK.Manipulators;

/// <Kim>
/// На самом деле тут карты в твоих руках. Мне важно, чтоб класс корректно работал и справлялся со своими задачами:
/// 1) Передавать сообщения о событиях разных клиентов;+
/// 2) Иметь возможность сверять координаты;+
/// 3) Дергать программу(через event) в случае сообщения. +
/// 4) Зачем статические методы?+
/// </Kim>
/// <Vladimir> Пострался учесть все пожелания 
/// Посмотри в функции SentEvent мне кажетля лучше будет использовать stringbuilder
/// но помойму это будет геморно писать. Скажи стоит ли пытаться?
/// </Vladimir>

namespace TZK
{
    public class Connection
    {
        ///<summary>IP текущего компьютера</summary>
        public IPAddress MyAddr { get; }
        ///<summary>IP компьютера к которому соединяемся</summary>
        IPAddress ConnectAddr { set; get; }
        public Socket ConnSocket; //Сокет, который используется соединением
        public Connection()
        {
            foreach (IPAddress ad in Dns.GetHostEntry("").AddressList)
            {
                if ((ad.AddressFamily == IPAddress.Any.AddressFamily) && (ad.ToString() != "192.168.56.1"))
                {
                    MyAddr = ad;
                }
            }
        }

        //Соединиться с пользователем с заданным IP
        public void ConnectTo(IPAddress Addr)
        {
            this.ConnectAddr = Addr;
            IPEndPoint ipEndPoint = new IPEndPoint(Addr, 11000);
            this.ConnSocket = new Socket(Addr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // Соединяем сокет с удаленной точкой
            //Console.WriteLine("=<>=");
            this.ConnSocket.Connect(ipEndPoint);
            //Console.WriteLine("Подключение завершено");
        }

        /// <summary>
        /// Открывает новый сокет, и ждет подключения (как бы "создает игру" и ожидает игроков)
        ///Желательно эту штуку выполнять в отедльном потоке, она просто будет ждать коннекта
        /// </summary>
        public void CreateConnetion()
        {
            //Создаем "Ожидающий" сокет по 11000 порту
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 11000);
            Socket TempSocket = new Socket(IPAddress.Any.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            TempSocket.Bind(ipEndPoint);
            TempSocket.Listen(1);
            //Ожидаем подключения для передачи его сокету InPutSocket
           // Console.WriteLine("=<>=");
            this.ConnSocket = TempSocket.Accept();
            //Console.WriteLine("К нам подключились, теперь ждите сообщенек");
        }

        

        /// <summary>
        /// Класс создан для того чтобы структурировать понятие Event
        /// таким образом Event состоит из типа, и присущих типу параметров
        /// </summary>
        public class Event
        {
            public GameEvent Type;
            public string[] param;
            public Event() { }
            public Event(GameEvent Type, params string [] values)
            {
                this.Type = Type;
                param = values;  
            }
            /// <summary>
            /// Печать информации содержащейся в Event
            /// </summary>
            public void Print()
            {
                Console.WriteLine("Тип Ивента:{0}", Type.ToString());
                for(int i = 0; i < param.Length; i++)
                Console.WriteLine("Параметр {0}: {1}", i, param[i]);
            }
        }


        /// <summary>
        ///Функции для обробатки поступающих и исходщих сообщений.
        ///Аргументы не должны содержать спец.символов и признаки конца строки.
        /// 
        /// Посылает Event
        /// <param name="Type"></param>
        /// <param name="values"></param>
        /// </summary>
        public void SendEvent(GameEvent Type, params string[] values)
        {
            Event TmpEvent = new Event(Type, values);
            this.SendEvent(TmpEvent);
        }
        
        public void SendEvent(Event Eve)
        {
            String strmsg = Eve.param.Length.ToString() + '\n';
            for(int i= 0; i < Eve.param.Length; i++)
            {
                strmsg += Eve.param[i] + '\n';
            }

            byte[] msg = Encoding.UTF8.GetBytes(((int)Eve.Type).ToString() + '\n' +  strmsg);
            int bytesSent = this.ConnSocket.Send(msg);
        }
        /// <summary>
        /// Читает пришедшее из сокета сообщение и парсит его, считая что оно подано в виде события
        /// </summary>
        /// <returns></returns>
        private Event GetEvent()
        {
            //Считываем данные из сокета
            string data = null;
            byte[] bytes = new byte[1024];
            int bytesRec = this.ConnSocket.Receive(bytes);
            data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

            //Парсим данные
            //Создаем регулярное выржание, для выделения членов из события
            Event Eve = new Event();
            Regex Ptrn = new Regex("\\b(.{1,})\\b");

            //матчим пришедшее сообщение
            Match match = Ptrn.Match(data);
            //Выделяем тип собития
            Eve.Type = (GameEvent) int.Parse(match.Groups[1].Value);
            match = match.NextMatch();
            //Выделяем аргументы
            string[] TmpArr = new string[int.Parse(match.Groups[1].Value)];
            for(int i = 0; i < TmpArr.Length; i++)
            {
                match = match.NextMatch();
                TmpArr[i] = match.Groups[1].Value;
            }
            Eve.param = TmpArr;

            return Eve;   
        }

        
        public bool isListen = true;
        /// <summary>
        ///  Фукнциая которая читает сообщения со входа, пока isListen = true;
        /// </summary>
        public void Listen()
        {
            while (isListen)
            {
                Event Eve;
                Eve = this.GetEvent();
                EHandleEv(this, Eve);
            }
        }
        /// <summary>
        /// Обработчик событий. Класс реализует единственное событие на получение нового Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Eve"></param>
        public delegate void EventHandler(object sender, Event Eve);
        public event EventHandler EHandleEv;

        /// <summary>
        /// Разьединяет соединение.
        /// </summary>
        public void Disconnect()
        {
            if (ConnSocket != null) {
                SendEvent(GameEvent.DisConnect);
                ConnSocket.Shutdown(SocketShutdown.Both);
                ConnSocket.Close();
                ConnSocket = null;
            }
        }

        ~Connection() {
            Disconnect();
        }

    }
}