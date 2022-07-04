using System;
using System.Linq;


namespace Ping.BO
{
    public class Monitoreo_BO
    {
        public string ipEquipo { get; set; }

        private DateTime timestamp;

        public DateTime Timestamp
        {
            get { return timestamp; }
            set
            {
                timestamp = value;
                var time = timestamp.Hour + ":" + timestamp.Minute + ":" + timestamp.Second;
                hora = Convert.ToDateTime(time).ToString("HH:mm:ss");
            }
        }

        public string hora { get; set; }

        public bool exitoPing { get; set; }
        public string exitoPingStr { get; set; }
        //public double latencia { get; set; }
        public int laTencia { get; set; }
        public int LATENCIA
        {
            get { return laTencia; }
            set
            {
                laTencia = value;
                latencia = string.Format("{0:n}", laTencia).Contains(',')
                             ? string.Format("{0:n}", laTencia).Split(',')[0]
                             : string.Format("{0:n}", laTencia);
            }
        }
        public string latencia { get; set; }
        //public double jitter { get; set; }
        public int jiTter { get; set; }
        public int JITTER
        {
            get { return laTencia; }
            set
            {
                jiTter = value;
                jitter = string.Format("{0:n}", jiTter).Contains(',')
                             ? string.Format("{0:n}", jiTter).Split(',')[0]
                             : string.Format("{0:n}", jiTter);
            }
        }
        public string jitter { get; set; }
        public int idGrupo { get; set; }
        public string nombreGrupo { get; set; }

        public override string ToString()
        {
            return ipEquipo;
        }

    }
}
