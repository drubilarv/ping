using Ping.BO;
using Ping.DAO;
using System.Collections.Generic;


namespace Ping.Accion
{
    public class TiposAcciones_action
    {
        public bool InsertTipoAcciones(List<int> ids, int rut)
        {
            var result = false;
            var tadao = new TiposAcciones_DAO();
            foreach (int id in ids)
            {
                var tiposAccion = new TiposAcciones_BO
                {
                    Id_tipo_accion = id,
                    Rut = rut
                };
                result = tadao.InsertTipoAcciones(tiposAccion);
            }
            return result;
        }
        public List<TiposAcciones_BO> ObtenerTiposAcciones(int rut)
        {
            var tactiondao = new TiposAcciones_DAO();
            return tactiondao.ObtenerTiposAcciones(rut);
        }
        public bool DeleteTipoAcciones(int rut)
        {
            var tactiondao = new TiposAcciones_DAO();
            var tbo = new TiposAcciones_BO
            {
                Rut = rut
            };
            return tactiondao.DeleteTipoAcciones(tbo);
        }
        public List<TiposAcciones_BO> ObtenerTiposAccionesActivas()
        {
            var tactiondao = new TiposAcciones_DAO();
            return tactiondao.ObtenerTiposAccionesActivas();
        }
    }
}
