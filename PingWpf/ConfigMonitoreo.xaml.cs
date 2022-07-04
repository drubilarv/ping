using System;
using Ping.Accion;
using Ping.BO;
using System.Windows;
using System.Windows.Input;


namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para ConfigMonitoreo.xaml
    /// </summary>
    public partial class ConfigMonitoreo : Window
    {
        ConfiguracionMonitoreo_BO config;
        private DevExpress.Xpf.Grid.GridControl grilla;
        public ConfigMonitoreo(ConfiguracionMonitoreo_BO config, DevExpress.Xpf.Grid.GridControl grilla)
        {
            try
            {
                InitializeComponent();
                this.grilla = grilla;
                if (config != null)
                    CargarDatos(config);
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ConfigMonitoreo.xaml.cs(metodo ConfigMonitoreo) " + ex.Message);
            }
        }

        private void TxtTamanoPack_OnKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                trackBarSizePack.Value = Convert.ToDouble(txtTamanoPack.Text.Trim('.'));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ConfigMonitoreo.xaml.cs(metodo TxtTamanoPack_OnKeyUp) " + ex.Message);
            }
        }

        private void TxtTamanoPack_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57) e.Handled = false;
            else e.Handled = true;
        }

        private void trackBarSizePack_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                txtTamanoPack.Text = trackBarSizePack.Value.ToString("N0");
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ConfigMonitoreo.xaml.cs(metodo trackBarSizePack_EditValueChanged) " + ex.Message);
            }
        }

        private void TxtFrecuency_OnKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                trackBarFrecuency.Value = Convert.ToDouble(txtFrecuency.Text);
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ConfigMonitoreo.xaml.cs(metodo TxtFrecuency_OnKeyUp) " + ex.Message);
            }
        }

        private void TxtFrecuency_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57) e.Handled = false;
            else e.Handled = true;
        }

        private void TrackBarFrecuency_OnEditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                txtFrecuency.Text = decimal.Parse("" + trackBarFrecuency.Value).ToString("N0");
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ConfigMonitoreo.xaml.cs(metodo TrackBarFrecuency_OnEditValueChanged) " + ex.Message);
            }
        }

        private void TxtTimeOut_OnKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                trackBarTimeout.Value = Convert.ToDouble(txtTimeOut.Text.Trim('.'));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ConfigMonitoreo.xaml.cs(metodo TxtTimeOut_OnKeyUp) " + ex.Message);
            }
        }

        private void TxtTimeOut_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57) e.Handled = false;
            else e.Handled = true;
        }

        private void trackBarTimeout_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try
            {
                txtTimeOut.Text = trackBarTimeout.Value.ToString("N0");
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ConfigMonitoreo.xaml.cs(metodo trackBarTimeout_EditValueChanged) " + ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                trackBarSizePack.Value = 1;
                trackBarFrecuency.Value = 1;
                trackBarTimeout.Value = 1;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ConfigMonitoreo.xaml.cs(metodo Button_Click_1) " + ex.Message);
            }
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var action_config = new ConfiguracionMonitoreo_action();

                if (isNum(txtConfigname.Text) | txtConfigname.Text.Length < 1)
                {
                    MessageBox.Show(this, "Nombre configuración inválido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    double frecuen = trackBarFrecuency.Value;
                    double tamanoPaq = trackBarSizePack.Value;
                    double timeOu = trackBarTimeout.Value;
                    if (frecuen <= 0 || tamanoPaq <= 0 || timeOu <= 0)
                    {
                        MessageBox.Show(this, "Ingrese valores mayor a 0", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    if (config == null)
                    {
                        if (action_config.InsertConfigMonitoreo(txtConfigname.Text, frecuen, tamanoPaq, timeOu))
                        {
                            var logeer = new LogErroresModificaciones__action();
                            logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, "Configuración de monitoreo insertada");
                            MessageBox.Show(this, "Registro insertado exitosamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            grilla.ItemsSource = action_config.ObtenerConfig();
                            Close();
                        }
                        else
                            MessageBox.Show(this, "No se pudo Insertar el registro", "información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        //if (!action_config.isEquiposActivosConfig(config.IdConfig))
                        //{
                            if (action_config.UpdateConfigMonitoreo(config.IdConfig, txtConfigname.Text, frecuen, tamanoPaq, timeOu))
                            {
                                string messageLog = "Configuracion de monitoreo modificada  nombre: " + txtConfigname.Text + " Frecuencia : " + trackBarFrecuency.Value + " Tamaño Paquete : " + trackBarSizePack.Value + " TimeOut :" + trackBarTimeout.Value;
                                MessageBox.Show(this, "Registro actualizado exitosamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                                var logeer = new LogErroresModificaciones__action();
                                logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, messageLog);
                                grilla.ItemsSource = action_config.ObtenerConfig();
                                Close();
                            }
                            else
                                MessageBox.Show(this, "No se pudo insertar el registro", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        //}
                        //else
                        //    MessageBox.Show(this, "Existen equipos activos con esta configuración,debe desactivarlos o cambiar su configuración antes de continuar esta operación", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ConfigMonitoreo.xaml.cs(metodo Aceptar_Click) " + ex.Message);
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
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ConfigMonitoreo.xaml.cs(metodo isNum) " + e.Message);
                return false;
            }
        }

        public void CargarDatos(ConfiguracionMonitoreo_BO config)
        {
            try
            {
                this.config = config;
                txtConfigname.Text = config.NombreConfig;
                trackBarFrecuency.Value = Convert.ToDouble(config.TiempoPing);
                trackBarSizePack.Value = Convert.ToDouble(config.TamPaquete);
                trackBarTimeout.Value = Convert.ToDouble(config.Timeout);
            }
            catch (Exception e)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ConfigMonitoreo.xaml.cs(metodo CargarDatos) " + e.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
