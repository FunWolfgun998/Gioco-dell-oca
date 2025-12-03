using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gioco_dell_oca
{
    public partial class FormNumeroGiocatori : Form
    {
        // Proprietà pubblica per leggere il valore dall'esterno
        public int NumeroDiGiocatori { get; private set; }

        public FormNumeroGiocatori()
        {
            InitializeComponent();
            // Impostazioni per rendere la finestra più pulita
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // --- IMPOSTAZIONI GRAFICHE ---
            // Imposto le dimensioni del form
            this.ClientSize = new Size(300, 200);
            this.Text = "Setup Partita";

            // Associo l'evento Load per centrare i controlli
            this.Load += new EventHandler(FormNumeroGiocatori_Load);
        }

        private void FormNumeroGiocatori_Load(object sender, EventArgs e)
        {
            // --- CENTRATURA AUTOMATICA DEI CONTROLLI ---

            int larghezzaControlli = 150;
            int spacing = 15; // Spazio verticale tra i controlli

            // 1. Label (lbl_n_giocatori)
            lbl_n_giocatori.AutoSize = false; // Disattivo AutoSize per centrare il testo
            lbl_n_giocatori.Size = new Size(this.ClientSize.Width - 40, 30); // Larga quasi tutto il form
            lbl_n_giocatori.TextAlign = ContentAlignment.MiddleCenter;
            lbl_n_giocatori.Location = new Point(
                (this.ClientSize.Width - lbl_n_giocatori.Width) / 2, // Centrata orizzontalmente
                20 // Margine dall'alto
            );

            // 2. NumericUpDown (nmr_Giocatori)
            nmr_Giocatori.Size = new Size(larghezzaControlli, 30);
            nmr_Giocatori.Location = new Point(
                (this.ClientSize.Width - nmr_Giocatori.Width) / 2, // Centrata orizzontalmente
                lbl_n_giocatori.Bottom + spacing // Posizionata sotto la label
            );
            nmr_Giocatori.Font = new Font(nmr_Giocatori.Font.FontFamily, 12); // Rendo il numero più grande

            // 3. Button (btt_done)
            btt_done.Size = new Size(larghezzaControlli, 40); // Pulsante un po' più alto
            btt_done.Location = new Point(
                (this.ClientSize.Width - btt_done.Width) / 2, // Centrata orizzontalmente
                nmr_Giocatori.Bottom + spacing // Posizionata sotto il NumericUpDown
            );
        }


        private void btt_done_Click(object sender, EventArgs e)
        {
            // Salvo il valore scelto nel NumericUpDown
            this.NumeroDiGiocatori = (int)nmr_Giocatori.Value;

            // Imposto il risultato a OK e chiudo la finestra
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}