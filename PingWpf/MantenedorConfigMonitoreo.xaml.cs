using Ping.Accion;
using Ping.BO;
using System;
using System.Windows;


namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para MantenedorConfigMonitoreo.xaml
    /// </summary>
    public partial class MantenedorConfigMonitoreo : Window
    {
        public MantenedorConfigMonitoreo()
        {
            try
            {
                InitializeComponent();
                var config_action = new ConfiguracionMonitoreo_action();
                GridConfigMonito.ItemsSource = config_action.ObtenerConfig();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorConfigMonitoreo.xaml.cs(metodo MantenedorConfigMonitoreo) " + ex.Message);
            }
        }

        private void agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var configMonito = new ConfigMonitoreo(null, GridConfigMonito);
                configMonito.Owner = this;
                configMonito.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorConfigMonitoreo.xaml.cs(metodo agregar_Click) " + ex.Message);
            }
        }

        private void editar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var config = GridConfigMonito.SelectedItem as ConfiguracionMonitoreo_BO;
                if (config.IdConfig == 4)
                    MessageBox.Show("No puede modificar la configuración por defecto", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    var configMonito = new ConfigMonitoreo(config, GridConfigMonito);
                    configMonito.Owner = this;
                    configMonito.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorConfigMonitoreo.xaml.cs(metodo editar_Click) " + ex.Message);
            }
        }

        private void borrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var config = GridConfigMonito.SelectedItem as ConfiguracionMonitoreo_BO;
                var config_action = new ConfiguracionMonitoreo_action();
                if (config.IdConfig == 1)
                    MessageBox.Show("No puede eliminar configuración por defecto", "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    var result = MessageBox.Show("¿Está seguro que desea eliminar esta Configuración de Monitoreo?", "Información", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        var resultado = config_action.DeleteConfig(config.IdConfig);
                        if (resultado)
                        {
                            var logeer = new LogErroresModificaciones__action();
                            logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, "Configuración de Monitoreo " + config.IdConfig + " Eliminada");
                        }
                        MessageBox.Show(resultado ? "Registro eliminado correctamente" : "No se pudo eliminar el registro, consulte si existen equipos o grupos con esta configuración asociada o contacte al administrador",
                            "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
                        GridConfigMonito.ItemsSource = config_action.ObtenerConfig();
                    }
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorConfigMonitoreo.xaml.cs(metodo borrar_Click) " + ex.Message);
            }
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
