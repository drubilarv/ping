using Microsoft.ApplicationBlocks.Data;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;


namespace Ping.DAO
{
    public class Monitoreo_DAO
    {
        static string _conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();
       public static async Task InsertMonitoreo(string ip, DateTime timespamp, bool exito, double latencia,  double timeEntrePing)
       {
           try
           {
               var parametros = new SqlParameter[5];
               parametros[0] = new SqlParameter("@IP_EQUIPO", ip);
               parametros[1] = new SqlParameter("@TIMESTAMP", timespamp);
               parametros[2] = new SqlParameter("@EXITO_PING", exito);
               parametros[3] = new SqlParameter("@LATENCIA", latencia);
               //parametros[4] = new SqlParameter("@JITTER", jitter);
               parametros[4] = new SqlParameter("@TIEMPO_ENTRE_PING", timeEntrePing);
               var conexion = new SqlConnection(_conexion);
               conexion.Open();
               //SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SP_SW15001_INSERT_MONITOREO", parametros);
               SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SP_SW15001_INSERT_MONITOREO_CON_JITTER", parametros);
               conexion.Close();
               conexion.Dispose();
           }
           catch (Exception ex)
           {
               var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
               logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Monitoreo_DAO.cs(metodo InsertMonitoreo) " + ex.Message);
           }
       }

       public List<Monitoreo_BO> GetMonitoreoPorIp(string ip,int segundosLapsoNoExito)
       {
           try
           {
               var monitoreo = new Monitoreo_BO();
               var list = new List<Monitoreo_BO>();

               var parametros = new SqlParameter[2];
               parametros[0] = new SqlParameter("@IP_EQUIPO", ip);
               parametros[1] = new SqlParameter("@SEGUNDOS_LAPSO_NO_EXITO", segundosLapsoNoExito);

               var conexion = new SqlConnection(_conexion);
               conexion.Open();
               DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_MONITOREO_POR_IP", parametros).Tables[0];
             
               foreach (DataRow dr in dt.Rows)
               {
                   monitoreo = new Monitoreo_BO();
                   monitoreo.ipEquipo = dr["IP_EQUIPO"].ToString();
                   monitoreo.Timestamp = Convert.ToDateTime(dr["TIMESTAMP"].ToString());
                   monitoreo.exitoPing = Convert.ToBoolean(dr["EXITO_PING"].ToString());
                   monitoreo.latencia = dr["LATENCIA"].ToString();
                   monitoreo.jitter = dr["JITTER"].ToString();

                   list.Add(monitoreo);
               }
               conexion.Close();
               conexion.Dispose();
               return list;
           }
           catch (Exception ex)
           {
               var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
               logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Monitoreo_DAO.cs(metodo GetMonitoreoPorIp) " + ex.Message);
               return null;
           }
       }
       public List<Monitoreo_BO> GetMonitoreoPingNoExitoPorIp(string ip, int segundosLapsoNoExito)
       {
           try
           {
               var monitoreo = new Monitoreo_BO();
               var list = new List<Monitoreo_BO>();

               var parametros = new SqlParameter[2];
               parametros[0] = new SqlParameter("@IP_EQUIPO", ip);
               parametros[1] = new SqlParameter("@SEGUNDOS_LAPSO_NO_EXITO", segundosLapsoNoExito);
               var conexion = new SqlConnection(_conexion);
               conexion.Open();
               DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_MONITOREO_PING_NO_EXITO_POR_IP", parametros).Tables[0];

               foreach (DataRow dr in dt.Rows)
               {
                   monitoreo = new Monitoreo_BO();
                   monitoreo.ipEquipo = dr["IP_EQUIPO"].ToString();
                   monitoreo.Timestamp = Convert.ToDateTime(dr["TIMESTAMP"].ToString());
                   monitoreo.exitoPing = Convert.ToBoolean(dr["EXITO_PING"].ToString());
                   monitoreo.latencia = dr["LATENCIA"].ToString();
                   monitoreo.jitter = dr["JITTER"].ToString();

                   list.Add(monitoreo);
               }
               conexion.Close();
               conexion.Dispose();
               return list;
           }
           catch (Exception ex)
           {
               var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
               logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Monitoreo_DAO.cs(metodo GetMonitoreoPingNoExitoPorIp) " + ex.Message);
               return null;
           }
       }
       public double GetLatenciaAnterior(string ip)
       {
           try
           {
               var parametros = new SqlParameter[1];
               parametros[0] = new SqlParameter("@IP_EQUIPO", ip);
               double latencia = 0;
               var conexion = new SqlConnection(_conexion);
               conexion.Open();
               SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW1501_SELECT_LATENCIA_PARA_JITTER", parametros);

               while (data.Read())
               {
                   latencia = Convert.ToDouble(data["LATENCIA"]);
               }
               conexion.Close();
               conexion.Dispose();
               return latencia;
           }
           catch (Exception ex)
           {
               var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
               logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Monitoreo_DAO.cs(metodo GetLatenciaAnterior) " + ex.Message);
               return 0;
           }
       }

