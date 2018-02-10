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
            public Position Pos;
            public String Direction{ get; set; }
            public int Valeur { get; set; }
            public Boolean Visited { get; set; }
        }

        Position pos = new Position
        {
            Ligne = 0,
            Colonne = 0
        };

        Dictionary<Position,Noeud> dictionary = new Dictionary<Position, Noeud>();
        private int[,] tableauHorizontal;
        private int[,] tableauVertical;
        private int nbLignes;
        private int nbColonnes;

        public Prim(int nbL,int nbC)
        {
            nbLignes = nbL;
            nbColonnes = nbC;
            int nbNoeuds = nbLignes * nbColonnes;
            int nbAretes = (nbLignes * (nbColonnes - 1)) + (nbColonnes * (nbLignes - 1)); //pas certain que cet info serve a quelque chose
            tableauHorizontal = new int[nbLignes, nbColonnes - 1];
            tableauVertical = new int[nbLignes - 1, nbColonnes];

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
                        Visited = false,
                        Pos = position
                    };

                    dictionary.Add(position, noeud);
                }
            }
       
            Position now = ProcessNode(dictionary[pos]);
            Console.WriteLine("Position actuel: [" + now.Ligne + "," + now.Colonne + "]");
            Position now2 = ProcessNode(dictionary[now]);
            Console.WriteLine("Position actuel: [" + now2.Ligne + "," + now2.Colonne + "]");
        }

        private Position ProcessNode(Noeud node)
        {
            Position result = new Position
            {
                Ligne = node.Pos.Ligne,
                Colonne = node.Pos.Colonne
            };

            Position resultUP = new Position
            {
                Ligne = node.Pos.Ligne -1,
                Colonne = node.Pos.Colonne
            };

            Position resultDOWN = new Position
            {
                Ligne = node.Pos.Ligne + 1,
                Colonne = node.Pos.Colonne
            };

            Position resultLEFT = new Position
            {
                Ligne = node.Pos.Ligne,
                Colonne = node.Pos.Colonne - 1
            };

            Position resultRIGHT = new Position
            {
                Ligne = node.Pos.Ligne,
                Colonne = node.Pos.Colonne + 1
            };

            int valeurAreteLeft;
            int valeurAreteRight;
            int valeurAreteUp;
            int valeurAreteDown;

            //Position 0,0 Depart, pas de verification si les noeuds adjacents ont ete visite
            if (result.Ligne == 0 && result.Colonne == 0)
            {
                //On va chercher la valeur des aretes adjacentes
                valeurAreteRight = tableauHorizontal[result.Ligne, result.Colonne];
                valeurAreteDown = tableauVertical[result.Ligne, result.Colonne];

                //On verifie l'arete la plus courte, on change la valeur de l'arete la plus courte pour 11, on ajuste la position de result.
                //down
                if (valeurAreteRight > valeurAreteDown)
                {
                    tableauVertical[result.Ligne, result.Colonne] = 11;
                    Console.WriteLine("Position actuel: [" + result.Ligne + "," + result.Colonne + "]\n");
                    result.Ligne = result.Ligne + 1;
                }
                //right
                else
                {
                    tableauHorizontal[result.Ligne, result.Colonne] = 11;
                    Console.WriteLine("Position actuel: [" + result.Ligne + "," + result.Colonne + "]\n");
                    result.Colonne = result.Colonne + 1;
                }
            }

            //TODO faire les fonctions suivantes ici
            //
            //Si la Position du Noeud est sur la Colonne = 0 && Ligne = nbLignes, traiter RIGHT, UP
            //Si la Position du Noeud est sur la Colonne = nbColonne && Ligne = nbLignes, traiter LEFT, UP
            //Si la Position du Noeud est sur la Colonne = nbColonne && Ligne = 0, traiter LEFT, DOWN
            //
            //TODO fin

            //Si la Position du Noeud est sur la ligne = 0
            else if (result.Ligne == 0)
            {
                //On va chercher la valeur des aretes adjacentes
                valeurAreteRight = tableauHorizontal[result.Ligne, result.Colonne];
                valeurAreteDown = tableauVertical[result.Ligne, result.Colonne];
                valeurAreteLeft = tableauHorizontal[result.Ligne, result.Colonne - 1];

                //On verifie que les noeuds au bout de ces arretes n'ont pas deja ete visite, si oui on change la valeur de l'arete pour 33-34 ou 35
                if(dictionary[resultDOWN].Visited==true)
                {
                    if (tableauVertical[result.Ligne, result.Colonne] != 11)
                        tableauVertical[result.Ligne, result.Colonne] = 33;
                }
                else if(dictionary[resultRIGHT].Visited == true)
                {
                    if (tableauHorizontal[result.Ligne, result.Colonne] != 11)
                        tableauHorizontal[result.Ligne, result.Colonne] = 34;
                }
                else if (dictionary[resultLEFT].Visited == true)
                {
                    if (tableauHorizontal[result.Ligne, result.Colonne - 1] != 11)
                        tableauHorizontal[result.Ligne, result.Colonne - 1] = 35;
                }
                
                //On verifie l'arete la plus courte, on change la valeur de l'arete la plus courte pour 11, on ajuste la position de result.
                //down
                if (valeurAreteRight > valeurAreteDown && valeurAreteLeft > valeurAreteDown)
                {
                    tableauVertical[result.Ligne, result.Colonne] = 11;
                    Console.WriteLine("Position actuel: [" + result.Ligne + "," + result.Colonne + "]\n");
                    result.Ligne = result.Ligne + 1;
                }
                //right
                else if (valeurAreteRight < valeurAreteDown && valeurAreteRight < valeurAreteLeft)
                {
                    tableauHorizontal[result.Ligne, result.Colonne] = 11;
                    Console.WriteLine("Position actuel: [" + result.Ligne + "," + result.Colonne + "]\n");
                    result.Colonne = result.Colonne + 1;
                }
                //left
                else
                {
                    tableauHorizontal[result.Ligne, result.Colonne] = 11;
                    Console.WriteLine("Position actuel: [" + result.Ligne + "," + result.Colonne + "]\n");
                    result.Colonne = result.Colonne - 1;
                }
            }

            //Si la Position du Noeud est sur la ligne = nbLignes, traiter RIGHT, UP, LEFT
            else if (result.Ligne == nbLignes - 1)
            {
                //On va chercher la valeur des aretes adjacentes
                valeurAreteRight = tableauHorizontal[result.Ligne, result.Colonne];
                valeurAreteUp = tableauVertical[result.Ligne, result.Colonne];
                valeurAreteLeft = tableauHorizontal[result.Ligne, result.Colonne - 1];

                //On verifie que les noeuds au bout de ces arretes n'ont pas deja ete visite, si oui on change la valeur de l'arete pour 33-34 ou 35
                if (dictionary[resultUP].Visited == true)
                {
                    if (tableauVertical[result.Ligne - 1, result.Colonne] != 11)
                        tableauVertical[result.Ligne - 1, result.Colonne] = 33;
                }
                else if (dictionary[resultRIGHT].Visited == true)
                {
                    if (tableauHorizontal[result.Ligne, result.Colonne] != 11)
                        tableauHorizontal[result.Ligne, result.Colonne] = 34; 
                }
                else if (dictionary[resultLEFT].Visited == true)
                {
                    if (tableauHorizontal[result.Ligne, result.Colonne - 1] != 11)
                        tableauHorizontal[result.Ligne, result.Colonne - 1] = 35;
                }

                //On verifie l'arete la plus courte, on change la valeur de l'arete la plus courte pour 11, on ajuste la position de result.
                //Up
                if (valeurAreteRight > valeurAreteUp && valeurAreteLeft > valeurAreteUp)
                {
                    tableauVertical[result.Ligne - 1, result.Colonne] = 11;
                    Console.WriteLine("Position actuel: [" + result.Ligne + "," + result.Colonne + "]\n");
                    result.Ligne = result.Ligne - 1;
                }
                //right
                else if (valeurAreteRight < valeurAreteUp && valeurAreteRight < valeurAreteLeft)
                {
                    tableauHorizontal[result.Ligne, result.Colonne] = 11;
                    Console.WriteLine("Position actuel: [" + result.Ligne + "," + result.Colonne + "]\n");
                    result.Colonne = result.Colonne + 1;
                }
                //left
                else
                {
                    tableauHorizontal[result.Ligne, result.Colonne] = 11;
                    Console.WriteLine("Position actuel: [" + result.Ligne + "," + result.Colonne + "]\n");
                    result.Colonne = result.Colonne - 1;
                }
            }
            //Si la Position du Noeud est sur la Colonne = 0, traiter RIGHT, UP, DOWN
            else if (result.Colonne == 0)
            {

            }
            //Si la Position du Noeud est sur la Colonne = nbColonnes, traiter LEFT, UP, DOWN
            else if (result.Colonne == nbColonnes - 1)
            {

            }
            //Est au centre, les 4 directions sont a prendre en compte
            else
            {

            }

            node.Visited = true;
            //TODO ajouter le noeud dans un array de noeuds traiter
            return result;
        }
    }
}