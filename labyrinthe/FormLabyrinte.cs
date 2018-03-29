using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace labyrinthe
{
    public partial class FormLabyrinte : Form
    {
        private int largeur;
        private int hauteur;
        public static FormLabyrinte UI;


        public FormLabyrinte()
        {
            InitializeComponent();
            UI = this;
        }

        public void setCounterPrim(int itr, int aff, int comp)
        {
            lblCounterPrim.Text = "Prim: " + itr + " iterations\n\n" +
                                  "Assigantions:   " + aff + "\nComparaisons:  " + comp;
        }

        public void setCounterInit(int nbOp)
        {
            lblCounterInit.Text = "Init: " + nbOp + " Opérations";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            lblCounterAffichage.Text = "Affichage: ";
            lblCounterPrim.Text = "Prim: ";

            double width = 0;
            double height = 0;
            double step = 0;
            int i2, j2, M_size;

            if (String.IsNullOrEmpty(txtLargeur.Text) || String.IsNullOrEmpty(txtHauteur.Text))
            {
                MessageBox.Show("Vous devez entrer une largeur et une hauteur avant de generer un labirythe", "Message d'avertissement !",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            try
            {
                largeur = Int32.Parse(txtLargeur.Text);
                hauteur = Int32.Parse(txtHauteur.Text);

                if (largeur >= hauteur)
                {
                    M_size = largeur;
                }
                else
                {
                    M_size = hauteur;
                }

                if (M_size < 36)
                {
                    width = height = 0.025 * (M_size * M_size) - (1.75 * M_size) + 37.6;
                }
                else if (M_size >= 36 && M_size < 101)
                {
                    width = height = -0.0615384615 * M_size + 8.153846154;
                }
                else
                {
                    MessageBox.Show("La limite d'afichage est de 100 noeuds de large ou de haut, vous devez entrer une valeur en dessous de la limite", "Message d'avertissement !",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                step = width + 1;
                int initComp = 0, primComp = 0, initIttr=0,initAff=0, primIttr=0, primAff=0;
                CGraphe monGraphe = new CGraphe(largeur, hauteur, 3, ref initComp, ref initAff, ref initIttr);
                monGraphe.Prim(ref primIttr, ref primAff, ref primComp);

                int nbAr = CCalcul.nBase(largeur, hauteur);

                lblCounterInit.Text = "Init: " + initIttr + " itérations\n\n"+initAff+ " affectations\n\n"
                    + initComp + " comparaisons\n\n\n\nNb arêtes au total: "+nbAr;
                lblCounterPrim.Text = "Prim: " + primIttr + " itérations\n\n" + primAff + " affectations\n\n"
                    + primComp + " comparaisons\n\n\n\nNb arêtes choisies: " + (largeur*hauteur-1);

                int[,] g1 = monGraphe.getGraphe();

                using (Graphics g = this.CreateGraphics())
                {
                    g.Clear(SystemColors.Control); //Clear the draw area
                    using (Pen pen = new Pen(Color.Black, 2))
                    {
                        for (int i = 0; i <= (g1.Length / 2) - 1; i++)
                        {

                            lblCounterAffichage.Text = "Affichage: " + ((i + 1) * 5 + (i + 1) * 2) + " opérations";


                            int pos_i = i / largeur;
                            int pos_j = i % largeur;

                            i2 = 2 * pos_i;
                            j2 = 2 * pos_j;

                            Rectangle rect = new Rectangle(new Point(5 + (int)step * j2, 5 + (int)step * i2), new Size((int)width, (int)height));
                            g.DrawRectangle(pen, rect);
                            g.FillRectangle(System.Drawing.Brushes.Black, rect);

                            if (g1[i, 0] == 0)
                            {
                                i2 = 2 * pos_i;
                                j2 = (2 * pos_j) + 1;

                                rect = new Rectangle(new Point(5 + (int)step * j2, 5 + (int)step * i2), new Size((int)width, (int)height));
                                g.DrawRectangle(pen, rect);
                                g.FillRectangle(System.Drawing.Brushes.Black, rect);
                            }

                            if (g1[i, 1] == 0)
                            {
                                i2 = (2 * pos_i) + 1;
                                j2 = 2 * pos_j;

                                rect = new Rectangle(new Point(5 + (int)step * j2, 5 + (int)step * i2), new Size((int)width, (int)height));
                                g.DrawRectangle(pen, rect);
                                g.FillRectangle(System.Drawing.Brushes.Black, rect);
                            }
                        }
                    }
                }
            }
            catch (System.FormatException e1)
            {

            }
        }
    }
}
