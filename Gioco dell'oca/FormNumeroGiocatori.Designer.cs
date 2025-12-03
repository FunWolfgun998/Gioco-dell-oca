namespace Gioco_dell_oca
{
    partial class FormNumeroGiocatori
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
            this.lbl_n_giocatori = new System.Windows.Forms.Label();
            this.nmr_Giocatori = new System.Windows.Forms.NumericUpDown();
            this.btt_done = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_Giocatori)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_n_giocatori
            // 
            this.lbl_n_giocatori.Location = new System.Drawing.Point(0, 0);
            this.lbl_n_giocatori.Name = "lbl_n_giocatori";
            this.lbl_n_giocatori.Size = new System.Drawing.Size(193, 23);
            this.lbl_n_giocatori.TabIndex = 0;
            this.lbl_n_giocatori.Text = "Inserire numero giocatori";
            // 
            // nmr_Giocatori
            // 
            this.nmr_Giocatori.Location = new System.Drawing.Point(0, 0);
            this.nmr_Giocatori.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmr_Giocatori.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nmr_Giocatori.Name = "nmr_Giocatori";
            this.nmr_Giocatori.Size = new System.Drawing.Size(120, 22);
            this.nmr_Giocatori.TabIndex = 1;
            this.nmr_Giocatori.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // btt_done
            // 
            this.btt_done.Location = new System.Drawing.Point(0, 0);
            this.btt_done.Name = "btt_done";
            this.btt_done.Size = new System.Drawing.Size(100, 30);
            this.btt_done.TabIndex = 2;
            this.btt_done.Text = "Fatto";
            this.btt_done.UseVisualStyleBackColor = true;
            this.btt_done.Click += new System.EventHandler(this.btt_done_Click);
            // 
            // FormNumeroGiocatori
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btt_done);
            this.Controls.Add(this.nmr_Giocatori);
            this.Controls.Add(this.lbl_n_giocatori);
            this.Name = "FormNumeroGiocatori";
            this.Text = "FormNumeroGiocatori";
            ((System.ComponentModel.ISupportInitialize)(this.nmr_Giocatori)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_n_giocatori;
        private System.Windows.Forms.NumericUpDown nmr_Giocatori;
        private System.Windows.Forms.Button btt_done;
    }
}