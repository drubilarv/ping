using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ping.DAO;

namespace Ping.Accion
{
    public class IpActivas_action
    {

        private IpActivas_DAO _IpActivasDao;

        //public static List<string> GetAllEquiposActivosPorGruposActivos()
        //{
        //    return IpActivas_DAO.GetAllEquiposActivosPorGruposActivos();
        //}
        public static DataTable GetAllEquiposActivosPorGruposActivos()
        {
            return IpActivas_DAO.GetAllEquiposActivosPorGruposActivos();
        }
        //public int GetNumeroEquiposActivosPorGruposActivos()
        //{
        //    _IpActivasDao = new IpActivas_DAO();
        //    return _IpActivasDao.GetNumeroEquiposActivosPorGruposActivos();
        //}
        public static int GetNumeroEquiposActivosPorGruposActivos()
        {
            return IpActivas_DAO.GetNumeroEquiposActivosPorGruposActivos();
        }

        public static List<string> GetAllIpEquiposActivosPorGruposActivos()
        {
            return IpActivas_DAO.GetAllIpEquiposActivosPorGruposActivos();
        }
    }
}
