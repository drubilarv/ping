using System.Collections.Generic;
using Ping.DAO;
using Ping.BO;

namespace Ping.Accion
{
   public class Equipos__action
    {
        private Equipos_DAO _equiposDao;

        public List<Equipos_BO> GetEquiposPorGrupos(int idGrupo)
        {
            _equiposDao = new Equipos_DAO();
            return _equiposDao.GetEquiposPorGrupos(idGrupo);
        }
        public List<Equipos_BO> GetEquiposActivosPorGrupo(int idGrupo)
        {
            _equiposDao = new Equipos_DAO();
            return _equiposDao.GetEquiposActivosPorGrupo(idGrupo);
        }
        public List<Equipos_BO> GetEquiposInactivosPorGrupo(int idGrupo)
        {
            _equiposDao = new Equipos_DAO();
            return _equiposDao.GetEquiposInactivosPorGrupo(idGrupo);
        }
        public bool UpdateActivaEquipoPorIp(string ip)
        {
            _equiposDao = new Equipos_DAO();
            return _equiposDao.UpdateActivaEquipoPorIp(ip);
        }
        public bool UpdateDesactivaEquipoPorIp(string ip)
        {
            _equiposDao = new Equipos_DAO();
            return _equiposDao.UpdateDesactivaEquipoPorIp(ip);
        }
        public static Equipos_BO GetEquipoPorIp(string ip)
        {
            return Equipos_DAO.GetEquipoPorIp(ip);
        }

        public bool UpdateDesactivaAlertaEquipoPorIp(string ip)
        {
            _equiposDao = new Equipos_DAO();
            return _equiposDao.UpdateDesactivaAlertaEquipoPorIp(ip);
        }
        public bool UpdateActivaAlertaEquipoPorIp(string ip)
        {
            _equiposDao = new Equipos_DAO();
            return _equiposDao.UpdateActivaAlertaEquipoPorIp(ip);
        }

        public List<Equipos_BO> ObtenerEquiposActivos()
        {
            _equiposDao = new Equipos_DAO();
            return _equiposDao.GetEquiposActivos();

        }
        public List<Equipos_BO> ObtenerEquipos()
        {
            var edao = new Equipos_DAO();
            return edao.ObtenerEquipos();

        }

        public bool InsertEquipo(string ip, ConfiguracionMonitoreo_BO config, Grupos_BO grupo, string nombre, string ubicacion, string descripcion, bool estado)
        {
            var edao = new Equipos_DAO();
            var equipo = new Equipos_BO
            {
                Id = ip,
                Config = config,
                Grupo = grupo,
                NombreEquipo = nombre,
                UbicacionEquipo = ubicacion,
                DescripcionEquipo = descripcion,
                Estado = estado//Estado_equipo = new Ping.BO.Estado(estado)

            };
            return edao.InsertEquipo(equipo);
        }
        public bool UpdateEquipo(string ip, ConfiguracionMonitoreo_BO config, Grupos_BO grupo, string nombre, string ubicacion, string descripcion, bool estado)
        {
            var edao = new Equipos_DAO();
            var equipo = new Equipos_BO
            {
                Id = ip,
                Config = config,
                Grupo = grupo,
                NombreEquipo = nombre,
                UbicacionEquipo = ubicacion,
                DescripcionEquipo = descripcion,
                Estado = estado

            };
            return edao.UpdateEquipo(equipo);
        }

        public bool DeleteEquipo(string ip)
        {
            var edao = new Equipos_DAO();
            var equipo = new Equipos_BO
            {
                Id = ip,
            };
            return edao.DeleteEquipo(equipo);
        }
        public bool UpdateDesactivaTodosEquipos()
        {
            _equiposDao = new Equipos_DAO();
            return _equiposDao.UpdateDesactivaTodosEquipos();
        }


    }
}