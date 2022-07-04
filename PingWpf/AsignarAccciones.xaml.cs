using Ping.Accion;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Windows;


namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para AsignarAccciones.xaml
    /// </summary>
    public partial class AsignarAccciones : Window
    {
        int rut;
        TiposAcciones_action tiposAction = new TiposAcciones_action();
        private const int RecepNotif = 10;
        private const int RecepNotifNoPing = 11;
        public AsignarAccciones(int rut)
        {
            try
            {
                InitializeComponent();
                var lista = tiposAction.ObtenerTiposAcciones(rut);
                //var listTipAcciones = tiposAction.ObtenerTiposAccionesActivas();
                //if (listTipAcciones.Count > 0)
                //    CheckReportes.Content = listTipAcciones[0].Descripcion;
                //if (listTipAcciones.Count > 1)
                //    CheckPings.Content = listTipAcciones[1].Descripcion;

                foreach (TiposAcciones_BO tipo in lista)
                {
                    if (tipo.Id_tipo_accion == RecepNotif)
                        CheckReportes.IsChecked = true;
                    else if (tipo.Id_tipo_accion == RecepNotifNoPing)
                        CheckPings.IsChecked = true;
                }
                this.rut = rut;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AsignarAccciones.xaml.cs(metodo AsignarAccciones) " + ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ids = new List<int>();
                if (CheckPings.IsChecked == true & CheckReportes.IsChecked == true)
                {
                    ids.Add(RecepNotif);
                    ids.Add(RecepNotifNoPing);
                }
                else
                {
                    if (CheckPings.IsChecked == true)
                        ids.Add(RecepNotifNoPing);
                    else
                    {
                        if (CheckReportes.IsChecked == true)
                            ids.Add(RecepNotif);
                    }
                }
                if (ids.Count > 0)
                {
                    tiposAction.DeleteTipoAcciones(rut);
                    tiposAction.InsertTipoAcciones(ids, rut);
                    var logeer = new LogErroresModificaciones__action();
                    logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, "Asignación exitosa al rut " + rut);
                    MessageBox.Show("Asignación exitosa.", "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    tiposAction.DeleteTipoAcciones(rut);
                    var logeer = new LogErroresModificaciones__action();
                    logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, "Desasignación exitosa al rut " + rut);
                    MessageBox.Show("Desasignación exitosa.", "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AsignarAccciones.xaml.cs(metodo Button_Click) " + ex.Message);
            }
        }
    }
}
