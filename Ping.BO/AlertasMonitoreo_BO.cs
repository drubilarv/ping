using System;

namespace Ping.BO
{
   public class AlertasMonitoreo_BO
    {
        public string ipEquipo { get; set; }
        public string nombreEquipo { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime Timestamp
        {
            get { return timestamp; }
            set
            {
                timestamp = value;

                var time = timestamp.Hour + ":" + timestamp.Minute + ":" + timestamp.Second;
                hora = Convert.ToDateTime(time).ToString("HH:mm:ss");
                //hora = timee.ToString("HH:mm:ss");
            }
        }
        public string hora { get; set; }
        public double porcentajePerdida { get; set; }
        public bool leido { get; set; }
        public int id { get; set; }
        public override string ToString()
        {
            return ipEquipo;
        }
    }
}
