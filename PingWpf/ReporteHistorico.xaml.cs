using Microsoft.Reporting.WinForms;
using Ping.Accion;
using Ping.BO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using PingWpf.SW15001DataSetTableAdapters;



namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para ReporteHistorico.xaml
    /// </summary>
    public partial class ReporteHistorico : Window
    {
        public ReporteHistorico()
        {
            try
            {
                InitializeComponent();
                //LoadDdlEquipos();
                LoadDdlGrupos();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ReporteHistorico.xaml.cs(metodo ReporteHistorico) " + ex.Message);
            }
        }
        private void LoadDdlEquipos(int idGrupo)
        {
            try
            {
                var eaction = new Equipos__action();
                cbbEquipos.ItemsSource = eaction.ObtenerEquipos();
                cbbEquipos.SelectedIndex = +1;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ReporteHistorico.xaml.cs(metodo LoadDdlEquipos) " + ex.Message);
            }
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var grupo = ((Grupos_BO)cbbGrupos.SelectedItem).Id;
                if (cbbEquipos.SelectedItem == null)
                {
                    MessageBox.Show("Ingrese Equipos o asegurese que el grupo tenga equipos");
                    return;
                }
                var ipEquipo = ((Equipos_BO)cbbEquipos.SelectedItem).Id;
                    DateTime? fechaInicio;
                    if (DatePickerFechaDesde.Text == string.Empty)
                        fechaInicio = null;
                    else
                        fechaInicio = Convert.ToDateTime(DatePickerFechaDesde.Text);

                    DateTime? fechaFin;
                    if (DatePickerFechaHasta.Text == string.Empty)
                        fechaFin = null;
                    else
                        fechaFin = Convert.ToDateTime(DatePickerFechaHasta.Text);

                    bool exitoPing = chkExito.IsChecked.Value;
                    Reporte(grupo, ipEquipo, fechaInicio, fechaFin, exitoPing);
                
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ReporteHistorico.xaml.cs(metodo btnBuscar_Click) " + ex.Message);
            }
        }
        private void LoadDdlGrupos()
        {
            try
            {
                var gaction = new Grupos__action();
                cbbGrupos.ItemsSource = gaction.ObtenerGruposMant();
                cbbGrupos.SelectedIndex = +1;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ReporteHistorico.xaml.cs(metodo LoadDdlGrupos) " + ex.Message);
            }
        }
        private void cbbGrupos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var amonitoaction = new AlertasMonitoreo_Action();
                //var id = ((Grupos_BO)cbbGrupos.SelectedItem).Id;
                var list = amonitoaction.ObtenerEquipos(((Grupos_BO)cbbGrupos.SelectedItem).Id);
                cbbEquipos.ItemsSource = list;
                cbbEquipos.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "ReporteHistorico.xaml.cs(metodo cbbGrupos_SelectionChanged) " + ex.Message);
            }
        }
        private void Reporte(int grupo, string ipEquipo, DateTime? fechaInicio, DateTime? fechaFin, bool exitoPing)
        {
            try
            {
                var reportDataSource1 = new ReportDataSource();
                var dataset = new SW15001DataSet();
                dataset.BeginInit();
                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                var conexion = ConfigurationManager.ConnectionStrings["SW15001"].ToString();

                if (DatePickerFechaDesde.Text.Length == 0 && DatePickerFechaHasta.Text.Length == 0)
                {
                    //data = monitoreoAction.GetMonitoreoReporSinFiltroFechaHistorico(((Grupos_BO)cbbGrupos.SelectedItem).Id, cbbEquipos.SelectedItem.ToString(), chkExito.IsChecked.Value);
                    reportDataSource1.Value = dataset.SP_SW15001_SELECT_MONITOREO_SIN_FILTRO_FECHA_REPORT_HISTORICO;
                    ReporteCuerpo.ProcessingMode = ProcessingMode.Local;
                    ReporteCuerpo.LocalReport.DataSources.Clear();
                    ReporteCuerpo.LocalReport.DataSources.Add(reportDataSource1);
                    ReporteCuerpo.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reportes\ReportHistorico.rdlc";

                    var parametros1 = new ReportParameter[3];
                    parametros1[0] = new ReportParameter("grupo", grupo.ToString());
                    parametros1[1] = new ReportParameter("equipo", ipEquipo);
                    parametros1[2] = new ReportParameter("exitoPing", exitoPing.ToString());
                    ReporteCuerpo.LocalReport.SetParameters(parametros1);
                    dataset.EndInit();
                    var adapter1 = new SP_SW15001_SELECT_MONITOREO_SIN_FILTRO_FECHA_REPORT_HISTORICOTableAdapter();

                    adapter1.Connection.ConnectionString = conexion;
                    adapter1.Fill(dataset.SP_SW15001_SELECT_MONITOREO_SIN_FILTRO_FECHA_REPORT_HISTORICO, grupo, ipEquipo, exitoPing);
                    ReporteCuerpo.RefreshReport();
                    return;
                }
                if (DatePickerFechaDesde.Text.Length != 0 && DatePickerFechaHasta.Text.Length == 0)
                {
                    //data = monitoreoAction.GetMonitoreoReporFiltroFechaInicioHistorico(((Grupos_BO)cbbGrupos.SelectedItem).Id, cbbEquipos.SelectedItem.ToString(), Convert.ToDateTime(DatePickerFechaDesde.Text), chkExito.IsChecked.Value);
                    reportDataSource1.Value = dataset.SP_SW15001_SELECT_MONITOREO_FILTRO_FECHA_INICIO_REPORT_HISTORICO;
                    ReporteCuerpo.ProcessingMode = ProcessingMode.Local;
                    ReporteCuerpo.LocalReport.DataSources.Clear();
                    ReporteCuerpo.LocalReport.DataSources.Add(reportDataSource1);
                    ReporteCuerpo.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reportes\ReportHistorico.rdlc";

                    var parametros2 = new ReportParameter[4];
                    parametros2[0] = new ReportParameter("grupo", grupo.ToString());
                    parametros2[1] = new ReportParameter("equipo", ipEquipo);
                    parametros2[2] = new ReportParameter("fechaInicio", fechaInicio.ToString());
                    parametros2[3] = new ReportParameter("exitoPing", exitoPing.ToString());
                    ReporteCuerpo.LocalReport.SetParameters(parametros2);
                    dataset.EndInit();
                    var adapter1 = new SP_SW15001_SELECT_MONITOREO_FILTRO_FECHA_INICIO_REPORT_HISTORICOTableAdapter();

                    adapter1.Connection.ConnectionString = conexion;
                    adapter1.Fill(dataset.SP_SW15001_SELECT_MONITOREO_FILTRO_FECHA_INICIO_REPORT_HISTORICO, grupo, ipEquipo, fechaInicio, exitoPing);
                    ReporteCuerpo.RefreshReport();
                    return;
                }
                if (DatePickerFechaDesde.Text.Length == 0 && DatePickerFechaHasta.Text.Length != 0)
                {
                    //data = monitoreoAction.GetMonitoreoReporFiltroFechaFinHistorico(((Grupos_BO)cbbGrupos.SelectedItem).Id, cbbEquipos.SelectedItem.ToString(), Convert.ToDateTime(DatePickerFechaHasta.Text), chkExito.IsChecked.Value);
                    reportDataSource1.Value = dataset.SP_SW15001_SELECT_MONITOREO_FILTRO_FECHA_FIN_REPORT_HISTORICO;
                    ReporteCuerpo.ProcessingMode = ProcessingMode.Local;
                    ReporteCuerpo.LocalReport.DataSources.Clear();
                    ReporteCuerpo.LocalReport.DataSources.Add(reportDataSource1);
                    ReporteCuerpo.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reportes\ReportHistorico.rdlc";

                    var parametros3 = new ReportParameter[4];
                    parametros3[0] = new ReportParameter("grupo", grupo.ToString());
                    parametros3[1] = new ReportParameter("equipo", ipEquipo);
                    parametros3[2] = new ReportParameter("fechaFin", fechaFin.ToString());
                    parametros3[3] = new ReportParameter("exitoPing", exitoPing.ToString());
                    ReporteCuerpo.LocalReport.SetParameters(parametros3);
                    dataset.EndInit();
                    var adapter1 = new SP_SW15001_SELECT_MONITOREO_FILTRO_FECHA_FIN_REPORT_HISTORICOTableAdapter();

                    adapter1.Connection.ConnectionString = conexion;
                    adapter1.Fill(dataset.SP_SW15001_SELECT_MONITOREO_FILTRO_FECHA_FIN_REPORT_HISTORICO, grupo, ipEquipo, fechaFin, exitoPing);
                    ReporteCuerpo.RefreshReport();
                    return;
                }
                reportDataSource1.Value = dataset.SP_SW15001_SELECT_MONITOREO_REPORT_HISTORICO;
                ReporteCuerpo.ProcessingMode = ProcessingMode.Local;
                ReporteCuerpo.LocalReport.DataSources.Clear();
                ReporteCuerpo.LocalReport.DataSources.Add(reportDataSource1);
                ReporteCuerpo.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reportes\ReportHistorico.rdlc";

                var parametros = new ReportParameter[5];
                parametros[0] = new ReportParameter("grupo", grupo.ToString());
                parametros[1] = new ReportParameter("equipo", ipEquipo);
                parametros[2] = new ReportParameter("fechaInicio", fechaInicio.ToString());
                parametros[3] = new ReportParameter("fechaFin", fechaFin.ToString());
                parametros[4] = new ReportParameter("exitoPing", exitoPing.ToString());
                ReporteCuerpo.LocalReport.SetParameters(parametros);
                dataset.EndInit();
                var adapter = new SP_SW15001_SELECT_MONITOREO_REPORT_HISTORICOTableAdapter();
                adapter.Connection.ConnectionString = conexion;
                adapter.Fill(dataset.SP_SW15001_SELECT_MONITOREO_REPORT_HISTORICO, grupo, ipEquipo, fechaInicio, fechaFin, exitoPing);
                ReporteCuerpo.RefreshReport();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName,
                    "ReporteHistorico.xaml.cs(metodo Reporte) " + ex.Message);
            }
        }
        private void btnExporExcel_Click(object sender, RoutedEventArgs e)
        {
            //List<Monitoreo_BO> datos = null;// (List<Monitoreo_BO>)griHistorico.ItemsSource;
            //if (datos == null)
            //{
            //    MessageBox.Show("Sin datos a exportar", "Información", MessageBoxButton.OK);
            //    return;
            //}

            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            //    xlApp.Visible = true;
            //    Workbook wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //    Worksheet ws = (Worksheet)wb.Worksheets[1];

            //    for (int i = 1; i < 7; i++)
            //         ws.Range["A" + i, "G" + i].Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

            //    var x = 7;
            //    ws.Cells[x, 1] = "EQUIPO";
            //    ws.Cells[x, 2] = "GRUPO";
            //    ws.Cells[x, 3] = "JITTER(ms)";
            //    ws.Cells[x, 4] = "LATENCIA(ms)";
            //    ws.Cells[x, 5] = "FECHA";
            //    ws.Cells[x, 6] = "HORA";
            //    ws.Cells[x, 7] = "EXITO PING";
            //    ws.Range["A7", "G7"].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            //    ws.Range["A7", "G7"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            //    ws.Range["A7", "G7"].ColumnWidth = 15;
            try
            {
                //string ruta = Environment.CurrentDirectory + @"\Images\protabLogo.png";
                //ws.Shapes.AddPicture(ruta, Microsoft.Office.Core.MsoTriState.msoFalse, MsoTriState.msoCTrue, 110, 10, 300, 50);//de izquiera a derecha,de abajo hacia arriba,largo de imagen,ancho de imagen

                //foreach (Monitoreo_BO monito in datos)
                //{
                //    ws.Cells[x + 1, 1] = monito.ipEquipo;
                //    ws.Cells[x + 1, 2] = monito.nombreGrupo;
                //    ws.Cells[x + 1, 3] = monito.jitter.Replace('.', ' ');
                //    ws.Cells[x + 1, 4] = monito.latencia.Replace('.',' ');
                //    ws.Cells[x + 1, 5] = monito.Timestamp.ToString("dd-MM-yyyy");
                //    ws.Cells[x + 1, 6] = monito.hora;
                //    ws.Cells[x + 1, 7] = monito.exitoPingStr;
                //    ws.Range["A"+x, "G"+x].Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                //    x++;
                //}
            }
            catch (Exception)
            {
                MessageBox.Show("Acaba de cerrar microsoft excel antes de que termine de cargar datos", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
