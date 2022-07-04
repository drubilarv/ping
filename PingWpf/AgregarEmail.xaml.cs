using Ping.Accion;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para AgregarEmail.xaml
    /// </summary>
    public partial class AgregarEmail : Window
    {
        private DevExpress.Xpf.Grid.GridControl grilla;
        private ContactosEmail_BO email;
        public AgregarEmail(DevExpress.Xpf.Grid.GridControl grilla, ContactosEmail_BO email)
        {
            try
            {
                InitializeComponent();
                this.grilla = grilla;
                if (email != null)
                    cargarDatos(email);

            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarEmail.xaml.cs(metodo AgregarEmail) " + ex.Message);
            }
        }

        private void txtDv_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtDv.Text.Length == 1)
                    txtDv.Text = "";
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarEmail.xaml.cs(metodo txtDv_KeyDown) " + ex.Message);
            }
        }
        public bool isNum(string valor)
        {
            try
            {
                int num = Convert.ToInt32(valor);
                return true;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarEmail.xaml.cs(metodo isNum) " + ex.Message);
                return false;
            }
        }
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var email_action = new ContactosEmail_action();
                char dv = ' ';
                if (email == null)
                {
                    if (string.IsNullOrEmpty(txtRut.Text) || string.IsNullOrEmpty(txtDv.Text))
                    {
                        MessageBox.Show(this, "Rut y digito verificador obligatorios", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }


                    if (ValidarRut(txtRut.Text, Convert.ToChar(txtDv.Text)))
                    {
                        dv = Convert.ToChar(txtDv.Text);
                        if (txtName.Text.Length > 0 & !isNum(txtName.Text))
                        {
                            if (isNum(txtFono.Text))
                            {
                                if (txtEmail.Text.Contains(".") & txtEmail.Text.Contains("@"))
                                {
                                    if (email_action.InsertEmail(Convert.ToInt32(txtRut.Text), dv, txtName.Text,
                                        txtEmail.Text, Convert.ToInt32(txtFono.Text)))
                                    {
                                        var logeer = new LogErroresModificaciones__action();
                                        logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, "Contactos email " + txtRut.Text + " insertado");
                                        MessageBox.Show(this, "Registro ingresado exitosamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                                        grilla.ItemsSource = email_action.ObtenerEmails();
                                        Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "Registro no pudo ser insertado, verifique si existen dicho contacto o comuníquese con el administrador",
                                            "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                }
                                else
                                    MessageBox.Show(this, "Email invalido", "Información", MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                            }
                            else
                                MessageBox.Show(this, "Teléfono invalido", "Información", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                        }
                        else
                            MessageBox.Show(this, "Nombre invalido", "Informacion", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show(this, "Rut invalido", "Información", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                }
                else
                {
                    if (txtName.Text.Length > 0 & !isNum(txtName.Text))
                    {
                        if (isNum(txtFono.Text))
                        {
                            if (txtEmail.Text.Contains(".") & txtEmail.Text.Contains("@"))
                            {
                                if (email_action.UpdateEmail(Convert.ToInt32(txtRut.Text), dv, txtName.Text,
                                    txtEmail.Text, Convert.ToInt32(txtFono.Text)))
                                {
                                    string messageLog = "Contacto email Modificado  Rut: " + txtRut.Text + "-" + dv + " Nombre: " + txtName + " Email: " + txtEmail.Text + " Fono: " + txtFono.Text;
                                    var logeer = new LogErroresModificaciones__action();
                                    logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, messageLog);
                                    MessageBox.Show(this, "Registro actualizado exitosamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                                    grilla.ItemsSource = email_action.ObtenerEmails();
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show(this, "Registro no pudo ser actualizado", "Información",
                                        MessageBoxButton.OK, MessageBoxImage.Information);
                                }

                            }
                            else
                                MessageBox.Show(this, "Email invalido", "Información", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                        }
                        else
                            MessageBox.Show(this, "Teléfono invalido", "Información", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show(this, "Nombre invalido", "Información", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarEmail.xaml.cs(metodo Agregar_Click) " + ex.Message);
            }
        }
        public bool ValidarRut(string str_rut, char dv)
        {
            bool validacion = false;
            try
            {
                int rut = Int32.Parse(str_rut);

                int m = 0, s = 1;
                for (; rut != 0; rut /= 10)
                {
                    s = (s + rut % 10 * (9 - m++ % 6)) % 11;//validador.
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
                return validacion;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarEmail.xaml.cs(metodo ValidarRut) " + ex.Message);
                return false;
            }
        }
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void cargarDatos(ContactosEmail_BO email)
        {
            try
            {
                this.email = email;
                txtRut.Text = "" + email.Rut;
                txtRut.IsEnabled = false;
                txtDv.Text = "" + email.Dv;
                txtDv.IsEnabled = false;
                txtName.Text = email.Nombre;
                txtEmail.Text = email.Email;
                txtFono.Text = "" + email.Fono;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarEmail.xaml.cs(metodo cargarDatos) " + ex.Message);
            }
        }
    }
}
