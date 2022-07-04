using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Ping.DAO
{
    public class IpActivas_DAO
    {
        static string _conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();

        //public static List<string> GetAllEquiposActivosPorGruposActivos()
        //{
        //    try
        //    {
        //        string equipo;
        //        var list = new List<string>();
        //        var dt = new DataTable();

        //        dt = SqlHelper.ExecuteDataset(_conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_TODOS_EQUIPOS_ACTIVOS_POR_GRUPOS_ACTIVOS").Tables[0];
        //        //dt = SqlHelper.ExecuteDataset(_conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_TODOS_EQUIPOS_ACTIVOS_Y_CON_VISUALIZACION_POR_GRUPOS_ACTIVOS").Tables[0];
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            equipo = dr["IP_EQUIPO"].ToString();
        //            list.Add(equipo);
        //        }
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
        //        logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "IpActivas_DAO.cs(metodo GetAllEquiposActivosPorGruposActivos) " + ex.Message);
        //        return null;
        //    }
        //}
        public static DataTable GetAllEquiposActivosPorGruposActivos()
        {
            try
            {
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_TODOS_EQUIPOS_ACTIVOS_POR_GRUPOS_ACTIVOS_NEW").Tables[0];
                //dt = SqlHelper.ExecuteDataset(_conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_TODOS_EQUIPOS_ACTIVOS_Y_CON_VISUALIZACION_POR_GRUPOS_ACTIVOS").Tables[0];
                conexion.Close();
                conexion.Dispose();
                return dt;

                //string equipo;
                //var list = new List<string>();
                //var dt2 = new DataTable();
                //dt2 = SqlHelper.ExecuteDataset(_conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_TODOS_EQUIPOS_ACTIVOS_POR_GRUPOS_ACTIVOS").Tables[0];
                //foreach (DataRow dr in dt2.Rows)
                //{
                //    equipo = dr["IP_EQUIPO"].ToString();
                //    list.Add(equipo);
                //}
                //return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "IpActivas_DAO.cs(metodo GetAllEquiposActivosPorGruposActivos) " + ex.Message);
                return null;
            }
        }
        public static int GetNumeroEquiposActivosPorGruposActivos()
        {
            try
            {
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_NRO_DE_TODOS_EQUIPOS_ACTIVOS_POR_GRUPOS_ACTIVOS").Tables[0];

                if (dt.Rows[0]["nro"] != null)
                {
                    conexion.Close();
                    conexion.Dispose();
                    return Convert.ToInt32(dt.Rows[0]["nro"]);
                }
                conexion.Close();
                conexion.Dispose();
                return 0;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "IpActivas_DAO.cs(metodo GetNumeroEquiposActivosPorGruposActivos) " + ex.Message);
                return 0;
            }
        }
        public static List<string> GetAllIpEquiposActivosPorGruposActivos()
        {
            try
            {
                string equipo;
                var list = new List<string>();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_TODAS_IP_EQUIPOS_ACTIVOS_POR_GRUPOS_ACTIVOS").Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    equipo = dr["IP_EQUIPO"].ToString();
                    list.Add(equipo);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "IpActivas_DAO.cs(metodo GetAllIpEquiposActivosPorGruposActivos) " + ex.Message);
                return null;
            }
        }




    }
}
