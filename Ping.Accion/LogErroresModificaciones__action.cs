using System;
using System.Collections.Generic;
using Ping.BO;
using Ping.DAO;

namespace Ping.Accion
{
    public class LogErroresModificaciones__action
    {
        public bool InsertErroresLog(int id_tipo_log, DateTime timestamp, string usuario, string mensaje)
        {
            var logem = new LogErroresModificaciones__DAO();
            var log = new LogErroresModificaciones_BO
            {
                Id_tipo_log = id_tipo_log,
                Timestamp = timestamp,
                UsuarioMaquina = usuario,
                Mensaje = mensaje
            };
            return logem.InsertErroresLog(log);
        }
        public List<LogErroresModificaciones_BO> ObtenerLogFecha(int id, DateTime inicio, DateTime fin)
        {
            var log_modificaciones = new LogErroresModificaciones__DAO();
            return log_modificaciones.ObtenerLogFecha(id, inicio, fin);
        }
        public List<LogErroresModificaciones_BO> ObtenerLogSinFiltroFechaInicio(int id, DateTime fin)
        {
            var log_modificaciones = new LogErroresModificaciones__DAO();
            return log_modificaciones.ObtenerLogSinFiltroFechaInicio(id, fin);
        }
        public List<LogErroresModificaciones_BO> ObtenerLogSinFiltroFechaFin(int id, DateTime inicio)
        {
            var log_modificaciones = new LogErroresModificaciones__DAO();
            return log_modificaciones.ObtenerLogSinFiltroFechaInicio(id, inicio);
        }
        public List<string> ObtenerLogId()
        {
            var log_modificaciones = new LogErroresModificaciones__DAO();
            return log_modificaciones.ObtenerLogId();
        }
    }
}
