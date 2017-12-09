using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NuageDesPointsCSharpGraphics
{
    public class NuagePoints
    {
        public int[] x;
        public int[] y;
        public Graphics Graphics;

        public NuagePoints(Graphics Graphics)
        {
            this.Graphics = Graphics;
        }
        public NuagePoints(Graphics Graphics, int[] x, int[] y)
        {
            this.Graphics = Graphics;
            this.x = x;
            this.y = y;
        }
        public int MaxValueTab(int[] t)
        {
            int max = t.Select((value, index) => new { value, index })
                 .OrderByDescending(vi => vi.value)
                 .First().value;
            return max;
        }

        public int MinValueTab(int[] t)
        {
            int max = t.Select((value, index) => new { value, index })
                 .OrderByDescending(vi => vi.value)
                 .Last().value;
            return max;
        }

        public int[] TabNormaliser(int[] t, int LenghtAxe)
        {
            int[] tn = new int[t.Length];
            float division;

            for (int i = 0; i < t.Length; i++)
            {
                division = (float)((float)t[i] / (float)MaxValueTab(t));
                tn[i] = (int)Math.Truncate(LenghtAxe * division);
            }

            return tn;

        }
        public int Normaliser(double val, int LenghtAxe,int[]t)
        {
            int val2 = 0;
            float division;

            division = (float)((float)val / (float)MaxValueTab(t));
            val2 = (int)Math.Truncate( LenghtAxe * division);

            return val2;
        }

        public void addStringToScreen(String s, float x, float y, float width, float height, int size)
        {

            // Create string to draw.
            String drawString = s;

            // Create font and brush.
            Font drawFont = new Font("Arial", size);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create rectangle for drawing.
            RectangleF drawRect = new RectangleF(x, y, width, height);

            // Draw rectangle to screen.
            Pen blackPen = new Pen(Color.Transparent);
            Graphics.DrawRectangle(blackPen, x, y, width, height);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            // Draw string to screen.
            Graphics.DrawString(drawString, drawFont, drawBrush, drawRect, drawFormat);
        }

        public List<Point>tabsToList(int[] x , int[] y)
        {
            List<Point> points = new List<Point>();
            for(int i = 0; i < x.Length; i++) { 
            points.Add(new Point(x[i], y[i]));
            }
            return points;
        }

        public void DroiteDeRegression()
        {
            List<Point> points = tabsToList(x, y);
           // var a = Variance(points, p => p.X, p => p.Y) / Variance(points, p => p.X, p => p.X);
            double  b = points.Average(p => p.Y) - 5 * points.Average(p => p.X);
            MessageBox.Show(b.ToString());

        }
        public double SommeTi(int[] t)
        {
            double s = 0;
            for (int i = 0; i < t.Length; i++)
            {
                s += t[i] ;
            }
            return s;
        }

        public double SommeXiYi()
        {
            double s =0;
            for(int i = 0; i < x.Length; i++)
            {
                s += (double)(x[i] * y[i]);
            }
            return s;
        }

        public double SommeTiCarre(int[] t)
        {
            double s = 0;
            for (int i = 0; i < t.Length; i++)
            {
                s += (double)(t[i] * t[i]);
            }
            return s;
        }

        public double ValueB()
        {
            double b =0 ;
            int n = x.Length;
            b = (double)((( n* SommeXiYi()) - (SommeTi(x) * SommeTi(y)))/(( n*SommeTiCarre(x))-(SommeTi(x)* SommeTi(x))));
            
            return b;
        }

        public double ValueA()
        {
            double a = 0;
            int n = x.Length;
            a = (double)((SommeTi(y) - ValueB() * SommeTi(x)) / (double)n);
            return a;
        }


        public double GetYfromX(double x)
        {
            double y = 0;
            y = (ValueB() * x) + ValueA();

            return y;
        }


        public void Show()
        {


            //Création des points 
            Point p1 = new Point(450, 600);
            Point p2 = new Point(1250, 600);
            Point p3 = new Point(450, 150);
            Point p4 = new Point(1250, 150);//completer le cadre du diagramme

            

            //création des lignes 
            Pen AxePen = new Pen(Color.Black, 2);
            Graphics.DrawLine(AxePen, p3, p1);
            Graphics.DrawLine(AxePen, p1, p2);
            Graphics.DrawLine(AxePen, p3, p4); //completer le cadre du diagramme
            Graphics.DrawLine(AxePen, p2, p4); //completer le cadre du diagramme

            //2 cadre a gauche 
            Graphics.DrawRectangle(AxePen, 80, 150, 300, 450);

            //longeur de l'axe des abcisses et des ordonnées en px 
            int LenghtAbcisse, LenghtOrdonnees;
            LenghtAbcisse = p2.X - p1.X - 50;
            LenghtOrdonnees = p1.Y - p3.Y - 50;

            //Max valeur tableau abicisse et ordonneés
            int maxValueTabAbcisse, maxValueTabOrdonnees;
            maxValueTabAbcisse = MaxValueTab(x);
            maxValueTabOrdonnees = MaxValueTab(y);

            //Normalisation des valeurs xn=XNormaliser
            int[] xn = TabNormaliser(x, LenghtAbcisse);
            int[] yn = TabNormaliser(y, LenghtOrdonnees);


            //Placement des points sur le plan
            Pen EllipsePen = new Pen(Color.Blue, 2);
            for (int i = 0; i < x.Length; i++)
            {
                Graphics.DrawEllipse(EllipsePen, xn[i] + 450 - 2, (LenghtOrdonnees + 200) - yn[i] - 4, 6, 6);
               
            }



            //Graduation du plan (Axe des ordonneés)
            Pen GraduationPen = new Pen(Color.Gray, 1);
            int GradutionUnit = LenghtOrdonnees/10;
            int SommeGradutionUnit = 0;
            int GradutionUnitValues = maxValueTabOrdonnees / 10;
            int SommeGradutionUnitValues = maxValueTabOrdonnees;
            for (int i = 0; i < 11; i++)
            {
                if (i!=10)
                Graphics.DrawLine(GraduationPen, new Point(p3.X , p3.Y+50+ SommeGradutionUnit), new Point(p4.X-15, p4.Y + 50 + SommeGradutionUnit));
                if (i != 10)
                    addStringToScreen(SommeGradutionUnitValues.ToString(), p3.X - 40, p3.Y + 30 + SommeGradutionUnit+12, 40, 20, 7);
                else
                {
                    addStringToScreen("0", p3.X - 40, p3.Y + 30 + SommeGradutionUnit + 12, 40, 20, 7);

                }
                SommeGradutionUnit += GradutionUnit;
                SommeGradutionUnitValues -= GradutionUnitValues;

            }


            //Graduation du plan (Axe des abcisses)
             GradutionUnit = LenghtAbcisse / 10;
             SommeGradutionUnit = 0;
            GradutionUnitValues = maxValueTabAbcisse / 10;
             SommeGradutionUnitValues = 0;

            for (int i = 0; i < 11; i++)
            {
               
                    addStringToScreen(SommeGradutionUnitValues.ToString(), p1.X + SommeGradutionUnit-20, p1.Y+10, 40, 40, 7);
                SommeGradutionUnit += GradutionUnit;
                SommeGradutionUnitValues += GradutionUnitValues;
                if (i!=10)
                Graphics.DrawLine(GraduationPen, new Point(p3.X  + SommeGradutionUnit, p3.Y+15 ), new Point(p1.X + SommeGradutionUnit, p1.Y ));
                
            }


            //trace droite de regression
            double x1 = MinValueTab(x);
            double x2 = MaxValueTab(x);


            double y1 = GetYfromX(x1);
            double y2 = GetYfromX(x2);


            int x1n = Normaliser(x1, LenghtAbcisse,x);
            int x2n= Normaliser(x2, LenghtAbcisse,x);


            int y1n = Normaliser(y1, LenghtOrdonnees,y);
            int y2n = Normaliser(y2, LenghtOrdonnees,y);

           


            Point pd1 = new Point(x1n + 450 - 2, (LenghtOrdonnees + 200)-y1n-4);
            Point pd2 = new Point(x2n + 450 - 2, (LenghtOrdonnees + 200)-y2n-4);

            Pen PenDroite = new Pen(Color.Red, 2);
             


            Graphics.DrawLine(PenDroite, pd1, pd2);

        }
    }
}
