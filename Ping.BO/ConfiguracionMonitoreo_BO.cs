using System.Linq;


namespace Ping.BO
{
    public class ConfiguracionMonitoreo_BO
    {
        public int IdConfig { get; set; }
        public string NombreConfig { get; set; }
        //public double TiempoPing { get; set; }
        public int tiempoPing { get; set; }
        public int tiempoping
        {
            get { return tiempoPing; }
            set
            {
                tiempoPing = value;
                TiempoPing = string.Format("{0:n}", tiempoPing).Contains(',')
                             ? string.Format("{0:n}", tiempoPing).Split(',')[0]
                             : string.Format("{0:n}", tiempoPing);
            }
        }
        public string TiempoPing { get; set; }
        //public double TamPaquete { get; set; }
        public int tamPaquete { get; set; }
        public int tampaquete
        {
            get { return tamPaquete; }
            set
            {
                tamPaquete = value;
                TamPaquete = string.Format("{0:n}", tamPaquete).Contains(',')
                              ? string.Format("{0:n}", tamPaquete).Split(',')[0]
                              : string.Format("{0:n}", tamPaquete);
            }
        }
        public string TamPaquete { get; set; }

        //public double Timeout { get; set; }
        public int timeOut { get; set; }
        public int TimeOut
        {
            get { return timeOut; }
            set
            {
                timeOut = value;
                Timeout = string.Format("{0:n}", timeOut).Contains(',')
                              ? string.Format("{0:n}", timeOut).Split(',')[0]
                              : string.Format("{0:n}", timeOut);
            }
        }
        public string Timeout { get; set; }
        public override string ToString()
        {
            return NombreConfig;
        }
    }
}
