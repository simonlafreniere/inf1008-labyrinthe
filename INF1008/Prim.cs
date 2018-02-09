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
            Console.WriteLine("");
            for (int i = 0; i < nbLignes - 1; i++)
            {
                for (int j = 0; j < nbColonnes; j++)
                {
                    tableauVertical[i, j] = rnd.Next(1, 10);
                    Console.WriteLine("[{0}, {1}] = {2}", i, j, tableauVertical[i, j]);//debug
                }
            }
            Console.WriteLine("");
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

            int valeurArete;
            valeurArete = tableauHorizontal[result.Ligne, result.Colonne];

            if (valeurArete > tableauVertical[result.Ligne, result.Colonne])
            {
                valeurArete = tableauVertical[result.Ligne, result.Colonne];
                node.Valeur = tableauHorizontal[result.Ligne, result.Colonne];
                node.Direction = "Horizontal";
                tableauVertical[result.Ligne, result.Colonne] = 11;
                Console.WriteLine(node.Direction + " " + node.Valeur);
                Console.WriteLine("Position actuel: [" + result.Ligne + "," + result.Colonne + "]\n");
                result.Ligne = result.Ligne + 1;
            }
            else
            {
                node.Valeur = tableauVertical[result.Ligne, result.Colonne];
                node.Direction = "Vertical";
                tableauHorizontal[result.Ligne, result.Colonne] = 11;
                Console.WriteLine(node.Direction + " " + node.Valeur);
                Console.WriteLine("Position actuel: [" + result.Ligne + "," + result.Colonne + "]\n");
                result.Colonne = result.Colonne + 1;
            }

            //TODO ajouter le noeud initial dans un array de noeuds traiter
            return result;
        }
    }
}