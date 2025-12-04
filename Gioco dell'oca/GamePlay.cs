using Gioco_dell_oca.Classes; // Assicurati che il namespace sia corretto
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gioco_dell_oca
{
    public partial class GamePlay : Form
    {
        // Lista giocatori <- normalmente 2 ma si può avere più giocatori
        private List<Player> giocatori = new List<Player>();

        // Lista PictureBox dei giocatori per muoverle sul tabellone
        private List<PictureBox> contenitoriPedine = new List<PictureBox>();

        // Dizionario dove ci sono le coordinate di tutte le caselle in base la numero della casella
        private Dictionary<int, Point> coordinateCaselle = new Dictionary<int, Point>();

        // Numero di caselle totali
        const int NUM_CASELLE = 63;
        private int indiceGiocatoreCorrente = 0; // 0 per il primo giocatore, 1 per il secondo, ecc.
        private Button btnLanciaDadi;
        private Label lblTurnoInfo;

        public GamePlay()
        {
            InitializeComponent();
            this.Size = new Size(1200, 950);
            this.DoubleBuffered = true;
        }

        private void GamePlay_Load(object sender, EventArgs e)
        {
            int numeroGiocatori;
            using (FormNumeroGiocatori formNum = new FormNumeroGiocatori())
            {
                if (formNum.ShowDialog() == DialogResult.OK)
                {
                    numeroGiocatori = formNum.NumeroDiGiocatori;
                }
                else
                {
                    this.Close();
                    return;
                }
            }
            // Metodo che controlla che tutto sia andato a buon fine durante la scelta delle pedine
            // Se l'utente ha annullato la selezione, il gioco chiude
            if (!SelezionaGiocatori(numeroGiocatori))
            {
                this.Close();
                return;
            }

            // Genera tutte le caselle graficamente a form spirale
            GeneraPercorsoSpiraleRotonda();

            // Genera e posizione a casella "0"
            PosizionaPedineIniziali();
            //imposta UI tramite codice
            InizializzaControlliDiGioco();
            IniziaTurno(); // Avvia il primo turno
        }
        private bool SelezionaGiocatori(int numeroDiGiocatori) // MODIFICA: Accetta il numero di giocatori
        {
            List<Image> pedineGiaScelte = new List<Image>();

            // Ciclo per creare il numero di giocatori scelto
            for (int i = 1; i <= numeroDiGiocatori; i++)
            {
                using (SelectPlayer formSelezione = new SelectPlayer($"Selezione Giocatore {i}", pedineGiaScelte))
                {
                    if (formSelezione.ShowDialog() == DialogResult.OK)
                    {
                        string nome = formSelezione.NomeGiocatore;
                        Image icona = formSelezione.PedinaScelta;

                        Player nuovoGiocatore = new Player(nome, icona);
                        giocatori.Add(nuovoGiocatore);

                        pedineGiaScelte.Add(icona);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true; // Finito senza problemi
        }
        // Generazione del percorso a spirale con celle
        private void GeneraPercorsoSpiraleRotonda()
        {
            int dimCasella = 65;
            double raggioIniziale = 380;
            double raggioFinale = 130; //spazio all'interno del 
            double giriTotali = 3.25;
            double offsetAngolo = Math.PI;
            List<int> caselleOca = new List<int> { 5, 9, 18, 27, 36, 45, 54 };// Celle oca
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            for (int i = 0; i < NUM_CASELLE; i++)
            {
                int numeroCasella = i + 1;
                double t = (double)i / (NUM_CASELLE - 1);
                double angolo = offsetAngolo + (t * (Math.PI * 2 * giriTotali));
                double raggioAttuale = raggioIniziale - (t * (raggioIniziale - raggioFinale));
                int x = (int)(centerX + raggioAttuale * Math.Cos(angolo));
                int y = (int)(centerY + raggioAttuale * Math.Sin(angolo));

                coordinateCaselle[numeroCasella] = new Point(x, y);

                x -= dimCasella / 2;
                y -= dimCasella / 2;

                Color coloreSfondo = Color.DarkGreen;
                Image immagineSfondo = null;
                if (caselleOca.Contains(numeroCasella)) { immagineSfondo = Properties.Resources.oca; }
                else
                {
                    switch (numeroCasella)
                    {
                        case 1: coloreSfondo = Color.LightGreen; break;
                        case 6: immagineSfondo = Properties.Resources.ponte; break;
                        case 19: immagineSfondo = Properties.Resources.casa; break;
                        case 31: immagineSfondo = Properties.Resources.prigione; break;
                        case 42: immagineSfondo = Properties.Resources.labirinto; break;
                        case 58: immagineSfondo = Properties.Resources.scheletro; break;
                        case 63: coloreSfondo = Color.Gold; break;
                    }
                }
                GeneraPanelRotondo(x, y, coloreSfondo, numeroCasella, dimCasella, immagineSfondo);
            }
        }
        private Panel GeneraPanelRotondo(int px, int py, Color c, int numero, int dimensione, Image bgImage = null)
        {
            Panel p = new Panel();
            p.Size = new Size(dimensione, dimensione);
            p.Location = new Point(px, py);

            if (bgImage != null)
            {
                p.BackgroundImage = bgImage;
                p.BackgroundImageLayout = ImageLayout.Zoom;
            }
            else
            {
                p.BackColor = c;
            }

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, dimensione, dimensione);
            p.Region = new Region(path);

            Label lbl = new Label();
            lbl.Text = numero.ToString();
            lbl.Font = new Font("Arial", 13, FontStyle.Bold);
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.BackColor = Color.Transparent;
            lbl.ForeColor = (bgImage != null) ? Color.DarkOrange : Color.YellowGreen;

            p.Controls.Add(lbl);
            this.Controls.Add(p);
            p.BringToFront();
            return p;
        }
        private void PosizionaPedineIniziali()
        {
            int offsetBase = 0;

            for (int i = 0; i < giocatori.Count; i++)
            {
                Player p = giocatori[i];
                int offsetCorrente = offsetBase + (i * 35);

                PictureBox container = new PictureBox();
                container.Size = new Size(70, 70);
                container.BackColor = Color.Transparent;

                PictureBox pedina = new PictureBox();
                pedina.Size = new Size(35, 35);
                pedina.SizeMode = PictureBoxSizeMode.Zoom;
                pedina.BackColor = Color.Transparent;
                pedina.Image = p.icon_player;
                pedina.Location = new Point((container.Width - pedina.Width) / 2, 5);

                GraphicsPath pathcontainer = new GraphicsPath();
                pathcontainer.AddEllipse(0, 0, container.Width, container.Height);
                container.Region = new Region(pathcontainer);

                Label nomeLabel = new Label();
                nomeLabel.Text = p.nome;
                nomeLabel.Font = new Font("Arial", 8, FontStyle.Bold);
                nomeLabel.ForeColor = Color.Black;
                nomeLabel.BackColor = Color.Transparent;
                nomeLabel.Size = new Size(container.Width, 20);
                nomeLabel.TextAlign = ContentAlignment.MiddleCenter;
                nomeLabel.Location = new Point(0, pedina.Bottom + 2);

                container.Controls.Add(pedina);
                container.Controls.Add(nomeLabel);

                Point centroCasella1 = coordinateCaselle[1];
                container.Location = new Point(centroCasella1.X - container.Width / 2 + offsetCorrente - 80, centroCasella1.Y - container.Height / 2);

                this.Controls.Add(container);
                container.BringToFront();
                contenitoriPedine.Add(container);
            }
        }
        private void InizializzaControlliDiGioco()
        {
            btnLanciaDadi = new Button();
            btnLanciaDadi.Text = "Lancia i dadi!";
            btnLanciaDadi.Size = new Size(150, 50);
            btnLanciaDadi.Font = new Font("Arial", 12, FontStyle.Bold);
            btnLanciaDadi.Location = new Point(20, 20);
            btnLanciaDadi.Click += BtnLanciaDadi_Click;
            this.Controls.Add(btnLanciaDadi);

            lblTurnoInfo = new Label();
            lblTurnoInfo.Text = "Inizio del gioco!";
            lblTurnoInfo.AutoSize = true;
            lblTurnoInfo.Font = new Font("Arial", 14, FontStyle.Regular);
            lblTurnoInfo.Location = new Point(btnLanciaDadi.Right + 20, 35);
            this.Controls.Add(lblTurnoInfo);
        }
        private void IniziaTurno()
        {
            Player giocatoreAttuale = giocatori[indiceGiocatoreCorrente];
            lblTurnoInfo.Text = $"È il turno di {giocatoreAttuale.nome}.";

            if (giocatoreAttuale.bloccato == -1) // Prigione
            {
                MessageBox.Show($"{giocatoreAttuale.nome} è in prigione e salta il turno.", "Turno Saltato");
                PassaTurno();
            }
            else if (giocatoreAttuale.bloccato > 0) // Casa
            {
                MessageBox.Show($"{giocatoreAttuale.nome} è bloccato per ancora {giocatoreAttuale.bloccato} turno/i.", "Turno Saltato");
                giocatoreAttuale.bloccato--;
                PassaTurno();
            }
            else
            {
                btnLanciaDadi.Enabled = true;
            }
        }
        private async void BtnLanciaDadi_Click(object sender, EventArgs e)
        {
            btnLanciaDadi.Enabled = false;

            Player giocatoreCorrente = giocatori[indiceGiocatoreCorrente];

            int movimento = giocatoreCorrente.Avanza();
            lblTurnoInfo.Text = $"{giocatoreCorrente.nome} ha lanciato i dadi e ha fatto {movimento}!";
            await Task.Delay(500);

            await MuoviGiocatore(indiceGiocatoreCorrente, giocatoreCorrente.casella_attuale);

            await ControllaCollisioni(giocatoreCorrente,movimento);

            while (true)
            {
                bool posizioneCambiata = await GestisciAzioneCasella(giocatoreCorrente, movimento);
                if (!posizioneCambiata)
                {
                    break;
                }
                await ControllaCollisioni(giocatoreCorrente, movimento);
            }

            if (giocatoreCorrente.casella_attuale == NUM_CASELLE)
            {
                MessageBox.Show($"{giocatoreCorrente.nome} ha vinto la partita!", "Vittoria!");
                btnLanciaDadi.Enabled = false;
                lblTurnoInfo.Text = "Fine del gioco!";
                //Richiesta restart gioco
                DialogResult scelta = MessageBox.Show("Vuoi fare un'altra partita?", "Fine Gioco", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (scelta == DialogResult.Yes)
                {
                    Application.Restart(); // Riavvia l'applicazione
                }
                else
                {
                    Application.Exit(); 
                }
                return;
            }

            if (giocatoreCorrente.casella_attuale == 31)
            {
                foreach (var p in giocatori)
                {
                    if (p != giocatoreCorrente && p.bloccato == -1)
                    {
                        p.bloccato = 0;
                        MessageBox.Show($"{giocatoreCorrente.nome} è arrivato alla prigione e ha liberato {p.nome}!", "Liberazione!");
                    }
                }
            }

            PassaTurno();
        }
        private async Task ControllaCollisioni(Player giocatoreAttivo, int movimento)
        {
            if (giocatoreAttivo.casella_attuale <= 0) return;

            foreach (var altroGiocatore in giocatori)
            {
                if (altroGiocatore != giocatoreAttivo && altroGiocatore.casella_attuale == giocatoreAttivo.casella_attuale)
                {
                    MessageBox.Show($"{giocatoreAttivo.nome} è finito sulla casella di {altroGiocatore.nome}, che torna al posto di {giocatoreAttivo.nome}!", "Collisione!");
                    altroGiocatore.casella_attuale = giocatoreAttivo.casella_attuale - movimento; // Manda l'altro giocatore alla posizione del primo giocatore
                    int indiceAltroGiocatore = giocatori.IndexOf(altroGiocatore);
                    await MuoviGiocatore(indiceAltroGiocatore, altroGiocatore.casella_attuale); // Aggiorna la sua posizione grafica
                    break; // Esco dal ciclo, non ci possono essere due giocatori sulla stessa casella
                }
            }
        }
        private async Task<bool> GestisciAzioneCasella(Player p, int ultimoMovimento)
        {
            int casellaPrimaAzione = p.casella_attuale;
            EventHandler<EventArgs> handlerDaEseguire = null;

            switch (p.casella_attuale)
            {
                case 5:
                case 9:
                case 18:
                case 27:
                case 36:
                case 45:
                case 54:
                case 6:
                    handlerDaEseguire = (sender, e) => AzioneRipetiMovimento(sender, e, ultimoMovimento);
                    break;
                case 19:
                    handlerDaEseguire = AzioneCasa;
                    break;
                case 31:
                    handlerDaEseguire = AzionePrigione;
                    break;
                case 42:
                    handlerDaEseguire = AzioneLabirinto;
                    break;
                case 58:
                    handlerDaEseguire = AzioneScheletro;
                    break;
                default:
                    return false;
            }

            if (handlerDaEseguire != null)
            {
                p.AzionePlayer += handlerDaEseguire;
                p.Azione();
                p.AzionePlayer -= handlerDaEseguire;
            }

            return p.casella_attuale != casellaPrimaAzione;
        }

        private async void AzioneRipetiMovimento(object sender, EventArgs e, int movimentoDaRipetere)
        {
            Player p = sender as Player;
            MessageBox.Show($"Oca/Ponte! Avanzi ancora di {movimentoDaRipetere} caselle.", "Azione Speciale!");
            p.casella_attuale+= movimentoDaRipetere;
            await MuoviGiocatore(giocatori.IndexOf(p), p.casella_attuale);
        }
        private void AzioneCasa(object sender, EventArgs e)
        {
            Player p = sender as Player;
            MessageBox.Show("Sei tornato a casa. Stai fermo 3 turni per riposare.", "Casa!");
            p.bloccato = 3;
        }
        private void AzionePrigione(object sender, EventArgs e)
        {
            Player p = sender as Player;
            MessageBox.Show($"{p.nome} è finito in prigione! Rimarrà fermo finché non verrà liberato.", "Prigione!");
            p.bloccato = -1;
        }
        private async void AzioneLabirinto(object sender, EventArgs e)
        {
            Player p = sender as Player;
            MessageBox.Show("Ti sei perso nel labirinto! Torni indietro alla casella 39.", "Labirinto!");
            p.casella_attuale = 39;
            await MuoviGiocatore(giocatori.IndexOf(p), p.casella_attuale);
        }
        private async void AzioneScheletro(object sender, EventArgs e)
        {
            Player p = sender as Player;
            MessageBox.Show("Trovi uno scheletro e muori di infarto. Torni all'inizio", "Scheletro!");
            p.casella_attuale = 0;
            await MuoviGiocatore(giocatori.IndexOf(p), p.casella_attuale);
        }
        private void PassaTurno()
        {
            indiceGiocatoreCorrente = (indiceGiocatoreCorrente + 1) % giocatori.Count;
            IniziaTurno();
        }
        private async Task MuoviGiocatore(int indiceGiocatore, int nuovaCasella)
        {
            //caso vittoria
            if (nuovaCasella > NUM_CASELLE) nuovaCasella = NUM_CASELLE;

            PictureBox contenitoreDaMuovere = contenitoriPedine[indiceGiocatore];
            Point centroCasella;

            if (nuovaCasella <= 0)
            {

                int offsetBase = -20 * (giocatori.Count / 2);
                int offsetCorrente = offsetBase + (indiceGiocatore * 40);
                centroCasella = new Point(coordinateCaselle[1].X - 80, coordinateCaselle[1].Y);

                contenitoreDaMuovere.Location = new Point(
                    centroCasella.X - contenitoreDaMuovere.Width / 2 + offsetCorrente,
                    centroCasella.Y - contenitoreDaMuovere.Height / 2
                );
            }
            else
            {
                centroCasella = coordinateCaselle[nuovaCasella];
                contenitoreDaMuovere.Location = new Point(
                    centroCasella.X - (contenitoreDaMuovere.Width / 2),
                    centroCasella.Y - (contenitoreDaMuovere.Height / 2) 
                );
            }

            contenitoreDaMuovere.BringToFront();
            await Task.Delay(200);
        }
    }
}