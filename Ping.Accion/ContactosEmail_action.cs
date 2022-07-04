using Ping.BO;
using Ping.DAO;
using System.Collections.Generic;


namespace Ping.Accion
{
    public class ContactosEmail_action
    {
        public List<ContactosEmail_BO> ObtenerEmails()
        {
            var emdao = new ContactosEmail_DAO();
            return emdao.ObtenerEmails();
        }
        public bool InsertEmail(int rut, char dv, string nombre, string mail, int fono)
        {
            var emdao = new ContactosEmail_DAO();
            var email = new ContactosEmail_BO
            {
                Rut = rut,
                Dv = dv,
                Nombre = nombre,
                Email = mail,
                Fono = fono
            };
            return emdao.InsertEmail(email);
        }
        public bool UpdateEmail(int rut, char dv, string nombre, string mail, int fono)
        {
            var emdao = new ContactosEmail_DAO();
            var email = new ContactosEmail_BO
            {
                Rut = rut,
                Dv = dv,
                Nombre = nombre,
                Email = mail,
                Fono = fono
            };
            return emdao.UpdateEmail(email);
        }
        public bool DeleteEmail(int rut)
        {
            var emdao = new ContactosEmail_DAO();
            var email = new ContactosEmail_BO
            {
                Rut = rut,
            };
            return emdao.DeleteEmail(email);
        }
    }
}
