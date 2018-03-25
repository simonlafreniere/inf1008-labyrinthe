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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* CGraphe monGraphe = new CGraphe(2, 3, 2);
             monGraphe.Prim();

             int[,] g = monGraphe.getGraphe();

            monGraphe.densite();*/
            int dim = 36;
            int[] div;
            div=CCalcul.PGF(dim);
            if (div[1] == dim)
                this.button1.Text = "nb premier";
            else
                this.button1.Text = div[0]+" * "+div[1]+" = "+dim;
        }
    }
}
