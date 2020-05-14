using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ping_pong
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Timer timer = new Timer();
        int FPS = 60;
        int player1y;
        int player2y;
        int count1 = 0, count2 = 0, count = 0;
        int ballx;
        int bally;
        int ballspeedx = 3;
        int ballspeedy = 3;
        public Form1()
        {
            InitializeComponent();
        }

        void DrawEllipse(int x, int y, int w, int h, SolidBrush Color) {
            graphics.FillEllipse(Color, new Rectangle(x, y, w, h));
        }
        void DrawRectangle(int x, int y, int w, int h, SolidBrush Color)
        {
            graphics.FillRectangle(Color, new Rectangle(x, y, w, h));
        }
        void UpdateBall() {
            ballx += ballspeedx;
            bally += ballspeedy;
            if (bally < 20 || bally + 40 > this.Height) {
                ballspeedy = -ballspeedy;
            }
            if (ballx < 20+ballspeedx && (bally + ballspeedy > player1y && bally + ballspeedy < player1y + 130))
            {
                ballspeedy = -ballspeedy;
            }
            if (ballx+ballspeedx < 20 && (bally + ballspeedy > player2y && bally + ballspeedy < player2y + 130))
            {
                ballspeedy = -ballspeedy;
            }
            if (IsCollided()) {
                ballspeedx = -ballspeedx;
            }
            if (ballx < 0) {
                count2++;
                label3.Text = Convert.ToString(count2);
                if (count2 < 3)
                {
                    timer.Tick -= new EventHandler(TimerCallBack);
                    timer.Tick += new EventHandler(counter);
                    timer.Interval = 1000;
                    ballx = this.Width / 2 + 10;
                    bally = this.Height / 2 + 10;
                    label2.Visible = true;
                }
                else {
                    label1.Text = "Выйграл Игрок №2";
                    label1.Visible = true;
                    timer.Stop();
                }

            }
            if (ballx > this.Width)
            {
                count1++;
                label4.Text = Convert.ToString(count1);
                if (count1 < 3)
                {
                    timer.Tick -= new EventHandler(TimerCallBack);
                    timer.Tick += new EventHandler(counter);
                    timer.Interval = 1000;
                    ballx = this.Width / 2 + 10;
                    bally = this.Height / 2 + 10;
                    count = 0;
                    label2.Visible = true;
                }
                else
                {
                    label1.Text = "Выйграл Игрок №1";
                    label1.Visible = true;
                    timer.Stop();
                }

            }

        }
        bool IsCollided()
        {
            if ((ballx < 20 && bally > player1y && bally < player1y + 130)||(ballx >(this.Width - 60) && bally > player2y && bally < player2y + 130))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void TimerCallBack(object sender, EventArgs e)
        {
            this.Refresh();
            DrawRectangle(this.Width - 40, player2y, 20, 120, new SolidBrush(Color.Red));
            DrawRectangle(0, player1y, 20, 130, new SolidBrush(Color.Blue));
            DrawEllipse(ballx, bally, 20, 20, new SolidBrush(Color.Black));
            UpdateBall();
         


            return;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            graphics = CreateGraphics();
        }
        private void Form1Paint(object sender, EventArgs e)
        {
            DrawRectangle(this.Width - 40, player2y, 20, 120, new SolidBrush(Color.Red));
            DrawRectangle(0, player1y, 20, 120, new SolidBrush(Color.Blue));
            DrawEllipse(ballx, bally, 20, 20, new SolidBrush(Color.Black));

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += new EventHandler(counter);
            ballx = this.Width / 2 + 10;
            bally = this.Height / 2 + 10;
            label7.Visible = false;
            label8.Visible = false;
            button1.Enabled = false;
            button1.Visible = false;
            label6.Visible = false;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int key = e.KeyValue;
            if (key == 38)
            {
                player2y -= 10;
            }

            if (key == 40)
            {
                player2y += 10;
            }
            if (key == 83)
            {
                player1y += 10;
            }
            if (key == 87)
            {
                player1y -= 10;
            }

        }
        void counter(object sender, EventArgs e) {
            count++;
            label2.Text = Convert.ToString(count);
            if (count >= 4) {
                timer.Tick -=new EventHandler(counter);
                label2.Visible = false;
                timer.Tick += new EventHandler(TimerCallBack);
                label2.Text = "1";
                count = 1;
                timer.Interval = 1000 / FPS;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                
            }
        }
    }
}
