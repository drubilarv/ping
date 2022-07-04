using Ping.Accion;
using Ping.BO;
using System;
using System.Windows;
using PingWpf.ViewModels;


namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para MantenedorEquipos.xaml
    /// </summary>
    public partial class MantenedorEquipos : Window
    {
        
        public MantenedorEquipos()
        {
            try
            {
                InitializeComponent();
                var equipos_action = new Equipos__action();
                GridEquipos.ItemsSource = equipos_action.ObtenerEquipos();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorEquipos.xaml.cs(metodo MantenedorEquipos) " + ex.Message);
            }
        }

        private void agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var pantallAgregar = new AgregarEquipo(null, GridEquipos);
                pantallAgregar.Owner = this;
                pantallAgregar.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorEquipos.xaml.cs(metodo agregar_Click) " + ex.Message);
            }
        }

        private void editar_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                var estado = GridEquipos.SelectedItem as Equipos_BO;

                if (estado == null)
                    MessageBox.Show("Primero debe seleccionar un registro", "Información", MessageBoxButton.OK);
                else
                {
                    
                    var gruposAction = new Grupos__action();
                    //if (estado.Estado && gruposAction.ObtenerGrupo(estado.IdGrupo).Estado)
                    if (MainWindowViewModel.IsMonitoring)
                    {
                        MessageBox.Show("Equipo en monitoreo, detenga monitoreo y luego edite", "Información", MessageBoxButton.OK);
                        return;
                    }

                    var pantallAgregar = new AgregarEquipo(estado, GridEquipos);
                    pantallAgregar.Owner = this;
                    pantallAgregar.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorEquipos.xaml.cs(metodo editar_Click) " + ex.Message);
            }
        }

        private void eliminar_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                var result = MessageBox.Show("¿Está seguro que desea eliminar este equipo?. Se eliminara el historial de todos los datos asociados al equipo", "Información",
                    MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    var estado = GridEquipos.SelectedItem as Equipos_BO;
                    if (!MainWindowViewModel.IsMonitoring)
                    {
                        var eaction = new Equipos__action();
                        var resultado = eaction.DeleteEquipo(estado.Id);
                        if (resultado)
                        {
                            var logeer = new LogErroresModificaciones__action();
                            logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName, "Equipo " + estado.Id + " Borrado");
                        }
                        MessageBox.Show(resultado ? "Registro eliminado correctamente" : "No se pudo eliminar el registro.",
                            "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
                        GridEquipos.ItemsSource = eaction.ObtenerEquipos();
                    }
                    else
                        MessageBox.Show("Debe Desactivar el equipo para poder eliminarlo", "Información",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorEquipos.xaml.cs(metodo eliminar_Click) " + ex.Message);
            }
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
