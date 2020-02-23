using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    class Ex1
    {
        private double ex1()
        {
            int m = 0;
            decimal dec = new decimal(-1 * m);
            double d = (double)dec;
            double u = Math.Pow(10, d);
            while(u>0 && u+1!=1)
            {
                m++;
                d = -1 * m;
                u = Math.Pow(10, d);
            }
            return u*10;
        }

    }
}
