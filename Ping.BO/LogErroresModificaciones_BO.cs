using System;

namespace Ping.BO
{
    public class LogErroresModificaciones_BO
    {
        public int Id_tipo_log { get; set; }
        public DateTime Timestamp { get; set; }
        public string Mensaje { get; set; }
        public string UsuarioMaquina { get; set; }
    }
}
