using System;
using System.Drawing;
using System.Windows.Forms;

namespace TZK.Manipulators {
    public class KeyManipulator:Manipulator {
       

        
        //private Connection connection;

        public KeyManipulator(Tank.Tank tank,  Control control):base(tank) {
            control.KeyDown += ButtonDown;
            control.KeyUp += ButtonUp;
        }

        #region ControlKeys

        public Keys UpKeys { set; get; }
        public Keys DownKeys { set; get; }
        public Keys LeftKeys { set; get; }
        public Keys RightKeys { set; get; }
        public Keys ShootKeys { set; get; }
        public Keys LeftRotKeys { get; set; }
        public Keys RightRotKeys { get; set; }

        #endregion


        private ActionType? ButtonToAction(Keys key) {
            if (key == UpKeys) {
                return ActionType.ForWard;
            }
            if (key == DownKeys) {
                return ActionType.BackWard;
            }
            if (key == LeftKeys) {
                return ActionType.Left;
            }
            if (key == RightKeys) {
                return ActionType.Right;
            }
            if (key == LeftRotKeys) {
                return ActionType.RotateBarrelLeft;
            }
            if (key == RightRotKeys) {
                return ActionType.RotateBarrelRight;
            }
            if (key == ShootKeys) {
                return ActionType.GunShot;
            }
            return null;
        }

        private void ButtonDown(object sender, KeyEventArgs e) {
            
            ActionType? act = ButtonToAction( e.KeyCode );
            if (act == null)
                return;
            if (!StartAction(act)) return;
            if (ButtonReaction == null)
                return;
            Connection.Event Eve = new Connection.Event( GameEvent.ActionBegin, ((int)act).ToString() );
            ButtonReaction( this, Eve );
        }


        private void ButtonUp(object sender, KeyEventArgs e) {
            ActionType? act = ButtonToAction(e.KeyCode);
            if(act == null) return;
            FinishAction(act);
            if (ButtonReaction == null) return;
            Connection.Event Eve = new Connection.Event(GameEvent.ActionEnd, ((int)act).ToString());
            ButtonReaction(this, Eve);
        }

        public event Action<object,Connection.Event> ButtonReaction;
    }
}