       public DataTable GetAllMonitoreoParaReportAutomat()
       {
           try
           {
               //var monitoreo = new Monitoreo_BO();
               //var list = new List<Monitoreo_BO>();
               var conexion = new SqlConnection(_conexion);
               conexion.Open();
               DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_MONITOREO_REPORT_AUTOMATICO").Tables[0];
               conexion.Close();
               conexion.Dispose();
               return dt;
               //foreach (DataRow dr in dt.Rows)
               //{
               //    monitoreo = new Monitoreo_BO();
               //    monitoreo.ipEquipo = dr["IP_EQUIPO"].ToString();
               //    monitoreo.timestamp = Convert.ToDateTime(dr["TIMESTAMP"].ToString());
               //    monitoreo.exitoPing = Convert.ToBoolean(dr["EXITO_PING"].ToString());
               //    monitoreo.latencia = Convert.ToDouble(dr["LATENCIA"].ToString());
               //    monitoreo.jitter = Convert.ToDouble(dr["JITTER"].ToString());

               //    list.Add(monitoreo);
               //}
               //return list;
           }
           catch (Exception ex)
           {
               var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
               logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Monitoreo_DAO.cs(metodo GetAllMonitoreoParaReportAutomat) " + ex.Message);
               return null;
           }
       }

       public List<Monitoreo_BO> GetMonitoreoReportHistorico(int idGrupo,string ip, DateTime fechaDesde, DateTime fechaHasta, bool exitoPing)
       {
           try
           {
               var parametros = new SqlParameter[5];
               parametros[0] = new SqlParameter("@ID_GRUPO", idGrupo);
               parametros[1] = new SqlParameter("@IP_EQUIPO", ip);
               parametros[2] = new SqlParameter("@fechaDesde", fechaDesde);
               parametros[3] = new SqlParameter("@fechaHasta", fechaHasta);
               parametros[4] = new SqlParameter("@EXITO_PING", exitoPing);

               var monitoreo = new Monitoreo_BO();
               var list = new List<Monitoreo_BO>();

               var conexion = new SqlConnection(_conexion);
               conexion.Open();
               DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_MONITOREO_REPORT_HISTORICO", parametros).Tables[0];

               foreach (DataRow dr in dt.Rows)
               {
                   monitoreo = new Monitoreo_BO();
                   monitoreo.ipEquipo = dr["IP_EQUIPO"].ToString();
                   monitoreo.Timestamp = Convert.ToDateTime(dr["TIMESTAMP"]);
                   monitoreo.exitoPingStr = dr["EXITO_PING"].ToString();
                   monitoreo.latencia =   (Convert.ToDouble(string.Format("{0:n}", dr["LATENCIA"]).Contains(',')
                                           ? string.Format("{0:n}", dr["LATENCIA"]).Split(',')[0]
                                            : string.Format("{0:n}", dr["LATENCIA"])) * 1000).ToString("N0");

                   monitoreo.jitter = (Convert.ToDouble(string.Format("{0:n}", dr["JITTER"]).Contains(',')
                                           ? string.Format("{0:n}", dr["JITTER"]).Split(',')[0]
                                            : string.Format("{0:n}", dr["JITTER"])) * 1000).ToString("N0");
                        
                   monitoreo.nombreGrupo = dr["DESC_GRUPO"].ToString();
                   list.Add(monitoreo);
               }
               conexion.Close();
               conexion.Dispose();
               return list;
           }
           catch (Exception ex)
           {
               var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
               logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Monitoreo_DAO.cs(metodo GetMonitoreoReportHistorico) " + ex.Message);
               return null;
           }
       }


