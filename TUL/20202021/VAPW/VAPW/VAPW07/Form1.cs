using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VAPW07
{
    public partial class Form1 : Form
    {
        private static readonly char[] symbols = "qwertyuiopasdfghjklzxcvbnm".ToCharArray();
        private List<Button> gameButtons = new List<Button>();
        private readonly string hide = "[]";
        private char[] map;
        private Random rnd = new Random();
        private bool firstClick = true;
        private int indexFirstClick = -1;
        private List<int> guessed = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void start_Click(object sender, EventArgs e)
        {
            GenerateField((int)size.Value);
        }

        private void GenerateField(int s)
        {
            firstClick = true;
            guessed.Clear();
            foreach (var button in gameButtons)
            {
                this.Controls.Remove(button);
                button.Click -= Button_Click;
            }
            gameButtons.Clear();
            map = new char[s * s];
            for (int i = 0; i < map.Length / 2; i++)
            {
                void add(char c)
                {
                    var j = rnd.Next(0, map.Length);
                    while (symbols.Contains(map[j]))
                    {
                        j = (j + 1) % map.Length;
                    }
                    map[j] = c;
                };
                add(symbols[i]);
                add(symbols[i]);
            }
            for (int i = 0; i < s * s; i++)
            {
                var x = i % s;
                var y = i / s;
                gameButtons.Add(new Button
                {
                    Location = new Point(x * 35 + 50, y * 35 + 75),
                    Text = hide,
                    Size = new Size(30, 30)
                });
            }
            foreach (var button in gameButtons)
            {
                this.Controls.Add(button);
                button.Click += Button_Click;
            }
        }

        private void HideAll()
        {
            gameButtons.ForEach(c => c.Enabled = true);
            for (int i = 0; i < gameButtons.Count; i++)
            {
                if (guessed.Contains(i))
                {
                    continue;
                }
                Button button = gameButtons[i];
                button.Text = hide;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var index = gameButtons.IndexOf((Button)sender);
            if (firstClick)
            {
                indexFirstClick = index;
            }
            else
            {
                gameButtons[index].Text = map[index].ToString();
                gameButtons[indexFirstClick].Text = map[indexFirstClick].ToString();
                if (map[index] == map[indexFirstClick])
                {
                    guessed.Add(index);
                    guessed.Add(indexFirstClick);
                }
                gameButtons.ForEach(c => c.Enabled = false);
                timer.Start();
            }
            firstClick = !firstClick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            HideAll();
        }
    }
}
