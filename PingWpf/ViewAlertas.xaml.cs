using Ping.Accion;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para ViewAlertas.xaml
    /// </summary>
    public partial class ViewAlertas : Window
    {
        public ViewAlertas()
        {
            try
            {
                InitializeComponent();
                var amonitoaction = new AlertasMonitoreo_Action();
                //GridAlertas.ItemsSource = amonitoaction.GetAlertaMonitoreo();
                cboxGrupo.ItemsSource = amonitoaction.ObtenerGrupos();
                cboxGrupo.SelectedIndex = +1;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ViewAlertas.xaml.cs(metodo ViewAlertas) " + ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var amonitoaction = new AlertasMonitoreo_Action();
                List<AlertasMonitoreo_BO> data;
                var grupo = (Grupos_BO)cboxGrupo.SelectedItem;
                var ipEquipo = ((Equipos_BO)cboxEquipo.SelectedItem).Id;

                if (fechaInicio.Text.Length == 0 && fechaFin.Text.Length == 0)
                {
                    data = amonitoaction.GetAlertaMonitoreoSinFiltroFechas(grupo, ipEquipo, ((bool)checkLeido.IsChecked));
                    if (data.Count > 0)
                        GridAlertas.ItemsSource = data;
                    else
                    {
                        GridAlertas.ItemsSource = data;
                        MessageBox.Show("Sin datos", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    return;
                }
                if (fechaInicio.Text.Length != 0 && fechaFin.Text.Length == 0)
                {
                    data = amonitoaction.GetAlertaMonitoreoConFiltroFechaInicio(grupo, ipEquipo, Convert.ToDateTime(fechaInicio.Text), ((bool)checkLeido.IsChecked));
                    if (data.Count > 0)
                        GridAlertas.ItemsSource = data;
                    else
                    {
                        GridAlertas.ItemsSource = data;
                        MessageBox.Show("Sin datos", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    return;
                }
                if (fechaInicio.Text.Length == 0 && fechaFin.Text.Length != 0)
                {
                    data = amonitoaction.GetAlertaMonitoreoConFiltroFechaFin(grupo, ipEquipo, Convert.ToDateTime(fechaFin.Text), ((bool)checkLeido.IsChecked));
                    if (data.Count > 0)
                        GridAlertas.ItemsSource = data;
                    else
                    {
                        GridAlertas.ItemsSource = data;
                        MessageBox.Show("Sin datos", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    return;
                }
                data = amonitoaction.GetAlertaMonitoreo(grupo, ipEquipo, Convert.ToDateTime(fechaInicio.Text), Convert.ToDateTime(fechaFin.Text), ((bool)checkLeido.IsChecked));
                if (data.Count > 0)
                    GridAlertas.ItemsSource = data;
                else
                {
                    GridAlertas.ItemsSource = data;
                    MessageBox.Show("Sin datos", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ViewAlertas.xaml.cs(metodo Button_Click) " + ex.Message);
            }
        }
        private void cboxGrupo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var amonitoaction = new AlertasMonitoreo_Action();
                //var id = ((Grupos_BO)cboxGrupo.SelectedItem).Id;
                var list = amonitoaction.ObtenerEquipos(((Grupos_BO)cboxGrupo.SelectedItem).Id);
                cboxEquipo.ItemsSource = list;
                cboxEquipo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ViewAlertas.xaml.cs(metodo cboxGrupo_SelectionChanged) " + ex.Message);
            }
        }
    }
}
