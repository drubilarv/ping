using Ping.Accion;
using Ping.BO;
using System;
using System.IO;
using System.Windows;


namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para ReporteAutomatico.xaml
    /// </summary>
    public partial class ReporteAutomatico : Window
    {
        public ReporteAutomatico()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, DateTime.Now, Environment.UserName, "ReporteAutomatico.xaml.cs(metodo ReporteAutomatico) " + ex.Message);
            }
        }

        private void btn_BuscarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DatePickInicio.Text == string.Empty || DatePickFin.Text == string.Empty)
                {
                    MessageBox.Show("Ingrese fechas", "Información", MessageBoxButton.OK);
                    return;
                }
                var reportaction = new Reporte_action();
                var data = reportaction.SelectReportRango(Convert.ToDateTime(DatePickInicio.Text), Convert.ToDateTime(DatePickFin.Text));
                if (data.Count > 0)
                    ReportGrid.ItemsSource = data;
                else
                {
                    ReportGrid.ItemsSource = null;
                    MessageBox.Show("Sin datos", "Información", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ReporteAutomatico.xaml.cs(metodo btn_BuscarClick) " + ex.Message);
            }
        }

        private void btnAbrir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ReportGrid.ItemsSource != null)
                {
                    //string ruta = ConfigurationManager.AppSettings.Get("rutaAlertaAutomatica");
                    string ruta = Environment.CurrentDirectory + @"\PdfAutom\";
                    var dato = (Reportes_BO)ReportGrid.SelectedItem;
                    string name = dato.name.Split('_')[1]; //dato.name.Substring(8,dato.name.Length-8);
                    var reportaction = new Reporte_action();
                    var archivo = reportaction.SelectReport(name.Split('.')[0]);
                    byte[] buffer = (byte[])archivo;
                    FileStream fs = File.Create(ruta + "archivoAutom.pdf");
                    fs.Close();
                    FileStream fs2 = File.OpenWrite(ruta + "archivoAutom.pdf");
                    fs2.Write(buffer, 0, buffer.Length);
                    fs2.Close();
                    System.Diagnostics.Process.Start(ruta + "archivoAutom.pdf");
                }
                else
                    MessageBox.Show("Debe buscar reportes para abrirlos", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ReporteAutomatico.xaml.cs(metodo btnAbrir_Click) " + ex.Message);
            }
        }
        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
