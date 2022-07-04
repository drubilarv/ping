using Ping.BO;
using Ping.DAO;
using System;
using System.Collections.Generic;


namespace Ping.Accion
{
    public class AlertasMonitoreo_Action
    {
        private AlertasMonitoreo_DAO _configuracionGeneralDao;

        public bool InsertAlertaMonitoreo(string ip, DateTime timespamp, double porcentajePerdida, bool leido)
        {
            _configuracionGeneralDao = new AlertasMonitoreo_DAO();
            return _configuracionGeneralDao.InsertAlertaMonitoreo(ip, timespamp, porcentajePerdida, leido);
        }
        public List<AlertasMonitoreo_BO> GetAlertaMonitoreo()
        {
            _configuracionGeneralDao = new AlertasMonitoreo_DAO();
            return _configuracionGeneralDao.GetAlertaMonitoreo();
        }
        public List<AlertasMonitoreo_BO> ObtenerAlertas_no_Leidas()
        {
            var adao = new AlertasMonitoreo_DAO();
            return adao.ObtenerAlertas_no_Leidas();
        }
        public void UpdateAlerta(List<AlertasMonitoreo_BO> datos)
        {
            AlertasMonitoreo_DAO adao = new AlertasMonitoreo_DAO();
            if (datos != null)
            {
                foreach (AlertasMonitoreo_BO alerta in datos)
                {
                    adao.UpdateAlertas(alerta);
                }
            }
        }
        public List<Grupos_BO> ObtenerGrupos()
        {
            var adao = new AlertasMonitoreo_DAO();
            return adao.ObtenerGrupos();
        }
        public List<Equipos_BO> ObtenerEquipos(int idGrupo)
        {
            var adao = new AlertasMonitoreo_DAO();
            return adao.ObtenerEquipos(idGrupo);
        }
        public List<AlertasMonitoreo_BO> GetAlertaMonitoreo(Grupos_BO grupo, string ipEquipo, DateTime inicio, DateTime fin, bool leido)
        {
            _configuracionGeneralDao = new AlertasMonitoreo_DAO();
            return _configuracionGeneralDao.GetAlertaMonitoreo(grupo, ipEquipo, inicio, fin, leido);
        }
        public List<AlertasMonitoreo_BO> GetAlertaMonitoreoSinFiltroFechas(Grupos_BO grupo, string ipEquipo, bool leido)
        {
            _configuracionGeneralDao = new AlertasMonitoreo_DAO();
            return _configuracionGeneralDao.GetAlertaMonitoreoSinFiltroFechas(grupo, ipEquipo, leido);
        }
        public List<AlertasMonitoreo_BO> GetAlertaMonitoreoConFiltroFechaInicio(Grupos_BO grupo, string ipEquipo, DateTime inicio, bool leido)
        {
            _configuracionGeneralDao = new AlertasMonitoreo_DAO();
            return _configuracionGeneralDao.GetAlertaMonitoreoConFiltroFechaInicio(grupo, ipEquipo, inicio, leido);
        }
        public List<AlertasMonitoreo_BO> GetAlertaMonitoreoConFiltroFechaFin(Grupos_BO grupo, string ipEquipo, DateTime fin, bool leido)
        {
            _configuracionGeneralDao = new AlertasMonitoreo_DAO();
            return _configuracionGeneralDao.GetAlertaMonitoreoConFiltroFechaFin(grupo, ipEquipo, fin, leido);
        }
    }
}
