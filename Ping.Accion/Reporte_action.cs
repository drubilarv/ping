using Ping.BO;
using Ping.DAO;
using System;
using System.Collections.Generic;
using System.Data;

namespace Ping.Accion
{
    public class Reporte_action
    {
        Reportes_DAO reportdao = new Reportes_DAO();
        public bool InsertReport(byte[] pdf)
        {
            try
            {
                var report = new Reportes_BO
                {
                    timestamp = System.DateTime.Now,
                    archivo = pdf
                };
                return reportdao.InsertReport(report);
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, ex.Message);
                return false;
            }
        }

        public object SelectReport(string fecha)
        {
            try
            {
                var report = new Reportes_BO
                {
                    timestamp = Convert.ToDateTime(fecha)
                };
                return reportdao.SelectReport(report);
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, ex.Message);
                return null;
            }
        }
        public List<Reportes_BO> SelectReportRango(DateTime inicio, DateTime fin)
        {
            try
            {
                return reportdao.SelectReport(inicio, fin);
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, ex.Message);
                return null;
            }
        }
        public DataTable SelectArchivoReport()
        {
            return reportdao.SelectArchivoReport();
        }
    }
}
