using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using Timer = System.Timers.Timer;

namespace TZK.Manipulators {
    /// <summary>
    ///Enum для основных событий. Используются для распознования информации,
    ///Передаваемой сообщением между игроками
    /// </summary>
    public enum GameEvent {
        DisConnect = 100,
        CheckPosition = 101,
        Hit = 102,
        ActionBegin = 103,
        ActionEnd = 104,
        Pause = 105,
        Start = 106
    }
    /// <summary>
    /// Еnum для движений
    /// </summary>
    public enum ActionType {
        ForWard,
        BackWard,
        Left,
        Right,
        RotateBarrelLeft,
        RotateBarrelRight,
        GunShot
    }
    /// <summary>
    /// 
    /// </summary>
    public class Manipulator {
        protected const int Interval = 30;

        protected readonly Tank.Tank Tank;
        protected Timer _timer;

        private int _buttonCounter;
        private bool _upPressed,
            _downPressed,
            _leftPressed,
            _rightPressed,
            _leftRotPressed,
            _rightRotPressed;

        protected Manipulator(Tank.Tank tank) {
            Tank = tank;
            _timer = new Timer (  Interval );
            _timer.Elapsed += Tick;
        }


        private void Tick( object sender, EventArgs e ) {
            if (_buttonCounter == 0) {
                _timer.Stop();
                return;
            }
            if (_upPressed) {
                Tank.Center = new PointF( Tank.Center.X + Tank.Velocity * (float)Math.Cos( -Tank.Angle * Math.PI / 180 ) * (float)_timer.Interval / 1000, Tank.Center.Y + Tank.Velocity * (float)Math.Sin( -Tank.Angle * Math.PI / 180 ) * (float)_timer.Interval / 1000 );
            }
            if (_downPressed) {
                Tank.Center = new PointF( Tank.Center.X - Tank.Velocity * (float)Math.Cos( -Tank.Angle * Math.PI / 180 ) * (float)_timer.Interval / 1000, Tank.Center.Y - Tank.Velocity * (float)Math.Sin( -Tank.Angle * Math.PI / 180 ) * (float)_timer.Interval / 1000 );
            }
            if (_leftPressed) {
                Tank.Angle += Tank.TankRotSp * (float)_timer.Interval / 1000;
            }
            if (_rightPressed) {
                Tank.Angle -= Tank.TankRotSp * (float)_timer.Interval / 1000;
            }
            if (_leftRotPressed) {
                Tank.BarrelAngle += Tank.BarrelRotSp * (float)_timer.Interval / 1000;
            }
            if (_rightRotPressed) {
                Tank.BarrelAngle -= Tank.BarrelRotSp * (float)_timer.Interval / 1000;
            }
            Tank.UpdateModel();
        }

        protected bool StartAction(ActionType? action) {
            if (action == null) return false;
            switch (action) {
                case ActionType.ForWard:
                    if (_upPressed) return false;
                    _upPressed = true;
                    _buttonCounter += 1;
                    break;
                case ActionType.BackWard:
                    if (_downPressed) return false;
                    _downPressed = true;
                    _buttonCounter += 1;
                    break;
                case ActionType.Left:
                    if (_leftPressed) return false;
                    _leftPressed = true;
                    _buttonCounter += 1;
                    break;
                case ActionType.Right:
                    if (_rightPressed) return false;
                    _rightPressed = true;
                    _buttonCounter += 1;
                    break;
                case ActionType.RotateBarrelLeft:
                    if (_leftRotPressed) return false;
                    _leftRotPressed = true;
                    _buttonCounter += 1;
                    break;
                case ActionType.RotateBarrelRight:
                    if (_rightRotPressed) return false;
                    _rightRotPressed = true;
                    _buttonCounter += 1;
                    break;
                case ActionType.GunShot:
                    throw new Exception();
                    break;
            }
            if (_buttonCounter != 0)
                _timer.Start();
            return true;
        }

        protected void FinishAction( ActionType? action ) {
            if (action == null) return;
            switch (action) {
                case ActionType.ForWard:
                    _upPressed = false;
                    _buttonCounter -= 1;
                    break;
                case ActionType.BackWard:
                    _downPressed = false;
                    _buttonCounter -= 1;
                    break;
                case ActionType.Left:
                    _leftPressed = false;
                    _buttonCounter -= 1;
                    break;
                case ActionType.Right:
                    _rightPressed = false;
                    _buttonCounter -= 1;
                    break;
                case ActionType.RotateBarrelLeft:
                    _leftRotPressed = false;
                    _buttonCounter -= 1;
                    break;
                case ActionType.RotateBarrelRight:
                    _rightRotPressed = false;
                    _buttonCounter -= 1;
                    break;
                case ActionType.GunShot:
                    throw new Exception();
                    break;
            }
            if (_buttonCounter == 0)
                _timer.Stop();
        }
    }
}