using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POINT_TEST
{

    public class Line
    {
        public static bool IsInsidePolygon(int ctr)
        {
            if ((ctr%2) != 0)
            {
                return false;
            }
            return true;
        }


        public static double Xintercept(double X1, double Y1, double X2, double Y2, double ptY)
        {
            double XI = 0;
            double M = 0;
            double C = 0;

            M = (Y2 - Y1) / (X2 - X1);
            C = Y1 - (M * X1);

            if(Y2 != Y1 && X2 != X1)
            {
                XI = (ptY - C) / M;
            }

            if(X2 == X1)
            {
                XI = X2;
            }

            if(Y2 == Y1)
            {
                XI = 0;
            }
            
            return XI;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            //Defining the polygon array
            int sides = 0;
            Console.Write("Please enter number of sides of polygon: ");
            sides = Convert.ToInt32(Console.ReadLine());

            int[,] PolygonPoints = new int[sides, 2];

            Console.WriteLine("\nEnter the coordinates of polygon points: ");

            //Getting the coordinates of points of the polygon
            int count = 1;
            while (sides >= count)
            {
                Console.WriteLine("\nCoordinates of Point " + count + ": ");
                Console.Write("X " + count + ": ");
                PolygonPoints[(count-1), 0] = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nY " + count + ": ");
                PolygonPoints[(count-1), 1] = Convert.ToInt32(Console.ReadLine());
                count++;
            }


            //Defining and getting the point co ordinates in the space
            int[] Point = new int[] { 0, 0 };
            Console.WriteLine("\nEnter the coordinates of the point: ");
            Console.Write("X: ");
            Point[0] = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nY: ");
            Point[1] = Convert.ToInt32(Console.ReadLine());

            /*Defining the equation of line for the point
              Utilizing the coordinates of point and let the line be horizontal*/
            //y = mx + c; x=0 therefore y=c is equation of line passing through point

            int Py = Point[1];

            /*Forming the equation of line for all sides of polygon
              Once the equation of each side is formed then we will check 
              whether sides intersect with the ray of line through point under observation*/

            int w = 0;
            double[] XIa = new double[sides];
            for (int i = 0; i < sides; i++)
            {
                int r = i+1;
                
                if (r == sides)
                {
                    r = 0;
                }

                XIa[w] = Line.Xintercept(PolygonPoints[i, 0], PolygonPoints[i, 1], PolygonPoints[r, 0], PolygonPoints[r, 1], Py);
                w++;
            }

            foreach (double b in XIa)
            {
                Console.WriteLine(b);
            }

            int counter = 0;
            int K = 0;
            for (int L = 0; L < sides; L++)
            {
                K = L+1;

                if(K == sides)
                {
                    K = 0;
                }
                if (XIa[L] > Point[0])
                {
                    if (((XIa[L]> PolygonPoints[L,0]) && (XIa[L] < PolygonPoints[K, 0]))  || ((XIa[L] < PolygonPoints[L, 0]) && (XIa[L] > PolygonPoints[K, 0])))
                    {
                        counter++;
                    }
                }
            }

            if (Line.IsInsidePolygon(counter))
            {
                Console.WriteLine("Point Lies inside Polygon!");
            }
            else
            {
                Console.WriteLine("Point do not lie inside Polygon!");
            }


            Console.ReadLine();
        }
    }
}
