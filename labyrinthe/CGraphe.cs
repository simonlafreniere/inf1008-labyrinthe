using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labyrinthe
{
    class CGraphe
    {
        private int[,] graphe;
        private int[] visites;
        private int largeur, hauteur, poidsMax, dimension;

        public CGraphe(int largeur, int hauteur, int poids)
        {
            this.largeur = largeur;
            this.hauteur = hauteur;
            poidsMax = poids;
            initGraphe();
        }

        public void initGraphe()
        {
            dimension=this.largeur * this.hauteur;
            graphe = new int[dimension,2];
            Random rdn = new Random();
            for(int i = 0; i < dimension; i++)
            {
                graphe[i, 1] = rdn.Next(1, poidsMax + 1);
                graphe[i, 2] = rdn.Next(1, poidsMax + 1);
            }

            visites= new int[dimension];
        }
    }
}
