using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeProject
{
    public partial class Form1 : Form
    {
        List<Button> buttons;

        public Form1()
        {
            InitializeComponent();
            buttons = new List<Button>() { button0, button1, button2, button3, button4 , button5,
            button6,button7,button8};
            SetDisable();
        }
        public void SetDisable()
        {
            for (int i = 0; i < 9; i++)
            {
                buttons[i].Enabled = false;
            }
        }
        public void SetEnable()
        {
            for (int i = 0; i < 9; i++)
            {
                buttons[i].Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button0_Click(object sender, EventArgs e)
        {
            ClickPlayer(button0);
            ClickAI(0,0);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClickPlayer(button1);
            ClickAI(0, 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClickPlayer(button2);
            ClickAI(0,2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClickPlayer(button3);
            ClickAI(1,0);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClickPlayer(button4);
            ClickAI(1, 1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClickPlayer(button5);
            ClickAI(1, 2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClickPlayer(button6);
            ClickAI(2, 0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ClickPlayer(button7);
            ClickAI(2, 1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ClickPlayer(button8);
            ClickAI(2, 2);
        }
        private void ClickAI(int x, int y,bool aiFirst=false)
        {
            int winner = MiniMax.FinishGame();
            if (winner == -1)
            {
                var aimove = MiniMax.AIMove(x, y, aiFirst);
                int index = aimove[0] * 3 + aimove[1];
                for (int i = 0; i < 9; i++)
                {
                    if (buttons[i].Name == $"button{index}")
                    {
                        buttons[i].Text = "X";
                        buttons[i].Enabled = false;
                    }
                }
                int winnerAfterAI = MiniMax.FinishGame();
                if(winnerAfterAI != -1)
                {
                    FinishGame(winnerAfterAI);
                }
            }
            else
            {
                FinishGame(winner);
            }
            
        }
        private void ClickPlayer(Button button)
        {
            button.Text = "0";
            button.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SetEnable();
            button9.Enabled = false;
            button13.Enabled = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SetEnable();
            ClickAI(-1,-1,true);
            button9.Enabled = false;
            button13.Enabled = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                buttons[i].Text = "";
            }
            MiniMax.ReInitializeBoard();
            button9.Enabled = true;
            button13.Enabled = true;
        }
        private void FinishGame(int score)
        {
            if (score == 0)
                MessageBox.Show(this, "TIE", "Tic Tac Toe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (score == 10)
                MessageBox.Show(this,"AI WON","Tic Tac Toe",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
                MessageBox.Show(this, "PLAYER WON", "Tic Tac Toe", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
