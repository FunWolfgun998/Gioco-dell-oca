using Gioco_dell_oca.Classes;
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
    public partial class SelectPlayer : Form
    {
        public string NomeGiocatore { get; private set; }
        public Image PedinaScelta { get; private set; }

        // Lista per contenere tutte le 10 immagini delle pedine
        private List<Image> tutteLePedine = new List<Image>();
        private List<Image> pedineNonDisponibili;
        private int indicePedinaCorrente = 0;

        public SelectPlayer(string titolo, List<Image> pedineGiaScelte)
        {
            InitializeComponent();
            this.Text = titolo;
            this.pedineNonDisponibili = pedineGiaScelte ?? new List<Image>();

            // Carica dinamicamente le risorse chiamate p1, p2, ..., p10
            System.Resources.ResourceManager rm = Properties.Resources.ResourceManager;
            for (int i = 1; i <= 10; i++)
            {
                Image img = (Image)rm.GetObject("p" + i);
                if (img != null)
                {
                    tutteLePedine.Add(img);
                }
            }

            // Imposta la prima pedina disponibile come quella di default
            while (pedineNonDisponibili.Contains(tutteLePedine[indicePedinaCorrente]))
            {
                indicePedinaCorrente++;
            }
            MostraPedinaCorrente();
        }

        private void MostraPedinaCorrente()
        {
            pct_skin.Image = tutteLePedine[indicePedinaCorrente];
        }

        private void btnSuccessivo_Click(object sender, EventArgs e)
        {
            // Cerca la prossima pedina DISPONIBILE
            do
            {
                indicePedinaCorrente++;
                if (indicePedinaCorrente >= tutteLePedine.Count)
                {
                    indicePedinaCorrente = 0; // Ricomincia da capo
                }
            } while (pedineNonDisponibili.Contains(tutteLePedine[indicePedinaCorrente]));

            MostraPedinaCorrente();
        }

        private void btnPrecedente_Click(object sender, EventArgs e)
        {
            // Cerca la precedente pedina DISPONIBILE
            do
            {
                indicePedinaCorrente--;
                if (indicePedinaCorrente < 0)
                {
                    indicePedinaCorrente = tutteLePedine.Count - 1; // Ricomincia dalla fine
                }
            } while (pedineNonDisponibili.Contains(tutteLePedine[indicePedinaCorrente]));

            MostraPedinaCorrente();
        }

        private void btnConferma_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_nameplayer.Text))
            {
                MessageBox.Show("Per favore, inserisci un nome.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Salva i dati scelti
            NomeGiocatore = txt_nameplayer.Text.Trim();
            PedinaScelta = tutteLePedine[indicePedinaCorrente];

            // Chiudi il form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
