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
        int[,] tableauHorizontal;
        int[,] tableauVertical;
        public Prim(int nbLignes,int nbColonnes)
        {

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

            for (int i = 0; i < nbLignes - 1; i++)
            {
                for (int j = 0; j < nbColonnes; j++)
                {
                    tableauVertical[i, j] = rnd.Next(1, 10);
                    Console.WriteLine("[{0}, {1}] = {2}", i, j, tableauVertical[i, j]);//debug
                }
            }

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


            Noeud node = ProcessPos(pos);
            Console.WriteLine("Position actuel: [" + node.Pos.Ligne + "," + node.Pos.Colonne + "]");


        }

        private Noeud ProcessPos(Position pos)
        {
            int valeurArete;
            Noeud node = dictionary[pos];
            valeurArete = tableauHorizontal[pos.Ligne, pos.Colonne];

            if (valeurArete > tableauVertical[pos.Ligne, pos.Colonne])
            {
                valeurArete = tableauVertical[pos.Ligne, pos.Colonne];
                node.Valeur = tableauHorizontal[pos.Ligne, pos.Colonne];
                node.Direction = "Horizontal";
                tableauVertical[pos.Ligne, pos.Colonne] = 11;
                pos.Ligne = pos.Ligne + 1;
            }
            else
            {
                node.Valeur = tableauVertical[pos.Ligne, pos.Colonne];
                node.Direction = "Vertical";
                tableauHorizontal[pos.Ligne, pos.Colonne] = 11;
                pos.Colonne = pos.Colonne + 1;
            }

            Console.WriteLine(node.Direction + " " + node.Valeur);
            Console.WriteLine("Position actuel: [" + pos.Ligne + "," + pos.Colonne + "]");

            return dictionary[pos];
        }
    }
}