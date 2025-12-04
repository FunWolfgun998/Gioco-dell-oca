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

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.ClientSize = new Size(300, 200);
            this.Text = "Setup Partita";

            this.Load += new EventHandler(FormNumeroGiocatori_Load);
        }

        private void FormNumeroGiocatori_Load(object sender, EventArgs e)
        {

            int larghezzaControlli = 150;
            int spacing = 15; // Spazio verticale tra i controlli

            lbl_n_giocatori.AutoSize = false;
            lbl_n_giocatori.Size = new Size(this.ClientSize.Width - 40, 30);
            lbl_n_giocatori.TextAlign = ContentAlignment.MiddleCenter;
            lbl_n_giocatori.Location = new Point(
                (this.ClientSize.Width - lbl_n_giocatori.Width) / 2,
                20
            );

            nmr_Giocatori.Size = new Size(larghezzaControlli, 30);
            nmr_Giocatori.Location = new Point(
                (this.ClientSize.Width - nmr_Giocatori.Width) / 2,
                lbl_n_giocatori.Bottom + spacing
            );
            nmr_Giocatori.Font = new Font(nmr_Giocatori.Font.FontFamily, 12);

            btt_done.Size = new Size(larghezzaControlli, 40);
            btt_done.Location = new Point(
                (this.ClientSize.Width - btt_done.Width) / 2,
                nmr_Giocatori.Bottom + spacing
            );
        }


        private void btt_done_Click(object sender, EventArgs e)
        {
            this.NumeroDiGiocatori = (int)nmr_Giocatori.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}