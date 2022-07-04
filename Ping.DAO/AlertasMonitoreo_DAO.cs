using Microsoft.ApplicationBlocks.Data;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Ping.DAO
{
   public class AlertasMonitoreo_DAO
    {
       string _conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();
        public bool InsertAlertaMonitoreo(string ip, DateTime timespamp, double porcentajePerdida, bool leido)
        {
            try
            {
                var parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("@IP_EQUIPO", ip);
                parametros[1] = new SqlParameter("@TIMESTAMP", timespamp);
                parametros[2] = new SqlParameter("@PORCENTAGE_PERDIDA", porcentajePerdida);
                parametros[3] = new SqlParameter("@LEIDO", leido);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SP_SW15001_INSERT_ALERTAS_MONITOREO", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "AlertasMonitoreo_DAO.cs(metodo InsertAlertaMonitoreo) " + ex.Message);
                return false;
            }
        }
        public List<AlertasMonitoreo_BO> GetAlertaMonitoreo()
        {
            try
            {
                var alertaMonit = new AlertasMonitoreo_BO();
                var list = new List<AlertasMonitoreo_BO>();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_ALERTAS_MONITOREO").Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    alertaMonit = new AlertasMonitoreo_BO();
                    alertaMonit.ipEquipo = dr["IP_EQUIPO"].ToString();
                    alertaMonit.Timestamp = Convert.ToDateTime(dr["TIMESTAMP"].ToString());
                    alertaMonit.porcentajePerdida = Convert.ToDouble(dr["PORCENTAGE_PERDIDA"].ToString());
                    alertaMonit.leido = Convert.ToBoolean(dr["LEIDO"].ToString());
                    list.Add(alertaMonit);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "AlertasMonitoreo_DAO.cs(metodo GetAlertaMonitoreo) " + ex.Message);
                return null;
            }
        }
        public List<AlertasMonitoreo_BO> ObtenerAlertas_no_Leidas()
        {
            var alertas = new List<AlertasMonitoreo_BO>();
            try
            {
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_ALERTAS_NO_LEIDAS");
                var alerta = new AlertasMonitoreo_BO();
                var equipo = new Equipos_DAO();
                while (data.Read())
                {
                    alerta = new AlertasMonitoreo_BO
                    {
                        id = Convert.ToInt32(data["id"]),
                        ipEquipo = Convert.ToString(data["IP_EQUIPO"]),
                        nombreEquipo = equipo.GetNombreEquipoPorIp(data["IP_EQUIPO"].ToString()),
                        Timestamp = Convert.ToDateTime(data["TIMESTAMP"]),
                        porcentajePerdida = Convert.ToDouble(data["PORCENTAGE_PERDIDA"]),
                        leido = Convert.ToBoolean(data["LEIDO"])
                    };
                    alertas.Add(alerta);
                }
                conexion.Close();
                conexion.Dispose();
                return alertas;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "AlertasMonitoreo_DAO.cs(metodo ObtenerAlertas_no_Leidas) " + ex.Message);
                return null;
            }
        }
        public bool UpdateAlertas(AlertasMonitoreo_BO alerta)
        {
            try
            {
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@ID", alerta.id);
                parametros[1] = new SqlParameter("@LEIDO", alerta.leido);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW15001_UPDATE_ALERTAS", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "AlertasMonitoreo_DAO.cs(metodo UpdateAlertas) " + ex.Message);
                return false;
            }
        }
        public List<AlertasMonitoreo_BO> GetAlertaMonitoreo(Grupos_BO grupo, string ipEquipo, DateTime fechaInicio, DateTime FechaFin, bool leido)
        {
            try
            {
                var alertaMonit = new AlertasMonitoreo_BO();
                var list = new List<AlertasMonitoreo_BO>();
                var parametros = new SqlParameter[5];
                parametros[0] = new SqlParameter("@ID_GRUPO", grupo.Id);
                parametros[1] = new SqlParameter("@IP_EQUIPO", ipEquipo);
                parametros[2] = new SqlParameter("@FECHAINICIO", fechaInicio);
                parametros[3] = new SqlParameter("@FECHAFIN", FechaFin);
                parametros[4] = new SqlParameter("@leido", leido);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                var data = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SW1501_SELECT_ALERTAS_FECHAS_LEIDO", parametros);
                DataTable dt = data.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    alertaMonit = new AlertasMonitoreo_BO();
                    alertaMonit.ipEquipo = dr["IP_EQUIPO"].ToString();
                    alertaMonit.nombreEquipo = dr["NOM_EQUIPO"].ToString();
                    alertaMonit.Timestamp = Convert.ToDateTime(dr["TIMESTAMP"].ToString());
                    alertaMonit.porcentajePerdida = Convert.ToDouble(dr["PORCENTAGE_PERDIDA"].ToString());
                    alertaMonit.leido = Convert.ToBoolean(dr["LEIDO"].ToString());
                    list.Add(alertaMonit);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "AlertasMonitoreo_DAO.cs(metodo GetAlertaMonitoreo) " + ex.Message);
                return null;
            }
        }
        public List<AlertasMonitoreo_BO> GetAlertaMonitoreoSinFiltroFechas(Grupos_BO grupo, string ipEquipo, bool leido)
        {
            try
            {
                var alertaMonit = new AlertasMonitoreo_BO();
                var list = new List<AlertasMonitoreo_BO>();
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@ID_GRUPO", grupo.Id);
                parametros[1] = new SqlParameter("@IP_EQUIPO", ipEquipo);
                parametros[2] = new SqlParameter("@leido", leido);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                var data = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SW1501_SELECT_ALERTAS_SIN_FILTRO_FECHAS", parametros);
                DataTable dt = data.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    alertaMonit = new AlertasMonitoreo_BO();
                    alertaMonit.ipEquipo = dr["IP_EQUIPO"].ToString();
                    alertaMonit.nombreEquipo = dr["NOM_EQUIPO"].ToString();
                    alertaMonit.Timestamp = Convert.ToDateTime(dr["TIMESTAMP"].ToString());
                    alertaMonit.porcentajePerdida = Convert.ToDouble(dr["PORCENTAGE_PERDIDA"].ToString());
                    alertaMonit.leido = Convert.ToBoolean(dr["LEIDO"].ToString());
                    list.Add(alertaMonit);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "AlertasMonitoreo_DAO.cs(metodo GetAlertaMonitoreoSinFiltroFechas) " + ex.Message);
                return null;
            }
        }
        public List<AlertasMonitoreo_BO> GetAlertaMonitoreoConFiltroFechaInicio(Grupos_BO grupo, string ipEquipo, DateTime fechaInicio, bool leido)
        {
            try
            {
                var alertaMonit = new AlertasMonitoreo_BO();
                var list = new List<AlertasMonitoreo_BO>();
                var parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("@ID_GRUPO", grupo.Id);
                parametros[1] = new SqlParameter("@IP_EQUIPO", ipEquipo);
                parametros[2] = new SqlParameter("@FECHAINICIO", fechaInicio);
                parametros[3] = new SqlParameter("@leido", leido);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                var data = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SW1501_SELECT_ALERTAS_FILTRO_FECHA_INICIO", parametros);
                DataTable dt = data.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    alertaMonit = new AlertasMonitoreo_BO();
                    alertaMonit.ipEquipo = dr["IP_EQUIPO"].ToString();
                    alertaMonit.nombreEquipo = dr["NOM_EQUIPO"].ToString();
                    alertaMonit.Timestamp = Convert.ToDateTime(dr["TIMESTAMP"].ToString());
                    alertaMonit.porcentajePerdida = Convert.ToDouble(dr["PORCENTAGE_PERDIDA"].ToString());
                    alertaMonit.leido = Convert.ToBoolean(dr["LEIDO"].ToString());
                    list.Add(alertaMonit);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "AlertasMonitoreo_DAO.cs(metodo GetAlertaMonitoreoConFiltroFechaInicio) " + ex.Message);
                return null;
            }
        }
        public List<AlertasMonitoreo_BO> GetAlertaMonitoreoConFiltroFechaFin(Grupos_BO grupo, string ipEquipo, DateTime FechaFin, bool leido)
        {
            try
            {
                var alertaMonit = new AlertasMonitoreo_BO();
                var list = new List<AlertasMonitoreo_BO>();
                var parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("@ID_GRUPO", grupo.Id);
                parametros[1] = new SqlParameter("@IP_EQUIPO", ipEquipo);
                parametros[2] = new SqlParameter("@FECHAFIN", FechaFin);
                parametros[3] = new SqlParameter("@leido", leido);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                var data = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SW1501_SELECT_ALERTAS_FILTRO_FECHA_FIN", parametros);
                DataTable dt = data.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    alertaMonit = new AlertasMonitoreo_BO();
                    alertaMonit.ipEquipo = dr["IP_EQUIPO"].ToString();
                    alertaMonit.nombreEquipo = dr["NOM_EQUIPO"].ToString();
                    alertaMonit.Timestamp = Convert.ToDateTime(dr["TIMESTAMP"].ToString());
                    alertaMonit.porcentajePerdida = Convert.ToDouble(dr["PORCENTAGE_PERDIDA"].ToString());
                    alertaMonit.leido = Convert.ToBoolean(dr["LEIDO"].ToString());
                    list.Add(alertaMonit);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "AlertasMonitoreo_DAO.cs(metodo GetAlertaMonitoreoConFiltroFechaFin) " + ex.Message);
                return null;
            }
        }
        public List<Equipos_BO> ObtenerEquipos(int idGrupo)
        {
            var parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@ID_GRUPO", idGrupo);
            var equipos = new List<Equipos_BO>();
            try
            {
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_EQUIPOS_POR_GRUPO", parametros);

                var equipo = new Equipos_BO();
                while (data.Read())
                {
                    equipo = new Equipos_BO
                    {
                        Id = Convert.ToString(data["IP_EQUIPO"]),
                        NombreEquipo = Convert.ToString(data["NOM_EQUIPO"])
                    };
                    equipos.Add(equipo);
                }
                conexion.Close();
                conexion.Dispose();
                return equipos;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "AlertasMonitoreo_DAO.cs(metodo ObtenerEquipos) " + ex.Message);
                return null;
            }
        }
        public List<Grupos_BO> ObtenerGrupos()
        {
            var grupos = new List<Grupos_BO>();
            try
            {
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW1501_SELECT_GRUPOS");
                var grupo = new Grupos_BO();
                while (data.Read())
                {
                    grupo = new Grupos_BO
                    {
                        Descripcion = Convert.ToString(data["DESC_GRUPO"]),
                        Id = Convert.ToInt32(data["ID_GRUPO"])
                    };
                    grupos.Add(grupo);
                }
                conexion.Close();
                conexion.Dispose();
                return grupos;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "AlertasMonitoreo_DAO.cs(metodo ObtenerGrupos) " + ex.Message);
                return null;
            }
        }
    }
}
