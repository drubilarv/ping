using Ping.Accion;
using Ping.BO;
using System;
using System.Windows;


namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para MantenedorContactMail.xaml
    /// </summary>
    public partial class MantenedorContactMail : Window
    {
        public MantenedorContactMail()
        {
            try
            {
                InitializeComponent();
                var mail_action = new ContactosEmail_action();
                GridMail.ItemsSource = mail_action.ObtenerEmails();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorContactMail.xaml.cs(metodo MantenedorContactMail) " + ex.Message);
            }
        }

        private void agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var agregar_email = new AgregarEmail(GridMail, null);
                agregar_email.Owner = this;
                agregar_email.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorContactMail.xaml.cs(metodo agregar_Click) " + ex.Message);
            }
        }

        private void editar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var email = GridMail.SelectedItem as ContactosEmail_BO;
                var agregar_email = new AgregarEmail(GridMail, email);
                agregar_email.Owner = this;
                agregar_email.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorContactMail.xaml.cs(metodo editar_Click) " + ex.Message);
            }
        }

        private void eliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var email = GridMail.SelectedItem as ContactosEmail_BO;
                var mail_action = new ContactosEmail_action();
                var result = MessageBox.Show("¿Está seguro que desea eliminar este contacto?", "Información",
                    MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    var resultado = mail_action.DeleteEmail(email.Rut);
                    if (resultado)
                    {
                        var logeer = new LogErroresModificaciones__action();
                        logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, "Contacto " + email.Rut + " Eliminado");
                    }
                    MessageBox.Show(resultado ? "Registro eliminado correctamente" : "No se pudo eliminar el registro,verifique si existen acciones asociadas a dicho contacto comuniquese con el administrador", "Resultado", MessageBoxButton.OK);
                    GridMail.ItemsSource = mail_action.ObtenerEmails();
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorContactMail.xaml.cs(metodo eliminar_Click) " + ex.Message);
            }
        }

        private void asignar_Acciones(object sender, RoutedEventArgs e)
        {
            try
            {
                var email = GridMail.SelectedItem as ContactosEmail_BO;
                var asign = new AsignarAccciones(email.Rut);
                asign.Owner = this;
                asign.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorContactMail.xaml.cs(metodo asignar_Acciones) " + ex.Message);
            }
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
