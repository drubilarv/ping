using System;
using System.Windows;
using Microsoft.Reporting.WinForms;
using Ping.Accion;
using System.Configuration;
using PingWpf.SW15001DataSetTableAdapters;

namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para ViewLog.xaml
    /// </summary>
    public partial class ViewLog : Window
    {
        public ViewLog()
        {
            try
            {
                InitializeComponent();

                var loaction = new LogErroresModificaciones__action();
                cboxLog.ItemsSource = loaction.ObtenerLogId();
                cboxLog.SelectedIndex = +1;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ViewLog.xaml.cs(metodo ViewLog) " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idTipoLog = cboxLog.SelectedIndex + 1;

                DateTime? fechaInicio;
                if (DatePickInicio.Text == string.Empty)
                    fechaInicio = null;
                else
                    fechaInicio = Convert.ToDateTime(DatePickInicio.Text);

                DateTime? fechaFin;
                if (DatePickFin.Text == string.Empty)
                    fechaFin = null;
                else
                    fechaFin = Convert.ToDateTime(DatePickFin.Text);

                var reportDataSource1 = new ReportDataSource();
                var dataset = new SW15001DataSet();
                dataset.BeginInit();
                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                var conexion = ConfigurationManager.ConnectionStrings["ConexPing"].ToString();

                if (DatePickInicio.Text.Length != 0 && DatePickFin.Text.Length == 0)
                {
                    //alog_modificaciones.ObtenerLogSinFiltroFechaFin(cboxLog.SelectedIndex + 1, dateinicio);
                    reportDataSource1.Value = dataset.SP_SW15001_SELECT_LOG_SIN_FILTRO_FECHA_FIN;
                    ReporteCuerpo.ProcessingMode = ProcessingMode.Local;
                    ReporteCuerpo.LocalReport.DataSources.Clear();
                    ReporteCuerpo.LocalReport.DataSources.Add(reportDataSource1);
                    ReporteCuerpo.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reportes\ReportLog.rdlc";

                    var parametros1 = new ReportParameter[2];
                    parametros1[0] = new ReportParameter("idTipoLog", idTipoLog.ToString());
                    parametros1[1] = new ReportParameter("fechaInicio", fechaInicio.ToString());

                    ReporteCuerpo.LocalReport.SetParameters(parametros1);
                    dataset.EndInit();
                    var adapter1 = new SP_SW15001_SELECT_LOG_SIN_FILTRO_FECHA_FINTableAdapter();

                    adapter1.Connection.ConnectionString = conexion;
                    adapter1.Fill(dataset.SP_SW15001_SELECT_LOG_SIN_FILTRO_FECHA_FIN, idTipoLog, fechaInicio);
                    ReporteCuerpo.RefreshReport();
                    return;
                }
                if (DatePickInicio.Text.Length == 0 && DatePickFin.Text.Length != 0)
                {
                    reportDataSource1.Value = dataset.SP_SW15001_SELECT_LOG_SIN_FILTRO_FECHA_INICIO;
                    ReporteCuerpo.ProcessingMode = ProcessingMode.Local;
                    ReporteCuerpo.LocalReport.DataSources.Clear();
                    ReporteCuerpo.LocalReport.DataSources.Add(reportDataSource1);
                    ReporteCuerpo.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reportes\ReportLog.rdlc";

                    var parametros2 = new ReportParameter[2];
                    parametros2[0] = new ReportParameter("idTipoLog", idTipoLog.ToString());
                    parametros2[1] = new ReportParameter("fechaFin", fechaFin.ToString());

                    ReporteCuerpo.LocalReport.SetParameters(parametros2);
                    dataset.EndInit();
                    var adapter1 = new SP_SW15001_SELECT_LOG_SIN_FILTRO_FECHA_INICIOTableAdapter();

                    adapter1.Connection.ConnectionString = conexion;
                    adapter1.Fill(dataset.SP_SW15001_SELECT_LOG_SIN_FILTRO_FECHA_INICIO, idTipoLog, fechaFin);
                    ReporteCuerpo.RefreshReport();
                    return;
                }
                if (DatePickInicio.Text.Length == 0 && DatePickFin.Text.Length == 0)
                {
                    reportDataSource1.Value = dataset.SP_SW15001_SELECT_LOG_SOLO_FILTRO_ID_TIPO_LOG;
                    ReporteCuerpo.ProcessingMode = ProcessingMode.Local;
                    ReporteCuerpo.LocalReport.DataSources.Clear();
                    ReporteCuerpo.LocalReport.DataSources.Add(reportDataSource1);
                    ReporteCuerpo.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reportes\ReportLog.rdlc";

                    var parametros3 = new ReportParameter[1];
                    parametros3[0] = new ReportParameter("idTipoLog", idTipoLog.ToString());

                    ReporteCuerpo.LocalReport.SetParameters(parametros3);
                    dataset.EndInit();
                    var adapter1 = new SP_SW15001_SELECT_LOG_SOLO_FILTRO_ID_TIPO_LOGTableAdapter();

                    adapter1.Connection.ConnectionString = conexion;
                    adapter1.Fill(dataset.SP_SW15001_SELECT_LOG_SOLO_FILTRO_ID_TIPO_LOG, idTipoLog);
                    ReporteCuerpo.RefreshReport();
                    return;
                }

                reportDataSource1.Value = dataset.SW1501_SELECT_LOG_FECHA;
                ReporteCuerpo.ProcessingMode = ProcessingMode.Local;
                ReporteCuerpo.LocalReport.DataSources.Clear();
                ReporteCuerpo.LocalReport.DataSources.Add(reportDataSource1);
                ReporteCuerpo.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reportes\ReportLog.rdlc";

                var parametros4 = new ReportParameter[3];
                parametros4[0] = new ReportParameter("idTipoLog", idTipoLog.ToString());
                parametros4[1] = new ReportParameter("fechaInicio", fechaInicio.ToString());
                parametros4[2] = new ReportParameter("fechaFin", fechaFin.ToString());

                ReporteCuerpo.LocalReport.SetParameters(parametros4);
                dataset.EndInit();
                var adapter = new SW1501_SELECT_LOG_FECHATableAdapter();

                adapter.Connection.ConnectionString = conexion;
                adapter.Fill(dataset.SW1501_SELECT_LOG_FECHA, idTipoLog, fechaInicio, fechaFin);
                ReporteCuerpo.RefreshReport();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ViewLog.xaml.cs(metodo Button_Click) " + ex.Message);
            }
        }
    }
}
