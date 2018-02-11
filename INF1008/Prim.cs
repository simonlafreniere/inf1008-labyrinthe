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
        private List<Noeud> NoeudsAdjacent = new List<Noeud>();
        private Noeud[] NoeudsTraite;
        private Noeud noeudCourant;

        public Prim(int nbL, int nbC)
        {
            nbLignes = nbL;
            nbColonnes = nbC;
            nbNoeuds = nbLignes * nbColonnes;
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
                        Poids = 999,
                        Direction = null
                    };

                    tableauNoeuds[i, j] = noeud;
                }
            }

            //Methode qui traite le point de depart pour creer le premier segment du graphe
            Start();
            TrouverAdjacent();
            TrouverAdjacent(); TrouverAdjacent();

            //debug for loop
            for (int i = 0; i < itr; i++)
            {
                Console.WriteLine("Noeud: [" + NoeudsAdjacent[i].Pos.Ligne + "," + NoeudsAdjacent[i].Pos.Colonne + "] ADJACENTE:   Valeur Poids: " + NoeudsAdjacent[i].Poids + "   Direction: " + NoeudsAdjacent[i].Direction + "    Predecesseur: [" + NoeudsTraite[NoeudsAdjacent[i].Predecesseur].Pos.Ligne + "," + NoeudsTraite[NoeudsAdjacent[i].Predecesseur].Pos.Colonne + "]");
            }
            for (int i = 0; i < ittr; i++)
            {
                Console.WriteLine("Noeud: [" + NoeudsTraite[i].Pos.Ligne + "," + NoeudsTraite[i].Pos.Colonne + "] TRAITE:   Valeur Poid: " + NoeudsTraite[i].Poids);
            }
        }

        private void Start()
        {
            //On initialise les vecteurs de depart
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
                NoeudsAdjacent.Add(tableauNoeuds[0, 1]);

                //On designe le dernier noeud comme noeudCourant
                noeudCourant = tableauNoeuds[1, 0];
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
                NoeudsAdjacent.Add(tableauNoeuds[1, 0]);

                //On designe le dernier noeud comme noeudCourant
                noeudCourant = tableauNoeuds[0, 1];
            }
        }



        private void TrouverAdjacent()
        {
            Console.WriteLine("step");
            //On recupere la position du noeudCourant
            int ligne = noeudCourant.Pos.Ligne;
            int colonne = noeudCourant.Pos.Colonne;

            //Si la Position du Noeud est sur la ligne = 0
            if (ligne == 0 && colonne != 0 && colonne != nbColonnes - 1)
            {
                //On verifie si les noeuds au bout de ces arretes sont permanent, si non on ajoute les noeuds au tableau noeudsAdjacent
                //Down
                if (tableauNoeuds[ligne + 1, colonne].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne + 1, colonne].Poids = tableauVertical[ligne, colonne];
                    tableauNoeuds[ligne + 1, colonne].Direction = "down";
                    tableauNoeuds[ligne + 1, colonne].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne + 1, colonne]);
                    itr++;
                }

                //Right
                if (tableauNoeuds[ligne, colonne + 1].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne, colonne + 1].Poids = tableauHorizontal[ligne, colonne];
                    tableauNoeuds[ligne, colonne + 1].Direction = "right";
                    tableauNoeuds[ligne, colonne + 1].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne, colonne + 1]);
                    itr++;

                }

                //Left
                if (tableauNoeuds[ligne, colonne - 1].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne, colonne - 1].Poids = tableauHorizontal[ligne, colonne - 1];
                    tableauNoeuds[ligne, colonne - 1].Direction = "left";
                    tableauNoeuds[ligne, colonne - 1].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne, colonne - 1]);
                }
            }

            //Si la Position du Noeud est sur la ligne = nbLignes, traiter RIGHT, UP, LEFT
            else if (ligne == nbLignes - 1 & colonne != 0 && colonne != nbColonnes - 1)
            {
                //On verifie si les noeuds au bout de ces arretes sont permanent, si non on ajoute les noeuds au tableau noeudsAdjacent
                //Up
                if (tableauNoeuds[ligne - 1, colonne].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne - 1, colonne].Poids = tableauVertical[ligne - 1, colonne];
                    tableauNoeuds[ligne - 1, colonne].Direction = "up";
                    tableauNoeuds[ligne - 1, colonne].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne - 1, colonne]);
                }

                //Right
                if (tableauNoeuds[ligne, colonne + 1].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne, colonne + 1].Poids = tableauHorizontal[ligne, colonne];
                    tableauNoeuds[ligne, colonne + 1].Direction = "right";
                    tableauNoeuds[ligne, colonne + 1].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne, colonne + 1]);
                    itr++;

                }

                //Left
                if (tableauNoeuds[ligne, colonne - 1].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne, colonne - 1].Poids = tableauHorizontal[ligne, colonne - 1];
                    tableauNoeuds[ligne, colonne - 1].Direction = "left";
                    tableauNoeuds[ligne, colonne - 1].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne, colonne - 1]);
                    itr++;
                }
            }

            //Si la Position du Noeud est sur la Colonne = 0, traiter RIGHT, UP, DOWN
            else if (colonne == 0 && ligne != 0 && ligne != nbLignes - 1)
            {
                //On verifie si les noeuds au bout de ces arretes sont permanent, si non on ajoute les noeuds au tableau noeudsAdjacent
                //Up
                if (tableauNoeuds[ligne - 1, colonne].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne - 1, colonne].Poids = tableauVertical[ligne - 1, colonne];
                    tableauNoeuds[ligne - 1, colonne].Direction = "up";
                    tableauNoeuds[ligne - 1, colonne].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne - 1, colonne]);
                    itr++;
                }

                //Right
                if (tableauNoeuds[ligne, colonne + 1].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne, colonne + 1].Poids = tableauHorizontal[ligne, colonne];
                    tableauNoeuds[ligne, colonne + 1].Direction = "right";
                    tableauNoeuds[ligne, colonne + 1].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne, colonne + 1]);
                    itr++;
                }

                //Down
                if (tableauNoeuds[ligne + 1, colonne].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne + 1, colonne].Poids = tableauVertical[ligne, colonne];
                    tableauNoeuds[ligne + 1, colonne].Direction = "down";
                    tableauNoeuds[ligne + 1, colonne].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne + 1, colonne]);
                }
            }

            //Si la Position du Noeud est sur la Colonne = nbColonnes, traiter LEFT, UP, DOWN
            else if (colonne == nbColonnes - 1 && ligne != 0 && ligne != nbLignes - 1)
            {
                //On verifie si les noeuds au bout de ces arretes sont permanent, si non on ajoute les noeuds au tableau noeudsAdjacent
                //Down
                if (tableauNoeuds[ligne + 1, colonne].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne + 1, colonne].Poids = tableauVertical[ligne, colonne];
                    tableauNoeuds[ligne + 1, colonne].Direction = "down";
                    tableauNoeuds[ligne + 1, colonne].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne + 1, colonne]);
                    itr++;
                }

                //Up
                if (tableauNoeuds[ligne - 1, colonne].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne - 1, colonne].Poids = tableauVertical[ligne - 1, colonne];
                    tableauNoeuds[ligne - 1, colonne].Direction = "up";
                    tableauNoeuds[ligne - 1, colonne].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne - 1, colonne]);
                    itr++;
                }

                //Left
                if (tableauNoeuds[ligne, colonne - 1].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne, colonne - 1].Poids = tableauHorizontal[ligne, colonne - 1];
                    tableauNoeuds[ligne, colonne - 1].Direction = "left";
                    tableauNoeuds[ligne, colonne - 1].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne, colonne - 1]);
                    itr++;
                }
            }

            //Si la Position du Noeud est sur la Colonne = nbColonne && Ligne = 0, traiter LEFT, DOWN
            else if (colonne == nbColonnes - 1 && ligne == 0)
            {
                //On verifie si les noeuds au bout de ces arretes sont permanent, si non on ajoute les noeuds au tableau noeudsAdjacent
                //Down
                if (tableauNoeuds[ligne + 1, colonne].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne + 1, colonne].Poids = tableauVertical[ligne, colonne];
                    tableauNoeuds[ligne + 1, colonne].Direction = "down";
                    tableauNoeuds[ligne + 1, colonne].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne + 1, colonne]);
                    itr++;
                }

                //Left
                if (tableauNoeuds[ligne, colonne - 1].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne, colonne - 1].Poids = tableauHorizontal[ligne, colonne - 1];
                    tableauNoeuds[ligne, colonne - 1].Direction = "left";
                    tableauNoeuds[ligne, colonne - 1].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne, colonne - 1]);
                    itr++;
                }
            }

            //Si la Position du Noeud est sur la Colonne = nbColonne && Ligne = nbLignes, traiter LEFT, UP
            else if (colonne == nbColonnes - 1 && ligne == nbLignes - 1)
            {
                //On verifie si les noeuds au bout de ces arretes sont permanent, si non on ajoute les noeuds au tableau noeudsAdjacent
                //Up
                if (tableauNoeuds[ligne - 1, colonne].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne - 1, colonne].Poids = tableauVertical[ligne - 1, colonne];
                    tableauNoeuds[ligne - 1, colonne].Direction = "up";
                    tableauNoeuds[ligne - 1, colonne].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne - 1, colonne]);
                    itr++;
                }

                //Left
                if (tableauNoeuds[ligne, colonne - 1].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne, colonne - 1].Poids = tableauHorizontal[ligne, colonne - 1];
                    tableauNoeuds[ligne, colonne - 1].Direction = "left";
                    tableauNoeuds[ligne, colonne - 1].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne, colonne - 1]);
                    itr++;
                }
            }
            //Si la Position du Noeud est sur la Colonne = 0 && Ligne = nbLignes, traiter RIGHT, UP
            else if (colonne == 0 && ligne == nbLignes - 1)
            {
                //On verifie si les noeuds au bout de ces arretes sont permanent, si non on ajoute les noeuds au tableau noeudsAdjacent
                //Up
                if (tableauNoeuds[ligne - 1, colonne].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne - 1, colonne].Poids = tableauVertical[ligne - 1, colonne];
                    tableauNoeuds[ligne - 1, colonne].Direction = "up";
                    tableauNoeuds[ligne - 1, colonne].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne - 1, colonne]);
                    itr++;
                }

                //Right
                if (tableauNoeuds[ligne, colonne + 1].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne, colonne + 1].Poids = tableauHorizontal[ligne, colonne];
                    tableauNoeuds[ligne, colonne + 1].Direction = "right";
                    tableauNoeuds[ligne, colonne + 1].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne, colonne + 1]);
                    itr++;
                }
            }
            //Est au centre, les 4 directions sont a prendre en compte
            else
            {
                //On verifie si les noeuds au bout de ces arretes sont permanent, si non on ajoute les noeuds au tableau noeudsAdjacent
                //Down
                if (tableauNoeuds[ligne + 1, colonne].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne + 1, colonne].Poids = tableauVertical[ligne, colonne];
                    tableauNoeuds[ligne + 1, colonne].Direction = "down";
                    tableauNoeuds[ligne + 1, colonne].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne + 1, colonne]);
                    itr++;
                }

                //Up
                if (tableauNoeuds[ligne - 1, colonne].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne - 1, colonne].Poids = tableauVertical[ligne - 1, colonne];
                    tableauNoeuds[ligne - 1, colonne].Direction = "up";
                    tableauNoeuds[ligne - 1, colonne].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne - 1, colonne]);
                    itr++;
                }

                //Right
                if (tableauNoeuds[ligne, colonne + 1].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne, colonne + 1].Poids = tableauHorizontal[ligne, colonne];
                    tableauNoeuds[ligne, colonne + 1].Direction = "right";
                    tableauNoeuds[ligne, colonne + 1].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne, colonne + 1]);
                    itr++;
                }

                //Left
                if (tableauNoeuds[ligne, colonne - 1].Permanent == false)
                {
                    //C'est un noeud candidat, On ajuste les parametres du noeud adjacent
                    tableauNoeuds[ligne, colonne - 1].Poids = tableauHorizontal[ligne, colonne - 1];
                    tableauNoeuds[ligne, colonne - 1].Direction = "left";
                    tableauNoeuds[ligne, colonne - 1].Predecesseur = ittr - 1;

                    //On ajoute le noeud au tableau NoeudsAdjacent
                    NoeudsAdjacent.Add(tableauNoeuds[ligne, colonne - 1]);
                    itr++;
                }

            }

            // Parcourrir le vecteur des NoeudsAdjacent pour trouver la plus petite arete
            int ValeurAreteMin = 99;
            int indexNoeud = 0;
            for(int i = 0; i < itr; i++)
            {
                if (ValeurAreteMin > NoeudsAdjacent[i].Poids)
                {
                    ValeurAreteMin = NoeudsAdjacent[i].Poids;
                    indexNoeud = i;
                }
            }

            //On mets le noeud permanent sur le tableaNoeuds
            tableauNoeuds[NoeudsAdjacent.ElementAt(indexNoeud).Pos.Ligne, NoeudsAdjacent.ElementAt(indexNoeud).Pos.Colonne].Permanent = true;

            //On ajuste le tableau des aretes correspondant et on ajoute le noeud au tableau NoeudTraite
            if (NoeudsAdjacent.ElementAt(indexNoeud).Direction.Equals("down"))
            {
                tableauVertical[ligne, colonne] = 11;
                NoeudsTraite[ittr++] = tableauNoeuds[NoeudsAdjacent.ElementAt(indexNoeud).Pos.Ligne, NoeudsAdjacent.ElementAt(indexNoeud).Pos.Colonne];
            }
            else if (NoeudsAdjacent.ElementAt(indexNoeud).Direction.Equals("up"))
            {
                tableauVertical[ligne - 1, colonne] = 11;
                NoeudsTraite[ittr++] = tableauNoeuds[NoeudsAdjacent.ElementAt(indexNoeud).Pos.Ligne, NoeudsAdjacent.ElementAt(indexNoeud).Pos.Colonne];
            }
            else if (NoeudsAdjacent.ElementAt(indexNoeud).Direction.Equals("right"))
            {
                tableauHorizontal[ligne, colonne] = 11;
                NoeudsTraite[ittr++] = tableauNoeuds[NoeudsAdjacent.ElementAt(indexNoeud).Pos.Ligne, NoeudsAdjacent.ElementAt(indexNoeud).Pos.Colonne];
            }
            else if (NoeudsAdjacent.ElementAt(indexNoeud).Direction.Equals("left"))
            {
                tableauHorizontal[ligne, colonne - 1] = 11;
                NoeudsTraite[ittr++] = tableauNoeuds[NoeudsAdjacent.ElementAt(indexNoeud).Pos.Ligne, NoeudsAdjacent.ElementAt(indexNoeud).Pos.Colonne];
            }

            //Mettre le noeud comme noeudCourant
            noeudCourant = tableauNoeuds[NoeudsAdjacent.ElementAt(indexNoeud).Pos.Ligne, NoeudsAdjacent.ElementAt(indexNoeud).Pos.Colonne];

            //Retirer le noeud des noeuds adjacents
            NoeudsAdjacent.RemoveAt(indexNoeud);
            itr--;


            Console.WriteLine("Valeur Arete Min: " + ValeurAreteMin + " index Noeud: " + indexNoeud);
            Console.WriteLine("Noeud courant: [" + noeudCourant.Pos.Ligne + "," + noeudCourant.Pos.Colonne + "]");
        }
    }
}