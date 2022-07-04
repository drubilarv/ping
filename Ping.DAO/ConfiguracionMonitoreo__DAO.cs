using Microsoft.ApplicationBlocks.Data;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace Ping.DAO
{
    public class ConfiguracionMonitoreo__DAO
    {
        string _conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();
        public bool InsertConfiguracionMonitoreo(ConfiguracionMonitoreo_BO config_monitoreo)
        {
            try
            {
                var parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("@nombre_configuracion", config_monitoreo.NombreConfig);
                parametros[1] = new SqlParameter("@tiempo_ping", config_monitoreo.TiempoPing);
                parametros[2] = new SqlParameter("@tamaño_patente", config_monitoreo.TamPaquete);
                parametros[3] = new SqlParameter("@timeout", config_monitoreo.Timeout);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_INSERT_CONFIGURACION_MONITOREO", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ConfiguracionMonitoreo_ DAO.cs(metodo InsertConfiguracionMonitoreo) " + ex.Message);
                return false;
            }
        }

        public ConfiguracionMonitoreo_BO ObtenerConfig(int id)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@ID", id);
                var configmonitoreo = new ConfiguracionMonitoreo_BO();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW1501_SELECT_CONFIG_MONITOREO", parametros);
                while (data.Read())
                {
                    configmonitoreo.NombreConfig = Convert.ToString(data["NON_CONFIGURACION"]);
                    configmonitoreo.IdConfig = Convert.ToInt16(data["ID_CONFIGURACION"]);
                }
                conexion.Close();
                conexion.Dispose();
                return configmonitoreo;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ConfiguracionMonitoreo_ DAO.cs(metodo ObtenerConfig(@)) " + ex.Message);
                return null;
            }
        }

        public List<ConfiguracionMonitoreo_BO> ObtenerConfig()
        {
            try
            {
                var list = new List<ConfiguracionMonitoreo_BO>();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW1501_SELECT_CONFIGURACION_MONITO");
                while (data.Read())
                {
                    var config = new ConfiguracionMonitoreo_BO
                    {
                        IdConfig = Convert.ToInt32(data["ID_CONFIGURACION"]),
                        NombreConfig = Convert.ToString(data["NON_CONFIGURACION"]),
                        TiempoPing = string.Format("{0:n}", data["TIEMPO_PING"]).Contains(',') ? string.Format("{0:n}", data["TIEMPO_PING"]).Split(',')[0] : string.Format("{0:n}", data["TIEMPO_PING"]),//
                        TamPaquete = string.Format("{0:n}", data["TAM_PAQUETE"]).Contains(',') ? string.Format("{0:n}", data["TAM_PAQUETE"]).Split(',')[0] : string.Format("{0:n}", data["TAM_PAQUETE"]),//
                        Timeout = string.Format("{0:n}", data["TIMEOUT"]).Contains(',') ? string.Format("{0:n}", data["TIMEOUT"]).Split(',')[0] : string.Format("{0:n}", data["TIMEOUT"]),//
                    };
                    list.Add(config);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ConfiguracionMonitoreo_ DAO.cs(metodo ObtenerConfig) " + ex.Message);
                return null;
            }
        }
        public bool DeleteConfig(ConfiguracionMonitoreo_BO config)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@ID_CONFIGURACION", config.IdConfig);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_DELETE_CONFIGURACION_MONITOREO", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ConfiguracionMonitoreo_ DAO.cs(metodo DeleteConfig) " + ex.Message);
                return false;
            }
        }
        public bool UpdateConfiguracionMonitoreo(ConfiguracionMonitoreo_BO config_monitoreo)
        {
            try
            {
                var parametros = new SqlParameter[5];
                parametros[0] = new SqlParameter("@ID_CONFIGURACION", config_monitoreo.IdConfig);
                parametros[1] = new SqlParameter("@NON_CONFIGURACION", config_monitoreo.NombreConfig);
                parametros[2] = new SqlParameter("@TIEMPO_PING", config_monitoreo.TiempoPing);
                parametros[3] = new SqlParameter("@TAM_PAQUETE", config_monitoreo.TamPaquete);
                parametros[4] = new SqlParameter("@TIMEOUT", config_monitoreo.Timeout);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "UPDATE_CONFIGURACION_MONITOREO", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ConfiguracionMonitoreo_ DAO.cs(metodo UpdateConfiguracionMonitoreo) " + ex.Message);
                return false;
            }
        }
        public bool isEquiposActivosConfig(int id)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@ID_CONFIGURACION", id);
                bool existen = false;
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SP_SW1001_SELECT_EQUIPOS_ACTIVOS_CONFIGURACION_ID", parametros);
                while (data.Read())
                {
                    if (Convert.ToInt32(data["cantidad"]) > 0)
                        existen = true;
                }
                conexion.Close();
                conexion.Dispose();
                return existen;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ConfiguracionMonitoreo_ DAO.cs(metodo isEquiposActivosConfig) " + ex.Message);
                return false;
            }
        }
        public ConfiguracionMonitoreo_BO GetConfiguracionPorId(int id)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@ID", id);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_CONFIG_MONITOREO_POR_ID", parametros).Tables[0];

                var configuracion = new ConfiguracionMonitoreo_BO();
                configuracion.IdConfig = Convert.ToInt32(dt.Rows[0]["ID_CONFIGURACION"].ToString());
                configuracion.NombreConfig = dt.Rows[0]["NON_CONFIGURACION"].ToString();
                configuracion.TiempoPing = dt.Rows[0]["TIEMPO_PING"].ToString();//
                configuracion.TamPaquete = dt.Rows[0]["TAM_PAQUETE"].ToString();//
                configuracion.Timeout = dt.Rows[0]["TIMEOUT"].ToString();//
                conexion.Close();
                conexion.Dispose();
                return configuracion;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ConfiguracionMonitoreo_ DAO.cs(metodo GetConfiguracionPorId) " + ex.Message);
                return null;
            }
        }
        public static int GetIdConfiguracion(string ip)
        {
            try
            {
                string _conex =  ConfigurationManager.ConnectionStrings["ConexPing"].ToString();

                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@IP", ip);

                DataTable dt = SqlHelper.ExecuteDataset(_conex, CommandType.StoredProcedure, "SP_SW15001_SELECT_EQUIPOS_POR_IP", parametros).Tables[0];
                int configStandar = Convert.ToInt32(ConfigurationManager.AppSettings.Get("idConfiguracionStandar"));

                if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["ID_CONFIGURACION"].ToString()))
                {
                    if (configStandar == Convert.ToInt32(dt.Rows[0]["ID_CONFIGURACION"]))
                    {
                        int idGrupo = Convert.ToInt32(dt.Rows[0]["ID_GRUPO"]);
                        var parametros2 = new SqlParameter[1];
                        parametros2[0] = new SqlParameter("@ID_GRUPO", idGrupo);
                        dt = SqlHelper.ExecuteDataset(_conex, CommandType.StoredProcedure, "SP_SW15001_SELECT_GRUPOS_POR_ID", parametros2).Tables[0];
                        if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["ID_CONFIGURACION"].ToString()))
                        {
                            if (configStandar != Convert.ToInt32(dt.Rows[0]["ID_CONFIGURACION"]))
                                return Convert.ToInt32(dt.Rows[0]["ID_CONFIGURACION"]);
                        }
                    }
                    else
                        return Convert.ToInt32(dt.Rows[0]["ID_CONFIGURACION"]);
                }

                return configStandar;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ConfiguracionMonitoreo_ DAO.cs(metodo GetIdConfiguracion) " + ex.Message);
                return 0;
            }
        }

        //public List<ConfiguracionMonitoreo_BO> GetConfigPorRecurso(int idConfig)
        //{
        //    ConfiguracionMonitoreo_BO conf;
        //    var list = new List<ConfiguracionMonitoreo_BO>();

        //    var parametros = new SqlParameter[1];
        //    parametros[0] = new SqlParameter("@ID_CONFIGURACION", idConfig);
        //    DataTable dt;

        //    dt = SqlHelper.ExecuteDataset(_conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_CONFIGURACION_POR_EQUIPO", parametros).Tables[0];

        //    if (dt.Rows.Count <= 0 && dt.Rows == null)
        //    dt = SqlHelper.ExecuteDataset(_conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_CONFIGURACION_POR_GRUPO", parametros).Tables[0];

        //    else if (dt.Rows.Count <= 0 && dt.Rows == null)
        //    {
        //        idConfig = Convert.ToInt32(ConfigurationManager.ConnectionStrings["Ping.View.Properties.Settings.idConfiguracionStandar"]);
        //        var parametros2 = new SqlParameter[1];
        //        parametros2[0] = new SqlParameter("@ID_CONFIGURACION", idConfig);
        //        dt = SqlHelper.ExecuteDataset(_conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_CONFIGURACION_STANDAR", parametros2).Tables[0];
        //    }

        //    else if (dt.Rows.Count == 0 && dt.Rows != null)
        //        return null;

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            conf = new ConfiguracionMonitoreo_BO();
        //            conf.IdConfig = Convert.ToInt32(dr["ID_CONFIGURACION"].ToString());
        //            conf.NombreConfig = dr["NON_CONFIGURACION"].ToString();
        //            conf.TiempoPing = Convert.ToDouble(dr["TIEMPO_PING"].ToString());
        //            conf.TamPaquete = Convert.ToDouble(dr["TAM_PAQUETE"].ToString());
        //            conf.Timeout = Convert.ToDouble(dr["TIMEOUT"].ToString());

        //            list.Add(conf);
        //        }

        //    return list;
        //}
    }
}
