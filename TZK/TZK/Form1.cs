using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZK.Manipulators;

namespace TZK {
    public partial class Form1 : Form {
        protected const int TankSize = 180;
        protected Tank.Tank _tank;
        protected Tank.Tank _opponentTank;
        private KeyManipulator KeyMan;
        private NetManipulator NetMan;

        public Form1() {
            InitializeComponent();
            Controls.Add(_tank = new Tank.Tank("Юдашкин", TankSize));
           
           
            KeyMan = new KeyManipulator(_tank, this) {
                UpKeys = Keys.W,
                DownKeys = Keys.S,
                LeftKeys = Keys.A,
                RightKeys = Keys.D,
                RightRotKeys = Keys.E,
                LeftRotKeys = Keys.Q
            };
        }

        private void Form1_KeyDown( object sender, KeyEventArgs e ) {

        }

        private void createGameToolStripMenuItem_Click( object sender, EventArgs e ) {
            Controls.Add( _opponentTank = new Tank.Tank( "Юдашкин", TankSize ) );
            InicializeStartPossition(_tank, _opponentTank);
            NetMan = new NetManipulator(_opponentTank);
            KeyMan.ButtonReaction += NetMan.SendEvent;
            ConnectionForm.ShowCreate(NetMan);
              
        }

        private void joinGameToolStripMenuItem_Click( object sender, EventArgs e ) {
            Controls.Add( _opponentTank = new Tank.Tank( "Юдашкин", TankSize ) );
            InicializeStartPossition(_opponentTank, _tank );
            NetMan = new NetManipulator( _opponentTank );
            KeyMan.ButtonReaction += NetMan.SendEvent;
            NetMan.ConnectTo(ConnectionForm.ShowInput());
            
        }

        private void InicializeStartPossition(Tank.Tank tank1, Tank.Tank tank2) {
            tank2.Angle = 180;
            tank2.BarrelAngle = 0;
            tank2.Center = new Point( (int)(this.Width - tank1._Koef * tank1.Size.Width / 2 - 20), this.Height / 2 );
            tank2.UpdateModel();
            tank1.Angle = 0;
            tank1.BarrelAngle = 0;
            tank1.Center = new Point( (int) (tank1._Koef*tank1.Size.Width/2 + 20) , this.Height / 2 );
            tank1.UpdateModel();
        }
        private void menuStrip1_ItemClicked( object sender, ToolStripItemClickedEventArgs e ) {

        }

        private void Form1_KeyPress( object sender, KeyPressEventArgs e ) {

        }

        private void exitToolStripMenuItem_Click( object sender, EventArgs e ) {
            Close();
        }
    }
}
