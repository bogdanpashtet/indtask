using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace lab8
{
    interface Idrawing {
        void Draw(Graphics e, int x, int y);
        void OxygenDraw(Graphics e, int x, int y, int[,] points);
    }
        
    public abstract class plant
    {
        #region fields        
        private int x;
        public int X 
        {
            set { x = value; }
            get { return x; }
        }

        private int y;
        public int Y
        {
            set { y = value; }
            get { return y; }
        }

        #endregion
        protected void Drow_plant100(Graphics e, int x, int y, int[,] points, Image newImage) 
        {
            PointF ulCorner = new PointF(x , y );   // 1
            if (OnLimit(x,y))
            {
                if (points[x / 56, y / 56] == 0 || points[x / 56, y / 56] == 6 )
                {
                    points[x / 56, y / 56] = 6;
                    e.DrawImage(newImage, ulCorner);
                }
            }
        }
        protected bool OnLimit(int x, int y) 
        {
            if (x / 56 < 16 && y / 56 < 9 && x >= 0 && y >= 0)
                return true;
            else 
                return false;           
        }
        public delegate void DrawPlantDelegate(Graphics e, int x, int y, int[,] points, Image newImage);
        public abstract void Clear(int[,] points);
    }

    public class tree : plant, Idrawing
    {
        #region constructors

        public tree()
        {
            X = 0;
            Y = 0;
        }

        public tree(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region draw by interface        

        public void Draw(Graphics e, int x, int y)
        {
            Image newImage = Image.FromFile(@"pictures\Sample.png");
            PointF ulCorner = new PointF(x, y);
            e.DrawImage(newImage, ulCorner);
        }

        public void OxygenDraw(Graphics e, int x, int y, int[,] points)
        {
            Image newImage = Image.FromFile(@"pictures\oxxy2.png");

            DrawPlantDelegate drawPoint = new DrawPlantDelegate(Drow_plant100);
            
            drawPoint(e, x - 56, y - 56, points,newImage);// 1
                                              
            drawPoint(e, x, y - 56, points, newImage);// 2
                                
            drawPoint(e, x + 56, y - 56, points, newImage);// 3
                       
            drawPoint(e, x - 56, y, points, newImage);// 4
                         
            drawPoint(e, x + 56, y, points, newImage);// 6
                                
            drawPoint(e, x - 56, y + 56, points, newImage);// 7

            drawPoint(e, x, y + 56, points, newImage); // 8
                    
            drawPoint(e, x + 56, y + 56, points, newImage);// 9
        }

        #endregion

        public override void Clear(int[,] points) 
        {
            if (OnLimit(X, Y)) points[X/ 56, Y/ 56] = 0;                            // 5
            if (OnLimit(X-56, Y-56)) points[(X - 56) / 56, (Y - 56) / 56] = 0;      // 1
            if (OnLimit(X, Y-56 )) points[X/ 56,( Y - 56) / 56] = 0;                // 2
            if (OnLimit(X+56, Y-56)) points[(X + 56) / 56, (Y - 56) / 56] = 0;      // 3
            if (OnLimit(X-56, Y)) points[(X - 56) / 56, Y/ 56] = 0;                 // 4
            if (OnLimit(X+56, Y)) points[(X + 56) / 56, Y/ 56] = 0;                 // 6
            if (OnLimit(X-56, Y+56)) points[(X - 56)/ 56, (Y + 56) / 56] = 0;       // 7
            if (OnLimit(X, Y+56)) points[X/ 56, (Y + 56)/ 56] = 0;                  // 8
            if (OnLimit(X+56, Y+56)) points[(X + 56) / 56, (Y + 56)/56] = 0;        // 9
        }
    }

    public class flower : plant, Idrawing
    {
        #region constructors

        public flower()
        {
            X = 0;
            Y = 0;
        }

        public flower(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region frow by interface

        public void Draw(Graphics e, int x, int y)
        {
            Image newImage = Image.FromFile(@"pictures\flower.png");

            PointF ulCorner = new PointF(x, y);

            e.DrawImage(newImage, ulCorner);
        }

        public void OxygenDraw(Graphics e, int x, int y, int[,] points)
        {
            Image newImage = Image.FromFile(@"pictures\oxxy2.png");

            DrawPlantDelegate drawPoint = new DrawPlantDelegate(Drow_plant100);

            drawPoint(e, x, y - 56, points, newImage);// 2

            drawPoint(e, x - 56, y, points, newImage);// 4
            
            drawPoint(e, x + 56, y, points, newImage);// 6
          
            drawPoint(e, x, y + 56, points, newImage);// 8
        }

        #endregion

        public override void Clear(int[,] points)
        {
            if (OnLimit(X, Y)) points[X / 56, Y / 56] = 0;                    // 5
            if (OnLimit(X, Y-56)) points[X / 56, (Y - 56) / 56] = 0;          // 2
            if (OnLimit(X-56, Y)) points[(X - 56) / 56, Y / 56] = 0;          // 4
            if (OnLimit(X+56, Y)) points[(X + 56) / 56, Y / 56] = 0;          // 6
            if (OnLimit(X, Y+56)) points[X / 56, (Y + 56) / 56] = 0;          // 8
        }

    }

    public class grove : plant, Idrawing 
    {
        #region constructres

        public grove()
        {
            X = 0;
            Y = 0; ;
        }

        public grove(int x, int y) 
        {
            X = x;
            Y = y; 
        }

        #endregion

        #region draw by Idrawing

        public void Draw(Graphics e, int x, int y)
        {
            Image newImage = Image.FromFile(@"pictures\grove1.png");

            PointF ulCorner = new PointF(x, y);

            e.DrawImage(newImage, ulCorner);
        }

        public void OxygenDraw(Graphics e, int x, int y, int[,] points)
        {
            Image newImage = Image.FromFile(@"pictures\oxxy2.png");

            DrawPlantDelegate drawPoint = new DrawPlantDelegate(Drow_plant100);

            drawPoint(e, x - 56, y - 56, points, newImage);// 1

            drawPoint(e, x, y - 56, points, newImage);// 2

            drawPoint(e, x + 56, y - 56, points, newImage);// 3

            drawPoint(e, x - 56, y, points, newImage);// 4

            drawPoint(e, x + 56, y, points, newImage);// 6

            drawPoint(e, x - 56, y + 56, points, newImage);// 7

            drawPoint(e, x, y + 56, points, newImage);// 8

            drawPoint(e, x + 56, y + 56, points, newImage);// 9

            drawPoint(e, x, y - 56*2, points, newImage ); // 2+

            drawPoint(e, x - 56*2, y, points, newImage);// 4+

            drawPoint(e, x + 56*2, y, points, newImage); // 6+

            drawPoint(e, x, y + 56*2, points, newImage); // 8+

        }

        #endregion

        public override void Clear(int[,] points)
        {
            if (OnLimit(X, Y)) points[X / 56, Y / 56] = 0;                              // 5
            if (OnLimit(X - 56, Y - 56)) points[(X - 56) / 56, (Y - 56) / 56] = 0;      // 1
            if (OnLimit(X, Y - 56)) points[X / 56, (Y - 56) / 56] = 0;                  // 2
            if (OnLimit(X + 56, Y - 56)) points[(X + 56) / 56, (Y - 56) / 56] = 0;      // 3
            if (OnLimit(X - 56, Y)) points[(X - 56) / 56, Y / 56] = 0;                  // 4
            if (OnLimit(X + 56, Y)) points[(X + 56) / 56, Y / 56] = 0;                  // 6
            if (OnLimit(X - 56, Y + 56)) points[(X - 56) / 56, (Y + 56) / 56] = 0;      // 7
            if (OnLimit(X, Y + 56)) points[X / 56, (Y + 56) / 56] = 0;                  // 8
            if (OnLimit(X + 56, Y + 56)) points[(X + 56) / 56, (Y + 56) / 56] = 0;      // 9
            if (OnLimit(X, Y- 56*2)) points[X / 56, (Y - 56*2) / 56] = 0;               // 2+
            if (OnLimit(X-56*2, Y)) points[(X - 56*2) / 56, Y  / 56] = 0;               // 4+
            if (OnLimit(X+56*2, Y)) points[(X + 56*2) / 56, Y  / 56] = 0;               // 6+
            if (OnLimit(X, Y+56*2)) points[X / 56, (Y + 56*2) / 56] = 0;                // 8+
        }
    }

}
