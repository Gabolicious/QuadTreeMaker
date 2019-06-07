using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuadTreeClicker
{
    public partial class Form1 : Form
    {
        class Rectangle
        {
            public int x;
            public int y;
            public int w;
            public int h;
            public Rectangle(int x, int y, int w, int h)
            {
                this.x = x;
                this.y = y;
                this.w = w;
                this.h = h;
            }
        }
        class QuadTree
        {
            private Rectangle Boundary;
            private Bitmap b;
            public QuadTree(Rectangle Boundary, Bitmap g)
            {
                this.Boundary = Boundary;
                b = g;
                Draw();
            }
            public void Subdivide()
            {
                new QuadTree(new Rectangle(Boundary.x, Boundary.y, Boundary.w / 2, Boundary.h / 2), b); //new tree for each area
                new QuadTree(new Rectangle(Boundary.x + Boundary.w / 2, Boundary.y, Boundary.w / 2, Boundary.h / 2), b);
                new QuadTree(new Rectangle(Boundary.x, Boundary.y + Boundary.h / 2, Boundary.w / 2, Boundary.h / 2), b);
                new QuadTree(new Rectangle(Boundary.x + Boundary.w / 2, Boundary.y + Boundary.h / 2, Boundary.w / 2, Boundary.h / 2), b);
            }
            public void Draw()
            {
                if (Boundary.w <= 1 || Boundary.h <= 1) return;

                //draw the tree
                gmain.DrawRectangle(new Pen(new SolidBrush(Color.Black)), Boundary.x, Boundary.y, Boundary.w, Boundary.h);
                
                //Loop through all pixels inside the tree
                for (int x = 0; x < Boundary.w; x++)
                {
                    for (int y = 0; y < Boundary.h; y++)
                    {
                        Color pixel = b.GetPixel(x + Boundary.x , y + Boundary.y);
                        if (pixel.Name == "ff808080")
                        {
                            Subdivide();
                            return;
                        }

                    }
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap b;
        public static Graphics gmain, gbuff;

        private void Form1_Load(object sender, EventArgs e)
        {
       
            gmain = panel1.CreateGraphics();
            b = new Bitmap(panel1.Width, panel1.Height);
            gbuff = Graphics.FromImage(b);
            
        }

        private void Go_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            gbuff.Clear(Color.White);
            gbuff.FillEllipse(new SolidBrush(Color.Gray), rnd.Next(20, panel1.Width - 20), rnd.Next(20, panel1.Height), 20, 20);
            gbuff.FillEllipse(new SolidBrush(Color.Gray), rnd.Next(60, panel1.Width- 60), rnd.Next(60, panel1.Height - 60), 60, 60);
            gmain.DrawImage(b, 0, 0);
            new QuadTree(new Rectangle(0, 0, panel1.Width, panel1.Height), b);

        }
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
