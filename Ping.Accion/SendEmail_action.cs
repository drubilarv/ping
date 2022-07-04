using Ping.DAO;
using System;

namespace Ping.Accion
{
    public class SendEmail_action
    {
        public static bool? Send(DateTime inicio, DateTime fin, string fileName)
        {
            return SendEmails_DAO.Send(inicio, fin, fileName);
        }
        public static bool? Send(string mensaje)
        {
            return SendEmails_DAO.Send(mensaje);
        }
    }
}
