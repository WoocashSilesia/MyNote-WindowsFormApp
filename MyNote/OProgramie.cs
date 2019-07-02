using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNote
{
    public partial class OProgramie : Form
    {
        OknoGlowne g;
        public OProgramie()
        {
            InitializeComponent();
        }

        public OProgramie(OknoGlowne g) : this()
        {
            this.g = g;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            g.Visible = true;
        }

        private void OProgramie_FormClosing(object sender, FormClosingEventArgs e)
        {
            g.Visible = true;
        }
    }
}
