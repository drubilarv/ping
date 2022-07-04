using Microsoft.ApplicationBlocks.Data;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Ping.DAO
{
    public class Reportes_DAO
    {
        string _conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();
        public bool InsertReport(Reportes_BO reporte)
        {
            try
            {
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@TIMESTAMP", reporte.timestamp);
                parametros[1] = new SqlParameter("@ARCHIVO", reporte.archivo);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW15001_INSERT_REPORT", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "Reportes_DAO.cs(metodo InsertReport) " + ex.Message);
                return false;
            }
        }

        public object SelectReport(Reportes_BO report)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@TIMESTAMP", report.timestamp);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader dt = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW15001_SELECT_FOR_FECHA_REPORT", parametros);
                object data = new Object();
                while (dt.Read())
                {
                    data = dt["archivo"];
                }
                conexion.Close();
                conexion.Dispose();
                return data;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "Reportes_DAO.cs(metodo SelectReport) " + ex.Message);
                return null;
            }
        }

        public List<Reportes_BO> SelectReport(DateTime inicio, DateTime fin)
        {
            try
            {
                var list = new List<Reportes_BO>();
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@INICIO", inicio);
                parametros[1] = new SqlParameter("@FIN", fin);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader dt = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW15001_SELECT_RANGO_FECHA_REPORT", parametros);
                while (dt.Read())
                {
                    var report = new Reportes_BO
                    {
                        name = "Reporte_" + dt["timestamp"].ToString() + ".pdf"
                    };
                    list.Add(report);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "Reportes_DAO.cs(metodo SelectReport) " + ex.Message);
                return null;
            }
        }
        public System.Data.DataTable SelectArchivoReport()
        {
            try
            {
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SW15001_SELECT_ARCHIVO_REPORTES").Tables[0];
                conexion.Close();
                conexion.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "Reportes_DAO.cs(metodo SelectArchivoReport) " + ex.Message);
                return null;
            }
        }
    }
}
