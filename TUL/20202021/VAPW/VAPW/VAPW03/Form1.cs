using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VAPW03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private class Person
        {
            public int HP;
            public int Attack;
            public string PersonName;

            public Person(Random rnd, byte hpDice, byte attackDice, string name)
            {
                HP = 12;
                for (int i = 0; i < hpDice; i++)
                {
                    HP += rnd.Next(1, 7);
                }
                Attack = 6;
                for (int i = 0; i < attackDice; i++)
                {
                    Attack += rnd.Next(1, 7);
                }
                PersonName = name;
            }

            public Person(int hP, int attack, string name)
            {
                HP = hP;
                Attack = attack;
                PersonName = name;
            }

            public override string ToString()
            {
                return $"{PersonName}\nHP:\t{HP}\nAttack\t{Attack}";
            }

            public int ToHit(Random rnd)
            {
                return rnd.Next(1, 7) + rnd.Next(1, 7) + Attack;
            }
        }
        private Random rnd = new Random();
        private Person hero;
        private Person enemy;
        private void Create_Click(object sender, EventArgs e)
        {
            hero = new Person(rnd, 2, 1, textBox1.Text);
            enemy = new Person(14, 8, "The Shadow");
            ShowPerson(label1, hero);
            ShowPerson(label2, enemy);
            Fight.Enabled = true;
        }

        private void Fight_Click(object sender, EventArgs e)
        {
            if (hero.ToHit(rnd) > enemy.ToHit(rnd))
            {
                hero.HP -= 2;
            }
            else
            {
                enemy.HP -= 2;
            }
            ShowPerson(label1, hero);
            ShowPerson(label2, enemy);
            if (enemy.HP <= 0 || hero.HP <= 0)
            {
                Fight.Enabled = false;
            }
        }

        private void ShowPerson(Label label, Person person)
        {
            label.Text = person.ToString();
        }
    }
}
