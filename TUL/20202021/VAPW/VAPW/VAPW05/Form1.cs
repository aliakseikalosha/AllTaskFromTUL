using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VAPW05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public abstract class UFigure
        {
            protected double length;
            public abstract double Area { get; }
            public abstract double Volume();
            public UFigure(double length)
            {
                this.length = length;
            }
        }

        public class Cube : UFigure
        {
            public override double Area => 6 * length * length;
            public override double Volume()
            {
                return length * length * length;
            }
            public Cube(double lenght) : base(lenght) { }
        }

        public class Sphere : UFigure
        {
            public override double Area => 4 * Math.PI * length * length;
            public override double Volume() => 4 / 3 * Math.PI * length * length * length;
            public Sphere(double lenght) : base(lenght) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cube = new Cube(1);
            var sphere = new Sphere(0.5);
            MessageBox.Show($"cube V :{cube.Volume()} S: {cube.Area}\nsphere V:{sphere.Volume()} S: {sphere.Area}");
        }
    }
}
