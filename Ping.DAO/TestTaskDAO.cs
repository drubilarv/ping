using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;

namespace Ping.DAO
{
    public class TestTaskDAO
    {
        string _conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();
        public List<string> GetAllIpEquiposActivosPorGruposActivos()
        {
            try
            {
                string equipo;
                var list = new List<string>();
                //var conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Ping.View.Properties.Settings.ConexPing"].ToString());
                //conexion.Open();
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
