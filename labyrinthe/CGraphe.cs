using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labyrinthe
{
    class CGraphe
    {
        private int[,] graphe, grapheDensite, grapheOriginal;
        private int[] visites;
        private int largeur, hauteur, poidsMax, dimension;
        private int selection, poids, entree, sortie;
        private char direction;

        public CGraphe()
        {
            Random rdn = new Random();
            largeur = rdn.Next(2, 11);
            hauteur = rdn.Next(2, 11);
            setPoids();
            dimension = largeur * hauteur;
            entree = 0;
            sortie = dimension - 1;
            initGraphe();
            copieGraphetoOriginal();//sauvegarde pour si on veut réutiliser le même graphe plus d'une fois
        }

        public CGraphe(int largeur, int hauteur, int poids)
        {
            setLargeur(largeur);
            setHauteur(hauteur);
            setPoids(poids);
            dimension = largeur * hauteur;
            entree = 0;
            sortie = dimension - 1;
            initGraphe();
            copieGraphetoOriginal();
        }

        public int[,] getGraphe()
        {
            return graphe;
        }

        public int[,] getGrapheOriginal()
        {
            return grapheOriginal;
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

        public int[,] getGrapheDensite()
        {
            return grapheDensite;
        }



        public void setLargeur(int largeur=3)
        {
            if (largeur < 2)
                this.largeur = 3;
            else
                this.largeur = largeur;
        }

        public void setHauteur(int hauteur=3)
        {
            if (hauteur < 2)
                this.hauteur = 3;
            else
                this.hauteur = hauteur;
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

        //choisit une entrée au hazard
        public bool setEntree()
        {
            //tableau minimal
            if (dimension == 4)
            {
                entree = 0;
                return true;
            }

            Random rnd = new Random();
            int pos;
            //1=gauche,2=dessus,3=droite,4=dessous
            int face = rnd.Next(1, 5);
            if (face % 2 == 0)
                pos = rnd.Next(0, largeur);
            else
                pos = rnd.Next(0, hauteur);

            switch (face)
            {
                case 1:
                    entree = 0 + pos * largeur;
                    break;
                case 2:
                    entree = 0 + pos;
                    break;
                case 3:
                    entree = largeur - 1 + pos * largeur;
                    break;
                case 4:
                    entree = dimension - largeur + pos;
                    break;
            }
            return true;
        }

        //choisit une entrée manuellement
        public bool setEntree(int pos)
        {
            //tableau minimal
            if (dimension == 4)
            {
                entree = 0;
                return true;
            }
            //si la position est sur un coté
            if (legal(pos))
            {
                entree = pos;
                return true;
            }
            return false;
        }

        //choisit une sortie au hazard
        public bool setSortie()
        {
            Random rnd = new Random();
            int pos, face;
            while (true)
            {
                face = rnd.Next(1, 5);
                if (face % 2 == 0)
                    pos = rnd.Next(0, largeur);
                else
                    pos = rnd.Next(0, hauteur);

                switch (face)
                {
                    case 1:
                        entree = 0 + pos * largeur;
                        break;
                    case 2:
                        entree = 0 + pos;
                        break;
                    case 3:
                        entree = largeur - 1 + pos * largeur;
                        break;
                    case 4:
                        entree = dimension - largeur + pos;
                        break;
                }
                if (setSortie(pos))
                    return true;
            }
        }

        //choisit une sortie manuellement
        public bool setSortie(int pos)
        {
            //tableau minimal
            if (dimension == 4)
            {
                sortie = 3;
                return true;
            }

            if (legal(pos))
            {
                if (loinEntree(pos))
                {
                    sortie = pos;
                    return true;
                }
            }
            return false;
        }

        //vérifie si la sortie est suffisament loin de l'entrée
        private bool loinEntree(int pos)
        {
            //position sur la première ligne
            if (pos >= 0 && pos < largeur && entree >= 0 && entree < largeur)
                return false;
            //position sur la première colonne
            if (pos % largeur == 0 && entree % largeur == 0)
                return false;
            //dernière ligne
            if (pos >= dimension - largeur && pos < dimension && entree >= dimension - largeur && entree < dimension)
                return false;
            //dernière colonne
            if (pos % largeur == largeur - 1 && entree % largeur == largeur - 1)
                return false;
            //si est à plus de 2 arêtes de distance
            if (pos - largeur - 1 == entree || pos - largeur + 1 == entree || pos + largeur - 1 == entree || pos + largeur + 1 == entree)
                return false;
            return true;

        }

        //vérifie si la position est sur un coté
        private bool legal(int pos)
        {
            //position sur la première ligne
            if (pos >= 0 && pos < largeur)
                return true;
            //position sur la première colonne
            if (pos % largeur == 0)
                return true;
            //dernière ligne
            if (pos >= dimension - largeur && pos < dimension)
                return true;
            //dernière colonne
            if (pos % largeur == largeur - 1)
                return true;
            return false;
        }

        //remplissage du graphe avec les poids des arêtes
        public void initGraphe()
        {
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

        //calcul de l'arbre sous-tendant minimal
        public void Prim()
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
                        eval(pos);
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
            }
        }

        //évaluation des arêtes
        private void eval(int noeud)
        {
            //est
            //si l'arête n'a pas déjà été utilisée et le noeud à droite existe
            if (graphe[noeud, 0] > 0)
            {   //si le noeud à droite a déjà été visité
                if (visites[noeud + 1] == 1)
                    graphe[noeud, 0] = -1;
                else
                   if (graphe[noeud, 0] > poids)
                {
                    selection = noeud + 1;
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
                    selection = noeud + largeur;
                    poids = graphe[noeud, 1];
                    direction = 's';
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
                }
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
            if ((noeud - largeur) > -1)
            {
                if (visites[noeud - largeur] == 1)
                {
                    if (graphe[noeud - largeur, 1] > 0)
                        graphe[noeud - largeur, 1] = -1;
                }
                else
                    if (graphe[noeud - largeur, 1] > poids)
                {
                    selection = noeud - largeur;
                    poids = graphe[noeud - largeur, 1];
                    direction = 'n';
                }
            }
        }

        //calcul du graphe de densité
        //256 passe, après afficher en valeur RGB
        //bool rdnES:réinicier entrée-sortie à chaque passe
        //bool rdnGraphe: réinicier le graphe à chaque passe
        public void densite(bool rdnES=false, bool rdnGraphe=true)
        {
            grapheDensite = new int[dimension, 2];
            //au moins un des deux
            if (rdnES || rdnGraphe)
            {
                for (int i = 0; i < 256; i++)
                {
                    if (rdnES)
                    {
                        setEntree();
                        setSortie(); 
                    }
                    if (rdnGraphe)
                        initGraphe();
                    else
                        copieOriginaltoGraphe();

                    Prim();
                    for (int j = 0; j < dimension; j++)
                    {
                        if (graphe[j, 0] == 0)
                            grapheDensite[j, 0]++;
                        if (graphe[j, 1] == 0)
                            grapheDensite[j, 1]++;
                    }
                }
            }
        }

        private void copieGraphetoOriginal()
        {
            grapheOriginal = new int[dimension, 2];
            for (int i = 0; i < dimension; i++)
            {
                grapheOriginal[i, 0] = graphe[i, 0];
                grapheOriginal[i, 1] = graphe[i, 1];
            }
        }

        private void copieOriginaltoGraphe()
        {
            for (int i = 0; i < dimension; i++)
            {
                graphe[i, 0] = grapheOriginal[i, 0];
                graphe[i, 1] = grapheOriginal[i, 1];
            }
        }
    }
}