       public List<Monitoreo_BO> GetMonitoreoReporSinFiltroFechaHistorico(int idGrupo, string ip, bool exitoPing)
       {
           try
           {
               var parametros = new SqlParameter[3];
               parametros[0] = new SqlParameter("@ID_GRUPO", idGrupo);
               parametros[1] = new SqlParameter("@IP_EQUIPO", ip);
               parametros[2] = new SqlParameter("@EXITO_PING", exitoPing);

               var monitoreo = new Monitoreo_BO();
               var list = new List<Monitoreo_BO>();

               var conexion = new SqlConnection(_conexion);
               conexion.Open();
               DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_MONITOREO_SIN_FILTRO_FECHA_REPORT_HISTORICO", parametros).Tables[0];

               foreach (DataRow dr in dt.Rows)
               {
                   monitoreo = new Monitoreo_BO();
                   monitoreo.ipEquipo = dr["IP_EQUIPO"].ToString();
                   monitoreo.Timestamp = Convert.ToDateTime(dr["TIMESTAMP"]);
                   monitoreo.exitoPingStr = dr["EXITO_PING"].ToString();
                   monitoreo.latencia = (Convert.ToDouble(string.Format("{0:n}", dr["LATENCIA"]).Contains(',')
                                         ? string.Format("{0:n}", dr["LATENCIA"]).Split(',')[0]
                                          : string.Format("{0:n}", dr["LATENCIA"])) * 1000).ToString("N0");

                   monitoreo.jitter = (Convert.ToDouble(string.Format("{0:n}", dr["JITTER"]).Contains(',')
                                           ? string.Format("{0:n}", dr["JITTER"]).Split(',')[0]
                                            : string.Format("{0:n}", dr["JITTER"])) * 1000).ToString("N0");
                   monitoreo.nombreGrupo = dr["DESC_GRUPO"].ToString();
                   list.Add(monitoreo);
               }
               conexion.Close();
               conexion.Dispose();
               return list;
           }
           catch (Exception ex)
           {
               var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
               logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Monitoreo_DAO.cs(metodo GetMonitoreoReporSinFiltroFechaHistorico) " + ex.Message);
               return null;
           }
       }
       public List<Monitoreo_BO> GetMonitoreoReporFiltroFechaInicioHistorico(int idGrupo, string ip,DateTime fechaDesde, bool exitoPing)
       {
           try
           {
               var parametros = new SqlParameter[4];
               parametros[0] = new SqlParameter("@ID_GRUPO", idGrupo);
               parametros[1] = new SqlParameter("@IP_EQUIPO", ip);
               parametros[2] = new SqlParameter("@fechaDesde", fechaDesde);
               parametros[3] = new SqlParameter("@EXITO_PING", exitoPing);

               var monitoreo = new Monitoreo_BO();
               var list = new List<Monitoreo_BO>();

               var conexion = new SqlConnection(_conexion);
               conexion.Open();
               DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_MONITOREO_FILTRO_FECHA_INICIO_REPORT_HISTORICO", parametros).Tables[0];

               foreach (DataRow dr in dt.Rows)
               {
                   monitoreo = new Monitoreo_BO();
                   monitoreo.ipEquipo = dr["IP_EQUIPO"].ToString();
                   monitoreo.Timestamp = Convert.ToDateTime(dr["TIMESTAMP"]);
                   monitoreo.exitoPingStr = dr["EXITO_PING"].ToString();
                   monitoreo.latencia = (Convert.ToDouble(string.Format("{0:n}", dr["LATENCIA"]).Contains(',')
                                           ? string.Format("{0:n}", dr["LATENCIA"]).Split(',')[0]
                                            : string.Format("{0:n}", dr["LATENCIA"])) * 1000).ToString("N0");

                   monitoreo.jitter = (Convert.ToDouble(string.Format("{0:n}", dr["JITTER"]).Contains(',')
                                           ? string.Format("{0:n}", dr["JITTER"]).Split(',')[0]
                                            : string.Format("{0:n}", dr["JITTER"])) * 1000).ToString("N0");
                   monitoreo.nombreGrupo = dr["DESC_GRUPO"].ToString();
                   list.Add(monitoreo);
               }
               conexion.Close();
               conexion.Dispose();
               return list;
           }
           catch (Exception ex)
           {
               var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
               logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Monitoreo_DAO.cs(metodo GetMonitoreoReporFiltroFechaInicioHistorico) " + ex.Message);
               return null;
           }
       }
       public List<Monitoreo_BO> GetMonitoreoReporFiltroFechaFinHistorico(int idGrupo, string ip, DateTime fechaHasta, bool exitoPing)
       {
           try
           {
               var parametros = new SqlParameter[4];
               parametros[0] = new SqlParameter("@ID_GRUPO", idGrupo);
               parametros[1] = new SqlParameter("@IP_EQUIPO", ip);
               parametros[2] = new SqlParameter("@fechaHasta", fechaHasta);
               parametros[3] = new SqlParameter("@EXITO_PING", exitoPing);

               var monitoreo = new Monitoreo_BO();
               var list = new List<Monitoreo_BO>();

               var conexion = new SqlConnection(_conexion);
               conexion.Open();
               DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_MONITOREO_FILTRO_FECHA_FIN_REPORT_HISTORICO", parametros).Tables[0];

               foreach (DataRow dr in dt.Rows)
               {
                   monitoreo = new Monitoreo_BO();
                   monitoreo.ipEquipo = dr["IP_EQUIPO"].ToString();
                   monitoreo.Timestamp = Convert.ToDateTime(dr["TIMESTAMP"]);
                   monitoreo.exitoPingStr = dr["EXITO_PING"].ToString();
                   monitoreo.latencia = (Convert.ToDouble(string.Format("{0:n}", dr["LATENCIA"]).Contains(',')
                                          ? string.Format("{0:n}", dr["LATENCIA"]).Split(',')[0]
                                           : string.Format("{0:n}", dr["LATENCIA"])) * 1000).ToString("N0");

                   monitoreo.jitter = (Convert.ToDouble(string.Format("{0:n}", dr["JITTER"]).Contains(',')
                                           ? string.Format("{0:n}", dr["JITTER"]).Split(',')[0]
                                            : string.Format("{0:n}", dr["JITTER"])) * 1000).ToString("N0");
                   monitoreo.nombreGrupo = dr["DESC_GRUPO"].ToString();
                   list.Add(monitoreo);
               }
               conexion.Close();
               conexion.Dispose();
               return list;
           }
           catch (Exception ex)
           {
               var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
               logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Monitoreo_DAO.cs(metodo GetMonitoreoReporFiltroFechaFinHistorico) " + ex.Message);
               return null;
           }
       }




