using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8
{
    public partial class Form1 : Form
    {
        int x;
        int y;
        int fl = 0;
        int fa = 0;
        bool grid = false;
        private List<tree> listTree = new List<tree>();
        private List<flower> listFlower = new List<flower>();
        private List<grove> listGrove = new List<grove>();

        int[,] points = new int[16, 9] {       // 0 - пусто
            {0,0,0,0,0,0,0,0,0},            // 1 - дерево
            {0,0,0,0,0,0,0,0,0},            // 2 - цветок
            {0,0,0,0,0,0,0,0,0},            // 3 - роща
            {0,0,0,0,0,0,0,0,0},            // 4 - цветочная поляна?
            {0,0,0,0,0,0,0,0,0},            // 5 - лужвйка?
            {0,0,0,0,0,0,0,0,0},            // 6 - navy blue с кислородом все нормально
            {0,0,0,0,0,0,0,0,0},            // 7 - light blue кислорода не достаточно
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            };



        public void refreshPictureBox(Graphics g) 
        {
            foreach (var element in listTree)
            {
                element.Draw(g, element.X, element.Y);
                element.OxygenDraw(g, element.X, element.Y, points);
            }

            foreach (var element in listFlower)
            {
                element.Draw(g, element.X, element.Y);
                element.OxygenDraw(g, element.X, element.Y, points);
            }
            foreach (var element in listGrove)
            {
                element.Draw(g, element.X, element.Y);
                element.OxygenDraw(g, element.X, element.Y, points);
            }

        }


public Form1()
        {
            InitializeComponent();
            // Connect the Paint event of the PictureBox to the event handler method.
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);

            // Add the PictureBox control to the Form.
            this.Controls.Add(pictureBox1);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (grid)
            {
                #region grid

                int numOfCells = 128;
                int cellSize = 56;
                Pen p = new Pen(Color.DarkGreen);


                for (int y = 0; y < numOfCells; ++y)
                {
                    g.DrawLine(p, 0, y * cellSize, numOfCells * cellSize, y * cellSize);
                }

                for (int x = 0; x < numOfCells; ++x)
                {
                    g.DrawLine(p, x * cellSize, 0, x * cellSize, numOfCells * cellSize);
                }

                #endregion
            }

            switch (fl) // отрисовать объекты или же посадить дерево или цветок
            {
                case 0:
                    refreshPictureBox(g);
                    fl = fa;
                    break;

                 case 1:

                    if (points[x / 56, y / 56] == 1) 
                    {
                        points[x / 56, y / 56] = 3;
                        grove oak_grove = new grove(x,y);
                        listGrove.Add(oak_grove);
                    }
                    else if ((points[x / 56, y / 56] == 0 ||
                        points[x / 56, y / 56] == 6 ||
                        points[x / 56, y / 56] == 7) && x>=0 && y>=0)
                    {
                        points[x / 56, y / 56] = 1;
                        tree oak = new tree(x,y);
                        listTree.Add(oak);
                    }
                    refreshPictureBox(g);
                    break;
                
                case 2:
                    if (points[x / 56, y / 56] == 0 ||
                        points[x / 56, y / 56] == 6 ||
                        points[x / 56, y / 56] == 7)
                    {
                        points[x / 56, y / 56] = 2;
                        flower flower1 = new flower(x,y); 
                        listFlower.Add(flower1);
                    }
                    refreshPictureBox(g);       
                    break;                    
            }      
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
            if (x % 56 != 0) x = (x/56)*56;
            if (y % 56 != 0) y = (y/56)*56;

            //MessageBox.Show(String.Format("({0}, {1})", x, y));
            pictureBox1.Refresh();
        }

        #region radioButton

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            fl = 1;
            x = -1;
            y = -1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            fl = 2;
        }

        #endregion

        #region buttons

        private void button1_Click(object sender, EventArgs e)
        {
            if (listTree.Count > 0)
            {
                tree oak  = listTree.ElementAt( listTree.Count - 1 );
                oak.Clear(points);
                listTree.RemoveAt(listTree.Count - 1);
                fa = 1;
                fl = 0;
                pictureBox1.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listFlower.Count > 0)
            {
                flower flower1 = listFlower.ElementAt(listFlower.Count - 1);
                flower1.Clear(points);
                listFlower.RemoveAt(listFlower.Count - 1);
                fa = 2;
                fl = 0;
                pictureBox1.Refresh();
            }

        }
 
        private void button3_Click(object sender, EventArgs e)
        {
            if (listGrove.Count > 0)
            {
                grove grove1 = listGrove.ElementAt(listGrove.Count - 1);
                grove1.Clear(points);
                listGrove.RemoveAt(listGrove.Count - 1);
                fa = 1;
                fl = 0;
                pictureBox1.Refresh();
            }
        }
       
        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            grid = !grid;
        }

       
    }
}
