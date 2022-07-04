using Microsoft.ApplicationBlocks.Data;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Ping.DAO
{
    public class TiposAcciones_DAO
    {
        string _conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();
        public bool InsertTipoAcciones(TiposAcciones_BO accion)
        {
            try
            {
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@RUT_CONTACTO", accion.Rut);
                parametros[1] = new SqlParameter("@ID_TIPO_ACCION", accion.Id_tipo_accion);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_INSERT_TIPO_ACCIONES_CONTACTOS", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "TiposAcciones_DAO.cs(metodo InsertTipoAcciones) " + ex.Message);
                return false;
            }
        }
        public List<TiposAcciones_BO> ObtenerTiposAcciones(int rut)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@RUT_CONTACTO", rut);
                var list = new List<TiposAcciones_BO>();
                TiposAcciones_BO tipoacciones;
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW1501_SELECT_TIPO_ACCIONES_CONTACTO", parametros);
                while (data.Read())
                {
                    tipoacciones = new TiposAcciones_BO
                    {
                        Id_tipo_accion = Convert.ToInt32(data["ID_TIPO_ACCION"]),
                        Rut = Convert.ToInt32(data["RUT_CONTACTO"])
                    };
                    list.Add(tipoacciones);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "TiposAcciones_DAO.cs(metodo ObtenerTiposAcciones) " + ex.Message);
                return null;
            }
        }
        public bool DeleteTipoAcciones(TiposAcciones_BO accion)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@RUT_CONTACTO", accion.Rut);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_DELETE_TIPO_ACCIONES_CONTACTOS", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "TiposAcciones_DAO.cs(metodo DeleteTipoAcciones) " + ex.Message);
                return false;
            }
        }
        public List<TiposAcciones_BO> ObtenerTiposAccionesActivas()
        {
            try
            {
                var list = new List<TiposAcciones_BO>();
                var tipoAcBO = new TiposAcciones_BO();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                DataTable dt = SqlHelper.ExecuteDataset(conexion, CommandType.StoredProcedure, "SP_SW15001_SELECT_TIPOS_ACCIONES_ACTIVOS").Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    tipoAcBO = new TiposAcciones_BO();
                    tipoAcBO.Id_tipo_accion = Convert.ToInt32(dr["ID_TIPO_ACCION"].ToString());
                    tipoAcBO.Descripcion = dr["DESC_TIPO_ACCION"].ToString();
                    list.Add(tipoAcBO);
                }
                conexion.Close();
                conexion.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "TiposAcciones_DAO.cs(metodo ObtenerTiposAccionesActivas) " + ex.Message);
                return null;
            }
        }
    }
}
