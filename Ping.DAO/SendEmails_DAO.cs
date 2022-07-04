using Ping.BO;
using System;
using System.Net;
using System.Net.Mail;


namespace Ping.DAO
{
    public class SendEmails_DAO
    {
        private static ContactosEmail_DAO contactos_dao = new ContactosEmail_DAO();
        public static bool? Send(string mensaje)
        {
            try
            {
                var config_dao = new ConfiguracionGeneral_DAO();
                var config_bo = config_dao.ObtenerConfigParaMails();
                var contactos = contactos_dao.ObtenerEmailsNotificaciones();
                if (contactos.Count <= 0)
                    return null;

                var email = new MailMessage();
                foreach (ContactosEmail_BO contacto in contactos)
                    email.To.Add(new MailAddress(contacto.Email));
                email.From = new MailAddress("example2@gmail.com");
                email.Subject = "Alerta";
                email.Body = mensaje;
                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;

                var smtp = new SmtpClient();
                smtp.Host = config_bo.Servidor_smtp; //"smtp.gmail.com";//smtp.live.com port=25(hotmail)
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(config_bo.Email, config_bo.Clave);

                smtp.Send(email);
                email.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "SendEmails_DAO.cs(metodo Send(@msje)) " + ex.Message);
                return false;
            }
        }
        public static bool? Send(DateTime inicio, DateTime fin, string fileName)
        {
            try
            {
                var config_dao = new ConfiguracionGeneral_DAO();
                var config_bo = config_dao.ObtenerConfigParaMails();
                var contactos = contactos_dao.ObtenerEmailsReport();
                if (contactos.Count <= 0)
                    return null;

                var email = new MailMessage();
                foreach (ContactosEmail_BO contacto in contactos)
                    email.To.Add(new MailAddress(contacto.Email));
                email.From = new MailAddress("example2@gmail.com");
                email.Subject = "reporte automatico Generado entre " + inicio + " y " + fin;
                email.Body = "reporte automatico " + DateTime.Now + " Generado entre " + inicio + " y " + fin;
                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;
                Attachment oAttach = new Attachment(fileName);
                email.Attachments.Add(oAttach);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = config_bo.Servidor_smtp; //"smtp.gmail.com";//smtp.live.com port=25(hotmail)
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(config_bo.Email.Trim(), config_bo.Clave);

                smtp.Send(email);
                email.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "SendEmails_DAO.cs(metodo Send(@inicio,@fin,@filename)) " + ex.Message);
                return false;
            }
        }
        //public static bool Send(string destinatario, NetworkCredential userPass, string mensaje, string server)
        //{
        //    try
        //    {
        //        MailMessage email = new MailMessage();
        //        email.To.Add(new MailAddress(destinatario));
        //        email.From = new MailAddress("example2@gmail.com");
        //        email.Subject = "Alerta";
        //        email.Body = mensaje;
        //        email.IsBodyHtml = true;
        //        email.Priority = MailPriority.Normal;

        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = server; //"smtp.gmail.com";//smtp.live.com port=25(hotmail)
        //        smtp.Port = 587;
        //        smtp.EnableSsl = true;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = userPass; //new NetworkCredential("pancho.alternative@gmail.com", "memento mori");

        //        smtp.Send(email);
        //        email.Dispose();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
        //        logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, ex.Message);
        //        return false;
        //    }
        //}

        //public static bool Send(string destinatario, string mensaje)
        //{
        //    try
        //    {
        //        ConfiguracionGeneral_DAO config_dao = new ConfiguracionGeneral_DAO();
        //        var config_bo = config_dao.ObtenerConfigParaMails();
        //        MailMessage email = new MailMessage();
        //        email.To.Add(new MailAddress(destinatario));
        //        email.From = new MailAddress("example2@gmail.com");
        //        email.Subject = "Alerta";
        //        email.Body = mensaje;
        //        email.IsBodyHtml = true;
        //        email.Priority = MailPriority.Normal;

        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = config_bo.Servidor_smtp; //"smtp.gmail.com";//smtp.live.com port=25(hotmail)
        //        smtp.Port = 587;
        //        smtp.EnableSsl = true;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = new NetworkCredential(config_bo.Email, config_bo.Clave);

        //        smtp.Send(email);
        //        email.Dispose();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
        //        logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, ex.Message);
        //        return false;
        //    }
        //}


        //public static bool Send(string destinatario, string mensaje, string fileName)
        //{
        //    try
        //    {
        //        ConfiguracionGeneral_DAO config_dao = new ConfiguracionGeneral_DAO();
        //        var config_bo = config_dao.ObtenerConfigParaMails();
        //        MailMessage email = new MailMessage();
        //        email.To.Add(new MailAddress(destinatario));
        //        email.From = new MailAddress("example2@gmail.com");
        //        email.Subject = "Alerta";
        //        email.Body = mensaje;
        //        email.IsBodyHtml = true;
        //        email.Priority = MailPriority.Normal;
        //        Attachment oAttach = new Attachment(fileName);
        //        email.Attachments.Add(oAttach);

        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = config_bo.Servidor_smtp; //"smtp.gmail.com";//smtp.live.com port=25(hotmail)
        //        smtp.Port = 587;
        //        smtp.EnableSsl = true;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = new NetworkCredential(config_bo.Email, config_bo.Clave);

        //        smtp.Send(email);
        //        email.Dispose();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
        //        logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, ex.Message);
        //        return false;
        //    }
        //}
        //public static bool Send(string destinatario, NetworkCredential userPass, string mensaje, string server, string fileName)
        //{
        //    try
        //    {
        //        MailMessage email = new MailMessage();
        //        email.To.Add(new MailAddress(destinatario));
        //        email.From = new MailAddress("example2@gmail.com");
        //        email.Subject = "Alerta";
        //        email.Body = mensaje;
        //        email.IsBodyHtml = true;
        //        email.Priority = MailPriority.Normal;
        //        Attachment oAttach = new Attachment(fileName);
        //        email.Attachments.Add(oAttach);

        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = server; //"smtp.gmail.com";//smtp.live.com port=25(hotmail)
        //        smtp.Port = 587;
        //        smtp.EnableSsl = true;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = userPass;

        //        smtp.Send(email);
        //        email.Dispose();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
        //        logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, ex.Message);
        //        return false;
        //    }
        //}
    }
}
