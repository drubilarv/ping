using Ping.BO;
using Ping.DAO;
namespace Ping.Accion
{
    public class ConfiguracionGeneral_action
    {
        private ConfiguracionGeneral_DAO _configuracionGeneralDao;
        public bool actualizaConfig(decimal porcentaje_perdida_ping_no_exitoso, int segundos_genera_alarma, int timepo_nueva_alerta, int frecuencia_alternativa_no_ping,
              string servidor_smtp, string email, string pass, int tiempo_proceso_reporte, int depuracion)
        {
            var general = new ConfiguracionGeneral_DAO();
            var generalConfig = new ConfiguracionGeneral_BO
            {
                Ping_no_exitoso = porcentaje_perdida_ping_no_exitoso,
                Generar_alarma = segundos_genera_alarma,
                Tiempo_nueva_alerta = timepo_nueva_alerta,
                Frecuencia_no_ping = frecuencia_alternativa_no_ping,
                Servidor_smtp = servidor_smtp,
                Clave = pass,
                Email = email,
                Tiempo_proceso_reporte = tiempo_proceso_reporte,
                Time_depuracion = depuracion
            };
            return general.ActualizaConfig(generalConfig);
        }

        public bool InsertConfig(decimal porcentaje_perdida_ping_no_exitoso, int segundos_genera_alarma, int timepo_nueva_alerta, int frecuencia_alternativa_no_ping,
          string servidor_smtp, string email, string pass, int tiempo_proceso_reporte)
        {
            var general = new ConfiguracionGeneral_DAO();
            var generalConfig = new ConfiguracionGeneral_BO
            {
                Ping_no_exitoso = porcentaje_perdida_ping_no_exitoso,
                Generar_alarma = segundos_genera_alarma,
                Tiempo_nueva_alerta = timepo_nueva_alerta,
                Frecuencia_no_ping = frecuencia_alternativa_no_ping,
                Servidor_smtp = servidor_smtp,
                Clave = pass,
                Email = email,
                Tiempo_proceso_reporte = tiempo_proceso_reporte
            };
            return general.InsertConfig(generalConfig);

        }
        public ConfiguracionGeneral_BO ObtenerConfig()
        {
            var cgeneral = new ConfiguracionGeneral_DAO();
            return cgeneral.ObtenerConfig();
        }
        public ConfiguracionGeneral_BO GetConfigGeneral()
        {
            _configuracionGeneralDao = new ConfiguracionGeneral_DAO();
            return _configuracionGeneralDao.GetConfigGeneral();
        }
    }
}
