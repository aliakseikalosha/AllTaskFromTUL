using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VAPW08
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public Person NewUser { get; private set; }
        private void Create_Click(object sender, EventArgs e)
        {
            NewUser = new Person { name = name.Text, email = email.Text };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
