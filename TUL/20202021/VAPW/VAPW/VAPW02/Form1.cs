using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VAPW02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double a, b, c;
            a = (double)numericUpDown1.Value;
            b = (double)numericUpDown2.Value;
            c = (double)numericUpDown3.Value;

            MessageBox.Show($"It is triangul? : {CheckForTriangular(a, b, c)}");
            MessageBox.Show($"Area is {CalculateArea(a, b, c)}");
        }

        private double CalculateArea(double a, double b, double c)
        {
            return Math.Sqrt((a + (b + c)) * (c - (a - b)) * (c + (a - b)) * (a + (b - c))) / 4;
        }

        private bool CheckForTriangular(double a, double b, double c)
        {

            return a < b + c && b < a + c && c < b + a;
        }
    }
}
