using Microsoft.ApplicationBlocks.Data;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Ping.DAO
{
    public class LogErroresModificaciones__DAO
    {
        string _conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();
        public bool InsertErroresLog(LogErroresModificaciones_BO logErroresModificaciones_BO)
        {
            try
            {
                var parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("@ID_TIPO_LOG", logErroresModificaciones_BO.Id_tipo_log);
                parametros[1] = new SqlParameter("@TIMESTAMP", logErroresModificaciones_BO.Timestamp);
                parametros[2] = new SqlParameter("@MENSAJE ", logErroresModificaciones_BO.Mensaje);
                parametros[3] = new SqlParameter("@USUARIO_MAQUINA ", logErroresModificaciones_BO.UsuarioMaquina);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_INSERT_LOG_ERRORES_MODIFICACIONES", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__DAO();
                var log = new LogErroresModificaciones_BO
                {
                    Id_tipo_log = 1,
                    Timestamp = System.DateTime.Now,
                    UsuarioMaquina = Environment.UserName,
                    Mensaje = "LogErroresModificaciones__DAO.cs(metodo InsertErroresLog) " + ex.Message
                };
                logeer.InsertErroresLog(log);
                return false;
            }
        }

        public List<LogErroresModificaciones_BO> ObtenerLogFecha(int id, DateTime inicio, DateTime fin)
        {
            try
            {
                var lista = new List<LogErroresModificaciones_BO>();
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@ID_TIPO_LOG ", id);
                parametros[1] = new SqlParameter("@FECHAINICIO", inicio);
                parametros[2] = new SqlParameter("@FECHAFIN", fin);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW1501_SELECT_LOG_FECHA", parametros);
                while (data.Read())
                {
                    var loges = new LogErroresModificaciones_BO
                    {
                        Id_tipo_log = Convert.ToInt32(data["ID_TIPO_LOG"]),
                        Mensaje = Convert.ToString(data["MENSAJE"]),
                        UsuarioMaquina = Convert.ToString(data["USUARIO_MAQUINA"]),
                        Timestamp = Convert.ToDateTime(data["TIMESTAMP"])
                    };
                    lista.Add(loges);
                }
                conexion.Close();
                conexion.Dispose();
                return lista;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "LogErroresModificaciones__DAO.cs(metodo ObtenerLogFecha) " + ex.Message);
                return null;
            }
        }
        public List<LogErroresModificaciones_BO> ObtenerLogSinFiltroFechaInicio(int id, DateTime fin)
        {
            try
            {
                var lista = new List<LogErroresModificaciones_BO>();
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@ID_TIPO_LOG ", id);
                parametros[1] = new SqlParameter("@FECHAFIN", fin);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_LOG_SIN_FILTRO_FECHA_INICIO", parametros);
                while (data.Read())
                {
                    var loges = new LogErroresModificaciones_BO
                    {
                        Id_tipo_log = Convert.ToInt32(data["ID_TIPO_LOG"]),
                        Mensaje = Convert.ToString(data["MENSAJE"]),
                        UsuarioMaquina = Convert.ToString(data["USUARIO_MAQUINA"]),
                        Timestamp = Convert.ToDateTime(data["TIMESTAMP"])
                    };
                    lista.Add(loges);
                }
                conexion.Close();
                conexion.Dispose();
                return lista;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "LogErroresModificaciones__DAO.cs(metodo ObtenerLogSinFiltroFechaInicio) " + ex.Message);
                return null;
            }
        }
        public List<LogErroresModificaciones_BO> ObtenerLogSinFiltroFechaFin(int id, DateTime inicio)
        {
            try
            {
                var lista = new List<LogErroresModificaciones_BO>();
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@ID_TIPO_LOG ", id);
                parametros[1] = new SqlParameter("@FECHAINICIO", inicio);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_LOG_SIN_FILTRO_FECHA_FIN", parametros);
                while (data.Read())
                {
                    var loges = new LogErroresModificaciones_BO
                    {
                        Id_tipo_log = Convert.ToInt32(data["ID_TIPO_LOG"]),
                        Mensaje = Convert.ToString(data["MENSAJE"]),
                        UsuarioMaquina = Convert.ToString(data["USUARIO_MAQUINA"]),
                        Timestamp = Convert.ToDateTime(data["TIMESTAMP"])
                    };
                    lista.Add(loges);
                }
                conexion.Close();
                conexion.Dispose();
                return lista;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "LogErroresModificaciones__DAO.cs(metodo ObtenerLogSinFiltroFechaFin) " + ex.Message);
                return null;
            }
        }
        public List<string> ObtenerLogId()
        {
            try
            {
                var lista = new List<string>();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW1501_SELECT_LOG_ID");
                while (data.Read())
                {
                    lista.Add(Convert.ToString(data["NOMBRE_TIPO_LOG"]));
                }
                conexion.Close();
                conexion.Dispose();
                return lista;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "LogErroresModificaciones__DAO.cs(metodo ObtenerLogId) " + ex.Message);
                return null;
            }
        }
        public bool InsertErroresLogDAO(int id_tipo_log, DateTime timestamp, string usuario, string mensaje)
        {
            try
            {
                var logem = new LogErroresModificaciones__DAO();
                var log = new LogErroresModificaciones_BO
                {
                    Id_tipo_log = id_tipo_log,
                    Timestamp = timestamp,
                    UsuarioMaquina = usuario,
                    Mensaje = mensaje

                };
                return logem.InsertErroresLog(log);
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "LogErroresModificaciones__DAO.cs(metodo InsertErroresLogDAO) " + ex.Message);
                return false;
            }
        }
    }
}
