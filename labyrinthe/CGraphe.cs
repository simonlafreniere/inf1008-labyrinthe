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
        private int selection, poids, entree, sortie;
        private char direction;

        public CGraphe(ref int nbOp)
        {
            Random rdn = new Random();
            largeur = rdn.Next(2, 11);
            hauteur = rdn.Next(2, 11);
            setPoids();
            dimension = largeur * hauteur;
            entree = 0;
            sortie = dimension - 1;
            initGraphe(ref nbOp);
        }

        public CGraphe(int largeur, int hauteur, int poids, ref int nbOp)
        {
            setLargeur(largeur);
            setHauteur(hauteur);
            setPoids(poids);
            dimension = largeur * hauteur;
            entree = 0;
            sortie = dimension - 1;
            initGraphe(ref nbOp);
        }


        /***************/
        /* --getters-- */
        /***************/
        public int[,] getGraphe()
        {
            return graphe;
        }

        public int getDimension()
        {
            return dimension;
        }

        public int getLargeur()
        {
            return largeur;
        }

        public int getHauteur()
        {
            return hauteur;
        }

        public int getPoids()
        {
            return poidsMax;
        }

        public int getEntree()
        {
            return entree;
        }

        public int getSortie()
        {
            return sortie;
        }


        /***************/
        /* --setters-- */
        /***************/
        public void setLargeur(int largeur = 3)
        {
            if (largeur < 2)
                this.largeur = 3;
            else
                this.largeur = largeur;
            dimension = hauteur * largeur;
        }

        public void setHauteur(int hauteur = 3)
        {
            if (hauteur < 2)
                this.hauteur = 3;
            else
                this.hauteur = hauteur;
            dimension = hauteur * largeur;
        }

        public void setPoids()
        {
            Random rnd = new Random();
            poidsMax = rnd.Next(2, 21);
        }

        public void setPoids(int poids)
        {
            if (poids > 1)
                poidsMax = poids;
            else
                setPoids();
        }



        //remplissage du graphe avec les poids des arêtes
        public void initGraphe(ref int nbOp)
        {
            graphe = new int[dimension, 2];
            Random rdn = new Random();
            for (int i = 0; i < dimension; i++)
            {
                graphe[i, 0] = rdn.Next(1, poidsMax + 1);
                graphe[i, 1] = rdn.Next(1, poidsMax + 1);

                nbOp += 2;
            }

            //dernière ligne
            int posArret = dimension - 1 - largeur;
            for (int i = dimension - 1; i > posArret; i--)
            {
                graphe[i, 1] = -1;

                nbOp++;
            }
            //dernière colonne
            for (int i = dimension - 1; i > 0; i -= largeur)
            {
                graphe[i, 0] = -1;

                nbOp++;
            }
        }


        /************/
        /* --Prim-- */
        /************/
        //calcul de l'arbre sous-tendant minimal
        public int[,] Prim(ref int nbOp)
        {
            //graphe des noeuds visités
            visites = new int[dimension];
            visites[entree] = 1;
            //pour les noeuds restants à visiter
            for (int i = dimension; i > 1; i--)
            {
                int pos = 0;
                selection = poids = 0;
                //pour chaque noeud déjà visité
                foreach (int noeud in visites)
                {
                    if (noeud == 1)
                        eval(pos, ref nbOp);
                    pos++;
                }

                switch (direction)
                {
                    case 'e':
                        graphe[selection - 1, 0] = 0;
                        break;
                    case 's':
                        graphe[selection - largeur, 1] = 0;
                        break;
                    case 'o':
                        graphe[selection, 0] = 0;
                        break;
                    case 'n':
                        graphe[selection, 1] = 0;
                        break;
                }
                visites[selection] = 1;
                nbOp+=2;
            }
            return graphe;
        }

        //évaluation des arêtes
        private void eval(int noeud, ref int nbOp)
        {
            //est
            //si l'arête n'a pas déjà été utilisée et le noeud à droite existe
            if (graphe[noeud, 0] > 0)
            {   //si le noeud à droite a déjà été visité
                if (visites[noeud + 1] == 1)
                {
                    graphe[noeud, 0] = -1;

                    nbOp++;
                }
                else
                   if (graphe[noeud, 0] > poids)
                {
                    selection = noeud + 1;
                    poids = graphe[noeud, 0];
                    direction = 'e';

                    nbOp += 3;
                }

                
            }

            //sud
            if (graphe[noeud, 1] > 0)
            {
                if (visites[noeud + largeur] == 1)
                {
                    graphe[noeud, 1] = -1;

                    nbOp++;
                }
                else
                  if (graphe[noeud, 1] > poids)
                {
                    selection = noeud + largeur;
                    poids = graphe[noeud, 1];
                    direction = 's';

                    nbOp += 3;
                }

               
            }

            //ouest
            //si il ne s'agit pas de la première colonne
            if (noeud > 0 && noeud % largeur != 0)
            {
                if (visites[noeud - 1] == 1)
                {
                    if (graphe[noeud - 1, 0] > 0)
                        graphe[noeud - 1, 0] = -1;

                    nbOp++;
                }
                else
                    if (graphe[noeud - 1, 0] > poids)
                {
                    selection = noeud - 1;
                    poids = graphe[noeud - 1, 0];
                    direction = 'o';

                    nbOp += 3;
                }

                
            }

            //nord
            //si il ne s'agit pas de la première ligne
            if ((noeud - largeur) > -1)
            {
                if (visites[noeud - largeur] == 1)
                {
                    if (graphe[noeud - largeur, 1] > 0)
                        graphe[noeud - largeur, 1] = -1;

                    nbOp++;
                }
                else
                    if (graphe[noeud - largeur, 1] > poids)
                {
                    selection = noeud - largeur;
                    poids = graphe[noeud - largeur, 1];
                    direction = 'n';

                    nbOp += 3;
                }

                
            }
        }


    }
}
