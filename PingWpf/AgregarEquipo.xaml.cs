using Ping.Accion;
using Ping.BO;
using System;
using System.Linq;
using System.Windows;
using PingWpf.ViewModels;


namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para AgregarEquipo.xaml
    /// </summary>
    public partial class AgregarEquipo : Window
    {
        private DevExpress.Xpf.Grid.GridControl grilla;
        private Equipos_BO equipo;
        private Grupos_BO grupo;
        private bool estado;

        
        public AgregarEquipo(Equipos_BO equipo, DevExpress.Xpf.Grid.GridControl grilla)
        {
            try
            {
                InitializeComponent();
                var gaction = new Grupos__action();
                var config_monitoreo = new ConfiguracionMonitoreo_action();
                cboxGrupo.ItemsSource = gaction.ObtenerGruposMant();
                cboxConfigu.ItemsSource = config_monitoreo.ObtenerConfig();
                CheckEstado.IsChecked = false;
                this.grilla = grilla;
                if (equipo != null)
                    CargarDatos(equipo);

            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarEquipo.xaml.cs(metodo AgregarEquipo) " + ex.Message);
            }
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Equipos__action eaction = new Equipos__action();

                if (!validaIp(txtIp.Text))
                    MessageBox.Show(this, "Ip inválida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    if (txtName.Text.Length == 0 | isNum(txtName.Text))
                        MessageBox.Show(this, "Debe entregar un nombre válido al equipo", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                    {
                        if (isNum(txtDesc.Text))
                            MessageBox.Show(this, "Descripción inválida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        else
                        {
                            if (isNum(txtUbicacion.Text))
                                MessageBox.Show(this, "Ubicación inválida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            else
                            {
                                if (equipo == null)
                                {
                                    var iconfig = (ConfiguracionMonitoreo_BO)cboxConfigu.SelectedItem;
                                    var grupo = (Grupos_BO)cboxGrupo.SelectedItem;
                                    bool estado = (bool)CheckEstado.IsChecked;

                                    if (eaction.InsertEquipo(txtIp.Text, iconfig, grupo, txtName.Text, txtUbicacion.Text, txtDesc.Text, estado))
                                    {
                                        var logeer = new LogErroresModificaciones__action();
                                        logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, "Equipo " + txtIp.Text + " Insertado");
                                        MessageBox.Show(this, "Registro ingresado exitosamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                                        grilla.ItemsSource = eaction.ObtenerEquipos();
                                        Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "Error al insertar registro", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                }
                                else
                                {
                                    var iconfig = (ConfiguracionMonitoreo_BO)cboxConfigu.SelectedItem;
                                    Grupos_BO grupo = (Grupos_BO)cboxGrupo.SelectedItem;
                                    bool estado = (bool)CheckEstado.IsChecked;
                                    //var v = this.grupo.Equals(grupo);

                                    //Solo se debe de validad que no exista en monitoreo

                                    //if (!this.grupo.Equals(grupo) & (this.estado == estado) & estado)
                                    if (MainWindowViewModel.IsMonitoring)
                                    {
                                        MessageBox.Show(this, "Debe detener el monitoreo para editar el equipo", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                    else
                                    {
                                        if (eaction.UpdateEquipo(txtIp.Text, iconfig, grupo, txtName.Text, txtUbicacion.Text, txtDesc.Text, estado))
                                        {
                                            var logeer = new LogErroresModificaciones__action();
                                            string messageLog = "Equipo Modificado  IP: " + txtIp.Text + " Nombre: " + txtName.Text + " Descripción: " + txtDesc.Text + " Ubicación: " + txtUbicacion.Text;
                                            logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, messageLog);
                                            MessageBox.Show(this, "Registro actualizado exitosamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                                            grilla.ItemsSource = eaction.ObtenerEquipos();
                                            Close();
                                        }
                                        else
                                        {
                                            MessageBox.Show(this, "Error al actualizar registro!", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                                        }
                                        grilla.ItemsSource = eaction.ObtenerEquipos();
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarEquipo.xaml.cs(metodo Agregar_Click) " + ex.Message);
            }
        }
        public bool validaIp(string ip)
        {
            try
            {
                if (ip.Contains('.'))
                {
                    var ipSegmentada = ip.Split('.');
                    if (ipSegmentada.Length != 4)
                        return false;

                    ipSegmentada[0] = ipSegmentada[0].TrimStart('0');

                    if (ipSegmentada[1].Length == 2)
                        ipSegmentada[1] = ipSegmentada[1].TrimStart('0');
                    else if (ipSegmentada[1].Length == 3)
                        ipSegmentada[1] = ipSegmentada[1].TrimStart('0');
                    else if (ipSegmentada[1].Length > 3)
                        return false;

                    if (ipSegmentada[2].Length == 2)
                        ipSegmentada[2] = ipSegmentada[2].TrimStart('0');
                    else if (ipSegmentada[2].Length == 3)
                        ipSegmentada[2] = ipSegmentada[2].TrimStart('0');
                    else if (ipSegmentada[2].Length > 3)
                        return false;

                    ipSegmentada[3] = ipSegmentada[3].TrimStart('0');

                    ip = ipSegmentada[0] + "." + ipSegmentada[1] + "." + ipSegmentada[2] + "." + ipSegmentada[3];

                    System.Net.IPAddress address;
                    if (System.Net.IPAddress.TryParse(ip, out address))
                    {
                        if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            txtIp.Text = ip;
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarEquipo.xaml.cs(metodo validaIp) " + ex.Message);
                return false;
            }
        }
        public bool isNum(string valor)
        {
            try
            {
                int num = Convert.ToInt32(valor);
                return true;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarEquipo.xaml.cs(metodo isNum) " + ex.Message);
                return false;
            }
        }
        public void CargarDatos(Equipos_BO equipo)
        {
            try
            {
                bool estado = false;
                this.equipo = equipo;
                txtIp.Text = equipo.Id;
                txtIp.IsEnabled = false;
                txtName.Text = equipo.NombreEquipo;
                txtDesc.Text = equipo.DescripcionEquipo;
                txtUbicacion.Text = equipo.UbicacionEquipo;
                estado = this.equipo.Estado;
                CheckEstado.IsChecked = estado;
                cboxConfigu.SelectedIndex = equipo.Config.IdConfig - 1;
                cboxGrupo.SelectedIndex = equipo.Grupo.Id - 1;
                this.grupo = (Grupos_BO)cboxGrupo.SelectedItem;
                this.estado = Convert.ToBoolean(CheckEstado.IsChecked);
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "AgregarEquipo.xaml.cs(metodo CargarDatos) " + ex.Message);
            }
        }
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
