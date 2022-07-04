using Ping.Accion;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Windows;


namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para AlertasMonitoreo_view.xaml
    /// </summary>
    public partial class AlertasMonitoreo_view : Window
    {
        public AlertasMonitoreo_view()
        {
            try
            {
                InitializeComponent();
                LoadGrilla();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AlertasMonitoreo_view.xaml.cs(metodo AlertasMonitoreo_view) " + ex.Message);
            }
        }
        private void LoadGrilla()
        {
            try
            {
                var amaction = new AlertasMonitoreo_Action();
                GridAlerta.ItemsSource = amaction.ObtenerAlertas_no_Leidas();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AlertasMonitoreo_view.xaml.cs(metodo LoadGrilla) " + ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var datos = (List<AlertasMonitoreo_BO>)GridAlerta.ItemsSource;
                var aaction = new AlertasMonitoreo_Action();
                aaction.UpdateAlerta(datos);
                MessageBox.Show("Datos actualizados", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadGrilla();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AlertasMonitoreo_view.xaml.cs(metodo Button_Click_1) " + ex.Message);
            }
        }
    }
}
