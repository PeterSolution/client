using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace serwerr
{
    public partial class Form1 : Form
    {
        public string ip;
        public static Form1 ins;
        public int liczba;
        public Form1()
        {
            InitializeComponent();
            ins=this;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            liczba = 1;
            ip = textBox2.Text;
            przenoszenie prze=new przenoszenie();
            prze.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            liczba = 2;
            ip = textBox2.Text;
            przenoszenie prze = new przenoszenie();
            prze.Show();
        }
    }
}
