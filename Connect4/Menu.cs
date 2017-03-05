using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        //Help/About button
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 helpForm = new Form2();
            helpForm.ShowDialog();
        }

        //Start Game
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 game = new Form1();
            game.setName1(textBox1.Text);
            game.setName2(textBox2.Text);
            game.ShowDialog();
        }

        //Leaderboard button
        private void button2_Click(object sender, EventArgs e)
        {
            Leaderboard leaderboard = new Leaderboard();
            leaderboard.ShowDialog();
        }
    }
}
