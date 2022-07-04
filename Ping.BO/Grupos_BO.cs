

namespace Ping.BO
{
    public class Grupos_BO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public ConfiguracionMonitoreo_BO ConfiguracionDeGrupo { get; set; }
        public bool Estado { get; set; }
        //public Estado Estado1 { get; set; }
        public override string ToString()
        {
            return Descripcion.ToString();
        }
    }
}
