using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ping.DAO;

namespace Ping.Accion
{
    public class TeastTaskAction
    {
        private TestTaskDAO test = null;
        public List<string> GetAllIpEquiposActivosPorGruposActivos()
        {
            test = new TestTaskDAO();
            return test.GetAllIpEquiposActivosPorGruposActivos();
        }
    }
}
