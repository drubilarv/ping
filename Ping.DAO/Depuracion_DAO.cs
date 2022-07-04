using Microsoft.ApplicationBlocks.Data;
using System;
using System.Configuration;
using System.Data;

namespace Ping.DAO
{
    public class Depuracion_DAO
    {
        string _conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();

        public bool Depuracion()
        {
            try
            {
                SqlHelper.ExecuteNonQuery(_conexion, CommandType.StoredProcedure, "SW1501_SELECT_DEPURACION");
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "Depuracion_DAO.cs(metodo Depuracion) " + ex.Message);
                return false;
            }
        }
    }
}
