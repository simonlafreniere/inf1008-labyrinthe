using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace INF1008
{
    internal class Prim
    {
        public struct Position
        {
            public int Ligne { get; set; }
            public int Colonne { get; set; }
        }


        public struct Noeud
        {
            public int Predecesseur { get; set; }
            public String Direction { get; set; }
            public int Poids { get; set; }
            public Boolean Permanent { get; set; }
            public Position Pos { get; internal set; }
        }

        private Noeud[,] tableauNoeuds;
        private int[,] tableauHorizontal;
        private int[,] tableauVertical;
        private int nbLignes;
        private int nbColonnes;
        private int nbNoeuds;
        private int itr = 0;
        private int ittr = 0;
        private int ProcessLine = 0;
        private int ProcessColumn = 0;
        private Noeud[] NoeudsAdjacent;
        private Noeud[] NoeudsTraite;
        private Noeud noeudCourant;

        public Prim(int nbL, int nbC)
        {
            nbLignes = nbL;
            nbColonnes = nbC;
            nbNoeuds = nbLignes * nbColonnes;
            int nbAretes = (nbLignes * (nbColonnes - 1)) + (nbColonnes * (nbLignes - 1)); //pas certain que cet info serve a quelque chose
            tableauHorizontal = new int[nbLignes, nbColonnes - 1];
            tableauVertical = new int[nbLignes - 1, nbColonnes];
            tableauNoeuds = new Noeud[nbLignes, nbColonnes];

            Random rnd = new Random();
            for (int i = 0; i < nbLignes; i++)
            {
                for (int j = 0; j < nbColonnes - 1; j++)
                {
                    tableauHorizontal[i, j] = rnd.Next(1, 10);
                    Console.WriteLine("[{0}, {1}] = {2}", i, j, tableauHorizontal[i, j]);//debug
                }
            }
            Console.WriteLine("");//debug
            for (int i = 0; i < nbLignes - 1; i++)
            {
                for (int j = 0; j < nbColonnes; j++)
                {
                    tableauVertical[i, j] = rnd.Next(1, 10);
                    Console.WriteLine("[{0}, {1}] = {2}", i, j, tableauVertical[i, j]);//debug
                }
            }
            Console.WriteLine("");//debug
            for (int i = 0; i < nbLignes; i++)
            {
                for (int j = 0; j < nbColonnes; j++)
                {
                    Position position = new Position
                    {
                        Ligne = i,
                        Colonne = j
                    };

                    Noeud noeud = new Noeud
                    {
                        Pos = position,
                        Permanent = false,
                        Predecesseur = -1,
                        Poids = 999
                    };

                    tableauNoeuds[i, j] = noeud;
                }
            }

            Start();

            //debug for loop
            for (int i = 0; i < itr; i++)
            {
                Console.WriteLine("Noeud: [" + NoeudsAdjacent[i].Pos.Ligne + "," + NoeudsAdjacent[i].Pos.Colonne + "] Valeur Poids Adj: " + NoeudsAdjacent[i].Poids + "   Direction: " + NoeudsAdjacent[i].Direction + "    Predecesseur: [" + NoeudsTraite[NoeudsAdjacent[i].Predecesseur].Pos.Ligne + "," + NoeudsTraite[NoeudsAdjacent[i].Predecesseur].Pos.Colonne + "]");
            }
            for (int i = 0; i < ittr; i++)
            {
                Console.WriteLine("Noeud: [" + NoeudsTraite[i].Pos.Ligne + "," + NoeudsTraite[i].Pos.Colonne + "] Valeur Poids Traite: " + NoeudsTraite[i].Poids);
            }


            /*    for (int i = 0; i < nbNoeuds; i++)
                {
                    ProcessNode();
               }
               */
        }

        private void Start()
        {
            //On initialise les vecteurs de depart
            NoeudsAdjacent = new Noeud[nbNoeuds];
            NoeudsTraite = new Noeud[nbNoeuds];

            //On ajuste les parametres du noeud de depart
            tableauNoeuds[0, 0].Permanent = true;
            tableauNoeuds[0, 0].Poids = 0;

            //On ajoute le noeud de depart au vecteur des NoeudsTraite
            NoeudsTraite[ittr++] = tableauNoeuds[0, 0];

            //On va chercher la valeur des aretes adjacentes
            int valeurAreteRight = tableauHorizontal[0, 0];
            int valeurAreteDown = tableauVertical[0, 0];

            //On verifie l'arete la plus courte, on ajoute le noeud correspondant au vecteur en .
            //down
            if (valeurAreteRight > valeurAreteDown)
            {
                //On ajuste les parametres du noeud candidat
                tableauNoeuds[1, 0].Permanent = true;
                tableauNoeuds[1, 0].Predecesseur = 0;
                tableauNoeuds[1, 0].Poids = tableauVertical[0, 0];

                //On ajuste les parametres du noeud adjacent restant au noeud de depart
                tableauNoeuds[0, 1].Predecesseur = 0;
                tableauNoeuds[0, 1].Direction = "Horizontal";
                tableauNoeuds[0, 1].Poids = tableauHorizontal[0, 0];

                //On change la valeur de l'arete la plus courte pour '11'
                tableauVertical[0, 0] = 11;

                //On ajoute les noeuds a leur vecteur respectifs
                NoeudsTraite[ittr++] = tableauNoeuds[1, 0];
                noeudCourant = tableauNoeuds[1, 0];
                NoeudsAdjacent[itr++] = tableauNoeuds[0, 1];

            }
            //right
            else
            {
                //On ajuste les parametres du noeud candidat
                tableauNoeuds[0, 1].Permanent = true;
                tableauNoeuds[0, 1].Predecesseur = 0;
                tableauNoeuds[0, 1].Poids = tableauHorizontal[0, 0];

                //On ajuste les parametres du noeud adjacent restant au noeud de depart
                tableauNoeuds[1, 0].Poids = tableauVertical[0, 0];
                tableauNoeuds[1, 0].Direction = "Vertical";
                tableauNoeuds[1, 0].Predecesseur = 0;

                //On change la valeur de l'arete la plus courte pour '11'
                tableauHorizontal[0, 0] = 11;

                //On ajoute les noeuds a leur vecteur respectifs
                NoeudsTraite[ittr++] = tableauNoeuds[0, 1];
                noeudCourant = tableauNoeuds[0, 1];
                NoeudsAdjacent[itr++] = tableauNoeuds[1, 0];
            }
        }



        private void ProcessNode()
        {
            int ligne = noeudCourant.Pos.Ligne;
            int colonne = noeudCourant.Pos.Colonne;

            int valeurAreteLeft;
            int valeurAreteRight;
            int valeurAreteUp;
            int valeurAreteDown;

        
            //TODO faire les fonctions suivantes ici
            //
            //Si la Position du Noeud est sur la Colonne = 0 && Ligne = nbLignes, traiter RIGHT, UP
            //Si la Position du Noeud est sur la Colonne = nbColonne && Ligne = nbLignes, traiter LEFT, UP
            //Si la Position du Noeud est sur la Colonne = nbColonne && Ligne = 0, traiter LEFT, DOWN
            //
            //TODO fin

            //Si la Position du Noeud est sur la ligne = 0
            if (ligne == 0)
            {
                //On va chercher la valeur des aretes adjacentes
                valeurAreteRight = tableauHorizontal[ligne, colonne];
                valeurAreteDown = tableauVertical[ligne, colonne];
                valeurAreteLeft = tableauHorizontal[ligne, colonne - 1];

                //On verifie que les noeuds au bout de ces arretes n'ont pas deja ete visite, si oui on change la valeur de l'arete pour 33-34 ou 35
                //Down
                if (tableauNoeuds[ligne + 1, colonne].Permanent == true)
                {
                    if (tableauVertical[ligne, colonne] != 11)
                        tableauVertical[ligne, colonne] = 33;
                }
                //Right
                else if (tableauNoeuds[ligne, colonne + 1].Permanent == true)
                {
                    if (tableauHorizontal[ligne, colonne] != 11)
                        tableauHorizontal[ligne, colonne] = 34;
                }
                //Left
                else if (tableauNoeuds[ligne, colonne - 1].Permanent == true)
                {
                    if (tableauHorizontal[ligne, colonne - 1] != 11)
                        tableauHorizontal[ligne, colonne - 1] = 35;
                }

                //On verifie l'arete la plus courte, on change la valeur de l'arete la plus courte pour 11, on ajuste la position de result.
                //down
                if (valeurAreteRight > valeurAreteDown && valeurAreteLeft > valeurAreteDown)
                {
                    tableauVertical[ligne, colonne] = 11;
                    Console.WriteLine("Position actuel: [" + ligne + "," + colonne + "]\n");
                    ligne = ligne + 1;
                }
                //right
                else if (valeurAreteRight < valeurAreteDown && valeurAreteRight < valeurAreteLeft)
                {
                    tableauHorizontal[ligne, colonne] = 11;
                    Console.WriteLine("Position actuel: [" + ligne + "," + colonne + "]\n");
                    colonne = colonne + 1;
                }
                //left
                else
                {
                    tableauHorizontal[ligne, colonne] = 11;
                    Console.WriteLine("Position actuel: [" + ligne + "," + colonne + "]\n");
                    colonne = colonne - 1;
                }
            }

            //Si la Position du Noeud est sur la ligne = nbLignes, traiter RIGHT, UP, LEFT
            else if (ligne == nbLignes - 1)
            {
                //On va chercher la valeur des aretes adjacentes
                valeurAreteRight = tableauHorizontal[ligne, colonne];
                valeurAreteUp = tableauVertical[ligne, colonne];
                valeurAreteLeft = tableauHorizontal[ligne, colonne - 1];

                //On verifie que les noeuds au bout de ces arretes n'ont pas deja ete visite, si oui on change la valeur de l'arete pour 33-34 ou 35
                //Up
                if (tableauNoeuds[ligne - 1, colonne].Permanent == true)
                {
                    if (tableauVertical[ligne - 1, colonne] != 11)
                        tableauVertical[ligne - 1, colonne] = 33;
                }
                //Right
                else if (tableauNoeuds[ligne, colonne + 1].Permanent == true)
                {
                    if (tableauHorizontal[ligne, colonne] != 11)
                        tableauHorizontal[ligne, colonne] = 34;
                }
                //Left
                else if (tableauNoeuds[ligne, colonne - 1].Permanent == true)
                {
                    if (tableauHorizontal[ligne, colonne - 1] != 11)
                        tableauHorizontal[ligne, colonne - 1] = 35;
                }

                //On verifie l'arete la plus courte, on change la valeur de l'arete la plus courte pour 11, on ajuste la position de result.
                //Up
                if (valeurAreteRight > valeurAreteUp && valeurAreteLeft > valeurAreteUp)
                {
                    tableauVertical[ligne - 1, colonne] = 11;
                    Console.WriteLine("Position actuel: [" + ligne + "," + colonne + "]\n");
                    ligne = ligne - 1;
                }
                //right
                else if (valeurAreteRight < valeurAreteUp && valeurAreteRight < valeurAreteLeft)
                {
                    tableauHorizontal[ligne, colonne] = 11;
                    Console.WriteLine("Position actuel: [" + ligne + "," + colonne + "]\n");
                    colonne = colonne + 1;
                }
                //left
                else
                {
                    tableauHorizontal[ligne, colonne] = 11;
                    Console.WriteLine("Position actuel: [" + ligne + "," + colonne + "]\n");
                    colonne = colonne - 1;
                }
            }
            //Si la Position du Noeud est sur la Colonne = 0, traiter RIGHT, UP, DOWN
            else if (colonne == 0)
            {

            }
            //Si la Position du Noeud est sur la Colonne = nbColonnes, traiter LEFT, UP, DOWN
            else if (colonne == nbColonnes - 1)
            {

            }
            //Est au centre, les 4 directions sont a prendre en compte
            else
            {

            }

            //TODO  Parcourrir le vecteur des NoeudsAdjacent pour trouver la plus petite arete du graphe qui pointe vers un noeud non visiter
            //      changer la valeur pour 11 de cet arrete dans le tableau correspondant Vertical ou Horizontal et se positionner le noeudCourant
            //      pointer par cet arrete, changer les parametres du noeud Courant


            //Ajouter le noeud dans un array de noeuds traiter
            tableauNoeuds[ligne, colonne].Permanent = true;
            NoeudsTraite[itr++] = tableauNoeuds[ligne, colonne];
            itr++;

            ProcessLine = ligne;
            ProcessColumn = colonne;


            for (int i = 0; i < itr; i++)
            {
                Console.WriteLine("Noeud: [" + NoeudsTraite[i].Pos.Ligne + "," + NoeudsTraite[i].Pos.Colonne + "]");
            }
        }
    }
}