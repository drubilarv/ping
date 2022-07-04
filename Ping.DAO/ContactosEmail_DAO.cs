using Microsoft.ApplicationBlocks.Data;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Ping.DAO
{
    public class ContactosEmail_DAO
    {
        string _conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();
        public List<ContactosEmail_BO> ObtenerEmails()
        {
            try
            {
                var emails = new List<ContactosEmail_BO>();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SW1501_SELECT_CONTACTOS_EMAIL");
                var email = new ContactosEmail_BO();
                while (data.Read())
                {
                    email = new ContactosEmail_BO
                    {
                        Rut = Convert.ToInt32(data["RUT_CONTACTO"]),
                        Dv = Convert.ToChar(data["DV_RUT_CONTACTO"]),
                        Nombre = Convert.ToString(data["NOMBRE"]),
                        Email = Convert.ToString(data["MAIL"]),
                        Fono = Convert.ToInt32(data["FONO"])
                    };
                    emails.Add(email);
                }
                conexion.Close();
                conexion.Dispose();
                return emails;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ContactosEmail_DAO.cs(metodo ObtenerEmails) " + ex.Message);
                return null;
            }
        }
        public bool InsertEmail(ContactosEmail_BO email)
        {
            try
            {
                var parametros = new SqlParameter[5];
                parametros[0] = new SqlParameter("@RUT_CONTACTO", email.Rut);
                parametros[1] = new SqlParameter("@DV_RUT_CONTACTO", email.Dv);
                parametros[2] = new SqlParameter("@NOMBRE", email.Nombre);
                parametros[3] = new SqlParameter("@MAIL", email.Email);
                parametros[4] = new SqlParameter("@FONO", email.Fono);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_INSERT_EMAIL", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ContactosEmail_DAO.cs(metodo InsertEmail) " + ex.Message);
                return false;
            }
        }
        public bool UpdateEmail(ContactosEmail_BO email)
        {
            try
            {
                var parametros = new SqlParameter[5];
                parametros[0] = new SqlParameter("@RUT_CONTACTO", email.Rut);
                parametros[1] = new SqlParameter("@DV_RUT_CONTACTO", email.Dv);
                parametros[2] = new SqlParameter("@NOMBRE", email.Nombre);
                parametros[3] = new SqlParameter("@MAIL", email.Email);
                parametros[4] = new SqlParameter("@FONO", email.Fono);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_UPDATE_CONTACTOS_EMAIL", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ContactosEmail_DAO.cs(metodo UpdateEmail) " + ex.Message);
                return false;
            }
        }
        public bool DeleteEmail(ContactosEmail_BO email)
        {
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@RUT_CONTACTO", email.Rut);
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlHelper.ExecuteNonQuery(conexion, CommandType.StoredProcedure, "SW1501_DELETE_CONTACTOS_EMAIL", parametros);
                conexion.Close();
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ContactosEmail_DAO.cs(metodo DeleteEmail) " + ex.Message);
                return false;
            }
        }
        public List<ContactosEmail_BO> ObtenerEmailsNotificaciones()
        {
            try
            {
                var emails = new List<ContactosEmail_BO>();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "select_recepcion_notificacion_contactos");
                var email = new ContactosEmail_BO();
                while (data.Read())
                {
                    email = new ContactosEmail_BO
                    {
                        Email = Convert.ToString(data["MAIL"])
                    };
                    emails.Add(email);
                }
                conexion.Close();
                conexion.Dispose();
                return emails;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ContactosEmail_DAO.cs(metodo ObtenerEmailsNotificaciones) " + ex.Message);
                return null;
            }
        }
        public List<ContactosEmail_BO> ObtenerEmailsReport()
        {
            try
            {
                var emails = new List<ContactosEmail_BO>();
                var conexion = new SqlConnection(_conexion);
                conexion.Open();
                SqlDataReader data = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "select_recepcion_report_contactos");
                var email = new ContactosEmail_BO();
                while (data.Read())
                {
                    email = new ContactosEmail_BO
                    {
                        Email = Convert.ToString(data["MAIL"])
                    };
                    emails.Add(email);
                }
                conexion.Close();
                conexion.Dispose();
                return emails;
            }
            catch (Exception ex)
            {
                var logErroresModificacionesDao = new LogErroresModificaciones__DAO();
                logErroresModificacionesDao.InsertErroresLogDAO(1, DateTime.Now, Environment.UserName, "ContactosEmail_DAO.cs(metodo ObtenerEmailsReport) " + ex.Message);
                return null;
            }
        }

    }
}
