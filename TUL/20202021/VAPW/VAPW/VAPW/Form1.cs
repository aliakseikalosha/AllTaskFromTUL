using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VAPW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rnd = new Random();
            var list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(rnd.Next(0, 5));
            }
            label1.Text = list.OrderByDescending(c => list.Count(x => x == c)).ToList()[0].ToString();
        }
    }
}
