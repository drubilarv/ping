using Ping.BO;
using Ping.DAO;
using System.Collections.Generic;


namespace Ping.Accion
{
   public class Grupos__action
    {
        private Grupos__DAO _gruposDao;

        public List<Grupos_BO> ObtenerGrupos()
        {
            _gruposDao = new Grupos__DAO();
            return _gruposDao.ObtenerGrupos();
        }
        public bool UpdateActivaGrupoPorId(int id)
        {
            _gruposDao = new Grupos__DAO();
            return _gruposDao.UpdateActivaGrupoPorId(id);
        }
        public bool UpdateDesactivaGrupoPorId(int id)
        {
            _gruposDao = new Grupos__DAO();
            return _gruposDao.UpdateDesactivaGrupoPorId(id);
        }
        public bool GetGrupoActivoPorId(int id)
        {
            _gruposDao = new Grupos__DAO();
            return _gruposDao.GetGrupoActivoPorId(id);
        }
        public List<Grupos_BO> ObtenerGruposActivos()
        {
            _gruposDao = new Grupos__DAO();
            return _gruposDao.ObtenerGruposActivos();
        }
        public List<Grupos_BO> ObtenerGruposMant()
        {
            var gdao = new Grupos__DAO();
            return gdao.ObtenerGruposMant();
        }
        public Grupos_BO ObtenerGrupo(int id)
        {
            var gdao = new Grupos__DAO();
            return gdao.ObtenerGrupo(id);
        }
        public bool InsertGrupo(ConfiguracionMonitoreo_BO config_id, string desc_grupo, bool estado)
        {
            var gdao = new Grupos__DAO();
            var grupo = new Grupos_BO
            {
                ConfiguracionDeGrupo = config_id,
                Descripcion = desc_grupo,
                Estado = estado
            };
            return gdao.InsertGrupo(grupo);
        }
        public bool UpdateGrupo(int id, ConfiguracionMonitoreo_BO config_id, string desc_grupo, bool estado)
        {
            var gdao = new Grupos__DAO();
            var grupo = new Grupos_BO
            {
                Id = id,
                ConfiguracionDeGrupo = config_id,
                Descripcion = desc_grupo,
                Estado = estado
            };
            return gdao.UpdateGrupo(grupo);
        }

        public bool DeleteGrupo(int id)
        {
            var gdao = new Grupos__DAO();
            var grupo = new Grupos_BO
            {
                Id = id
            };
            return gdao.DELETE_GRUPO(grupo);
        }
        public bool UpdateDesactivaTodosGrupos()
        {
            _gruposDao = new Grupos__DAO();
            return _gruposDao.UpdateDesactivaTodosGrupos();
        }
    }
}
