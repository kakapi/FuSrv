using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CCEncryptor
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new CryWin().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Cryp().Show();
        }
    }
}
