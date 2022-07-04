

namespace Ping.BO
{
    public class Equipos_BO
    {
        public string Id { get; set; }
        public int ConfiguracionDeGrupo { get; set; }
        public int IdGrupo { get; set; }
        public string NombreEquipo { get; set; }
        public string UbicacionEquipo { get; set; }
        public string DescripcionEquipo { get; set; }
        public bool Estado { get; set; }
        public bool AlertaEstado { get; set; }
        public override string ToString()
        {
            return NombreEquipo;
        }
        //public Estado Estado_equipo { get; set; }
        //public string Ip { get; set; }
        public ConfiguracionMonitoreo_BO Config { get; set; }
        public Grupos_BO Grupo { get; set; }
        //public string Nombre { get; set; }
        //public string Ubicacion_equipo { get; set; }
        //public string Descripcion_equipo { get; set; }
    }
}
