using System;
using System.Collections.Generic;
using System.Text;

namespace Imagify
{
    public class Util
    {
        public static bool isPerfect(int N)
        {
            if ((Math.Sqrt(N) - Math.Floor(Math.Sqrt(N))) != 0)
                return false;
            return true;
        }

        public static int getClosestPerfectSquare(int N)
        {
            if (isPerfect(N))
            {
                return N;
            }

            // Variables to store first perfect 
            // square number 
            // above and below N 
            int aboveN = -1, belowN = -1;
            int n1;

            // Finding first perfect square 
            // number greater than N 
            n1 = N + 1;
            while (true)
            {
                if (isPerfect(n1))
                {
                    aboveN = n1;
                    return n1;
                }
                else
                    n1++;
            }
        }
    }
}
