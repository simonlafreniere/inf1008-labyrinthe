﻿namespace INF1008
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLargeur = new System.Windows.Forms.Label();
            this.lblHauteur = new System.Windows.Forms.Label();
            this.cbLargeur = new System.Windows.Forms.ComboBox();
            this.cbHauteur = new System.Windows.Forms.ComboBox();
            this.lblControles = new System.Windows.Forms.Label();
            this.btnPrimLabGen = new System.Windows.Forms.Button();
            this.lblErreur = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblErreur);
            this.panel1.Controls.Add(this.btnPrimLabGen);
            this.panel1.Controls.Add(this.lblLargeur);
            this.panel1.Controls.Add(this.lblHauteur);
            this.panel1.Controls.Add(this.cbLargeur);
            this.panel1.Controls.Add(this.cbHauteur);
            this.panel1.Location = new System.Drawing.Point(664, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 412);
            this.panel1.TabIndex = 0;
            // 
            // lblLargeur
            // 
            this.lblLargeur.AutoSize = true;
            this.lblLargeur.Location = new System.Drawing.Point(25, 72);
            this.lblLargeur.Name = "lblLargeur";
            this.lblLargeur.Size = new System.Drawing.Size(46, 13);
            this.lblLargeur.TabIndex = 3;
            this.lblLargeur.Text = "Largeur:";
            // 
            // lblHauteur
            // 
            this.lblHauteur.AutoSize = true;
            this.lblHauteur.Location = new System.Drawing.Point(25, 45);
            this.lblHauteur.Name = "lblHauteur";
            this.lblHauteur.Size = new System.Drawing.Size(48, 13);
            this.lblHauteur.TabIndex = 2;
            this.lblHauteur.Text = "Hauteur:";
            // 
            // cbLargeur
            // 
            this.cbLargeur.FormattingEnabled = true;
            this.cbLargeur.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cbLargeur.Location = new System.Drawing.Point(134, 69);
            this.cbLargeur.Name = "cbLargeur";
            this.cbLargeur.Size = new System.Drawing.Size(56, 21);
            this.cbLargeur.TabIndex = 1;
            // 
            // cbHauteur
            // 
            this.cbHauteur.FormattingEnabled = true;
            this.cbHauteur.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cbHauteur.Location = new System.Drawing.Point(134, 42);
            this.cbHauteur.Name = "cbHauteur";
            this.cbHauteur.Size = new System.Drawing.Size(56, 21);
            this.cbHauteur.TabIndex = 0;
            // 
            // lblControles
            // 
            this.lblControles.AutoSize = true;
            this.lblControles.Location = new System.Drawing.Point(667, 9);
            this.lblControles.Name = "lblControles";
            this.lblControles.Size = new System.Drawing.Size(51, 13);
            this.lblControles.TabIndex = 0;
            this.lblControles.Text = "Controles";
            // 
            // btnPrimLabGen
            // 
            this.btnPrimLabGen.Location = new System.Drawing.Point(28, 122);
            this.btnPrimLabGen.Name = "btnPrimLabGen";
            this.btnPrimLabGen.Size = new System.Drawing.Size(162, 23);
            this.btnPrimLabGen.TabIndex = 4;
            this.btnPrimLabGen.Text = "Generer labyrinthe Prim";
            this.btnPrimLabGen.UseVisualStyleBackColor = true;
            this.btnPrimLabGen.Click += new System.EventHandler(this.btnPrimLabGen_Click);
            // 
            // lblErreur
            // 
            this.lblErreur.AutoSize = true;
            this.lblErreur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblErreur.Location = new System.Drawing.Point(28, 103);
            this.lblErreur.Name = "lblErreur";
            this.lblErreur.Size = new System.Drawing.Size(0, 13);
            this.lblErreur.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 436);
            this.Controls.Add(this.lblControles);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblControles;
        private System.Windows.Forms.Label lblLargeur;
        private System.Windows.Forms.Label lblHauteur;
        private System.Windows.Forms.ComboBox cbLargeur;
        private System.Windows.Forms.ComboBox cbHauteur;
        private System.Windows.Forms.Label lblErreur;
        private System.Windows.Forms.Button btnPrimLabGen;
    }
}

