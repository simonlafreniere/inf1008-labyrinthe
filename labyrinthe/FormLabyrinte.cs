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
        int largeur;
        int hauteur;

        public FormLabyrinte()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
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

                CGraphe monGraphe = new CGraphe(largeur, hauteur, 2);
                monGraphe.Prim();
                int[,] g1 = monGraphe.getGraphe();

                using (Graphics g = this.CreateGraphics())
                {
                    g.Clear(SystemColors.Control); //Clear the draw area
                    using (Pen pen = new Pen(Color.Black, 2))
                    {
                        for (int i = 0; i <= (g1.Length / 2) - 1; i++)
                        {
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

        private void txtNbNoeuds_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int dimension = Int32.Parse(txtNbNoeuds.Text);
                int[] facteurs = CCalcul.PGF(dimension);
                if (facteurs[0] == dimension)
                                    System.Windows.Forms.MessageBox.Show(dimension + " est un nombre premier.. la dimension d'une matrice ne peut être un nombre premier.. recomencez svp.");
                else
                {
                    txtLargeur.Text = facteurs[0].ToString();
                    txtHauteur.Text = facteurs[1].ToString();
                }
                
            }
        }

        /* private void txtNbNoeuds_TextChanged(object sender, EventArgs e)
         {
             int dimension = Int32.Parse(txtNbNoeuds.Text);
             int[] facteurs = CCalcul.PGF(dimension);
             if(facteurs[1]==dimension)
             {
                 System.Windows.Forms.MessageBox.Show(dimension+"est un nombre premier.. la dimension d'une matrice ne peut être un nombre premier.. recomencez svp.");

             }
         }*/
    }
}
