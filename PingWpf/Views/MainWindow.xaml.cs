using System;
using System.Windows;
using Ping.Accion;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;




namespace PingWpf.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AlertasMonitoreo_Action _alertasMonitoreoAction;

        public MainWindow()
        {
            InitializeComponent();
            VentanaWindow_Principal.WindowState = WindowState.Maximized;
            AlarmasHilo();
            EliminaRegistrosAntiguos();
            ReporteAutomatico();
        }
        
        public void AlarmasHilo()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        _alertasMonitoreoAction = new AlertasMonitoreo_Action();
                        while (true)
                        {
                            Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings.Get("SleepAlertas")));
                            if (_alertasMonitoreoAction.ObtenerAlertas_no_Leidas().Count > 0)
                                BtnAlerta.Dispatcher.Invoke(AlertasNoleidas);
                            else
                                BtnAlerta.Dispatcher.Invoke(AlertasLeidas);
                        }
                    }
                    catch (Exception ex)
                    {
                        var logeer = new LogErroresModificaciones__action();
                        logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.xaml.cs(metodo AlarmasHilo) " + ex.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.xaml.cs(metodo AlarmasHilo) " + ex.Message);
            }
        }
        private void EliminaRegistrosAntiguos()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        var isRun = true;
                        while (isRun)
                        {
                            var deaction = new Depuracion_action();
                            isRun = deaction.Depuracion();

                            int tiempoDemoraDepuracion = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TiemGeneraDepuracion"));
                            Thread.Sleep(tiempoDemoraDepuracion);
                        }
                        if (!isRun)
                        {
                            SendEmail_action.Send("Estimado,se informa que la aplicación no tiene acceso a la base de datos");
                        }
                    }
                    catch (Exception ex)
                    {
                        var logeer = new LogErroresModificaciones__action();
                        logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindow.xaml.cs(metodo EliminaRegistrosAntiguos(dentro)) " + ex.Message);

                        SendEmail_action.Send("Alerta, Problema al eliminar registros antiguos de la base datos!");
                    }
                });
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindow.xaml.cs(metodo EliminaRegistrosAntiguos(fuera)) " + ex.Message);
            }
        }
        private void AlertasNoleidas()
        {
            try
            {
                BtnAlerta.Foreground = System.Windows.Media.Brushes.Red;
                BtnAlerta.Content = "ALERTAS NO LEÍDAS";
                LayoutPanelAlertas.AutoHidden = false;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.xaml.cs(metodo AlertasNoleidas) " + ex.Message);
            }
        }
        public void ReporteAutomatico()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        while (true)
                        {
                            var FechaReporteInicio = System.DateTime.Now;
                            var configuracionGeneralAction = new ConfiguracionGeneral_action();
                            var timeProcesoReporte = configuracionGeneralAction.ObtenerConfig().Tiempo_proceso_reporte;
                            Thread.Sleep(timeProcesoReporte);

                            var monitoreoAction = new Monitoreo_action();
                            var FechaReporteFin = System.DateTime.Now;
                            ExportToPdf(monitoreoAction.GetAllMonitoreoParaReportAutomat(), FechaReporteInicio, FechaReporteFin);
                        }
                    }
                    catch (Exception ex)
                    {
                        var logeer = new LogErroresModificaciones__action();
                        logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindow.xaml.cs(metodo ReporteAutomatico(dentro)) " + ex.Message);

                        SendEmail_action.Send("Alerta, Problema al generar reporte automático!");
                    }
                });
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, DateTime.Now, Environment.UserName, "MainWindow.xaml.cs(metodo ReporteAutomatico(fuera)) " + ex.Message);
            }
        }
        public void ExportToPdf(DataTable myDataTable, System.DateTime inicoReport, System.DateTime finReport)
        {
            try
            {
                var document = new Document(PageSize.A4, 1, 1, 1, 1);
                var ms = new System.IO.MemoryStream();
                string reportname = "Report_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".pdf";
                //PdfWriter writerSend = PdfWriter.GetInstance(document, new FileStream(ConfigurationManager.AppSettings.Get("rutaAlertaAutomatica") + reportname, FileMode.Create));
                PdfWriter writerSend = PdfWriter.GetInstance(document, new FileStream(Environment.CurrentDirectory + @"\PdfAutom\" + reportname, FileMode.Create));
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                int cols = myDataTable.Columns.Count;
                int rows = myDataTable.Rows.Count;
                document.Open();

                var tableData = new PdfPTable(2);
                tableData.HorizontalAlignment = 1;
                tableData.TotalWidth = 520f;
                tableData.LockedWidth = true;
                tableData.DefaultCell.Padding = 0f;

                float[] todoWidths = new float[] { 1f, 1f };
                tableData.SetWidths(todoWidths);

                string espacio = "\n";
                var cellSpacio = new PdfPCell(new Phrase(espacio, new Font(Font.BOLD, 15, Font.NORMAL, Color.BLACK)));
                cellSpacio.Colspan = 2;
                cellSpacio.BorderWidth = 0f;
                tableData.AddCell(cellSpacio);

                string titulo = "\n\n Reporte Automático";
                var cellTitulo = new PdfPCell(new Phrase(titulo, new Font(Font.BOLD, 15, Font.NORMAL, Color.BLACK)));
                cellTitulo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                cellTitulo.VerticalAlignment = 0;
                cellTitulo.BorderWidth = 0f;
                tableData.AddCell(cellTitulo);

                // Creamos la imagen y le ajustamos el tamaño
                string ruta = Environment.CurrentDirectory + @"\Images\protabLogo.png";
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(ruta);
                imagen.Alignment = Element.ALIGN_RIGHT;
                tableData.AddCell(imagen);

                string espacio1 = "\n";
                var cellSpacio1 = new PdfPCell(new Phrase(espacio1, new Font(Font.BOLD, 15, Font.NORMAL, Color.BLACK)));
                cellSpacio1.Colspan = 2;
                cellSpacio1.BorderWidth = 0f;
                tableData.AddCell(cellSpacio1);

                document.Add(tableData);

                var tableDataCuerpo = new PdfPTable(10);
                tableDataCuerpo.HorizontalAlignment = 1;
                tableDataCuerpo.TotalWidth = 520f;
                tableDataCuerpo.LockedWidth = true;
                tableDataCuerpo.DefaultCell.Padding = 0f;

                float[] todoWidthsCuerpo = new float[] { 0.8f, 1.2f, 1f, 1f, 1.1f, 0.9f, 1f, 1f, 1f, 1f };
                tableDataCuerpo.SetWidths(todoWidthsCuerpo);

                //table headers
                for (int i = 0; i < cols; i++)
                {
                    var cellCols = new PdfPCell(new Phrase(myDataTable.Columns[i].ColumnName.Replace('_', ' '), new Font(Font.BOLD, 8, Font.NORMAL, Color.WHITE)));
                    cellCols.BackgroundColor = iTextSharp.text.Color.BLACK;
                    //cellCols.Width = 10;
                    cellCols.BorderWidth = 1f;
                    cellCols.BorderColor = iTextSharp.text.Color.WHITE;
                    cellCols.VerticalAlignment = 1;
                    cellCols.HorizontalAlignment = 1;//0=Left, 1=Centre, 2=Right
                    tableDataCuerpo.AddCell(cellCols);
                }

                //table data 
                for (int k = 0; k < rows; k++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        var cellRows = new PdfPCell(new Phrase(myDataTable.Rows[k][j].ToString(), new Font(Font.BOLD, 8, Font.NORMAL, Color.BLACK)));
                        if (j != 0 && j != 1 && j != 2)
                            cellRows.HorizontalAlignment = 2;//0=Left, 1=Centre, 2=Right
                        tableDataCuerpo.AddCell(cellRows);
                    }
                }

                document.Add(tableDataCuerpo);
                document.Close();
                writer.Close();
                var reportaction = new Reporte_action();
                byte[] datos = ms.ToArray();
                reportaction.InsertReport(datos);

                writerSend.Close();
                //string filename = ConfigurationManager.AppSettings.Get("rutaAlertaAutomatica") + reportname;
                string filename = Environment.CurrentDirectory + @"\PdfAutom\" + reportname;
                SendEmail_action.Send(inicoReport, finReport, filename);

                if (System.IO.File.Exists(Environment.CurrentDirectory + @"\PdfAutom\" + reportname))
                    System.IO.File.Delete(Environment.CurrentDirectory + @"\PdfAutom\" + reportname);
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, DateTime.Now, Environment.UserName, "MainWindow.xaml.cs(metodo ExportToPdf) " + ex.Message);
            }
        }
        private void AlertasLeidas()
        {
            try
            {
                BtnAlerta.Foreground = System.Windows.Media.Brushes.MediumSeaGreen;
                BtnAlerta.Content = "SIN ALERTAS";
                LayoutPanelAlertas.AutoHidden = true;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.xaml.cs(metodo AlertasLeidas) " + ex.Message);
            }
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var mEquipos = new MantenedorEquipos();
                mEquipos.Owner = this;
                mEquipos.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.xaml.cs(metodo MenuItem_Click_1) " + ex.Message);
            }
        }
        private void btnAlerta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BtnAlerta.Content == "SIN ALERTAS")
                {
                    MessageBox.Show(this, "No existen alertas vigentes", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var alerta = new AlertasMonitoreo_view();
                alerta.Owner = this;
                alerta.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindow.xaml.cs(metodo btnAlerta_Click) " + ex.Message);
            }
        }
        private void CheckBox_OnChecked(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItemContactoEmail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manteContacto = new MantenedorContactMail();
                manteContacto.Owner = this;
                manteContacto.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.xaml.cs(metodo MenuItemContactoEmail_Click) " + ex.Message);
            }
        }
        private void MenuItemGrupos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mGrupos = new MantenedorGrupos();
                mGrupos.Owner = this;
                mGrupos.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.xaml.cs(metodo MenuItemGrupos_Click) " + ex.Message);
            }
        }
        private void MenuItemConfiguracionMonitoreo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mConfigMonito = new MantenedorConfigMonitoreo();
                mConfigMonito.Owner = this;
                mConfigMonito.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.xaml.cs(metodo MenuItemConfiguracionMonitoreo_Click) " + ex.Message);
            }
        }
        private void MenuItemParametrosSitema_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var msystem = new MantenedorSistema();
                msystem.Owner = this;
                if (msystem.IsShowDialog)
                    msystem.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.xaml.cs(metodo ItemParametros_ItemClick) " + ex.Message);
            }
        }
        private void MenuItemReporteHistorico_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var rHistorico = new ReporteHistorico();
                rHistorico.Owner = this;
                rHistorico.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, ex.Message);
            }
        }
        private void MenuItemReporteAutomatico_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var report = new ReporteAutomatico();
                report.Owner = this;
                report.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, ex.Message);
            }
        }
        private void MenuItemConsultaLog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var log = new ViewLog();
                log.Owner = this;
                log.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.xaml.cs(metodo ItemConsulLog_ItemClick) " + ex.Message);
            }
        }
        private void MenuItemConsultaAlertas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var viewAlertas = new ViewAlertas();
                viewAlertas.Owner = this;
                viewAlertas.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.xaml.cs(metodo viewAlertas_ItemClick) " + ex.Message);
            }
        }
      
      

      

       

       
    }
}
