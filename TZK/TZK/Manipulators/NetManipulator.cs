
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace TZK.Manipulators {
    public class NetManipulator:Manipulator {
        #region Fields

        private const double CheckInterval = 5000;

        private Connection _connection;
        private Thread thread;

        private Timer _checkTimer;
        #endregion



        public NetManipulator( Tank.Tank tank):base(tank) {
            
            _connection = new Connection();

            _connection.EHandleEv += ReceiveEvent;

            _checkTimer = new Timer(CheckInterval);

            _checkTimer.Elapsed += CheckElapsed;
        }

        #region ConnectionWork
        public void ConnectTo(IPAddress ip) {
            _connection.ConnectTo(ip);
            _connection.isListen = true;
            (thread = new Thread( _connection.Listen )).Start();
            
        }

        public IPAddress GetIP() {
            return _connection.MyAddr;

        }

        public void CreateConnection() {
            _connection.CreateConnetion();
            _connection.isListen = true;
            (thread = new Thread( _connection.Listen )).Start();
        }
        #endregion

        #region Events
        private void CheckElapsed(object sender, EventArgs e) {
            Thread thread = new Thread(SendCheckInfo);
            thread.Start();
        }


        private void ReceiveEvent(object sender, Connection.Event e) {
           
            switch (e.Type) {
                case GameEvent.ActionBegin:
                    StartAction( (ActionType?)int.Parse( e.param[0] ) );
                    break;

                case GameEvent.ActionEnd:
                    FinishAction( (ActionType?)int.Parse( e.param[0] ) );
                    break;

                case GameEvent.CheckPosition:
                    Tank.Center = new PointF(float.Parse(e.param[0]),float.Parse(e.param[1]));
                    Tank.Angle = float.Parse(e.param[2]);
                    Tank.BarrelAngle = float.Parse(e.param[3]);
                    break;
                case GameEvent.Hit:

                    break;
                case GameEvent.Pause:

                    break;
                case GameEvent.Start:

                    break;
                case GameEvent.DisConnect:


                    break;
            }
           
        }
        #endregion

        public void SendEvent(object sender, Connection.Event e) {
            _connection.SendEvent(e);
        }
        private void SendCheckInfo() {
            _connection.SendEvent(
                GameEvent.CheckPosition,

                Tank.Center.X.ToString(),
                Tank.Center.Y.ToString(),
                Tank.Angle.ToString(),
                Tank.BarrelAngle.ToString());
        }

        public void StopListen() {
            _connection.isListen = false;
        }

        ~NetManipulator() {
            StopListen();
            _connection.Disconnect();
        }
    }
}