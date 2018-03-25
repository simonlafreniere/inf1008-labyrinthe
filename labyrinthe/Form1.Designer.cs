namespace labyrinthe
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenerer = new System.Windows.Forms.Button();
            this.panelControl = new System.Windows.Forms.Panel();
            this.txtHauteur = new System.Windows.Forms.TextBox();
            this.txtLargeur = new System.Windows.Forms.TextBox();
            this.lblHauteur = new System.Windows.Forms.Label();
            this.lblLargeur = new System.Windows.Forms.Label();
            this.lblControl = new System.Windows.Forms.Label();
            this.panelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerer
            // 
            this.btnGenerer.Location = new System.Drawing.Point(46, 121);
            this.btnGenerer.Name = "btnGenerer";
            this.btnGenerer.Size = new System.Drawing.Size(75, 23);
            this.btnGenerer.TabIndex = 0;
            this.btnGenerer.Text = "Generer";
            this.btnGenerer.UseVisualStyleBackColor = true;
            this.btnGenerer.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelControl
            // 
            this.panelControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelControl.Controls.Add(this.txtHauteur);
            this.panelControl.Controls.Add(this.txtLargeur);
            this.panelControl.Controls.Add(this.lblHauteur);
            this.panelControl.Controls.Add(this.lblLargeur);
            this.panelControl.Controls.Add(this.btnGenerer);
            this.panelControl.Location = new System.Drawing.Point(779, 15);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(165, 174);
            this.panelControl.TabIndex = 1;
            // 
            // txtHauteur
            // 
            this.txtHauteur.Location = new System.Drawing.Point(96, 54);
            this.txtHauteur.Name = "txtHauteur";
            this.txtHauteur.Size = new System.Drawing.Size(47, 20);
            this.txtHauteur.TabIndex = 4;
            // 
            // txtLargeur
            // 
            this.txtLargeur.Location = new System.Drawing.Point(96, 27);
            this.txtLargeur.Name = "txtLargeur";
            this.txtLargeur.Size = new System.Drawing.Size(47, 20);
            this.txtLargeur.TabIndex = 3;
            // 
            // lblHauteur
            // 
            this.lblHauteur.AutoSize = true;
            this.lblHauteur.Location = new System.Drawing.Point(43, 62);
            this.lblHauteur.Name = "lblHauteur";
            this.lblHauteur.Size = new System.Drawing.Size(48, 13);
            this.lblHauteur.TabIndex = 2;
            this.lblHauteur.Text = "Hauteur:";
            // 
            // lblLargeur
            // 
            this.lblLargeur.AutoSize = true;
            this.lblLargeur.Location = new System.Drawing.Point(43, 35);
            this.lblLargeur.Name = "lblLargeur";
            this.lblLargeur.Size = new System.Drawing.Size(46, 13);
            this.lblLargeur.TabIndex = 1;
            this.lblLargeur.Text = "Largeur:";
            // 
            // lblControl
            // 
            this.lblControl.AutoSize = true;
            this.lblControl.Location = new System.Drawing.Point(784, 3);
            this.lblControl.Name = "lblControl";
            this.lblControl.Size = new System.Drawing.Size(40, 13);
            this.lblControl.TabIndex = 2;
            this.lblControl.Text = "Control";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 650);
            this.Controls.Add(this.lblControl);
            this.Controls.Add(this.panelControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerer;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Label lblControl;
        private System.Windows.Forms.TextBox txtHauteur;
        private System.Windows.Forms.TextBox txtLargeur;
        private System.Windows.Forms.Label lblHauteur;
        private System.Windows.Forms.Label lblLargeur;
    }
}

