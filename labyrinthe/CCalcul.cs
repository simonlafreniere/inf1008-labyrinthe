using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labyrinthe
{
    class CCalcul
    {
        //plus grands facteurs
        public static int[] PGF(int nb)
        {
            int[] result = new int[2];
            int n=1, m=nb;

            while (n <= m)
            {
                if (nb % n == 0)
                    m = nb / n;
                n++;
            }

            result[0] = nb/m;
            result[1] = m;
            return result;
        }
    }
}
