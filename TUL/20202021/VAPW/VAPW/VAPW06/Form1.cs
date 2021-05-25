using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VAPW06
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Person
        {
            public string name;

            public override string ToString()
            {
                return $"{name}";
            }
        }

        public class Worker : Person
        {
            public decimal payment;
            public override string ToString()
            {
                return base.ToString() + $", payment: {payment}";
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            listOfPerson.BeginUpdate();
            listOfPerson.Items.Add(person.Checked ? new Person { name = name.Text } : new Worker { name = name.Text, payment = payment.Value });
            listOfPerson.EndUpdate();
        }

        private void prev_Click(object sender, EventArgs e)
        {
            listOfPerson.SetSelected(Math.Max(listOfPerson.SelectedIndex - 1, 0), true);
            Describe();
        }

        private void next_Click(object sender, EventArgs e)
        {
            listOfPerson.SetSelected(Math.Min(listOfPerson.SelectedIndex + 1, listOfPerson.Items.Count - 1), true);
            Describe();
        }

        private void Describe()
        {
            description.Text = listOfPerson.SelectedItem.ToString();
        }
    }
}
