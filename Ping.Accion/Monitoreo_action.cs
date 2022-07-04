using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ping.DAO;
using Ping.BO;

namespace Ping.Accion
{
    public class Monitoreo_action
    {
        private Monitoreo_DAO _monitoreoDao;

        //public static async Task InsertMonitoreo(string ip, DateTime timespamp, bool exito, double latencia, double jitter)
        //{
        //    _monitoreoDao = new Monitoreo_DAO();
        //    await _monitoreoDao.InsertMonitoreo(ip,timespamp,exito,latencia,jitter);
        //}
        public List<Monitoreo_BO> GetMonitoreoPorIp(string ip, int segundosLapsoNoExito)
        {
            _monitoreoDao = new Monitoreo_DAO();
            return _monitoreoDao.GetMonitoreoPorIp(ip, segundosLapsoNoExito);
        }
        public List<Monitoreo_BO> GetMonitoreoPingNoExitoPorIp(string ip, int segundosLapsoNoExito)
        {
            _monitoreoDao = new Monitoreo_DAO();
            return _monitoreoDao.GetMonitoreoPingNoExitoPorIp(ip, segundosLapsoNoExito);
        }
        public double GetLatenciaAnterior(string ip)
        {
            var mdao = new Monitoreo_DAO();
            return mdao.GetLatenciaAnterior(ip);
        }

        public DataTable GetAllMonitoreoParaReportAutomat()
        {
            var mdao = new Monitoreo_DAO();
            return mdao.GetAllMonitoreoParaReportAutomat();
        }

        public List<Monitoreo_BO> GetMonitoreoReportHistorico(int idGrupo, string ip, DateTime fechaDesde, DateTime fechaHasta, bool exitoPing)
        {
            var mdao = new Monitoreo_DAO();
            return mdao.GetMonitoreoReportHistorico(idGrupo, ip, fechaDesde, fechaHasta, exitoPing);
        }
        public List<Monitoreo_BO> GetMonitoreoReporSinFiltroFechaHistorico(int idGrupo, string ip, bool exitoPing)
        {
            var mdao = new Monitoreo_DAO();
            return mdao.GetMonitoreoReporSinFiltroFechaHistorico(idGrupo, ip, exitoPing);
        }
        public List<Monitoreo_BO> GetMonitoreoReporFiltroFechaInicioHistorico(int idGrupo, string ip, DateTime fechaDesde, bool exitoPing)
        {
            var mdao = new Monitoreo_DAO();
            return mdao.GetMonitoreoReporFiltroFechaInicioHistorico(idGrupo, ip, fechaDesde, exitoPing);
        }
        public List<Monitoreo_BO> GetMonitoreoReporFiltroFechaFinHistorico(int idGrupo, string ip, DateTime fechaHasta, bool exitoPing)
        {
            var mdao = new Monitoreo_DAO();
            return mdao.GetMonitoreoReporFiltroFechaFinHistorico(idGrupo, ip, fechaHasta, exitoPing);
        }


    }
}
