
namespace Ping.BO
{
    public  class ConfiguracionGeneral_BO
    {
        public decimal Ping_no_exitoso { get; set; }
        public double Generar_alarma { get; set; }
        public double Tiempo_nueva_alerta { get; set; }
        public double Frecuencia_no_ping { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public int Tiempo_proceso_reporte { get; set; }
        public string Servidor_smtp { get; set; }
        public int Time_depuracion { get; set; }
        public bool Alerta_activada { get; set; }
    }
}