       public static async Task InsertMonitoreoPost(string p1, DateTime dateTime, bool p2, long p3, double jitter, int timeEntrePing)
       {
           try
           {
               int pingexitoso = p2 == true ? 1 : 0;
               string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                    "localhost", 5432, "administrador", "sa12345.", "MonitoreoPong");

               NpgsqlConnection conn = new NpgsqlConnection(connstring);
               conn.Open();



               string sql = "INSERT INTO MONITOREO(ip_equipo,timestamp,exito_ping,latencia,jitter,tiempo_entre_ping) VALUES ('" + p1 + "','" + dateTime.ToString() + "',B'" + pingexitoso + "'," + p3 + "," + jitter + "," + timeEntrePing + ");";

               NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);


               cmd.ExecuteNonQuery();

               //var parametros = new SqlParameter[6];
               //parametros[0] = new SqlParameter("@IP_EQUIPO", ip);
               //parametros[1] = new SqlParameter("@TIMESTAMP", timespamp);
               //parametros[2] = new SqlParameter("@EXITO_PING", exito);
               //parametros[3] = new SqlParameter("@LATENCIA", latencia);
               //parametros[4] = new SqlParameter("@JITTER", jitter);
               //parametros[5] = new SqlParameter("@TIEMPO_ENTRE_PING", timeEntrePing);

               //var conexion = new SqlConnection(_conexion);
               //conexion.Open();
               //SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SP_SW15001_INSERT_MONITOREO", parametros);
               //conexion.Close();
               //conexion.Dispose();
           }
           catch (Exception ex)
           {
               //var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
               //logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Monitoreo_DAO.cs(metodo InsertMonitoreo) " + ex.Message);
           }
       }
    }
}



    


