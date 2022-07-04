using Microsoft.ApplicationBlocks.Data;
using Ping.BO;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Ping.DAO
{
    public class ConfiguracionGeneral_DAO
    {
        string _conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();
        public bool ActualizaConfig(ConfiguracionGeneral_BO config)
        {
            try
            {
                var parametros = new SqlParameter[9];
                parametros[0] = new SqlParameter("@PORCENTAGE_PERDIDA_PING_NO_EXITOSO", config.Ping_no_exitoso);
                parametros[1] = new SqlParameter("@SEGUNDOS_GENERA_ALARMA", config.Generar_alarma);
                parametros[2] = new SqlParameter("@TIEMPO_NUEVA_ALERTA", config.Tiempo_nueva_alerta);
                parametros[3] = new SqlParameter("@FRECUENCIA_ALTERNATIVA_NO_PING", config.Frecuencia_no_ping);
                parametros[4] = new SqlParameter("@SERVIDOR_SMTP", config.Servidor_smtp);
                parametros[5] = new SqlParameter("@EMAIL", config.Email);
                parametros[6] = new SqlParameter("@PASS_EMAIL", config.Clave);
                parametros[7] = new SqlParameter("@TIME_PROCESO_REPORTE", config.Tiempo_proceso_reporte);
                parametros[8] = new SqlParameter("@TIME_DEPURACION", config.Time_depuracion);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_UPDATE_CONFIGURACION_GENERAL", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "ConfiguracionGeneral_DAO.cs(metodo ActualizaConfig) " + ex.Message);
                return false;
            }
        }

        public bool InsertConfig(ConfiguracionGeneral_BO config)
        {
            try
            {
                var parametros = new SqlParameter[8];
                parametros[0] = new SqlParameter("@PORCENTAGE_PERDIDA_PING_NO_EXITOSO", config.Ping_no_exitoso);
                parametros[1] = new SqlParameter("@SEGUNDOS_GENERA_ALARMA", config.Generar_alarma);
                parametros[2] = new SqlParameter("@TIEMPO_NUEVA_ALERTA", config.Tiempo_nueva_alerta);
                parametros[3] = new SqlParameter("@FRECUENCIA_ALTERNATIVA_NO_PING", config.Frecuencia_no_ping);
                parametros[4] = new SqlParameter("@SERVIDOR_SMTP", config.Servidor_smtp);
                parametros[5] = new SqlParameter("@EMAIL", config.Email);
                parametros[6] = new SqlParameter("@PASS_EMAIL", config.Clave);
                parametros[7] = new SqlParameter("@TIME_PROCESO_REPORTE", config.Tiempo_proceso_reporte);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_INSERT_CONFIG_GENERAL", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, System.DateTime.Now, Environment.UserName, "ConfiguracionGeneral_DAO.cs(metodo InsertConfig) " + ex.Message);
                return false;
            }
        }

        public ConfiguracionGeneral_BO ObtenerConfig()
        {
            var config = new ConfiguracionGeneral_BO();
            try
            {
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW1501_SELECT_CONFIGURACION_GENERAL");
                while (data.Read())
                {
                    config = new ConfiguracionGeneral_BO
                    {
                        Ping_no_exitoso = Convert.ToDecimal(data["PORCENTAGE_PERDIDA_PING_NO_EXITOSO"]),
                        Generar_alarma = Convert.ToInt32(data["SEGUNDOS_GENERA_ALARMA"]),
                        Tiempo_nueva_alerta = Convert.ToInt32(data["TIEMPO_NUEVA_ALERTA"]),
                        Servidor_smtp = Convert.ToString(data["SERVIDOR_SMTP"]),
                        Frecuencia_no_ping = Convert.ToInt32(data["FRECUENCIA_ALTERNATIVA_NO_PING"]),
                        Email = Convert.ToString(data["EMAIL"]),
                        Tiempo_proceso_reporte = Convert.ToInt32(data["TIME_PROCESO_REPORTE"]),
                        Time_depuracion = Convert.ToInt32(data["TIME_DEPURACION"])
                    };
                }
                conexion.Close();
                conexion.Dispose();
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ConfiguracionGeneral_DAO.cs(metodo ObtenerConfig) " + ex.Message);
            }
            return config;
        }

        public ConfiguracionGeneral_BO ObtenerConfigParaMails()
        {
            var config = new ConfiguracionGeneral_BO();
            try
            {
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW1501_SELECT_CONFIGURACION_GENERAL_PARA_CORREO");
                while (data.Read())
                {
                    config = new ConfiguracionGeneral_BO
                    {
                        Servidor_smtp = Convert.ToString(data["SERVIDOR_SMTP"]),
                        Clave = Convert.ToString(data["PASS_EMAIL"]),
                        Email = Convert.ToString(data["EMAIL"]),
                    };
                }
                conexion.Close();
                conexion.Dispose();
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ConfiguracionGeneral_DAO.cs(metodo ObtenerConfigParaMails) " + ex.Message);
            }
            return config;
        }
        /////////////////////////////////////////////////////////////////////////////////
        //public bool UpdateActivaAlertaConfigGeneral()
        //{
        //    bool respuesta;
        //    try
        //    {
        //        SqlHelper.ExecuteNonQuery(_conexion, CommandType.StoredProcedure, "SP_SW15001_UPDATE_ACTIVA_ALERTA_CONFIG_GENERAL");
        //        respuesta = true;
        //    }
        //    catch (Exception e)
        //    {
        //        respuesta = false;
        //    }
        //    return respuesta;
        //}
        //public bool UpdateDesactivaAlertaConfigGeneral()
        //{
        //    bool respuesta;
        //    try
        //    {
        //        SqlHelper.ExecuteNonQuery(_conexion, CommandType.StoredProcedure, "SP_SW15001_UPDATE_DESACTIVA_ALERTA_CONFIG_GENERAL");
        //        respuesta = true;
        //    }
        //    catch (Exception e)
        //    {
        //        respuesta = false;
        //    }
        //    return respuesta;
        //}
        public ConfiguracionGeneral_BO GetConfigGeneral()
        {
            try
            {
                var configGeneral = new ConfiguracionGeneral_BO();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_CONFIG_GENERAL").Tables[0];
                configGeneral = new ConfiguracionGeneral_BO();
                configGeneral.Ping_no_exitoso = Convert.ToDecimal(dt.Rows[0]["PORCENTAGE_PERDIDA_PING_NO_EXITOSO"].ToString());
                configGeneral.Generar_alarma = Convert.ToDouble(dt.Rows[0]["SEGUNDOS_GENERA_ALARMA"].ToString());
                configGeneral.Tiempo_nueva_alerta = Convert.ToDouble(dt.Rows[0]["TIEMPO_NUEVA_ALERTA"].ToString());
                configGeneral.Frecuencia_no_ping = Convert.ToDouble(dt.Rows[0]["FRECUENCIA_ALTERNATIVA_NO_PING"].ToString());
                configGeneral.Email = dt.Rows[0]["SERVIDOR_SMTP"].ToString();
                configGeneral.Clave = dt.Rows[0]["EMAIL"].ToString();
                configGeneral.Tiempo_proceso_reporte = Convert.ToInt32(dt.Rows[0]["TIME_PROCESO_REPORTE"].ToString());
                configGeneral.Servidor_smtp = dt.Rows[0]["SERVIDOR_SMTP"].ToString();
                configGeneral.Time_depuracion = Convert.ToInt32(dt.Rows[0]["TIME_DEPURACION"].ToString());
                //configGeneral.Alerta_activada = Convert.ToBoolean(dt.Rows[0]["ALERTA_ACTIVADA"].ToString());
                conexion.Close();
                conexion.Dispose();
                return configGeneral;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ConfiguracionGeneral_DAO.cs(metodo GetConfigGeneral) " + ex.Message);
                return null;
            }
        }
    }
}
