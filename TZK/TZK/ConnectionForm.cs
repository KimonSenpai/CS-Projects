using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZK.Manipulators;

namespace TZK {
    public partial class ConnectionForm : Form {
       

        private ConnectionForm() {
            InitializeComponent();
        }

        public new static  IPAddress ShowInput() {
            ConnectionForm form = new ConnectionForm();
            form.ShowDialog();
            IPAddress address ;
            if (!IPAddress.TryParse(form.textBox1.Text, out address)) throw new Exception("Wrong IP");
            return address;
        }

        public static ConnectionForm ShowCreate(NetManipulator NetMan) {
            ConnectionForm form = new ConnectionForm();
            form.label1.Text = "Your IP:";
            form.textBox1.Enabled = false;
            form.textBox1.Text = NetMan.GetIP().ToString();
            form.button1.Visible = false;
            Action act = new Action(NetMan.CreateConnection);
            IAsyncResult a = act.BeginInvoke(null,null);
            form.Show();
            act.EndInvoke(a);
            form.Hide();

            return form;
        }
        private void button1_Click( object sender, EventArgs e ) {
            Hide();
        }
    }
}
