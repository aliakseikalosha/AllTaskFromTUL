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
    public partial class Form1 : Form
    {
        private List<Thing> storage = new List<Thing> {
            new Thing {
                name = "a",
                count = 100,
                price = 0.1,
            }, new Thing {
                name = "b",
                count = 500,
                price = 0.15,
            }, new Thing {
                name = "c",
                count = 5,
                price = 0.1,
            },
            new Thing {
                name = "vine",
                count = 10,
                price = 112.5,
            },
            new Thing {
                name = "glass",
                count = 50,
                price = 2.5,
            },
        };
        private List<Person> users = new List<Person>();
        public Form1()
        {
            InitializeComponent();
            SetItems(listOfStorage, storage);
        }

        private void SetItems<T>(ListBox list, List<T> data)
        {
            list.BeginUpdate();
            list.Items.Clear();
            data.ForEach(c => list.Items.Add(c));
            list.EndUpdate();
        }

        private void AddItem<T>(ListBox list, T data)
        {
            list.BeginUpdate();
            list.Items.Add(data);
            list.EndUpdate();
        }
        private void storage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void plus_Click(object sender, EventArgs e)
        {
            if (UserSelected && ThingSelected)
            {
                var user = users[listOfUsers.SelectedIndex];
                var thing = storage[listOfStorage.SelectedIndex];
                thing.count--;
                user.Add(new Thing { name = thing.name, price = thing.price, count = 1 });
                SetItems(listOfStorage, storage);
                SetItems(order, user.things);
            }
        }

        private void minus_Click(object sender, EventArgs e)
        {
            if (UserSelected && ThingSelected)
            {
                var user = users[listOfUsers.SelectedIndex];
                var thing = storage[listOfStorage.SelectedIndex];
                thing.count++;
                user.Remove(new Thing { name = thing.name, price = thing.price, count = 1 });
                SetItems(listOfStorage, storage);
                SetItems(order, user.things);
            }
        }

        private void addUser_Click(object sender, EventArgs e)
        {
            using (var f = new Form2())
            {
                var res = f.ShowDialog();
                if (res == DialogResult.OK)
                {
                    users.Add(f.NewUser);
                    AddItem(listOfUsers, f.NewUser);
                }
            }

        }

        private bool UserSelected => listOfUsers.SelectedIndex >= 0;
        private bool ThingSelected => listOfStorage.SelectedIndex >= 0;

        private void printTotal_Click(object sender, EventArgs e)
        {
            if (UserSelected)
            {
                var index = listOfUsers.SelectedIndex;
                var user = users[index];
                total.Text = $"{(user.things.Count>0?user.things.Aggregate("", (a, b) => a + b.ToString()):"Nothing")}\n-------\nTotal \t{user.TotalPrice}";
            }
        }

        private void listOfUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var user = users[listOfUsers.SelectedIndex];
            SetItems(order, user.things);
        }
    }
}
