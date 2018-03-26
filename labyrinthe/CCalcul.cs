using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labyrinthe
{
    class CCalcul
    {
        public static int nBase(int n, int m)
        {
            int result = n * (m - 1) + m * (n - 1);
            return result;
        }

        public static int factorielle(int nombre)
        {
            if (nombre == 1)
                return 1;
            else
                return nombre * factorielle(nombre - 1);
        }

        public static string ordreToString(int ordre)
        {
            string result = "ordre inconnu..";
            switch (ordre)
            {
                case 0:
                    result = "(1)";
                    break;
                case 1:
                    result = "log(n)";
                    break;
                case 2:
                    result = "n";
                    break;
                case 3:
                    result = "nlog(n)";
                    break;
                case 4:
                    result = "n!";
                    break;
                case 5:
                    result = "n carré";
                    break;
                case 6:
                    result = "n cube ou pire..";
                    break;
            }
            return result;
        }
    }
}
