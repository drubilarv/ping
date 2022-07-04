using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Apex.MVVM;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using Ping.Accion;
using Ping.BO;
using PingWpf.Views;


namespace PingWpf.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private Grupos__action _gruposAction;
        private Equipos__action _equiposAction;
        public ObservableCollection<GruposItem> _collect { get; set; }
        public static bool IsMonitoring { get; set; } //Valido si existe el monitoreo

        private CancellationTokenSource cts;
        public ObservableCollection<PingTaskViewModel> PingTasks { get; private set; }
        public Command AddClient { get; private set; }
        public Command TaskCancel { get; private set; }
        public Command ClearList { get; private set; }
        public Command UpdateEquipClient { get; private set; }

        private string address = "localhost"; //inicia por defecto para hacer el ping
        public string Address
        {
            get { return address; }
            set
            {
                if (address == value) return;
                address = value;
                RaisePropertyChanged();
            }
        }
        private bool canAddClient = true;
        public bool CanAddClient
        {
            get { return canAddClient; }
            set
            {
                if (canAddClient == value) return;
                canAddClient = value;
                RaisePropertyChanged();
            }
        }
        public MainWindowViewModel()
        {
            PingTasks = new ObservableCollection<PingTaskViewModel>();
            AddClient = new Command(DoAddClient);
            TaskCancel = new Command(CancelarTask);
            UpdateEquipClient = new Command(BtnActualizaEquipos);
            CargarCapaDispositivo();
        }
        public void CancelarTask()
        {
            IsMonitoring = false;
            if (cts == null) return;
            cts.Cancel();
            ActivarBotones();
            PingTasks.Clear();
        }
        //private void ClearListView()
        //{
        //    var listPrincipal = App.Current.Windows[0].FindName("ListViewTask") as ListView;
        //    if (listPrincipal != null)
        //    {
        //        listPrincipal.ItemsSource = null;
        //        listPrincipal.Items.Clear();
        //    }
        //}
        private void ActivarBotones()
        {
            var menu = App.Current.Windows[0].FindName("BtnIniciarMonitoreo") as MenuItem;
            if (menu != null) menu.IsEnabled = true;


            menu = App.Current.Windows[0].FindName("BtnActualizarEquipos") as MenuItem;
            if (menu != null) menu.IsEnabled = true;

            menu = App.Current.Windows[0].FindName("BtnDetenerMonitoreo") as MenuItem;
            if (menu != null) menu.IsEnabled = false;
        }
        private void BloquearBotones()
        {
            //IsEnabled=False cuando preciono iniciar
            var menu = App.Current.Windows[0].FindName("BtnIniciarMonitoreo") as MenuItem;
            if (menu != null) menu.IsEnabled = false;

           
            menu = App.Current.Windows[0].FindName("BtnActualizarEquipos") as MenuItem;
            if (menu != null) menu.IsEnabled = false;

            menu = App.Current.Windows[0].FindName("BtnDetenerMonitoreo") as MenuItem;
            if (menu != null) menu.IsEnabled = true;
        }

        public async void DoAddClient()
        {
            BloquearBotones();
            IsMonitoring = true;
            //var listPrincipal = App.Current.Windows[0].FindName("ListViewTask") as ListView;
            //PingTasks = new ObservableCollection<PingTaskViewModel>();
            //if (listPrincipal != null) listPrincipal.ItemsSource = PingTasks;
            cts = new CancellationTokenSource();
            try
            {
                var dt = IpActivas_action.GetAllEquiposActivosPorGruposActivos();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                     CanAddClient = false;
                    string ip = dt.Rows[i]["IP_EQUIPO"].ToString();
                    string name = dt.Rows[i]["NOM_EQUIPO"].ToString();
                    var pingTask = await PingTaskViewModel.CreateVmAsync(ip, name, cts.Token, vm => PingTasks.Remove(vm));
                        PingTasks.Add(pingTask);
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.cs(metodo DoAddClient()) " + ex.Message);
            }
            finally
            {
                CanAddClient = false;
            }
        }
        private void BtnActualizaEquipos()
        {
            try
            {
                CancelarTask();
                _gruposAction.UpdateDesactivaTodosGrupos();
                _equiposAction.UpdateDesactivaTodosEquipos();

                var grupos = _collect.ToList();
                foreach (var gru in grupos)
                {
                    if (gru.Checked)
                    {
                        _gruposAction.UpdateActivaGrupoPorId(Convert.ToInt32(gru.Name.Id)); //activa grupos chequeados
                        foreach (var equi in gru.Children.ToList())
                        {
                            var ip = equi.Name.Id;
                            if (equi.Checked)
                                _equiposAction.UpdateActivaEquipoPorIp(ip); //activa equipos chequeados
                        }
                    }
                }
                MessageBox.Show("Actualización exitosa!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.cs(metodo BtnActualizaEquipos()) " + ex.Message);
            }
        }
        public void CargarCapaDispositivo()
        {
            try
            {
                _gruposAction = new Grupos__action();
                _equiposAction = new Equipos__action();
                var grupos = _gruposAction.ObtenerGrupos();
                _collect = new ObservableCollection<GruposItem>();
                foreach (var grupo in grupos)
                {
                    var capaItem = new GruposItem
                    {
                        Name = grupo,
                        Checked = grupo.Estado
                    };
                    capaItem.Children = new ObservableCollection<EquiposItem>();
                    var equipos = _equiposAction.GetEquiposPorGrupos(grupo.Id);
                    foreach (var equip in equipos)
                    {
                        var equiposItem = new EquiposItem
                        {
                            Name = equip,
                            Checked = equip.Estado
                        };
                        capaItem.Children.Add(equiposItem);
                    }
                    capaItem.Checked = grupo.Estado;
                    _collect.Add(capaItem);
                }
                //this.DataContext = _collect;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "MainWindowViewModel.cs(metodo CargarCapaDispositivo) " + ex.Message);
            }
        }
        public class GruposItem
        {
            public Grupos_BO Name { get; set; }
            public string Descripcion { get; set; }
            public bool Checked { get; set; }
            public ObservableCollection<EquiposItem> Children { get; set; }
        }
        public class EquiposItem
        {
            public Equipos_BO Name { get; set; }
            public bool Checked { get; set; }
        }
    }
}
