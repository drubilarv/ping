using Microsoft.ApplicationBlocks.Data;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Ping.DAO
{
    public class Grupos__DAO
    {
        string _conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();
        public List<Grupos_BO> ObtenerGrupos()
        {
            try
            {
                var lista = new List<Grupos_BO>();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_TODOS_GRUPOS");
                while (data.Read())
                {
                    var gru = new Grupos_BO
                    {
                        Id = int.Parse(data["ID_GRUPO"].ToString()),
                        ConfiguracionDeGrupo = new ConfiguracionMonitoreo_BO { IdConfig = Convert.ToInt32(data["ID_CONFIGURACION"].ToString()) },
                        Descripcion = data["DESC_GRUPO"].ToString(),
                        Estado = (bool)data["ESTADO_GRUPO"],
                    };
                    lista.Add(gru);
                }
                conexion.Close();
                conexion.Dispose();
                return lista;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Grupos_ DAO.cs(metodo ObtenerGrupos) " + ex.Message);
                return null;
            }
        }
        public bool UpdateActivaGrupoPorId(int id)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@ID", id);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SP_SW15001_UPDATE_ACTIVA_GRUPO_POR_ID", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Grupos_ DAO.cs(metodo UpdateActivaGrupoPorId) " + ex.Message);
                return false;
            }
        }
        public bool UpdateDesactivaGrupoPorId(int id)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@ID", id);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SP_SW15001_UPDATE_DESACTIVA_GRUPO_POR_ID", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Grupos_ DAO.cs(metodo UpdateDesactivaGrupoPorId) " + ex.Message);
                return false;
            }
        }
        public bool GetGrupoActivoPorId(int id)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@ID_GRUPO", id);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_GRUPOS_ACTIVOS_POR_ID", parametros).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    conexion.Close();
                    conexion.Dispose();
                    return true;
                }
                conexion.Close();
                conexion.Dispose();
                return false;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Grupos_ DAO.cs(metodo GetGrupoActivoPorId) " + ex.Message);
                return false;
            }
        }
        public Grupos_BO ObtenerGrupo(int id)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@ID", id);
                var grupo = new Grupos_BO();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW1501_SELECT_GRUPO", parametros);
                while (data.Read())
                {
                    grupo.Id = Convert.ToInt16(data["ID_GRUPO"]);
                    grupo.Descripcion = Convert.ToString(data["DESC_GRUPO"]);
                    grupo.Estado = Convert.ToBoolean(data["ESTADO_GRUPO"]);
                }
                conexion.Close();
                conexion.Dispose();
                return grupo;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Grupos_ DAO.cs(metodo ObtenerGrupo) " + ex.Message);
                return null;
            }
        }
        public List<Grupos_BO> ObtenerGruposMant()
        {
            try
            {
                var list = new List<Grupos_BO>();
                var configdao = new ConfiguracionMonitoreo__DAO();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_TODOS_GRUPOS");
                while (data.Read())
                {
                    var grupo = new Grupos_BO
                    {
                        Id = Convert.ToInt16(data["ID_GRUPO"]),
                        ConfiguracionDeGrupo = configdao.ObtenerConfig(Convert.ToInt16(data["ID_CONFIGURACION"])),
                        Descripcion = Convert.ToString(data["DESC_GRUPO"]),
                        Estado = Convert.ToBoolean(data["ESTADO_GRUPO"])
                    };
                    list.Add(grupo);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Grupos_ DAO.cs(metodo ObtenerGruposMant) " + ex.Message);
                return null;
            }
        }
        public bool InsertGrupo(Grupos_BO grupo)
        {
            try
            {
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@ID_CONFIGURACION", grupo.ConfiguracionDeGrupo.IdConfig);
                parametros[1] = new SqlParameter("@DESC_GRUPO", grupo.Descripcion);
                parametros[2] = new SqlParameter("@ESTADO_GRUPO ", grupo.Estado);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_INSERT_GRUPO", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Grupos_ DAO.cs(metodo InsertGrupo) " + ex.Message);
                return false;
            }
        }
        public bool UpdateGrupo(Grupos_BO grupo)
        {
            try
            {
                var parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("@ID_GRUPO", grupo.Id);
                parametros[1] = new SqlParameter("@ID_CONFIGURACION", grupo.ConfiguracionDeGrupo.IdConfig);
                parametros[2] = new SqlParameter("@DESC_GRUPO", grupo.Descripcion);
                parametros[3] = new SqlParameter("@ESTADO_GRUPO", grupo.Estado);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_UPDATE_GRUPO", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Grupos_ DAO.cs(metodo UpdateGrupo) " + ex.Message);
                return false;
            }
        }
        public bool DELETE_GRUPO(Grupos_BO grupo)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@ID_GRUPO", grupo.Id);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_DELETE_GRUPO", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Grupos_ DAO.cs(metodo DELETE_GRUPO) " + ex.Message);
                return false;
            }
        }
        public List<Grupos_BO> ObtenerGruposActivos()
        {
            try
            {
                var lista = new List<Grupos_BO>();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_GRUPOS_ACTIVOS");
                while (data.Read())
                {
                    var gru = new Grupos_BO
                    {
                        Id = int.Parse(data["ID_GRUPO"].ToString()),
                        ConfiguracionDeGrupo = new ConfiguracionMonitoreo_BO { IdConfig = Convert.ToInt32(data["ID_CONFIGURACION"].ToString()) },
                        Descripcion = data["DESC_GRUPO"].ToString(),
                        Estado = (bool)data["ESTADO_GRUPO"],
                    };
                    lista.Add(gru);
                }
                conexion.Close();
                conexion.Dispose();
                return lista;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Grupos_ DAO.cs(metodo ObtenerGruposActivos) " + ex.Message);
                return null;
            }
        }
        public bool UpdateDesactivaTodosGrupos()
        {
            try
            {
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SP_SW15001_UPDATE_DESACTIVA_TODOS_GRUPOS");
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Grupos_ DAO.cs(metodo UpdateDesactivaTodosGrupos) " + ex.Message);
                return false;
            }
        }
    }
}
