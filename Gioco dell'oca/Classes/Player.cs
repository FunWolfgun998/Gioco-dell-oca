using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gioco_dell_oca.Classes
{
    public class Player
    {
        public event EventHandler<EventArgs> AzionePlayer;
        public string nome { get; private set; }

        Random r;

        //0 = libero -1 = bloccato al cimitero n = turni bloccato
        public int bloccato { get; set; } 
        public int casella_attuale { get; set; }
        public Image icon_player { get; private set; }
        public Player() 
        {
            this.nome = "Pippo Baldo";
            this.bloccato = 0;
            this.casella_attuale = 0;
            this.r = new Random(Environment.TickCount);
        }
        public Player(string name, Image icon_player): this()
        {
            this.nome = name;
            this.icon_player = icon_player;
        }
        public int Avanza()
        {
            if (bloccato>=1)
            {
                bloccato--;
                return 0;
            }
            int n = r.Next(2, 13);
            casella_attuale += n;
            if (casella_attuale > 63)
            {
                int eccesso = casella_attuale - 63;
                casella_attuale = 63 - eccesso;
            }
            return n;
        }
        public void Azione()
        {
            AzionePlayer?.Invoke(this, EventArgs.Empty);
        }
    }
}
