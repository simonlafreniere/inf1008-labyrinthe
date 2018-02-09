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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPrimLabGen_Click(object sender, EventArgs e)
        {
            if(cbHauteur.SelectedIndex == -1 || cbLargeur.SelectedIndex == -1)
            {
                lblErreur.Text = "Selectionner la hauteur et la largeur";
            }
            else
            {
                lblErreur.Text = "";

                int nbLignes = int.Parse(cbHauteur.SelectedItem.ToString());
                int nbColonnes = int.Parse(cbLargeur.SelectedItem.ToString());

                Prim prim = new Prim(nbLignes, nbColonnes);

            }
        }
    }
}
