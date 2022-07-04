using Microsoft.ApplicationBlocks.Data;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Ping.DAO
{
    public class Equipos_DAO
    {
        string _conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();
        public List<Equipos_BO> GetEquiposPorGrupos(int idGrupo)
        {
            try
            {
                var equipo = new Equipos_BO();
                var list = new List<Equipos_BO>();
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@ID_GRUPO", idGrupo);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_EQUIPOS_POR_GRUPO", parametros).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    equipo = new Equipos_BO();
                    equipo.Id = dr["IP_EQUIPO"].ToString();
                    equipo.ConfiguracionDeGrupo = int.Parse(dr["ID_CONFIGURACION"].ToString());
                    equipo.IdGrupo = int.Parse(dr["ID_GRUPO"].ToString());
                    equipo.NombreEquipo = dr["NOM_EQUIPO"].ToString();
                    equipo.UbicacionEquipo = dr["UBICACION_EQUIPO"].ToString();
                    equipo.DescripcionEquipo = dr["DESCRIPCION_EQUIPO"].ToString();
                    equipo.Estado = Convert.ToBoolean(dr["ESTADO_EQUIPO"].ToString());
                    list.Add(equipo);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo GetEquiposPorGrupos) " + ex.Message);
                return null;
            }
        }
        public List<Equipos_BO> GetEquiposActivosPorGrupo(int idGrupo)
        {
            try
            {
                var equipo = new Equipos_BO();
                var list = new List<Equipos_BO>();

                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@ID_GRUPO", idGrupo);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_EQUIPOS_ACTIVOS_POR_GRUPO", parametros).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    equipo = new Equipos_BO();
                    equipo.Id = dr["IP_EQUIPO"].ToString();
                    equipo.ConfiguracionDeGrupo = int.Parse(dr["ID_CONFIGURACION"].ToString());
                    equipo.IdGrupo = int.Parse(dr["ID_GRUPO"].ToString());
                    equipo.NombreEquipo = dr["NOM_EQUIPO"].ToString();
                    equipo.UbicacionEquipo = dr["UBICACION_EQUIPO"].ToString();
                    equipo.DescripcionEquipo = dr["DESCRIPCION_EQUIPO"].ToString();
                    equipo.Estado = Convert.ToBoolean(dr["ESTADO_EQUIPO"].ToString());
                    list.Add(equipo);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo GetEquiposActivosPorGrupo) " + ex.Message);
                return null;
            }
        }

        public List<Equipos_BO> GetEquiposInactivosPorGrupo(int idGrupo)
        {
            try
            {
                var equipo = new Equipos_BO();
                var list = new List<Equipos_BO>();
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@ID_GRUPO", idGrupo);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_EQUIPOS_INACTIVOS_POR_GRUPO", parametros).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    equipo = new Equipos_BO();
                    equipo.Id = dr["IP_EQUIPO"].ToString();
                    equipo.ConfiguracionDeGrupo = int.Parse(dr["ID_CONFIGURACION"].ToString());
                    equipo.IdGrupo = int.Parse(dr["ID_GRUPO"].ToString());
                    equipo.NombreEquipo = dr["NOM_EQUIPO"].ToString();
                    equipo.UbicacionEquipo = dr["UBICACION_EQUIPO"].ToString();
                    equipo.DescripcionEquipo = dr["DESCRIPCION_EQUIPO"].ToString();
                    equipo.Estado = Convert.ToBoolean(dr["ESTADO_EQUIPO"].ToString());
                    list.Add(equipo);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo GetEquiposInactivosPorGrupo) " + ex.Message);
                return null;
            }
        }

        public bool UpdateActivaEquipoPorIp(string ip)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@IP", ip);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SP_SW15001_UPDATE_ACTIVA_EQUIPO_POR_IP", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo UpdateActivaEquipoPorIp) " + ex.Message);
                return false;
            }
        }

        public bool UpdateDesactivaEquipoPorIp(string ip)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@IP", ip);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SP_SW15001_UPDATE_DESACTIVA_EQUIPO_POR_IP", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo UpdateDesactivaEquipoPorIp) " + ex.Message);
                return false;
            }
        }


        public static Equipos_BO GetEquipoPorIp(string ip)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@IP", ip);
                var conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexPing"].ToString());
                conexion.Open();
                DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_EQUIPOS_POR_IP", parametros).Tables[0];

                var equipo = new Equipos_BO();
                equipo.Id = dt.Rows[0]["IP_EQUIPO"].ToString();
                equipo.ConfiguracionDeGrupo = Convert.ToInt32(dt.Rows[0]["ID_CONFIGURACION"].ToString());
                equipo.IdGrupo = Convert.ToInt32(dt.Rows[0]["ID_GRUPO"].ToString());
                equipo.NombreEquipo = dt.Rows[0]["NOM_EQUIPO"].ToString();
                equipo.UbicacionEquipo = dt.Rows[0]["UBICACION_EQUIPO"].ToString();
                equipo.DescripcionEquipo = dt.Rows[0]["DESCRIPCION_EQUIPO"].ToString();
                equipo.Estado = Convert.ToBoolean(dt.Rows[0]["ESTADO_EQUIPO"].ToString());
                //equipo.AlertaEstado = Convert.ToBoolean(dt.Rows[0]["ALERTA_ESTADO"].ToString());
                conexion.Close();
                conexion.Dispose();
                return equipo;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo GetEquipoPorIp) " + ex.Message);
                return null;
            }
        }

        public string GetNombreEquipoPorIp(string ip)
        {
            try
            {
                Equipos_BO equipo;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@IP", ip);
                DataTable dt = SqlHelper.ExecuteDataset(_conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_EQUIPOS_POR_IP", parametros).Tables[0];
                equipo = new Equipos_BO();
                //equipo.Id = dt.Rows[0]["IP_EQUIPO"].ToString();
                //equipo.ConfiguracionDeGrupo = Convert.ToInt32(dt.Rows[0]["ID_CONFIGURACION"].ToString());
                //equipo.IdGrupo = Convert.ToInt32(dt.Rows[0]["ID_GRUPO"].ToString());
                return equipo.NombreEquipo = dt.Rows[0]["NOM_EQUIPO"].ToString();
                //equipo.UbicacionEquipo = dt.Rows[0]["UBICACION_EQUIPO"].ToString();
                //equipo.DescripcionEquipo = dt.Rows[0]["DESCRIPCION_EQUIPO"].ToString();
                //equipo.Estado = Convert.ToBoolean(dt.Rows[0]["ESTADO_EQUIPO"].ToString());
                //equipo.AlertaEstado = Convert.ToBoolean(dt.Rows[0]["ALERTA_ESTADO"].ToString());
                //return equipo;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo GetNombreEquipoPorIp) " + ex.Message);
                return " ";
            }
        }
        public bool UpdateDesactivaAlertaEquipoPorIp(string ip)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@IP", ip);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SP_SW15001_UPDATE_DESACTIVA_ALERTA_EQUIPO_POR_IP", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo UpdateDesactivaAlertaEquipoPorIp) " + ex.Message);
                return false;
            }
        }
        public bool UpdateActivaAlertaEquipoPorIp(string ip)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@IP", ip);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SP_SW15001_UPDATE_ACTIVA_ALERTA_EQUIPO_POR_IP", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo UpdateActivaAlertaEquipoPorIp) " + ex.Message);
                return false;
            }
        }
        public List<Equipos_BO> GetEquiposActivos()
        {
            try
            {
                var equipos = new List<Equipos_BO>();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW1501_SELECT_EQUIPOS_ACTIVOS");
                var configdao = new ConfiguracionMonitoreo__DAO();
                var gdao = new Grupos__DAO();
                var equipo = new Equipos_BO();
                while (data.Read())
                {
                    equipo = new Equipos_BO
                    {
                        Id = Convert.ToString(data["IP_EQUIPO"]),
                        Grupo = gdao.ObtenerGrupo(Convert.ToInt16(data["ID_GRUPO"])),
                        NombreEquipo = Convert.ToString(data["NOM_EQUIPO"]),
                        DescripcionEquipo = Convert.ToString(data["DESCRIPCION_EQUIPO"]),
                        Estado = Convert.ToBoolean(data["ESTADO_EQUIPO"]),
                        UbicacionEquipo = Convert.ToString(data["UBICACION_EQUIPO"]),
                        Config = configdao.ObtenerConfig(Convert.ToInt16(data["ID_CONFIGURACION"]))
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
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo GetEquiposActivos) " + ex.Message);
                return null;
            }
        }
        public List<Equipos_BO> ObtenerEquipos()
        {
            try
            {
                var equipos = new List<Equipos_BO>();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW1501_SELECT_EQUIPOS");
                var configdao = new ConfiguracionMonitoreo__DAO();
                var gdao = new Grupos__DAO();
                var equipo = new Equipos_BO();
                while (data.Read())
                {
                    equipo = new Equipos_BO
                    {
                        Id = Convert.ToString(data["IP_EQUIPO"]),
                        Grupo = gdao.ObtenerGrupo(Convert.ToInt16(data["ID_GRUPO"])),
                        IdGrupo = gdao.ObtenerGrupo(Convert.ToInt16(data["ID_GRUPO"])).Id,
                        NombreEquipo = Convert.ToString(data["NOM_EQUIPO"]),
                        DescripcionEquipo = Convert.ToString(data["DESCRIPCION_EQUIPO"]),
                        Estado = Convert.ToBoolean(data["ESTADO_EQUIPO"]),
                        UbicacionEquipo = Convert.ToString(data["UBICACION_EQUIPO"]),
                        Config = configdao.ObtenerConfig(Convert.ToInt16(data["ID_CONFIGURACION"]))
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
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo ObtenerEquipos) " + ex.Message);
                return null;
            }
        }
        public bool InsertEquipo(Equipos_BO equipo)
        {
            try
            {
                var parametros = new SqlParameter[7];
                parametros[0] = new SqlParameter("@IP_EQUIPO", equipo.Id);
                parametros[1] = new SqlParameter("@ID_CONFIGURACION", equipo.Config.IdConfig);
                parametros[2] = new SqlParameter("@ID_GRUPO", equipo.Grupo.Id);
                parametros[3] = new SqlParameter("@NOM_EQUIPO", equipo.NombreEquipo);
                parametros[4] = new SqlParameter("@UBICACION_EQUIPO", equipo.UbicacionEquipo);
                parametros[5] = new SqlParameter("@DESCRIPCION_EQUIPO", equipo.DescripcionEquipo);
                parametros[6] = new SqlParameter("@ESTADO_EQUIPO", equipo.Estado);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_INSERT_EQUIPO", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo InsertEquipo) " + ex.Message);
                return false;
            }
        }

        public bool UpdateEquipo(Equipos_BO equipo)
        {
            try
            {
                var parametros = new SqlParameter[7];
                parametros[0] = new SqlParameter("@IP_EQUIPO", equipo.Id);
                parametros[1] = new SqlParameter("@ID_CONFIGURACION", equipo.Config.IdConfig);
                parametros[2] = new SqlParameter("@ID_GRUPO", equipo.Grupo.Id);
                parametros[3] = new SqlParameter("@NOM_EQUIPO", equipo.NombreEquipo);
                parametros[4] = new SqlParameter("@UBICACION_EQUIPO", equipo.UbicacionEquipo);
                parametros[5] = new SqlParameter("@DESCRIPCION_EQUIPO", equipo.DescripcionEquipo);
                parametros[6] = new SqlParameter("@ESTADO_EQUIPO", equipo.Estado);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_UPDATE_EQUIPO", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo UpdateEquipo) " + ex.Message);
                return false;
            }
        }

        public bool DeleteEquipo(Equipos_BO equipo)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@IP_EQUIPO", equipo.Id);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_DELETE_EQUIPO", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo DeleteEquipo) " + ex.Message);
                return false;
            }
        }
        public bool UpdateDesactivaTodosEquipos()
        {
            try
            {
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SP_SW15001_UPDATE_DESACTIVA_TODOS_EQUIPOS");
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Equipos_DAO.cs(metodo UpdateDesactivaEquipoPorIp) " + ex.Message);
                return false;
            }
        }
    }
}
