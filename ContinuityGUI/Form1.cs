using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuityGUI
{
    public partial class Form1 : Form
    {
        bool glob1 = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!glob1)
            {
                label1.Text = "oh boy like when I was a kid!";
                button1.Text = "don't click anymore";
                glob1 = true;
            }
            else
            {
                MessageBox.Show(this,"I TOLD YOU NOT TO CLICK. BEST WAY OF TELLING PEOPLE TO ACTUALLY DO IT EH?");
                throw new ApplicationException();
            }

        }
        
    }
}
