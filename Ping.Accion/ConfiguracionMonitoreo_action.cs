using Ping.DAO;
using Ping.BO;
using System.Collections.Generic;
namespace Ping.Accion
{
   public class ConfiguracionMonitoreo_action
    {
        private ConfiguracionMonitoreo__DAO _configuracionMonitoreoDao;

        public bool InsertConfigMonitoreo(string nombre_config, double tiempo_ping, double tamaño_paquete, double timeout)
        {
            var cmdao = new ConfiguracionMonitoreo__DAO();
            var cmbo = new ConfiguracionMonitoreo_BO
            {
                NombreConfig = nombre_config,
                TamPaquete = tamaño_paquete.ToString(),//
                TiempoPing = tiempo_ping.ToString(),//
                Timeout = timeout.ToString()//
            };
            return cmdao.InsertConfiguracionMonitoreo(cmbo);
        }
        public bool UpdateConfigMonitoreo(int id, string nombre_config, double tiempo_ping, double tamaño_paquete, double timeout)
        {
            var cmdao = new ConfiguracionMonitoreo__DAO();
            var cmbo = new ConfiguracionMonitoreo_BO
            {
                IdConfig = id,
                NombreConfig = nombre_config,
                TamPaquete = tamaño_paquete.ToString(),//
                TiempoPing = tiempo_ping.ToString(),//
                Timeout = timeout.ToString()//
            };
            return cmdao.UpdateConfiguracionMonitoreo(cmbo);
        }
        public List<ConfiguracionMonitoreo_BO> ObtenerConfig()
        {
            var cmdao = new ConfiguracionMonitoreo__DAO();
            return cmdao.ObtenerConfig();
        }
        public bool DeleteConfig(int id)
        {
            var cmdao = new ConfiguracionMonitoreo__DAO();
            var confg = new ConfiguracionMonitoreo_BO
            {
                IdConfig = id
            };
            return cmdao.DeleteConfig(confg);
        }
        public bool isEquiposActivosConfig(int id)
        {
            var configdao = new ConfiguracionMonitoreo__DAO();
            return configdao.isEquiposActivosConfig(id);

        }
        //////////////////////////////////////////////////////

        //public List<ConfiguracionMonitoreo_BO> GetConfigPorRecurso(int idConfig)
        //{
        //    _configuracionMonitoreoDao = new ConfiguracionMonitoreo__DAO();
        //    return _configuracionMonitoreoDao.GetConfigPorRecurso(idConfig);
        //}
        public static int GetIdConfiguracion(string ip)
        {
            return ConfiguracionMonitoreo__DAO.GetIdConfiguracion(ip);
        }
        public ConfiguracionMonitoreo_BO GetConfiguracionPorId(int id)
        {
            _configuracionMonitoreoDao = new ConfiguracionMonitoreo__DAO();
            return _configuracionMonitoreoDao.GetConfiguracionPorId(id);
        }
    }
}
