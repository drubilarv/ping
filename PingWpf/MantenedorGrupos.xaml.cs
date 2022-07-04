using Ping.Accion;
using Ping.BO;
using System;
using System.Windows;


namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para MantenedorGrupos.xaml
    /// </summary>
    public partial class MantenedorGrupos : Window
    {
        public MantenedorGrupos()
        {
            try
            {
                InitializeComponent();
                var gaction = new Grupos__action();
                GridGrupos.ItemsSource = gaction.ObtenerGruposMant();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorGrupos.xaml.cs(metodo MantenedorGrupos) " + ex.Message);
            }
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mantenedorGrupo = new AgregarGrupo(GridGrupos, null);
                mantenedorGrupo.Owner = this;
                mantenedorGrupo.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorGrupos.xaml.cs(metodo agregar_Click) " + ex.Message);
            }
        }

        private void editar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var estado = GridGrupos.SelectedItem as Grupos_BO;
                var mantenedorGrupo = new AgregarGrupo(GridGrupos, estado);
                mantenedorGrupo.Owner = this;
                mantenedorGrupo.ShowDialog();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorGrupos.xaml.cs(metodo editar_Click) " + ex.Message);
            }
        }

        private void eliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("¿Está seguro que desea eliminar este grupo?", "Información",
                    MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    var estado = GridGrupos.SelectedItem as Grupos_BO;
                    var gaction = new Grupos__action();
                    var resultado = gaction.DeleteGrupo(estado.Id);
                    if (resultado)
                    {
                        var logeer = new LogErroresModificaciones__action();
                        logeer.InsertErroresLog(2, System.DateTime.Now, Environment.UserName,
                            "Grupo " + estado.Id + " Eliminado");
                    }
                    MessageBox.Show(resultado ? "Registro eliminado correctamente" : "No se pudo eliminar el registro,Consulte si hay equipos en este grupo",
                        "Resultado", MessageBoxButton.OK);
                    GridGrupos.ItemsSource = gaction.ObtenerGruposMant();
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MantenedorGrupos.xaml.cs(metodo eliminar_Click) " + ex.Message);
            }
        }
    }
}
