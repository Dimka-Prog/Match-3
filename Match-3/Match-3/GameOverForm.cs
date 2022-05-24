using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Match_3
{
    public partial class GameOverForm : Form
    {
        public Form1 form = new Form1();
        public StartForm start = new StartForm();
        public GameOverForm()
        {
            InitializeComponent();
        }

        private void buttonGameOver_Click(object sender, EventArgs e)
        {
            Close();
            start.Show();
        }

        private void GameOverForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            start.Show();
        }
    }
}
