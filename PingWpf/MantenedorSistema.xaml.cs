using Ping.Accion;
using Ping.BO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para MantenedorSistema.xaml
    /// </summary>
    public partial class MantenedorSistema : Window
    {
        public bool IsShowDialog { get; set; }
        public MantenedorSistema()
        {
            try
            {
                InitializeComponent();
                txtPass.Password = "password";
                var generalConfig = new ConfiguracionGeneral_action();
                var config = generalConfig.ObtenerConfig();
                if (!(config.Servidor_smtp == null))
                    CargarDatos(config);
                else
                {
                    var logeer = new LogErroresModificaciones__action();
                    string mensaje = "No se han establecido los parametros de sistema o no se a podido establecer comunicación con la base de datos";
                    logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, "No se han establecido los parametros de sistema");
                    MessageBox.Show(mensaje, "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
                    IsShowDialog = false;
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorSistema.xaml.cs(metodo MantenedorSistema) " + ex.Message);
            }
        }
        private void actualizar_click(object sender, RoutedEventArgs e)
        {
            try
            {
                var generalConfig = new ConfiguracionGeneral_action();

                if (Convert.ToInt32(spinPing_no_Exitoso.Text) > 100)
                {
                    MessageBox.Show("Debe ingresar porcentaje ping no exitoso válido", "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (!(txtEmail.Text.Contains(".") & txtEmail.Text.Contains("@")))
                        MessageBox.Show("Email invalido", "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                    {
                        if (txtPass.Password.Length == 0)
                            MessageBox.Show("Debe ingresar clave email", "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            if (isNum(txtServidorSmtp.Text))
                                MessageBox.Show("Servidor smtp invalidos", "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Warning);
                            else
                            {
                                int segGeneraAlarma = Convert.ToInt32(txtSegundos_genera_alarma.Text.Contains(".") ? txtSegundos_genera_alarma.Text.Replace(".", string.Empty).Trim() : txtSegundos_genera_alarma.Text);
                                int timeNuevaAlerta = Convert.ToInt32(txtTiempo_nueva_alerta.Text.Contains(".") ? txtTiempo_nueva_alerta.Text.Replace(".", string.Empty).Trim() : txtTiempo_nueva_alerta.Text);
                                int frecuencia = Convert.ToInt32(txtFrecuenciaAlternativaNoPing.Text.Contains(".") ? txtFrecuenciaAlternativaNoPing.Text.Replace(".", string.Empty).Trim() : txtFrecuenciaAlternativaNoPing.Text);
                                int timeProcesoReport = Convert.ToInt32(txtTiempoProcesoReporte.Text.Contains(".") ? txtTiempoProcesoReporte.Text.Replace(".", string.Empty).Trim() : txtTiempoProcesoReporte.Text);

                                if (generalConfig.actualizaConfig(Convert.ToInt32(spinPing_no_Exitoso.Text),
                                                                    segGeneraAlarma,
                                                                    timeNuevaAlerta,
                                                                    frecuencia,
                                                                    txtServidorSmtp.Text,
                                                                    txtEmail.Text,
                                                                    txtPass.Password,
                                                                    timeProcesoReport,
                                                                    Convert.ToInt32(txtDepure.Text)))
                                {
                                    string logMessage = " \n  Porcentaje perdida ping no exitoso: " + spinPing_no_Exitoso.Text
                                        + " \n  Segundos en generar alarma: " + txtSegundos_genera_alarma.Text + " \n  Tiempo genera nueva alerta: " + txtTiempo_nueva_alerta.Text +
                                        "\n Frecuencia alternativa de no ping: " + txtFrecuenciaAlternativaNoPing.Text + " \n   Email: " + txtEmail.Text + " \n  Tiempo proceso reporte " + txtTiempoProcesoReporte.Text + " \n Tiempo proceso depuración: " +
                                        txtDepure.Text;
                                    var logeer = new LogErroresModificaciones__action();
                                    logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, "Parametros de sistema modificados" + " \n " + logMessage);
                                    MessageBox.Show("Datos actualizados exitosamente.", "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No se pudieron actualizar los parametros de sistema,favor contactar al administrador.",
                                        "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorSistema.xaml.cs(metodo actualizar_click) " + ex.Message);
            }
        }
        public bool isNum(string valor)
        {
            try
            {
                int num = Convert.ToInt32(valor);
                return true;
            }
            catch (Exception e)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorSistema.xaml.cs(metodo isNum) " + e.Message);
                return false;
            }
        }

        public void CargarDatos(ConfiguracionGeneral_BO config)
        {
            try
            {
                IsShowDialog = true;
                spinPing_no_Exitoso.Text = Convert.ToInt32(config.Ping_no_exitoso).ToString("N0");
                txtTiempo_nueva_alerta.Text = "" + config.Tiempo_nueva_alerta.ToString("N0");
                txtFrecuenciaAlternativaNoPing.Text = "" + config.Frecuencia_no_ping.ToString("N0");
                txtSegundos_genera_alarma.Text = "" + config.Generar_alarma.ToString("N0");
                txtEmail.Text = config.Email;
                txtTiempoProcesoReporte.Text = "" + config.Tiempo_proceso_reporte.ToString("N0");
                txtServidorSmtp.Text = "" + config.Servidor_smtp;
                txtDepure.Text = "" + config.Time_depuracion.ToString("N0");
            }
            catch (Exception e)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorSistema.xaml.cs(metodo CargarDatos) " + e.Message);
            }
        }
        private void spinPing_no_Exitoso_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57) e.Handled = false;
            else e.Handled = true;
        }
        private void txtDepure_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57) e.Handled = false;
            else e.Handled = true;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void txtSegundos_genera_alarma_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57) e.Handled = false;
            else e.Handled = true;
        }
        private void txtTiempo_nueva_alerta_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57) e.Handled = false;
            else e.Handled = true;
        }
        private void txtFrecuenciaAlternativaNoPing_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57) e.Handled = false;
            else e.Handled = true;
        }
        private void txtTiempoProcesoReporte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57) e.Handled = false;
            else e.Handled = true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
