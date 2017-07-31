using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//-------------------------------
using System.Text.RegularExpressions;

namespace Regexp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Match.Checked)
            {
                Res.Text = Regex.IsMatch(Str.Text, "^" + Reg.Text + "$", ((iCase.Checked)?(RegexOptions.IgnoreCase):(RegexOptions.None))).ToString();
            }
            if (Search.Checked)
            {
                Res.Text = Regex.Match(Str.Text, Reg.Text, ((iCase.Checked) ? (RegexOptions.IgnoreCase) : (RegexOptions.None))).Success.ToString();
            }
            if (Replace.Checked)
            {
               
                Res.Text = Regex.Replace(Str.Text, Reg.Text, What.Text, ((iCase.Checked) ? (RegexOptions.IgnoreCase) : (RegexOptions.None)));
            }
        }

        private void Replace_CheckedChanged(object sender, EventArgs e)
        {
            if (Replace.Checked)
                What.Visible = true;
            else
                What.Visible = false; 
        }
    }
}
