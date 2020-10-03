using System;

namespace ConsoleApplication

{
    class Program
    {
        static void Main()
        {
            
            
            //Considering XY Co-ordinate System
            //(Y = 0) for Ground Level, building base coordinates will have y coordinate to be zero
            

            
            //(input) getting the coordinates of the building
            //                               (NUM OF BUILDING)  (FOUR CORNER)     (X AND Y)
            double[,,] Building = new double         [5,              4,             2]
            {
                //  P             Q                R               S
                //LOWER LEFT    TOP LEFT        TOP RIGHT       LOWER RIGHT
                { {20,0},       {20,10},        {25,10},        {25,0} },  // Coordinates of building one
                { {30,0},       {30,40},        {40,40},        {40,0} },  // Coordinates of building two
                { {50,0},       {50,24},        {60,24},        {60,0} },  // Coordinates of building three
                { {80,0},       {80,10},        {100,10},       {100,0} }, // Coordinates of building four
                { {120,0},      {120,6},        {150,6},        {150,0} }  // Coordinates of building five
            };

            
            
            
            
            //(input) getting the coordinates of Sun
            double[] Sun = new double[] { 0, 70 };





            //getting the x intercept at y = 0 of every building;
            //For this we will form equation of line for every building with Top Right and Sun point Coordinates
            //Storing the X intercept in array
            double[] XIz = new double[5];

            for (int n = 4; n >= 0; n-- )
            {
                double mz = (Building[n, 2, 1] - Sun[1]) / (Building[n, 2, 0] - Sun[0]);
                double cz = Sun[1] - (mz * (Sun[0]));
                XIz[n] = -(cz / mz);
            }





            //(Remove commented lines for inspection)
            ////printing x intercept of every building associated with shadow line 
            //Console.WriteLine("\nprinting x intercept of every building associated with shadow line");
            //foreach (double k in XIz)
            //{
            //    Console.WriteLine(k);
            //}





            //X intercept value to every building, this value is X intercept max (impact) from previos buildings
            //The previous building whose X intercept is maximum will result more shadow on buildings ahead of it
            double[] XIz_Impact = new double[5] { 0, 0, 0, 0, 0 };

            for (int n = 4; n >=0; n--)
            {
                int m = n;

                if (m != 0)
                {
                    XIz_Impact[n] = XIz[m - 1];

                    while (m > 0)
                    {
                        double Z;

                        Z = XIz[m - 1];

                        if (XIz_Impact[n] < Z)
                        {
                            XIz_Impact[n] = Z;
                        }
                        m--;
                    }
                }
                
            }




            //(Remove commented lines for inspection)
            ////printing x intercept which is max for the particular building from its previous buildings
            //Console.WriteLine("\nprinting x intercept which is max for the particular building from its previous buildings");
            //foreach (double g in XIz_Impact)
            //{
            //    Console.WriteLine(g);
            //}




            //getting the XIh intercept for every building with the XIz_Impact point and Sun point Line equation
            //Xintercept at height of building i.e Y=Height of building and solve the equation of line
            double[] XIh = new double[5] { 0, 0, 0, 0, 0 };

            double M_Impact = 0;
            double C_Impact = 0;
            for (int n = 4; n>0; n--)
            {
                double mh = (Sun[1] - 0) / (Sun[0] - XIz_Impact[n]);
                double ch = Sun[1] - (mh * Sun[0]);

                XIh[n] = (Building[n, 2, 1] - ch) / mh;

                //storing mh and ch to get height of building under shadow in later part of program
                M_Impact = mh;
                C_Impact = ch;
            }
            
            
            
            
            
            //Uncomment these lines for inspection check
            //Console.WriteLine("\nprinting the XIh intercept for every building with the XIz_Impact point and Sun point Line equation");
            //foreach (double p in XIh)
            //{
            //    Console.WriteLine(p);
            //}





            //getting the shadow length on each building from its previous buildings
            double[] mEL = new double[5] { 0, 0, 0, 0, 0 };
            for (int n = 4; n>0; n--)
            {
                if(Building[n,0,0] >= XIz_Impact[n])
                {
                    mEL[n] = 0;
                }

                if (Building[n,2,0] <= XIh[n])
                {
                    mEL[n] = Building[n, 1, 1] + (Building[n, 3, 0] - Building[n, 0, 0]);
                }

                if (Building[n,0,0] < XIz_Impact[n] && Building[n,2,0] > XIh[n])
                {
                    if (XIh[n] < Building[n,2,0] && XIh[n] > Building[n,1,0])
                    {
                        mEL[n] = Building[n, 1, 1] + (XIh[n] - Building[n, 1, 0]);
                    } 
                }

                if (XIz_Impact[n] > Building[n,0,0] && XIh[n] < Building[n,1,0])
                {
                    double[] Y_Impact = new double[5] { 0, 0, 0, 0, 0 };
                    Y_Impact[n] = (M_Impact * Building[n, 0, 0]) + C_Impact;
                    if (Y_Impact[n] > 0)
                    {
                        mEL[n] = Y_Impact[n];
                    }
                }
            }




            //Uncomment these lines for inspection check
            //Console.WriteLine("\nPrinting the lenth value of shadow on each building");
            //foreach (double q in mEL)
            //{
            //    Console.WriteLine(q);
            //}




            //Getting the Output
            //Exposed length to sunlight EL
            //Consider entire length is exposed to sunlight initially and then substract the shadow length

            double EL = 0; //variable to store exposed length to sunlight
            double ShadowLength = 0; //variable to store entire shadow length
            for (int n = 4; n>=0; n--)
            {
                //          height                     roof length
                EL = EL + (Building[n, 1, 1] + (Building[n, 3, 0] - Building[n, 0, 0]));
            }

            Console.WriteLine("Total Exposed Length without considering shadow : "  + EL);

            foreach (int r in mEL)
            {
                ShadowLength = ShadowLength + r;
            }

            Console.WriteLine("Shadow Length : " + ShadowLength);

            EL = EL - ShadowLength;

            Console.WriteLine("The summation of sun exposed length of all buildings is: " + EL);
            Console.ReadLine();
        }
    }
}
