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
        private int selection, poids;
        private char direction;

        public CGraphe(int largeur, int hauteur, int poids)
        {
            this.largeur = largeur;
            this.hauteur = hauteur;
            poidsMax = poids;
            initGraphe();
        }

        //remplissage du graphe avec les poids des arrêtes
        public void initGraphe()
        {
            dimension = largeur * this.hauteur;
            graphe = new int[dimension, 2];
            Random rdn = new Random();
            for (int i = 0; i < dimension; i++)
            {
                graphe[i, 0] = rdn.Next(1, poidsMax + 1);
                graphe[i, 1] = rdn.Next(1, poidsMax + 1);
            }

            //dernière ligne
            int posArret = dimension - 1 - largeur;
            for (int i = dimension - 1; i > posArret; i--)
                graphe[i, 1] = -1;
            //dernière colonne
            for (int i = dimension - 1; i > 0; i -= largeur)
                graphe[i, 0] = -1;
        }

        public void Prim(int entree)
        {
            //graphe des noeuds visités
            visites = new int[dimension];
            visites[entree] = 1;
            for (int i = dimension; i > 0; i--)
            {
                int pos = 0;
                selection = poids = 0;
                foreach (int noeud in visites)
                {
                    if (noeud == 1)
                        eval(pos);
                    pos++;
                }
                visites[selection] = 1;
                switch (direction)
                {
                    case 'e':
                        graphe[selection-1, 0] = 0;
                        break;
                    case 's':
                        graphe[selection -largeur, 1] = 0;
                        break;
                    case 'o':
                        graphe[selection, 0] = 0;
                        break;
                    case 'n':
                        graphe[selection, 1] = 0;
                        break;
                }
            }
        }

        public void eval(int noeud)
        {
            //est
            if (graphe[noeud, 0] > 0)
            {
                if (visites[noeud + 1] == 1)
                    graphe[noeud, 0] = -1;
                else
                  if (graphe[noeud, 0] > poids)
                {
                    selection = noeud+1;
                    poids = graphe[noeud, 0];
                    direction = 'e';
                }
            }

            //sud
            if (graphe[noeud, 1] > 0)
            {
                if (visites[noeud + largeur] == 1)
                    graphe[noeud, 1] = -1;
                else
                  if (graphe[noeud, 1] > poids)
                {
                    selection = noeud+largeur;
                    poids = graphe[noeud, 1];
                    direction = 's';
                }
            }

            //ouest
            //si il ne s'agit pas de la première colonne
            if ((noeud - 1) % largeur != largeur - 1 && noeud - 1 > 0)
            {
                if (visites[noeud - 1] == 1)
                    graphe[noeud - 1, 0] = -1;
                else
                    if (graphe[noeud - 1, 0] > poids)
                {
                    selection = noeud - 1;
                    poids = graphe[noeud - 1, 0];
                    direction = 'o';
                }
            }

            //nord
            //si il ne s'agit pas de la première ligne
            if ((noeud - largeur) > 0)
            {
                if (visites[noeud - largeur] == 1)
                    graphe[noeud - largeur, 1] = -1;
                else
                    if (graphe[noeud - largeur, 1] > poids)
                {
                    selection = noeud - largeur;
                    poids = graphe[noeud - largeur, 1];
                    direction = 'n';
                }
            }
        }
    }
}
