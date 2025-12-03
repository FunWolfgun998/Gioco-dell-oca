namespace Gioco_dell_oca
{
    partial class SelectPlayer
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.txt_nameplayer = new System.Windows.Forms.TextBox();
            this.btt_play = new System.Windows.Forms.Button();
            this.pct_skin = new System.Windows.Forms.PictureBox();
            this.btt_leftskin = new System.Windows.Forms.Button();
            this.btt_rightskin = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pct_skin)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_nameplayer
            // 
            this.txt_nameplayer.Location = new System.Drawing.Point(270, 241);
            this.txt_nameplayer.Name = "txt_nameplayer";
            this.txt_nameplayer.Size = new System.Drawing.Size(150, 22);
            this.txt_nameplayer.TabIndex = 1;
            // 
            // btt_play
            // 
            this.btt_play.Location = new System.Drawing.Point(310, 269);
            this.btt_play.Name = "btt_play";
            this.btt_play.Size = new System.Drawing.Size(75, 23);
            this.btt_play.TabIndex = 2;
            this.btt_play.Text = "Done";
            this.btt_play.UseVisualStyleBackColor = true;
            this.btt_play.Click += new System.EventHandler(this.btnConferma_Click);
            // 
            // pct_skin
            // 
            this.pct_skin.BackColor = System.Drawing.Color.Transparent;
            this.pct_skin.Location = new System.Drawing.Point(270, 70);
            this.pct_skin.Name = "pct_skin";
            this.pct_skin.Size = new System.Drawing.Size(150, 150);
            this.pct_skin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pct_skin.TabIndex = 3;
            this.pct_skin.TabStop = false;
            // 
            // btt_leftskin
            // 
            this.btt_leftskin.BackColor = System.Drawing.SystemColors.Control;
            this.btt_leftskin.Location = new System.Drawing.Point(224, 130);
            this.btt_leftskin.Name = "btt_leftskin";
            this.btt_leftskin.Size = new System.Drawing.Size(40, 40);
            this.btt_leftskin.TabIndex = 4;
            this.btt_leftskin.Text = "<";
            this.btt_leftskin.UseVisualStyleBackColor = false;
            this.btt_leftskin.Click += new System.EventHandler(this.btnPrecedente_Click);
            // 
            // btt_rightskin
            // 
            this.btt_rightskin.BackColor = System.Drawing.SystemColors.Control;
            this.btt_rightskin.Location = new System.Drawing.Point(426, 130);
            this.btt_rightskin.Name = "btt_rightskin";
            this.btt_rightskin.Size = new System.Drawing.Size(40, 40);
            this.btt_rightskin.TabIndex = 5;
            this.btt_rightskin.Text = ">";
            this.btt_rightskin.UseVisualStyleBackColor = false;
            this.btt_rightskin.Click += new System.EventHandler(this.btnSuccessivo_Click);
            // 
            // SelectPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.ClientSize = new System.Drawing.Size(682, 351);
            this.Controls.Add(this.btt_rightskin);
            this.Controls.Add(this.btt_leftskin);
            this.Controls.Add(this.pct_skin);
            this.Controls.Add(this.btt_play);
            this.Controls.Add(this.txt_nameplayer);
            this.Name = "SelectPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleziona pedina";
            ((System.ComponentModel.ISupportInitialize)(this.pct_skin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txt_nameplayer;
        private System.Windows.Forms.Button btt_play;
        private System.Windows.Forms.PictureBox pct_skin;
        private System.Windows.Forms.Button btt_leftskin;
        private System.Windows.Forms.Button btt_rightskin;
    }
}

