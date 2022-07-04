using Ping.DAO;

namespace Ping.Accion
{
   public class Depuracion_action
    {
       public bool Depuracion()
       {
           var depudao = new Depuracion_DAO();
           return depudao.Depuracion();
       }
    }
}
