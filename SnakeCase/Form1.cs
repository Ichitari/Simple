using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace SnakeCase
{
    public partial class Form1 : Form
    {
        private PictureBox[] box = new PictureBox[400];
        private int dirX, dirY;
        private int height = 800;
        private int width = 900;
        private int sides = 40;
        private PictureBox item;
        private int w, q;
        private int wrop = 0;
        private Label ValueWroop;
        public Form1()
        {
            this.Width = width;
            this.Height = height;
            dirX = 1;
            dirY = 0;
            item = new PictureBox();
            item.BackColor = Color.Red;
            item.Size = new Size(sides, sides);
            box[0] = new PictureBox();
            box[0].Location = new Point(201, 201);
            box[0].Size = new Size(sides-1, sides-1);
            box[0].BackColor = Color.LimeGreen;
            this.Controls.Add(box[0]);
            InitializeComponent();
            GenerateMap();
            MadeItem();
            timer.Tick += new EventHandler(update);
            timer.Interval = 200;
            timer.Start();
            this.KeyDown += new KeyEventHandler(OKP);
            ValueWroop = new Label();
            ValueWroop.Text = "Очки: 0";
            ValueWroop.Location = new Point(800, 10);
            this.Controls.Add(ValueWroop);
        }

        private void MadeItem()
        {
            Random h = new Random();
            w = h.Next(0, height - sides);
            int traficW = w % sides;
            w -= traficW;
            q = h.Next(0, height - sides);
            int traficQ = q % sides;
            q -= traficQ;
            w++;
            q++;
            item.Location = new Point(w, q);
            this.Controls.Add(item);
        }
        
        private void LineBoard()
        {
            if (box[0].Location.X <0)
            {
                for(int p = 1; p <= wrop; p++)
                {
                    this.Controls.Remove(box[p]);
                }
                wrop = 0;
                ValueWroop.Text = "Очки: " + wrop;
                dirX = 1;

            }
            if (box[0].Location.X > height)
            {
                for (int p = 1; p <= wrop; p++)
                {
                    this.Controls.Remove(box[p]);
                }
                wrop = 0;
                ValueWroop.Text = "Очки: " + wrop;
                dirX = -1;

            }
            if (box[0].Location.Y < 0)
            {
                for (int p = 1; p <= wrop; p++)
                {
                    this.Controls.Remove(box[p]);
                }
                wrop = 0;
                ValueWroop.Text = "Очки: " + wrop;
                dirY = 1;

            }
            if (box[0].Location.Y > width)
            {
                for (int p = 1; p <= wrop; p++)
                {
                    this.Controls.Remove(box[p]);
                }
                wrop = 0;
                ValueWroop.Text = "Очки: " + wrop;
                dirX = -1;

            }
        }
        private void GoBox()
        {
            for(int i = wrop; i >= 1; i--)
            {
                box[i].Location = box[i-1].Location;
            }
            box[0].Location = new Point(box[0].Location.X + dirX * (sides), box[0].Location.Y + dirY * (sides));
            DestroyBox();
        }

        private void DestroyItem()
        {
            if(box[0].Location.X == w && box[0].Location.Y == q)
            {
                ValueWroop.Text = "Очки" + ++wrop;
                box[wrop] = new PictureBox();
                box[wrop].Location = new Point(box[wrop - 1].Location.X + 40 * dirX, box[wrop - 1].Location.Y - 40 * dirY);
                box[wrop].Size = new Size(sides-1, sides-1);
                box[wrop].BackColor = Color.LimeGreen;
                this.Controls.Add(box[wrop]);
                MadeItem();
            }
        }
        private void GenerateMap()
        {
            for(int i = 0; i < width / sides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.White;
                pic.Location = new Point(0, sides * i);
                pic.Size = new Size(Width - 100, 1);
                this.Controls.Add(pic);
            }

            for (int i = 0; i <= height / sides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.White;
                pic.Location = new Point(sides * i,0);
                pic.Size = new Size(1,width);
                this.Controls.Add(pic);
            }
        }
        private void update(object myObject, EventArgs eventArgs)
        {
            LineBoard();
            DestroyItem();
            GoBox();
            //cube.Location = new Point(cube.Location.X + dirX * sides, cube.Location.Y + dirY * sides);
        }

        private void DestroyBox()
        {
            for(int k=1; k<wrop; k++)
            {
                if (box[0].Location == box[k].Location)
                {
                    for (int n = k; n < wrop; n++)
                    {
                        this.Controls.Remove(box[n]);
                    }
                    wrop = wrop - (wrop - k + 1);
                }
            }
        }

        private void OKP (object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    //cube.Location = new Point(cube.Location.X + sides, cube.Location.Y);
                    dirX = 1;
                    dirY = 0;
                    break;
                case "Left":
                    //cube.Location = new Point(cube.Location.X - sides, cube.Location.Y);
                    dirX = -1;
                    dirY = 0;
                    break;
                case "Down":
                    dirY = 1;
                    dirX = 0;
                    //cube.Location = new Point(cube.Location.X, cube.Location.Y + sides);
                    break;
                case "Up":
                    dirY = -1;
                    dirX = 0;
                    //cube.Location = new Point(cube.Location.X, cube.Location.Y - sides);
                    break;
            }
        }


    }
}
