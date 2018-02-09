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
            public Boolean Visited { get; set; }
        }

        Position pos = new Position
        {
            Ligne = 0,
            Colonne = 0
        };

        Dictionary<Position,Noeud> dictionary = new Dictionary<Position, Noeud>();

        public Prim(int nbLignes,int nbColonnes)
        {

            int nbNoeuds = nbLignes * nbColonnes;
            int nbAretes = (nbLignes * (nbColonnes - 1)) + (nbColonnes * (nbLignes - 1)); //pas certain que cet info serve a quelque chose
            int[,] tableauHorizontal = new int[nbLignes, nbColonnes - 1];
            int[,] tableauVertical = new int[nbLignes - 1, nbColonnes];

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
                        Visited = false
                    };

                    dictionary.Add(position, noeud);

                }
            }

            pos.Ligne = pos.Ligne + 1;
            pos.Colonne = pos.Colonne + 1;

            if (dictionary.ContainsKey(pos))
            {
                Noeud node = dictionary[pos];
                if(node.Visited == false)
                Console.WriteLine("not visited!" + " " + pos.Ligne + "," + pos.Colonne);
                Console.WriteLine("valeur arrete horizontal: " + tableauHorizontal[pos.Ligne,pos.Colonne]);
                Console.WriteLine("valeur arrete vertical: " + tableauVertical[pos.Ligne, pos.Colonne]);
            }
        }
    }
}