using Ping.Accion;
using Ping.BO;
using System;
using System.Collections.Generic;
using System.Windows;


namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para AgregarGrupo.xaml
    /// </summary>
    public partial class AgregarGrupo : Window
    {
        private DevExpress.Xpf.Grid.GridControl grilla;
        private Grupos_BO grupo;
        public AgregarGrupo(DevExpress.Xpf.Grid.GridControl grilla, Grupos_BO grupo)
        {
            try
            {
                InitializeComponent();
                var config_monitoreo = new ConfiguracionMonitoreo_action();
                cboxConfigu.ItemsSource = config_monitoreo.ObtenerConfig();
                List<bool> list = new List<bool>();
                list.Add(true);
                list.Add(false);
                //cboxEstado.ItemsSource = list;
                CheckEstado.IsChecked = true;
                this.grilla = grilla;
                if (grupo != null)
                    CargarDatos(grupo);

            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarGrupo.xaml.cs(metodo AgregarGrupo) " + ex.Message);
            }
        }

        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void CargarDatos(Grupos_BO grupo)
        {
            try
            {
                this.grupo = grupo;
                txtDesc.Text = grupo.Descripcion;
                if (grupo.Estado)
                    CheckEstado.IsChecked = true;
                else
                    CheckEstado.IsChecked = false;

                //cboxEstado.SelectedIndex = index;
                cboxConfigu.SelectedIndex = grupo.ConfiguracionDeGrupo.IdConfig - 1;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarGrupo.xaml.cs(metodo CargarDatos) " + ex.Message);
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
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarGrupo.xaml.cs(metodo isNum) " + e.Message);
                return false;
            }
        }
        private void agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (grupo == null)
                {
                    if (txtDesc.Text.Length == 0 | isNum(txtDesc.Text))
                        MessageBox.Show(this, "Descripción del grupo invalida", "Información", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    else
                    {
                        var gaction = new Grupos__action();
                        var iconfig = (ConfiguracionMonitoreo_BO)cboxConfigu.SelectedItem;
                        //bool estado = (bool) cboxEstado.SelectedItem;
                        bool estado = (bool)CheckEstado.IsChecked;
                        if (gaction.InsertGrupo(iconfig, txtDesc.Text, estado))
                        {
                            var logeer = new LogErroresModificaciones__action();
                            logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, "Grupo " + txtDesc.Text + " Insertado");
                            MessageBox.Show(this, "Registro insertado exitosamente!", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            grilla.ItemsSource = gaction.ObtenerGruposMant();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "Error al insertar registro!", "Información", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                        }
                    }
                }
                else
                {
                    if (txtDesc.Text.Length == 0 | isNum(txtDesc.Text))
                        MessageBox.Show(this, "Debe entregar una descripción del grupo", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                    {
                        var gaction = new Grupos__action();
                        var iconfig = (ConfiguracionMonitoreo_BO)cboxConfigu.SelectedItem;
                        //bool estado = (bool) cboxEstado.SelectedItem;
                        bool estado = (bool)CheckEstado.IsChecked;
                        if (gaction.UpdateGrupo(grupo.Id, iconfig, txtDesc.Text, estado))
                        {
                            string messageLog = "Grupo Modificado  Descripcion: " + txtDesc.Text + " Configuracion: " + iconfig.NombreConfig + " Estado: " + estado;
                            var logeer = new LogErroresModificaciones__action();
                            logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, messageLog);
                            MessageBox.Show(this, "Registro actualizado exitosamente!", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            grilla.ItemsSource = gaction.ObtenerGruposMant();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "Error al actualizar registro!", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarGrupo.xaml.cs(metodo agregar_Click) " + ex.Message);
            }
        }
    }
}
